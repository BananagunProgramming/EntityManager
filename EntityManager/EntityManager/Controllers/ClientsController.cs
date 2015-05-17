using System;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [Authorize(Roles = "canEdit")]
    public class ClientsController : Controller
    {
        public static readonly AzureWriter AuditLog = new AzureWriter();
        //todo should I implement a controller base for this logger? If I find one more reason yes

        private readonly IClientQueryService _clientQueryService;
        private readonly IClientCommandService _clientCommandService;
        private readonly IUserService _userService;

        public ClientsController(
            IClientQueryService clientQueryService, 
            IClientCommandService clientCommandService, IUserService userService)
        {
            _clientQueryService = clientQueryService;
            _clientCommandService = clientCommandService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var eCode = _userService.GetEntityCode(User);

            return View(_clientQueryService.GetAllEntities<Client>());
        }

        public ActionResult Details(Guid id)
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Name,EntityCode,YearIncorporated,TaxId,Phone,Fax,Email,Website,Schedule,YearEndDate,FiscalYearEndDate,Managed,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientCommandService.CreateEntity(client);

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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _clientCommandService.DeleteEntity<Client>(id);

            return RedirectToAction("Index");
        }
    }
}
