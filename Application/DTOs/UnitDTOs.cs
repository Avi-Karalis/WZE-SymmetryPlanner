
namespace Application.DTOs;
public class UnitReadDto {
    public Guid Id { get; set; }
    public string Faction { get; set; }
    public string UnitType { get; set; }
    public IEnumerable<string> Designation { get; set; }
    public string? DesignationTypeLimit { get; set; }
    public sbyte DesignationLimitValue { get; set; }
    public sbyte DPCost { get; set; }
    public sbyte SPCost { get; set; }
    public sbyte MV { get; set; }
    public sbyte MW { get; set; }
    public sbyte CC { get; set; }
    public sbyte ST { get; set; }
    public sbyte DEF { get; set; }
    public sbyte AR { get; set; }
    public sbyte W { get; set; }
    public sbyte PW { get; set; }
    public sbyte LD { get; set; }
    public sbyte Base { get; set; }
    public sbyte Status { get; set; }

    public IEnumerable<UnitSpecialAbilityReadDto> UnitSpecialAbilities { get; set; } = new List<UnitSpecialAbilityReadDto>();
    public IEnumerable<WeaponReadDto> Weapons { get; set; } = new List<WeaponReadDto>();
    public IEnumerable<string>? FactionAvailabilities { get; set; }
}

public class UnitCreateDto {
    public string Faction { get; set; }
    public string UnitType { get; set; }
    public IEnumerable<string> Designation { get; set; }
    public string? DesignationTypeLimit { get; set; }
    public sbyte DesignationLimitValue { get; set; }
    public sbyte DPCost { get; set; }
    public sbyte SPCost { get; set; }
    public sbyte MV { get; set; }
    public sbyte MW { get; set; }
    public sbyte CC { get; set; }
    public sbyte ST { get; set; }
    public sbyte DEF { get; set; }
    public sbyte AR { get; set; }
    public sbyte W { get; set; }
    public sbyte PW { get; set; }
    public sbyte LD { get; set; }
    public sbyte Base { get; set; }

    public IEnumerable<Guid>? UnitSpecialAbilityIds { get; set; }
    public IEnumerable<Guid>? WeaponIds { get; set; }
    public IEnumerable<string>? FactionAvailabilities { get; set; }
}

public class UnitUpdateDto : UnitCreateDto {
    // For now identical to CreateDto, could add optional fields for patch-like updates
}
