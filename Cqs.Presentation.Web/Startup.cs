using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cqs.Presentation.Web.Startup))]
namespace Cqs.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
