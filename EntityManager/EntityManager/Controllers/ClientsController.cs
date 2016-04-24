using System;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Infrastructure;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [ValidateAntiForgeryTokenOnController(HttpVerbs.Post)]
    public class ClientsController : Controller
    {
        public static readonly AzureWriter AuditLog = new AzureWriter();
        //todo should I implement a controller base for this logger? If I find one more reason yes

        private readonly IClientQueryService _clientQueryService;
        private readonly IClientCommandService _clientCommandService;
        
        public ClientsController(
            IClientQueryService clientQueryService, 
            IClientCommandService clientCommandService)
        {
            _clientQueryService = clientQueryService;
            _clientCommandService = clientCommandService;
        }

        [HttpGet]
        public ActionResult Index()
        {//example of how controller should go through the associated service to get info so that I 
            //can perform authorization and  return only that clients information, vertical striation.
            return View(_clientQueryService.GetClientSpecificEntities());
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var client = _clientQueryService.GetClientById(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var client = _clientQueryService.GetClientById(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var client = _clientQueryService.GetClientById(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,EntityCode,YearIncorporated,TaxId,Phone,Fax,Email,Website,Schedule,YearEndDate,FiscalYearEndDate,Managed")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientCommandService.CreateClient(client);

                return RedirectToAction("Index");
            }

            return View(client);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,EntityCode,YearIncorporated,TaxId,Phone,Fax,Email,Website,Schedule,YearEndDate,FiscalYearEndDate,Managed,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientCommandService.UpdateClient(client);

                return RedirectToAction("Index");
            }
            return View(client);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _clientCommandService.DeleteClient(id);

            return RedirectToAction("Index");
        }
    }
}
