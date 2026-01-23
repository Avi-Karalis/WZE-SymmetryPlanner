using Domain.Entities;
using Application.DTOs;
namespace Application.Interfaces;
public interface IWeaponSpecialAbilityService : IGenericService<WeaponSpecialAbility, WeaponSpecialAbilityReadDto, WeaponSpecialAbilityCreateDto, WeaponSpecialAbilityUpdateDto> {
    Task<IEnumerable<WeaponSpecialAbilityReadDto>> GetByNameAsync(string name);
}