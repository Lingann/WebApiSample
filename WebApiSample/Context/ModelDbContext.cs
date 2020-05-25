using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;
namespace WebApiSample.Context
{
    public class ModelDbContext :DbContext
    {

        public ModelDbContext(DbContextOptions options) : base (options) { }

        public DbSet<Model> Models { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=sinzone.db");
    }
}
