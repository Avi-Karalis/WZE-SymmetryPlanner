

namespace Infrastructure.DTOs {
    public record WeaponReadDto(
        Guid Id,
        string Name,
        sbyte? CCMod,
        sbyte? CCDam,
        sbyte? ShortRange,
        sbyte? ShortRangeMod,
        sbyte? ShortRangeDam,
        sbyte? LongRange,
        sbyte? LongRangeMod,
        sbyte? LongRangeDam,
        sbyte CCDamageMultiplier,
        sbyte SRDamageMultiplier,
        sbyte LRDamageMultiplier,
        sbyte CritFail,
        bool DynamicDAM,
        IEnumerable<WeaponSpecialAbilityReadDto> SpecialAbilities
    );
    public record WeaponCreateDto(
    string Name,
    sbyte? CCMod,
    sbyte? CCDam,
    sbyte? ShortRange,
    sbyte? ShortRangeMod,
    sbyte? ShortRangeDam,
    sbyte? LongRange,
    sbyte? LongRangeMod,
    sbyte? LongRangeDam,
    sbyte CCDamageMultiplier,
    sbyte SRDamageMultiplier,
    sbyte LRDamageMultiplier,
    sbyte CritFail,
    bool DynamicDAM,
    IEnumerable<Guid>? WeaponSpecialAbilityIds
);
    public record WeaponUpdateDto(
    Guid Id,
    string Name,
    sbyte? CCMod,
    sbyte? CCDam,
    sbyte? ShortRange,
    sbyte? ShortRangeMod,
    sbyte? ShortRangeDam,
    sbyte? LongRange,
    sbyte? LongRangeMod,
    sbyte? LongRangeDam,
    sbyte CCDamageMultiplier,
    sbyte SRDamageMultiplier,
    sbyte LRDamageMultiplier,
    sbyte CritFail,
    bool DynamicDAM,
    IEnumerable<Guid>? WeaponSpecialAbilityIds
);

}
