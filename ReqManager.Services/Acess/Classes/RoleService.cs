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

namespace ReqManager.Services.Acess.Classes
{
    public class RoleService : ServiceBase<Role>, IRoleService
    {
        public RoleService(IRoleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }
    }
}