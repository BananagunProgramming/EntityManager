using System.Web.Mvc;
using EntityManager.Domain.Services;

namespace EntityManager.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public static readonly AzureWriter AuditLog = new AzureWriter();
        //todo should I implement a controller base for this logger? If I find one more reason yes

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //AuditLog.Audit("HomeController - About");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //AuditLog.Audit("HomeController - Contact");

            return View();
        }
    }
}