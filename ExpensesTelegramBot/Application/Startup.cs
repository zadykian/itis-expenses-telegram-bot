using Ninject;

namespace Application
{
    public class Startup
    {
        private readonly StandardKernel container;

        public Startup()
        {
            container = new StandardKernel();
        }

        public void ConfigureServices()
        {
            container.Bind<IRequestHandler>().To<RequestHandler>();
            container.Bind<IRouter>().To<Router>();
            container.Bind<IModelBinder>().To<ModelBinder>();
        }

        public IRequestHandler GetRequestHandler() => container.Get<IRequestHandler>();
    }
}
