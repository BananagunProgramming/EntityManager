using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityManager.Domain.CodeFirst;
using EntityManager.Infrastructure;
using EntityManager.Services;

namespace EntityManager.Controllers
{
    [Authorize(Roles = "admin")]
    [ValidateInput(false)]
    [ValidateAntiForgeryTokenOnController(HttpVerbs.Post)]
    public class SubgroupController : Controller
    {
        private readonly ISubgroupQueryService _subgroupQueryService;
        private readonly ISubgroupCommandService _subgroupCommandService;

        public SubgroupController(ISubgroupQueryService subgroupQueryService, ISubgroupCommandService subgroupCommandService)
        {
            _subgroupQueryService = subgroupQueryService;
            _subgroupCommandService = subgroupCommandService;
        }

        // GET: Subgroup
        [HttpGet]
        public ActionResult Index()
        {
            var subGroups = _subgroupQueryService.GetAllSubgroups();

            return View(subGroups);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var subgroup = _subgroupQueryService.GetSubgroupById(id);

            return View(subgroup);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            //authorization
            var subGroup = _subgroupQueryService.GetSubgroupById(id);

            var vm = new Subgroup
            {
                Id = subGroup.Id,
                Name = subGroup.Name,
                Description = subGroup.Description,
                LastUpdatedBy = subGroup.LastUpdatedBy,
                LastUpdateDate = subGroup.LastUpdateDate
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var subGroup = _subgroupQueryService.GetSubgroupById(id);

            if (subGroup == null)
            {
                return HttpNotFound();
            }

            return View(subGroup);
        }

        //POST: Subgroup
        [HttpPost]
        public ActionResult Create(Subgroup model)
        {
            //authorization
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = _subgroupCommandService.Create(model);

            return RedirectToAction("Details", new { id });
        }

        public ActionResult Save(Subgroup model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            if (ModelState.IsValid)
            {
                _subgroupCommandService.UpdateSubgroup(model);
            }

            return RedirectToAction("Details", new { @id = model.Id });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _subgroupCommandService.DeleteSubgroup(id);

            return RedirectToAction("Index");
        }
    }
}