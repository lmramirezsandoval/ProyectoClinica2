using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace ProyectoClinica2.DependencyInjection
{
    public class DependencyInjectionConfiguration : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public DependencyInjectionConfiguration(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new DependencyInjectionConfiguration(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}