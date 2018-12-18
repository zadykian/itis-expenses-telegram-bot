using Infrastructure;
using MvcWebLibrary;

namespace Application
{
    public abstract class DbAccessController : ControllerBase
    {
        protected readonly ApplicationContext dbContext;

        public DbAccessController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}