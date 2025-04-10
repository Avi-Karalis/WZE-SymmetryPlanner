using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<SpecialAbility> SpecialAbilities => Set<SpecialAbility>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Soft delete filter (optional)
            modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt != null);
            modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt != null);
            modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt != null);

        }
    }
}
