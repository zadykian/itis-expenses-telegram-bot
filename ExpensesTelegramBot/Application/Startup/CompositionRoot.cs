using Ninject;
using Ninject.Extensions.NamedScope;
using Ninject.Extensions.Conventions;
using Infrastructure;
using System;

namespace Application
{
    public class CompositionRoot : ICompositionRoot
    {
        private readonly StandardKernel container;

        public CompositionRoot()
        {
            container = new StandardKernel();
        }

        public void ConfigureServices()
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