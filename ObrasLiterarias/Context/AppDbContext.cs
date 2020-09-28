using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObrasLiterarias.Model;

namespace ObrasLiterarias.Context
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            { 
            }
            public DbSet<Autor> Autores { get; set; }
            protected override void OnModelCreating(ModelBuilder builder)
            {
            builder.Entity<Autor>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
            }
    }
}



