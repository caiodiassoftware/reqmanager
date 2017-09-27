﻿using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class ProjectEntity
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Phases")]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(300)]
        [Display(Name = "Path")]
        public string pathForTraceability { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Environmental Information")]
        public string environmentalInformation { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Management Information")]
        public string managementInformation { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? endDate { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectPhasesEntity ProjectPhases { get; set; }
    }
}
