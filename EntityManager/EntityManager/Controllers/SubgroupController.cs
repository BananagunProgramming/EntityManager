using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public SubgroupController(ISubgroupQueryService subgroupQueryService)
        {
            _subgroupQueryService = subgroupQueryService;
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
    }
}