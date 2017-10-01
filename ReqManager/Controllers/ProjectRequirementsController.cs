﻿using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class ProjectRequirementsController : BaseController<ProjectRequirementsEntity>
    {
        public ProjectRequirementsController(
            IProjectRequirementsService service,
            IProjectService projectService,
            IUserService userService,
            IRequirementService reqService)
            : base(service)
        {
            ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
        }
    }
}
