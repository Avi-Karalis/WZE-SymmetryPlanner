using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.MigrationConfiguration {
    public class UnitWeaponConfiguration : IEntityTypeConfiguration<UnitWeapon>{
        public void Configure(EntityTypeBuilder<UnitWeapon> builder) {
            builder.HasKey(uw => new { uw.UnitId, uw.WeaponId }); // Composite key configuration

            builder.HasOne(uw => uw.Unit)
                   .WithMany(u => u.UnitWeapon)
                   .HasForeignKey(uw => uw.UnitId);

            builder.HasOne(uw => uw.Weapon)
                   .WithMany(w=>w.UnitWeapon)
                   .HasForeignKey(uw => uw.WeaponId);
        }
    }
}
