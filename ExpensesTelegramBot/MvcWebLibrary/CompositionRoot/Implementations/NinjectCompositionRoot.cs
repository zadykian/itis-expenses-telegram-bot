using Ninject;
using Ninject.Extensions.NamedScope;
using Ninject.Extensions.Conventions;
using System;

namespace MvcWebLibrary
{
    internal class NinjectCompositionRoot : ICompositionRoot
    {
        private readonly StandardKernel container;

        public NinjectCompositionRoot()
        {
            container = new StandardKernel();
            ServiceConfigurator = new NinjectServiceConfigurator(container);
            ConfigureInitialServices();
        }

        public IServiceConfigurator ServiceConfigurator { get; }

        public IHttpRequestHandler GetHttpRequestHandler() 
            => container.Get<IHttpRequestHandler>();

        public ControllerBase GetControllerInstance(Type controllerType)
            => (ControllerBase)container.Get(controllerType);

        private void ConfigureInitialServices()
        {
            container.Bind<IRouter>()
                .To<Router>()
                .InSingletonScope();

            container.Bind<IModelBinder>()
                .To<ModelBinder>()
                .InSingletonScope();

            container.Bind<IHttpRequestHandler>()
                .To<HttpRequestHandler>()
                .InSingletonScope()
                .WithConstructorArgument((ICompositionRoot)this);

            container.Bind(configure => configure
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom(typeof(ControllerBase)));
        }
    }
}