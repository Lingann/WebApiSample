using System;
using System.Data.Common;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Owner> Owners { get; set; }
    
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=sinzone.db");
    }
}
