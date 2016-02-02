using System;
using System.Linq;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Infrastructure;
using EntityManager.Models.GroupSubgroup;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [Authorize(Roles = "admin")]
    [ValidateInput(false)]
    [ValidateAntiForgeryTokenOnController(HttpVerbs.Post)]
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

        [HttpGet]
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
            var group = _groupQueryService.GetGroupById(id);

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
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(e => e.Errors).First().ErrorMessage;

                return Json(new { success = false, responseText = firstError }, JsonRequestBehavior.AllowGet);
                
            }

            _groupCommandService.UpdateGroup(updateGroupModel);

            //var vm = new GroupGeneralViewModel
            //{
            //    Id = updateGroupModel.Id,
            //    Name = updateGroupModel.Name,
            //    Description = updateGroupModel.Description
            //};
            return Json(new { success = true, responseText = "Record saved successfully" }, JsonRequestBehavior.AllowGet);
            //return PartialView(vm);
            
        }

        [HttpPost]
        public ActionResult Create(Group model)
        {
            //authorization
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = _groupCommandService.Create(model);

            return RedirectToAction("Manage", new { @id = id });
        }

        public ActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
