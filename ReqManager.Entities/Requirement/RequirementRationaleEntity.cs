﻿using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementRationaleEntity
    {
        [Key]
        public int RequirementRationaleID { get; set; }
        public int RequirementID { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionType { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionSubType { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionStakeholder { get; set; }
        [Required]
        [MaxLength(50)]
        public string importance { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionStatus { get; set; }
        [Required]
        public DateTime changedDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        public string rationale { get; set; }
    }
}
