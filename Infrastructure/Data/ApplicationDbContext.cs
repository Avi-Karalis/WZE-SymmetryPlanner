
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
        public DbSet<UnitWeapon> UnitWeapons { get; set; }
        public DbSet<WeaponSpecialAbility> WeaponSpecialAbilities => Set<WeaponSpecialAbility>();
        public DbSet<UnitSpecialAbility> UnitSpecialAbilities => Set<UnitSpecialAbility>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Weapon> Weapons => Set<Weapon>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<UnitWeapon>().HasOne(uw => uw.Unit).WithMany(u => u.Weapons).HasForeignKey(uw=>uw.UnitId);
            modelBuilder.Entity<UnitWeapon>().HasOne(uw => uw.Weapon).WithMany(u => u.UnitWeapon).HasForeignKey(uw=>uw.WeaponId);

            // Soft delete filter (optional)
            //modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt == null);
            //modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt == null);
            //modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt == null);

        }
    }
}
