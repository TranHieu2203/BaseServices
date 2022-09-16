using Autofac;
using Infrastructure.Interfaces;
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


            // Sau khi thêm mới services và interface thì khai báo để sử dụng
            builder.RegisterType<CustommerService>().As<ICustommerService>().InstancePerLifetimeScope();
            builder.RegisterType<SeUserService>().As<ISeUserService>().InstancePerLifetimeScope();

        }
    }
}
