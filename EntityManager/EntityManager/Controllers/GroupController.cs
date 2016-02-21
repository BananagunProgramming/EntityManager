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

        #region GETs
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
        public ActionResult Details(Guid id)
        {
            var groupDetailModel = _groupQueryService.GetGroupModelById(id);

            return View("Details", groupDetailModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var group = _groupQueryService.GetGroupById(id);

            var vm = new GroupModel
            {
                Id = id,
                Name = group.Name,
                Description = group.Description
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var group = _groupQueryService.GetGroupModelById(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult Create(Group model)
        {
            //authorization
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = _groupCommandService.Create(model);

            return RedirectToAction("Details", new { @id = id });
        }

        [HttpPost]
        public ActionResult Save(GroupModel updateGroupModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", updateGroupModel);
            }

            if (ModelState.IsValid)
            {
                _groupCommandService.UpdateGroup(updateGroupModel);
            }
            
            return RedirectToAction("Details", new {@id = updateGroupModel.Id});
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _groupCommandService.DeleteGroup(id);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
