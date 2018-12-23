using Infrastructure;
using MvcWebLibrary;

namespace Application
{
    public abstract class DbAccessController : ControllerBase
    {
        protected readonly ApplicationContext dbContext;

        protected override void Dispose(bool disposing)
        {
            dbContext.SaveChanges();
        }

        public DbAccessController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}