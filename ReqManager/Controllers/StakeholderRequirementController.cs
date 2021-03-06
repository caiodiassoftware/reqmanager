﻿using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        private IRequirementService requirementService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }
        private IStakeholderRequirementService service { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IRequirementService requirementService,
            IStakeholdersProjectService stakeholderProjectService) : base(service)
        {
            this.service = service;
            this.requirementService = requirementService;
            this.stakeholderProjectService = stakeholderProjectService;

            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("RequirementID", new SelectList(requirementService.getAll(), "RequirementID", "DisplayName"));
        }

        [HttpPost, ActionName("EditImportance")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int StakeholderRequirementID, int importanceValue)
        {
            StakeholderRequirementEntity entity = Service.get(StakeholderRequirementID);
            entity.importanceValue = importanceValue;
            return base.Edit(entity);
        }

        [HttpPost]
        public JsonResult Add(int StakeholderProjectID, int RequirementID, int importanceValue)
        {
            bool success = false;
            string message = string.Empty;

            try
            {
                StakeholderRequirementEntity stakeholder = new StakeholderRequirementEntity();
                stakeholder.StakeholdersProjectID = StakeholderProjectID;
                stakeholder.RequirementID = RequirementID;
                stakeholder.importanceValue = importanceValue;
                setCreationDate(ref stakeholder);
                if (TryValidateModel(stakeholder))
                {
                    Service.add(ref stakeholder);
                    success = true;
                    message = "Register was made with Success!";
                }
                else
                {
                    message = "Please enter all the fields!";
                }
            }
            catch (DbUpdateException)
            {
                message = "This requirement is already linked to this stakeholder!";
            }

            return Json(new { success, message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStakeholdersFromRequirement(int RequirementID)
        {
            try
            {
                return Json(service.getStakeholdersFromRequirement(RequirementID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ActionResult Create(StakeholderRequirementEntity entity)
        {
            base.Create(entity);
            return RedirectToAction("Details", "Requirement", new { @id = entity.RequirementID});
        }
    }
}
