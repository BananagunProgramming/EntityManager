using System;
using System.Linq;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Helpers;
using EntityManager.Infrastructure;
using EntityManager.Models.Groups;
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
        private readonly ISubgroupQueryService _subgroupQueryService;

        public GroupController(
            IGroupQueryService groupQueryService, 
            IGroupCommandService groupCommandService, 
            ISubgroupQueryService subgroupQueryService)
        {
            _groupQueryService = groupQueryService;
            _groupCommandService = groupCommandService;
            _subgroupQueryService = subgroupQueryService;
        }

        //GET: group
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
            var group = _groupQueryService.GetGroup(id);

            return View("Details", group);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var group = _groupQueryService.GetGroup(id);
            var subgroups = _subgroupQueryService.MarkSelectedSubgroups(group);

            var vm = new GroupUpdateModel
            {
                Group = group,
                Subgroups = subgroups
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var group = _groupQueryService.GetGroup(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        //POST: group
        [HttpPost]
        public ActionResult Create(Group model)
        {
            //authorization
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = _groupCommandService.Create(model);

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public ActionResult Save(GroupUpdateModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Edit", model);
            //}

            if (ModelState.IsValid)
            {
                _groupCommandService.UpdateGroup(model);
                _groupCommandService.SetUserGroups(model);
            }
            
            return RedirectToAction("Details", new {@id = model.Group.Id});
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _groupCommandService.DeleteGroup(id);

            return RedirectToAction("Index");
        }
    }
}
