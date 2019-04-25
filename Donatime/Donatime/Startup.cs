using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Donatime.Startup))]
namespace Donatime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
