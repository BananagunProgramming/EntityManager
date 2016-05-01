using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Infrastructure;
using EntityManager.Models.Client;
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
        private readonly ISubgroupQueryService _subgroupQueryService;
        
        public ClientsController(
            IClientQueryService clientQueryService, 
            IClientCommandService clientCommandService, 
            ISubgroupQueryService subgroupQueryService)
        {
            _clientQueryService = clientQueryService;
            _clientCommandService = clientCommandService;
            _subgroupQueryService = subgroupQueryService;
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
            var newClient = new ClientManageModel
            {
                Subgroups = _subgroupQueryService.GetAllSubgroups()
            };
            return View(newClient);
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

            var model = new ClientManageModel
            {
                Name = client.Name,
                EntityCode = client.EntityCode,
                YearIncorporated = client.YearIncorporated,
                TaxId = client.TaxId,
                Phone = client.Phone,
                Fax = client.Fax,
                Email = client.Email,
                Website = client.Website,
                Schedule = client.Schedule,
                YearEndDate = client.YearEndDate,
                FiscalYearEndDate = client.FiscalYearEndDate,
                Managed = client.Managed,
                SubgroupId = client.SubgroupId,
                Subgroups = _subgroupQueryService.GetAllSubgroups()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ClientManageModel client)
        {
            if (ModelState.IsValid)
            {
                var model = new Client
                {
                    Name = client.Name,
                    EntityCode = client.EntityCode,
                    YearIncorporated = client.YearIncorporated,
                    TaxId = client.TaxId,
                    Phone = client.Phone,
                    Fax = client.Fax,
                    Email = client.Email,
                    Website = client.Website,
                    Schedule = client.Schedule,
                    YearEndDate = client.YearEndDate,
                    FiscalYearEndDate = client.FiscalYearEndDate,
                    Managed = client.Managed,
                    SubgroupId = client.SubgroupId
                };
                
                _clientCommandService.CreateClient(model);

                return RedirectToAction("Index");
            }

            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
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
