

namespace Application.DTOs;
public class WeaponReadDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public sbyte? CCMod { get; set; }
    public sbyte? CCDam { get; set; }
    public sbyte? ShortRange { get; set; }
    public sbyte? ShortRangeMod { get; set; }
    public sbyte? ShortRangeDam { get; set; }
    public sbyte? LongRange { get; set; }
    public sbyte? LongRangeMod { get; set; }
    public sbyte? LongRangeDam { get; set; }
    public sbyte CCDamageMultiplier { get; set; }
    public sbyte SRDamageMultiplier { get; set; }
    public sbyte LRDamageMultiplier { get; set; }
    public sbyte CritFail { get; set; }
    public bool DynamicDAM { get; set; }
    public bool DynamicRange { get; set; }

    public IEnumerable<WeaponSpecialAbilityReadDto> WeaponSpecialAbilities { get; set; } = new List<WeaponSpecialAbilityReadDto>();
}

public class WeaponCreateDto {
    public string Name { get; set; }
    public sbyte? CCMod { get; set; }
    public sbyte? CCDam { get; set; }
    public sbyte? ShortRange { get; set; }
    public sbyte? ShortRangeMod { get; set; }
    public sbyte? ShortRangeDam { get; set; }
    public sbyte? LongRange { get; set; }
    public sbyte? LongRangeMod { get; set; }
    public sbyte? LongRangeDam { get; set; }
    public sbyte? CCDamageMultiplier { get; set; }
    public sbyte? SRDamageMultiplier { get; set; }
    public sbyte? LRDamageMultiplier { get; set; }
    public sbyte CritFail { get; set; } = 20;
    public bool DynamicDAM { get; set; } = false;
    public bool DynamicRange { get; set; } = false;

    public IEnumerable<Guid>? WeaponSpecialAbilityIds { get; set; }
}

public class WeaponUpdateDto : WeaponCreateDto { }
