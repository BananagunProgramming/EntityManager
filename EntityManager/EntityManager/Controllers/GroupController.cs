using System;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Models.GroupSubgroup;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [Authorize(Roles = "admin")]
    //[ValidateAntiForgeryTokenOnController(HttpVerbs.Post)]
    public class GroupController : Controller
    {
        public static readonly AzureWriter AuditLog = new AzureWriter();

        private readonly IGroupQueryService _groupQueryService;
        private readonly IGroupCommandService _groupCommandService;

        public GroupController(IGroupQueryService groupQueryService, 
            IGroupCommandService groupCommandService)
        {
            _groupQueryService = groupQueryService;
            _groupCommandService = groupCommandService;
        }

        public ActionResult Index()
        {
            var groups = _groupQueryService.GetAllGroups();

            return View(groups);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Manage(Guid id)
        {
            var groupDetailModel = _groupQueryService.GetGeneralModelById(id);

            return View("Manage", groupDetailModel);
        }

        [HttpPost]
        public ActionResult ManageGeneral(GroupInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Create(Group input)
        {
            //authorization

            if (ModelState.IsValid)
            {
                _groupCommandService.Create(input); 
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
