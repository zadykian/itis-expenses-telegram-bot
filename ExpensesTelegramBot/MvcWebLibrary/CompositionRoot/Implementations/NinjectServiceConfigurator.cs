using Ninject;
using Ninject.Extensions.NamedScope;

namespace MvcWebLibrary
{
    internal class NinjectServiceConfigurator : IServiceConfigurator
    {
        private readonly StandardKernel container;

        public NinjectServiceConfigurator(StandardKernel container)
        {
            this.container = container;
        }

        public void AddParentScopedServiceWithConstructorArg<TService, TArgument>(TArgument arg)
        {
            container.Bind<TService>()
                .ToSelf()
                .InParentScope()
                .WithConstructorArgument(arg);
        }
    }
}