using Vuefinity.Data.Models;
using Vuefinity.Services;

namespace Vuefinity.Services.Users
{
    public interface IUserService : ICrudService<User, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.
        IQueryable<User> Users { get; }

        Task DeleteAsync(int userId);

    }
}



