﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class ProjectService : ServiceBase<Model.Project, ProjectEntity>, IProjectService
    {
        private IHistoryProjectService historyProjectService { get; set; }

        public ProjectService(
            IProjectRepository repository,
            IHistoryProjectService historyProjectService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.historyProjectService = historyProjectService;
        }

        public decimal getCost(int ProjectID)
        {
            try
            {
                return 1;
                //return requirementService.getRequirementsByProject(ProjectID).Sum(r => r.cost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update(ref ProjectEntity entity, int UserID)
        {
            try
            {
                unit.BeginTransaction();

                ProjectEntity prj = get(entity.ProjectID);

                base.update(ref entity, false);

                HistoryProjectEntity history = new HistoryProjectEntity();
                history.ProjectID = entity.ProjectID;
                history.CreationUserID = UserID;
                history.descriptionPhases = prj.ProjectPhases.description;
                history.endDate = Convert.ToDateTime(prj.endDate);
                history.startDate = Convert.ToDateTime(prj.startDate);
                historyProjectService.add(ref history, false);

                unit.Commit();
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }

        public bool isPreTraceability(ProjectEntity project)
        {
            try
            {
                return project.ProjectPhases.ProjectPhasesID.Equals(1) || project.ProjectPhases.ProjectPhasesID.Equals(2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool isPosTraceability(ProjectEntity project)
        {
            try
            {
                return !isPreTraceability(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
