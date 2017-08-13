using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebTraffic.Startup))]
namespace WebTraffic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
