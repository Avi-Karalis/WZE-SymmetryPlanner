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

            // -------------------
            // Units & Weapons
            // -------------------
            modelBuilder.Entity<UnitWeapon>()
                .HasKey(uw => new { uw.UnitId, uw.WeaponId });

            modelBuilder.Entity<UnitWeapon>()
                .HasOne(uw => uw.Unit)
                .WithMany(u => u.UnitWeapon)
                .HasForeignKey(uw => uw.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UnitWeapon>()
                .HasOne(uw => uw.Weapon)
                .WithMany(w => w.UnitWeapon)
                .HasForeignKey(uw => uw.WeaponId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------------------
            // UnitSpecialAbility join table
            // -------------------
            modelBuilder.Entity<UnitUnitSpecialAbility>(entity => {
                entity.ToTable("unitunitspecialabilities"); // lowercase
                entity.HasKey(e => new { e.UnitId, e.UnitSpecialAbilityId });

                entity.HasOne(e => e.Unit)
                      .WithMany(u => u.UnitUnitSpecialAbility)
                      .HasForeignKey(e => e.UnitId)
                      .HasConstraintName("FK_unitunitspecialabilities_units_unitid");

                entity.HasOne(e => e.UnitSpecialAbility)
                      .WithMany(us => us.UnitUnitSpecialAbility)
                      .HasForeignKey(e => e.UnitSpecialAbilityId)
                      .HasConstraintName("FK_unitunitspecialabilities_unitspecialabilities_unitspecialabilityid");
            });

            // -------------------
            // WeaponSpecialAbility join table
            // -------------------
            modelBuilder.Entity<WeaponWeaponSpecialAbility>(entity => {
                entity.ToTable("weaponweaponspecialabilities"); // lowercase
                entity.HasKey(e => new { e.WeaponId, e.WeaponSpecialAbilityId });

                entity.HasOne(e => e.Weapon)
                      .WithMany(w => w.WeaponWeaponSpecialAbility)
                      .HasForeignKey(e => e.WeaponId);

                entity.HasOne(e => e.WeaponSpecialAbility)
                      .WithMany(ws => ws.WeaponWeaponSpecialAbility)
                      .HasForeignKey(e => e.WeaponSpecialAbilityId);
            });

            // -------------------
            // SpecialAbilities tables
            // -------------------
            modelBuilder.Entity<UnitSpecialAbility>(entity => {
                entity.ToTable("unitspecialabilities"); // lowercase
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<WeaponSpecialAbility>(entity => {
                entity.ToTable("weaponspecialabilities"); // lowercase
                entity.HasKey(e => e.Id);
            });

            // Apply any additional configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Optional: soft delete filters
            // modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt == null);
            // modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt == null);
            // modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt == null);
        }
    }
}
