using AutoMapper;
using Domain.Entities;
using Application.DTOs;

public class MappingProfile : Profile {
    public MappingProfile() {
        // ===== UnitSpecialAbility =====
        CreateMap<Unit, UnitReadDto>()
            .ForMember(dest => dest.UnitSpecialAbilities,
                       opt => opt.MapFrom(src => src.UnitUnitSpecialAbilities.Select(uusa => uusa.UnitSpecialAbility)))
            .ForMember(dest => dest.Weapons,
                       opt => opt.MapFrom(src => src.UnitWeapon.Select(uw => uw.Weapon)));

        CreateMap<UnitCreateDto, Unit>();
        CreateMap<UnitUpdateDto, Unit>();

        // ===== UnitSpecialAbility =====
        CreateMap<UnitSpecialAbility, UnitSpecialAbilityReadDto>();
        CreateMap<UnitSpecialAbilityCreateDto, UnitSpecialAbility>();
        CreateMap<UnitSpecialAbilityUpdateDto, UnitSpecialAbility>();

        // ===== Weapon =====
        CreateMap<Weapon, WeaponReadDto>()
            .ForMember(dest => dest.WeaponSpecialAbilities,
                       opt => opt.MapFrom(src => src.WeaponWeaponSpecialAbility.Select(wwsa => wwsa.WeaponSpecialAbility)));

        CreateMap<WeaponCreateDto, Weapon>();
        CreateMap<WeaponUpdateDto, Weapon>();

        // ===== WeaponSpecialAbility =====
        CreateMap<WeaponSpecialAbility, WeaponSpecialAbilityReadDto>();
        CreateMap<WeaponSpecialAbilityCreateDto, WeaponSpecialAbility>();
        CreateMap<WeaponSpecialAbilityUpdateDto, WeaponSpecialAbility>();

        CreateMap<ForceListCreateDto, ForceList>();
        CreateMap<ForceList, ForceListReadDto>();
        CreateMap<ForceList, ForceListUpdateDto>();
        CreateMap<ForceListReadDto, ForceList>();
        CreateMap<ForceListUpdateDto, ForceList>();
    }
}
