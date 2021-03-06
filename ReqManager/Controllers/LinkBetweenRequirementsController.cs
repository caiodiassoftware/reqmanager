﻿using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System;
using System.Collections.Generic;
using ReqManager.Entities.Requirement;
using System.Net;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private IRequirementTraceabilityMatrixService matrixService { get; set; }
        private ILinkBetweenRequirementsService linkService { get; set; }
        private IRequirementService requirementService { get; set; }
        private ITypeLinkService typeLinkService { get; set; }

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService,
            IRequirementTraceabilityMatrixService matrixService) : base(linkService)
        {
            this.typeLinkService = typeLinkService;
            this.matrixService = matrixService;
            this.linkService = linkService;
            this.requirementService = requirementService;
        }

        #region GETS

        public override ActionResult Create(LinkBetweenRequirementsEntity entity)
        {
            base.Create(entity);
            ModelState.Clear();
            return RedirectToAction("CreateNewLink", new { id = entity.RequirementOriginID });
        }

        public JsonResult GetWithCode(string code)
        {
            try
            {
                return Json(linkService.getWithCode(code),
                    JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("Invalid Request!");
            }

            LinkBetweenRequirementsEntity link = Service.get(id);

            if (link == null)
            {
                throw new ArgumentException("Invalid Request!");
            }

            ViewData.Add("RequirementOriginID", new SelectList(
                        new List<RequirementEntity>() { link.RequirementOrigin }, "RequirementID", 
                        "DisplayName", link.RequirementOrigin.RequirementID));

            ViewData.Add("RequirementTargetID", new SelectList(
                        new List<RequirementEntity>() { link.RequirementTarget }, "RequirementID", 
                        "DisplayName", link.RequirementTarget.RequirementID));

            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description", link.TypeLinkID));

            return View(link);
        }

        public ActionResult CreateNewLink(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("Invalid Request!");
                }

                RequirementEntity reqOrigin = requirementService.get(id);

                if (reqOrigin == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewData.Add("RequirementOriginID", new SelectList(
                        new List<RequirementEntity>() { reqOrigin }, "RequirementID", "DisplayName", id));

                ViewData.Add("RequirementTargetID", new SelectList(
                        requirementService.getRequirementsToLink(reqOrigin.RequirementID), "RequirementID",
                        "DisplayName"));

                ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetLink(string ReqOrigin, string ReqTarget)
        {
            try
            {
                return Json(linkService.get(ReqOrigin, ReqTarget), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetLinkRequirementsFromProject(string ProjectID)
        {
            try
            {
                IEnumerable<RequirementEntity> requirements = null;

                HashSet<LinkBetweenRequirementsEntity> links = new HashSet<LinkBetweenRequirementsEntity>();

                foreach (RequirementEntity req in requirements)
                {
                    IEnumerable<LinkBetweenRequirementsEntity> linksFilter =
                        linkService.filter(l => l.RequirementOriginID.Equals(req.RequirementID)
                        || l.RequirementTargetID.Equals(req.RequirementID));
                    foreach (LinkBetweenRequirementsEntity link in linksFilter)
                    {
                        links.Add(link);
                    }
                }

                return Json(links, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
