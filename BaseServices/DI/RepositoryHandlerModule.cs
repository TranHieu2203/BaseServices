using Autofac;
using Core.Interfaces;
using Infrastructure.UOW;
using Service.Interface;
using Service.Service;

namespace BaseServices.DI
{
    public class RepositoryHandlerModule : Module
    {
        /// <summary>
        /// Load register DI for builder
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();
            builder.RegisterType<CustommerService>().As<ICustommerService>().InstancePerLifetimeScope();


        }
    }
}
