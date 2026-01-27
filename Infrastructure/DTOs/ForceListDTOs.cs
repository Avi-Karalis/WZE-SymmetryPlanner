using Domain.Entities;


namespace Infrastructure.DTOs {
    public record CreateForceListDto(
        string Name,
        string Faction,
        int MaxDp,
        AllegianceType Allegiance,
        Guid UserId
    );

    public record AddUnitDto(Guid UnitId);
}
