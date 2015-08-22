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

        public ActionResult Index()
        {//example of how controller should go through the associated service to get info so that I 
            //can perform authorization and  return only that clients information, vertical striation.
            return View(_clientQueryService.GetClientSpecificEntities());
        }

        public ActionResult Details(Guid id)
        {//example of how not to do it. Going to service base class and using the generic methods directly. No chance for auth or vertical here.
            var client = _clientQueryService.GetEntity<Client>(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ClientId,Name,EntityCode,YearIncorporated,TaxId,Phone,Fax,Email,Website,Schedule,YearEndDate,FiscalYearEndDate,Managed")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientCommandService.Create(client);

                return RedirectToAction("Index");
            }

            return View(client);
        }

        public ActionResult Edit(Guid id)
        {
            var client = _clientQueryService.GetEntity<Client>(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ClientId,Name,EntityCode,YearIncorporated,TaxId,Phone,Fax,Email,Website,Schedule,YearEndDate,FiscalYearEndDate,Managed,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientCommandService.UpdateEntity(client);

                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult Delete(Guid id)
        {
            var client = _clientQueryService.GetEntity<Client>(id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(Guid id)
        {
            _clientCommandService.DeleteEntity<Client>(id);

            return RedirectToAction("Index");
        }
    }
}
