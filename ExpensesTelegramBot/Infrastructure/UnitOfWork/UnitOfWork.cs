namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        public UnitOfWork(ApplicationContext context)
        {
            UserRepository = new UserRepository(context);
            SingleExpenseRepository = new SingleExpenseRepository(context);
            this.context = context;
        }

        public IUserRepository UserRepository { get; private set; }
        public ISingleExpenseRepository SingleExpenseRepository { get; private set; }

        public int Save() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}