using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEB_PROJECT.Startup))]
namespace WEB_PROJECT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
