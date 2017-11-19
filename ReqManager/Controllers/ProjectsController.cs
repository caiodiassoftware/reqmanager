﻿using System;
using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using ReqManager.Services.Directories.Interfaces;
using ReqManager.ViewModels;
using System.Linq;

namespace ReqManager.Controllers
{
    public class ProjectsController : BaseController<ProjectEntity>
    {
        private IHistoryProjectService historyProjectService { get; set; }
        private IScanDirectoryService directory { get; set; }
        private IProjectService projectService { get; set; }
        private IProjectArtifactService projectArtifact { get; set; }
        private IProjectRequirementsService projectRequirements { get; set; }

        public ProjectsController(
            IProjectService projectService,
            IUserService userService,
            IProjectArtifactService projectArtifact,
            IProjectRequirementsService projectRequirements,
            IProjectPhasesService phasesService,
            IHistoryProjectService historyProjectService,
            IScanDirectoryService directory) : base(projectService)
        {
            this.projectArtifact = projectArtifact;
            this.projectRequirements = projectRequirements;
            this.historyProjectService = historyProjectService;
            this.directory = directory;
            this.projectService = projectService;

            ViewData.Add("ProjectPhasesID", new SelectList(phasesService.getAll(), "ProjectPhasesID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public JsonResult GetFolders(int ProjectID)
        {
            try
            {
                ProjectEntity prj = Service.get(ProjectID);
                var path = directory.getFolders(prj.pathForTraceability);
                return Json(path, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ActionResult Details(int? id)
        {
            try
            {
                ProjectDetailsViewModel prj = new ProjectDetailsViewModel();

                prj.project = projectService.get(id);
                prj.artifacts = projectArtifact.getArtifactsByProject(Convert.ToInt32(id));
                prj.requirements = projectRequirements.getRequirementsByProject(Convert.ToInt32(id)).Select(r => r.Requirement);

                return View(prj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(ProjectEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(ref entity);

                    HistoryProjectEntity history = new HistoryProjectEntity();
                    ProjectEntity project = Service.get(entity.ProjectID);
                    history.ProjectID = project.ProjectID;
                    history.UserID = getIdUser();
                    history.descriptionPhases = project.ProjectPhases.description;
                    history.endDate = Convert.ToDateTime(project.endDate);
                    history.startDate = Convert.ToDateTime(project.startDate);

                    historyProjectService.add(ref history);

                    Service.saveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (DbEntityValidationException ex)
            {
                return getMessageDbValidation(entity, ex);
            }
            catch (DbUpdateException ex)
            {
                return getMessageDbUpdateException(entity, ex);
            }
            catch (Exception ex)
            {
                return getMessageGeralException(entity, ex);
            }
        }
    }
}
