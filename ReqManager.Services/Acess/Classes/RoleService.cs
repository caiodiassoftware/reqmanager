﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqManager.Entities.Acess;
using AutoMapper;
using System.Reflection;
using ReqManager.Data.Repositories.Requirements.Interfaces;

namespace ReqManager.Services.Acess.Classes
{
    public class RoleService : ServiceBase<Role, RoleEntity>, IRoleService
    {
        public RoleService(IRoleRepository repository, IRequirementTraceabilityMatrixRepository traceability, IUnitOfWork unit) : base(repository, unit)
        {
            traceability.getMatrix();
        }
    }
}
