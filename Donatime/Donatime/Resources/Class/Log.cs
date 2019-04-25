using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Donatime.Resources.Class
{
    public class Log
    {
        public static void Write(string message, string type)
        {
            WriteDB(type + "  --->" + message, true);
        }

        public static void Write(string message)
        {
            WriteDB(message, true);
        }

        // Util por si solo queremos utilizar el Log para debug
        public static void WriteDB(string message, bool show)
        {
            if (show)
                Console.WriteLine(message);
        }

        /// <summary>
        /// Print errors on model state
        /// </summary>
        /// <param name="modelState"></param>
        public static void writeModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            foreach (var item in errors)
            {
                Log.Write(item.Key + " ||" + item.Errors, "Error");
            }
        }

        public static void writeDataBaseError(DbEntityValidationException e)
        {
            foreach (var eve in e.EntityValidationErrors)
            {
                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
        }


    }

}