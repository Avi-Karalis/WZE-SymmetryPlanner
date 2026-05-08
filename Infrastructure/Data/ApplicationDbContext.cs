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
        public DbSet<ForceList> ForceLists { get; set; }
        public DbSet<ForceListUnit> ForceListUnits { get; set; }
        public DbSet<User> Users { get; set; }
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
            modelBuilder.Entity<UnitUnitSpecialAbility>(entity =>
            {
                entity.ToTable("UnitUnitSpecialAbilities");
                entity.HasKey(e => new { e.UnitId, e.UnitSpecialAbilityId });

                entity.HasOne(e => e.Unit)
                      .WithMany(u => u.UnitUnitSpecialAbilities)  // <--- use correct property
                      .HasForeignKey(e => e.UnitId)
                      .HasConstraintName("FK_UnitUnitSpecialAbilities_Units_UnitId");

                entity.HasOne(e => e.UnitSpecialAbility)
                      .WithMany(us => us.UnitUnitSpecialAbility)  // <--- this is correct
                      .HasForeignKey(e => e.UnitSpecialAbilityId)
                      .HasConstraintName("FK_UnitUnitSpecialAbilities_UnitSpecialAbilities_UnitSpecialAbilityId");
            });

            // -------------------
            // WeaponSpecialAbility join table
            // -------------------
            modelBuilder.Entity<WeaponWeaponSpecialAbility>(entity => {
                entity.ToTable("WeaponWeaponSpecialAbilities"); // lowercase
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
                entity.ToTable("UnitSpecialAbilities"); // lowercase
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<WeaponSpecialAbility>(entity => {
                entity.ToTable("WeaponSpecialAbilities"); // lowercase
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<ForceListUnit>()
                .HasKey(flu => flu.Id);

            modelBuilder.Entity<ForceListUnit>()
                .HasOne(flu => flu.ForceList)
                .WithMany(fl => fl.ForceListUnits)
                .HasForeignKey(flu => flu.ForceListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForceListUnit>()
                .HasOne(flu => flu.Unit)
                .WithMany()
                .HasForeignKey(flu => flu.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Apply any additional configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // -------------------
            // Users
            // -------------------
            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(u => u.ProviderUserId).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });
  
            // Optional: soft delete filters
            // modelBuilder.Entity<SpecialAbility>().HasQueryFilter(w => w.DeletedAt == null);
            // modelBuilder.Entity<Unit>().HasQueryFilter(u => u.DeletedAt == null);
            // modelBuilder.Entity<Weapon>().HasQueryFilter(w => w.DeletedAt == null);
        }
    }
}
