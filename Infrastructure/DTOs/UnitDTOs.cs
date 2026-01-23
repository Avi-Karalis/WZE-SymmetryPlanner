
namespace Infrastructure.DTOs {
    public record UnitReadDto(
        Guid Id,
        string Faction,
        string UnitType,
        IEnumerable<string> Designation,
        string? DesignationTypeLimit,
        sbyte DesignationLimitValue,
        sbyte DPCost,
        sbyte SPCost,
        sbyte MV,
        sbyte MW,
        sbyte CC,
        sbyte ST,
        sbyte DEF,
        sbyte AR,
        sbyte W,
        sbyte PW,
        sbyte LD,
        sbyte Base,
        IEnumerable<string>? FactionAvailabilities,
        IEnumerable<UnitSpecialAbilityReadDto> SpecialAbilities,
        IEnumerable<WeaponReadDto> Weapons
    );
    public record UnitCreateDto(
        string Faction,
        string UnitType,
        IEnumerable<string> Designation,
        string? DesignationTypeLimit,
        sbyte DesignationLimitValue,
        sbyte DPCost,
        sbyte SPCost,
        sbyte MV,
        sbyte MW,
        sbyte CC,
        sbyte ST,
        sbyte DEF,
        sbyte AR,
        sbyte W,
        sbyte PW,
        sbyte LD,
        sbyte Base,
        IEnumerable<string>? FactionAvailabilities,
        IEnumerable<Guid>? UnitSpecialAbilityIds,
        IEnumerable<Guid>? WeaponIds
    );

    public record UnitUpdateDto(
        Guid Id,
        string Faction,
        string UnitType,
        IEnumerable<string> Designation,
        string? DesignationTypeLimit,
        sbyte DesignationLimitValue,
        sbyte DPCost,
        sbyte SPCost,
        sbyte MV,
        sbyte MW,
        sbyte CC,
        sbyte ST,
        sbyte DEF,
        sbyte AR,
        sbyte W,
        sbyte PW,
        sbyte LD,
        sbyte Base,
        IEnumerable<string>? FactionAvailabilities,
        IEnumerable<Guid>? UnitSpecialAbilityIds,
        IEnumerable<Guid>? WeaponIds
     );

}
