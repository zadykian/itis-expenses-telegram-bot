using Infrastructure;

namespace Application
{
    public abstract class DbAccessController
    {
        protected readonly ApplicationContext dbContext;

        public DbAccessController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}