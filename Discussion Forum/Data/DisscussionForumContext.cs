using Discussion_Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Data
{
    public class DisscussionForumContext : IdentityDbContext<UserAccount, IdentityRole<int>, int>//,IdentityRole<int>,int>/*DbContext*/
    {
        public DisscussionForumContext(DbContextOptions<DisscussionForumContext> options)
            : base(options)
        {
        }
        public DbSet<UserAccount> User { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAccount>().ToTable("User");

        }
    }

}

