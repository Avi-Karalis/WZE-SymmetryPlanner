
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options) { }

        public DbSet<WeaponSpecialAbility> WeaponSpecialAbilities { get; set; }
        public DbSet<UnitSpecialAbility> UnitSpecialAbilities { get; set; }
        public DbSet<UnitWeapon> UnitWeapons { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UnitWeapon>()
               .HasOne(uw => uw.Unit)
               .WithMany(u => u.UnitWeapon)  // Unit has many UnitWeapons
               .HasForeignKey(uw => uw.UnitId)  // Foreign key for Unit
               .OnDelete(DeleteBehavior.Cascade);  // Handle cascading delete if needed

            modelBuilder.Entity<UnitWeapon>()
                .HasOne(uw => uw.Weapon)
                .WithMany(w => w.UnitWeapon)  // Weapon has many UnitWeapons
                .HasForeignKey(uw => uw.WeaponId)  // Foreign key for Weapon
                .OnDelete(DeleteBehavior.Cascade);  // Handle cascading delete if needed

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Soft delete filter (optional)
            //modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt == null);
            //modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt == null);
            //modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt == null);

        }

    }
}
