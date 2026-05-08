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

        CreateMap<ForceListCreateDto, ForceList>()
            .ForMember(dest => dest.Allegiance,
                       opt => opt.MapFrom(src => new Allegiance { Type = src.Allegiance }))
            .ForMember(dest => dest.ForceListUnits, opt => opt.Ignore()) // ignore the collection
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .AfterMap((src, dest) => {
                dest.ForceListUnits = new List<ForceListUnit>();
                // If DTO had UnitIds, you could do:
                // foreach (var unitId in src.UnitIds)
                //     dest.ForceListUnits.Add(new ForceListUnit { UnitId = unitId });
            });

        // Domain → Read DTO
        CreateMap<ForceList, ForceListReadDto>()
            .ForCtorParam("allegiance", opt => opt.MapFrom(src => src.Allegiance.Name))
            .ForCtorParam("maxSp", opt => opt.MapFrom(src =>
                src.ForceListUnits.Where(flu => flu.Unit != null && flu.Unit.SPCost < 0)
                                  .Sum(flu => (int)Math.Abs(flu.Unit.SPCost))))
            .ForMember(dest => dest.Units, opt => opt.Ignore())
            .AfterMap((src, dest, ctx) => {
                dest.Units = src.ForceListUnits
                                .Where(flu => flu.Unit != null)
                                .Select(flu => ctx.Mapper.Map<UnitReadDto>(flu.Unit))
                                .ToList();
            });
        CreateMap<ForceListReadDto, ForceListUpdateDto>();

    }
}
