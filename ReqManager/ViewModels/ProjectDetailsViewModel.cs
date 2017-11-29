﻿using ReqManager.Entities.Artifact;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using System.Collections.Generic;
using System.Data;

namespace ReqManager.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectEntity project { get; set; }
        public IEnumerable<RequirementEntity> requirements { get; set; }
        public IEnumerable<ProjectArtifactEntity> artifacts { get; set; }
        public IEnumerable<StakeholdersProjectEntity> stakeholders { get; set; }
        public DataTable requirementMatrix { get; set; }

        public ProjectDetailsViewModel()
        {
            stakeholders = new List<StakeholdersProjectEntity>();
            requirements = new List<RequirementEntity>();
            artifacts = new List<ProjectArtifactEntity>();
        }
    }
}