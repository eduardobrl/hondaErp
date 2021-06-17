using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hondaerp.Supliers.Models;
using hondaerp.Dealerships.Models;

namespace hondaerp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Suplier>()
                .HasIndex(u => u.CNPJ)
                .IsUnique();
        }

        public DbSet<Suplier> Suplier { get; set; }
        public DbSet<Dealership> Dealership { get; set; }
    }
}
