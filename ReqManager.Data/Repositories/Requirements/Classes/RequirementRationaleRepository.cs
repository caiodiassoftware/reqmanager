﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementRationaleRepository : RepositoryBase<RequirementRationale>, IRequirementRationaleRepository
    {
        public RequirementRationaleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
