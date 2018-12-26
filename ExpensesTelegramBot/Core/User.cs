using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class User : IEntity, IEquatable<User>
    {
        private List<RegularExpensesCategory> regularExpensesCategories;

        private List<Channel> channels;

        private User()
        {
        }

        public User(string secretLogin)
        {
            SecretLogin = secretLogin?.ToLowerInvariant() ??
                throw new ArgumentNullException(nameof(secretLogin));
        }

        public string SecretLogin { get; private set; }

        public IEnumerable<RegularExpensesCategory> RegularExpensesCategories
            => regularExpensesCategories.AsEnumerable();

        public IEnumerable<Channel> Channels => channels.AsEnumerable();

        public void AddRegularExpensesCategory(RegularExpensesCategory regularExpensesCategory)
        {
            if (!regularExpensesCategories.Contains(regularExpensesCategory))
            {
                regularExpensesCategories.Add(regularExpensesCategory);
            }
        }

        public void RemoveRegularExpensesCategory(RegularExpensesCategory regularExpensesCategory)
            => regularExpensesCategories.Remove(regularExpensesCategory);

        public void AddChannel(Channel channel) => channels.Add(channel);

        #region Equals
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (other == null) return false;
            return SecretLogin == other.SecretLogin;
        }

        public override int GetHashCode() => SecretLogin.GetHashCode();
        #endregion
    }
}