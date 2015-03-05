using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EntityManager.Startup))]
namespace EntityManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
