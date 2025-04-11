
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
        public DbSet<WeaponSpecialAbility> WeaponSpecialAbilities => Set<WeaponSpecialAbility>();
        public DbSet<UnitSpecialAbility> UnitSpecialAbilities => Set<UnitSpecialAbility>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Weapon> Weapons => Set<Weapon>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Unit>().HasMany(u => u.Weapons).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Unit>().HasMany(u => u.UnitSpecialAbilities).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Weapon>().HasMany(w => w.WeaponSpecialAbilities).WithOne().OnDelete(DeleteBehavior.NoAction);

            // Soft delete filter (optional)
            //modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt == null);
            //modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt == null);
            //modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt == null);

        }
    }
}
