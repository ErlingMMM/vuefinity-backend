﻿using Vuefinity.Data.Models;
using Vuefinity.Services;

namespace Vuefinity.Services.Users
{
    public interface IUserService : ICrudService<User, string>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.
    }
}



