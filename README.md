1. Core> Entities > Tạo mới
2. Core> Interface > IClass
3. Core> IUnitOfwork>      (ICustommerReponsitory Custommers { get; })
4. Infrastructure> Respository > RespositoryClass
5. Infrastructure> UOW > (public ICustommerReponsitory Custommers { get; private set; }///    Custommers = new CustommerRepository(_context, _logger);)
6. Services>DTO
7. Services>Automapper
8. Services>Interface
9. Services>Services
10. BaseServices>DI>RepositoryHandlerModule( builder.RegisterType<CustommerService>().As<ICustommerService>().InstancePerLifetimeScope();)
11. BaseServices>Controller
