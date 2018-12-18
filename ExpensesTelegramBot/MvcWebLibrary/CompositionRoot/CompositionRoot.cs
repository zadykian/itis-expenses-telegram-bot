using Ninject;
using Ninject.Extensions.NamedScope;
using Ninject.Extensions.Conventions;
using System;

namespace MvcWebLibrary
{
    internal class CompositionRoot : ICompositionRoot
    {
        private readonly StandardKernel container;

        public CompositionRoot()
        {
            container = new StandardKernel();
            ConfigureInitialServices();
        }

        private void ConfigureInitialServices()
        {
            container.Bind<ApplicationContext>()
                .ToSelf()
                .InParentScope();

            container.Bind<IRouter>()
                .To<Router>()
                .InSingletonScope();

            container.Bind<IModelBinder>()
                .To<ModelBinder>()
                .InSingletonScope();

            container.Bind<IRequestHandler>()
                .To<RequestHandler>()
                .InSingletonScope()
                .WithConstructorArgument((ICompositionRoot)this);

            container.Bind(configure => configure
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom(typeof(ControllerBase)));
        }

        public IRequestHandler GetRequestHandler() => container.Get<IRequestHandler>();

        public ControllerBase GetControllerInstance(Type controllerType)
            => (ControllerBase)container.Get(controllerType);
    }
}