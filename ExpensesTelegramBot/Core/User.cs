﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class User : Entity
    {
        public int PasswordHash { get; set; }

        public void UpdatePassword(string newPassword)
        {
            PasswordHash = newPassword.GetHashCode();
        }
    }
}