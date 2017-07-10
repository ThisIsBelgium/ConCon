using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConCon.Startup))]
namespace ConCon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
