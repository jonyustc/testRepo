using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GAMCUC.Web.Startup))]
namespace GAMCUC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
