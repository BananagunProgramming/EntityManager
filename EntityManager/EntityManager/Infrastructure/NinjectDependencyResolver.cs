using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EF.Implementation;
using EF.Implementation.Abstract;
using EntityManager.Services;
using Ninject;

namespace EntityManager.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver, IDisposable
    {
        private readonly IKernel _kernel;
        
        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            var dbContextScopeFactory = new DbContextScopeFactory();

            _kernel.Bind<IClientQueryService>().To<ClientQueryService>().WithConstructorArgument(dbContextScopeFactory);
            _kernel.Bind<IClientCommandService>().To<ClientCommandService>().WithConstructorArgument(dbContextScopeFactory);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}