﻿using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementTemplateService : IService<RequirementTemplateEntity>
    {
        List<RequirementTemplateEntity> filterByRequirementType(int RequirementType);
    }
}
