﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Tasks.Interfaces;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Task.Interfaces;

namespace ReqManager.Services.Task.Classes
{
    public class HistoryTaskService : ServiceBase<HistoryTask>, IHistoryTaskService
    {
        public HistoryTaskService(IHistoryTaskRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}