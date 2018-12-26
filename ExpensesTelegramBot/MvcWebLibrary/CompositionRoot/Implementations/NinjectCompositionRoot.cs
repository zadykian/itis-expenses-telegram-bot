using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Reflection;

namespace MvcWebLibrary
{
    internal class NinjectCompositionRoot : ICompositionRoot
    {
        private readonly StandardKernel container;
        private readonly Assembly callingAssembly;

        public NinjectCompositionRoot(Assembly callingAssembly)
        {
            this.callingAssembly = callingAssembly;
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

            container.Bind<Assembly>()
                .ToConstant(callingAssembly)
                .InThreadScope();

            container.Bind(configure => configure
                .From(callingAssembly)
                .SelectAllClasses()
                .InheritedFrom(typeof(ControllerBase)));
        }
    }
}