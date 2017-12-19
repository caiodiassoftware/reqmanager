﻿using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Linq;

namespace ReqManager.Controllers
{
    public class StakeholdersProjectController : BaseController<StakeholdersProjectEntity>
    {
        public StakeholdersProjectController(
            IStakeholdersProjectService service,
            IProjectService projectService,
            IStakeholdersService stakeholderService) : base(service)
        {
            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "description");
            ViewBag.StakeholderID = new SelectList(stakeholderService.getAll(), "StakeholderID", "DisplayName");
        }

        [HttpPost]
        public void Add(int StakeholderID, int ProjectID, string description)
        {
            try
            {
                StakeholdersProjectEntity stakeholderProject = new StakeholdersProjectEntity();
                stakeholderProject.ProjectID = ProjectID;
                stakeholderProject.StakeholderID = StakeholderID;
                stakeholderProject.description = description;
                base.Create(stakeholderProject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetStakeholdersFromProject(int ProjectID)
        {
            try
            {
                return Json(Service.getAll().Where(s => s.ProjectID.Equals(ProjectID)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
