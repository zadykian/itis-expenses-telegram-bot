namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext applicationContext;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            UserRepository = new UserRepository(applicationContext);
            SingleExpenseRepository = new SingleExpenseRepository(applicationContext);
            this.applicationContext = applicationContext;
        }

        public IUserRepository UserRepository { get; private set; }
        public ISingleExpenseRepository SingleExpenseRepository { get; private set; }

        public int Save() => applicationContext.SaveChanges();

        public void Dispose() => applicationContext.Dispose();
    }
}