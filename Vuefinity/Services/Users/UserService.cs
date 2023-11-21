using Microsoft.EntityFrameworkCore;
using Vuefinity.Data;
using Vuefinity.Data.Exceptions;
using Vuefinity.Data.Models;

namespace Vuefinity.Services.Users
{
    public class UserService : IUserService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly VuefinityDdContext _context;

        public UserService(VuefinityDdContext context)
        {
            _context = context;
        }


        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.Where(e => e.Id == id).FirstAsync();

            if (user is null)
                throw new EntityNotFoundException(nameof(user), id);

            return user;
        }
        public async Task<User> AddAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
  
        public async Task<User> UpdateAsync(User obj)
        {
            {
                if (!await UserExistsAsync(obj.Id))
                    throw new EntityNotFoundException(nameof(User), obj.Id);


                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return obj;
            }
        }

      

        //Helper Methods
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
    }
}

