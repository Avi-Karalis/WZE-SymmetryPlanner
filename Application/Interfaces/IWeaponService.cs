using Domain.Entities;
using Application.DTOs;
namespace Application.Interfaces {
    public interface IWeaponService : IGenericService<Weapon, WeaponReadDto, WeaponCreateDto, WeaponUpdateDto>{
        Task<WeaponReadDto> GetFullByIdAsync(Guid id);
        Task<IEnumerable<WeaponReadDto>> GetAllFullAsync();
    }
}
