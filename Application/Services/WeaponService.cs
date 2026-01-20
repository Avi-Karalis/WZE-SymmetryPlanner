using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services {
    public class WeaponService : GenericService<Weapon>, IWeaponService {
        private readonly IWeaponRepository _weaponRepository;
        public WeaponService(IWeaponRepository repository) : base(repository) {
            _weaponRepository = repository;
        }
    }
}
