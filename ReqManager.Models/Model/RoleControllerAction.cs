﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Models
{
    [Table("ROLE_CONTROLLER_ACTION", Schema = "ACESS")]
    public class RoleControllerAction
    {        
        [Key]
        [ScaffoldColumn(false)]
        public int ID_roleControllerAction { get; set; }
        public int ID_role { get; set; }
        public int ID_controllerAction { get; set; }

        [ForeignKey("ID_role")]
        public virtual Role roles { get; set; }
        [ForeignKey("ID_controllerAction")]
        public virtual ControllerAction controllerActions { get; set; }
    }
}
