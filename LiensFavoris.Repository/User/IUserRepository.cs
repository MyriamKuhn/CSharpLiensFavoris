﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.User
{
    public interface IUserRepository
    {
        public List<UserModel> GetAllUsers();
    }
}
