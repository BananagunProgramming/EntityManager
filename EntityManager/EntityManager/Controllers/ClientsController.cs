using System;
using System.Web.Mvc;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Models;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    public class ClientsController : Controller
    {
        //poor mans di
        private readonly ClientQueryService _clientQueryService = new ClientQueryService(new DbContextScopeFactory());
        private readonly ClientCommandService _clientCommandService = new ClientCommandService(new DbContextScopeFactory());

        private EntityManagerDbContext db = new EntityManagerDbContext();

        public ActionResult Index()
        {
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
