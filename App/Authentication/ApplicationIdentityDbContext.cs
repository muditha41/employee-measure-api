using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Authentication
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserFriends> UserFriends { get; set; }
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          

            builder.Entity<UserFriends>()
                .HasOne(i=>i.Friend)
                .WithOne(x=>x.Friend)
                .HasForeignKey<UserFriends>(p => p.FriendId)
                .OnDelete(DeleteBehavior.NoAction); 
            builder.Entity<UserFriends>()
            .HasOne(i => i.User)
            .WithOne(y=>y.UserFriend)
            .HasForeignKey<UserFriends>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
