﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class StakeholderClassificationEntity
    {
        [Key]
        public int ClassificationID { get; set; }
        [MinLength(6)]
        [MaxLength(50)]
        [Display(Name = "Classification Description")]
        public string description { get; set; }

    }
}
