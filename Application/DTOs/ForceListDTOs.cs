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
    int CurrentDp,
    int CurrentSp,
    Guid UserId,
    ICollection<UnitReadDto> Units
    );
    public record ForceListUpdateDto(
        Guid Id,
        string Name,
        string Faction,
        Allegiance Allegiance,
        ICollection<Unit> Units,
        int MaxDp,
        int MaxSp,
        int CurrentDp,
        int CurrentSp,
        Guid UserId,
        User User
    );
    public record ForceListAddUnitDto(Guid UnitId);
    public record ForceListRemoveUnitDto(Guid UnitId);
}
