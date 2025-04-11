using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data {
    public static class SeedData {
        public static void Seed(ApplicationDbContext context) {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if any data already exists to avoid seeding again
            if (context.Units.Any() || context.Weapons.Any()) {
                return; // Data is already seeded
            }
            List<WeaponSpecialAbility> weaponSpecialAbilities = new() { };
            List<UnitSpecialAbility> unitSpecialAbilities = new() { };
            List<Weapon> weapons = new() { };

            context.Weapons.AddRange(weapons);

            // Save all the data to the database
            context.SaveChanges();
            // Seed data for Units
            List<Unit> units = new (){ };

            context.Units.AddRange(units);
            context.SaveChanges();
            // Seed data for Weapons
         
        }
    }
}
