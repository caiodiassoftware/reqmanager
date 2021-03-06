﻿using ReqManager.Entities.Acess;
using ReqManager.Entities.Artifact;
using ReqManager.Entities.Requirement;
using System;
using System.ComponentModel.DataAnnotations;
namespace ReqManager.Entities.Link
{
    public class LinkBetweenRequirementsArtifactsEntity
    {
        [Key]
        [Display(Name = "Requirement Artifact Link")]
        public int LinkArtifactRequirementID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Artifact")]
        public int ProjectArtifactID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Type Link")]
        public int TypeLinkID { get; set; }
        [MaxLength(25)]
        [Display(Name = "R-A Code")]
        public string code { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; }

        public string DisplayName
        {
            get
            {
                return this.Requirement.code + " to " + this.ProjectArtifact.code + " using " + this.TypeLink.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectArtifactEntity ProjectArtifact { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
