using System;
using System.Web.Mvc;
using log4net;

namespace EntityManager.Controllers
{
    public class HomeController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(HomeController));
        //todo should I implement a controller base for this logger? If I find one more reason yes

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}