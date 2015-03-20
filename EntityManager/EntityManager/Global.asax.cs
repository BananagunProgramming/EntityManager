using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EntityManager.Infrastructure;
using log4net;

namespace EntityManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError().GetBaseException();

            var logger = LogManager.GetLogger(typeof(MvcApplication));

            logger.Error(ex.Message,ex);
        }
    }
}
