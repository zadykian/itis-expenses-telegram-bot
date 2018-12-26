using System.Collections.Generic;
using System;

namespace Core
{
    public class RegularExpensesCategory : IValueObject
    {
        private RegularExpensesCategory()
        {
        }
        
        public RegularExpensesCategory(User user, string category)
        {
            Id = Guid.NewGuid();

            User = user;

            UserSecretLogin = User?.SecretLogin;

            Category = category?.ToLowerInvariant() ??
                throw new ArgumentNullException(nameof(category));
        }

        public Guid Id { get; private set; }

        public string UserSecretLogin { get; private set; }

        public User User { get; private set; }

        public string Category { get; private set; }

        public void SetUser(User user)
        {
            if (User != null)
            {
                throw new InvalidOperationException(
                    "User cannot be assinged more than once");
            }
            User = user;
            UserSecretLogin = user.SecretLogin;
        }
    }
}
