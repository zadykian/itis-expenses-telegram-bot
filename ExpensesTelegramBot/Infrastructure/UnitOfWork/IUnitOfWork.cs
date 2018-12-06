using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ISingleExpenseRepository SingleExpenseRepository { get; }
        int Save();
    }
}
