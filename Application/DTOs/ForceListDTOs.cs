using Domain.Entities;


namespace Application.DTOs {
    public record ForceListCreateDto(
        string Name,
        string Faction,
        int MaxDp,
        AllegianceType Allegiance,
        Guid UserId
    );

    public record ForceListReadDto(
        Guid Id,
        string Name,
        string Faction,
        string Allegiance,          // string for UI
        int MaxDp,
        int MaxSp,
        ICollection<UnitReadDto> Units,
        Guid UserId
    );
    public record ForceListUpdateDto(
        string Name,
        string Faction,
        AllegianceType Allegiance,
        int MaxDp,
        int MaxSp
    );
    public record ForceListAddUnitDto(Guid UnitId);
    public record ForceListRemoveUnitDto(Guid UnitId);
}
