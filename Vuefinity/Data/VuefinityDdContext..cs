using Microsoft.EntityFrameworkCore;
using Vuefinity.Data.Models;
using System;

namespace Vuefinity.Data
{
    public class VuefinityDdContext : DbContext
    {

        public VuefinityDdContext(DbContextOptions<VuefinityDdContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "TestUser1",
                    Email = "test@mail.com",
                    Score = 0
                },
                new User
                {
                    Id = 2,
                    Name = "TestUser2",
                    Email = "test2@mail.com",
                    Score = 0
                }
            );
        }
    }
}
