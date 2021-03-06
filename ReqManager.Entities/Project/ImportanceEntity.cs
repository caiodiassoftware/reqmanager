﻿using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class ImportanceEntity
    {
        [Key]
        [Display(Name = "Importance")]
        public int ImportanceID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Importance")]
        public string description { get; set; }
    }
}
