﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Tasks.Interfaces;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Task.Classes
{

    public class TaskTypeService : ServiceBase<TaskType>, ITaskTypeService
    {
        public TaskTypeService(ITaskTypeRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
