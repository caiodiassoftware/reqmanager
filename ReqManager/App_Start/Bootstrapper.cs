﻿using Autofac;
using Autofac.Integration.Mvc;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories;
using ReqManager.Filters;
using ReqManager.Notifications.Classes;
using ReqManager.Notifications.Interfaces;
using ReqManager.Services.Acess.Classes;
using ReqManager.Services.Task.Classes;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ReqManager.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<NotifierService>().As<INotifierService>().InstancePerRequest();
            builder.RegisterType<NotifierFilterAttribute>().AsActionFilterFor<Controller>().PropertiesAutowired();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(NotifierService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(StatusTaskService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}