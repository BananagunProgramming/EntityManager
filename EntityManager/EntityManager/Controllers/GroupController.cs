using System;
using System.Web.Mvc;
using System.Web.Routing;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Models.GroupSubgroup;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [Authorize(Roles = "admin")]
    [ValidateInput(false)]
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

        [HttpGet]
        public ActionResult ManageGeneral(Guid id)
        {
            var group = _groupQueryService.GetEntity<Group>(id);

            var vm = new GroupGeneralViewModel
            {
                Id = id,
                Name = group.Name,
                Description = group.Description
            };

            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult ManageGeneral(GroupInputModel updateGroupModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError();
            //}
            if (ModelState.IsValid)
            {
                _groupCommandService.UpdateGroup(updateGroupModel);
            }

            var vm = new GroupGeneralViewModel
            {
                Id = updateGroupModel.Id,
                Name =  updateGroupModel.Name,
                Description = updateGroupModel.Description
            };

            return PartialView(vm);
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
