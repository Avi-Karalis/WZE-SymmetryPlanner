using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class WeaponRepository : GenericRepository<Weapon>, IWeaponRepository {
        private readonly ApplicationDbContext _context;

        public WeaponRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

    }
}
