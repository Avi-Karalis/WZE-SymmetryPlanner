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
        Guid UserId
    ) {
        public ICollection<UnitReadDto> Units { get; set; } = new List<UnitReadDto>();
    };
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
