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
        string Allegiance,         
        int MaxDp,
        int MaxSp,
        int CurrentDp,
        int CurrentSp,
        Guid UserId
    ) {
        public ICollection<UnitReadDto> Units { get; set; } = new List<UnitReadDto>();
        public ICollection<AssetReadDTO> Assets { get; set; } = new List<AssetReadDTO>();
    };

    public record ForceListDeletedReadDto(
        Guid Id,
        string Name,
        string Faction,
        string Allegiance,
        int MaxDp,
        int CurrentDp,
        Guid UserId,
        string UserName,
        string UserEmail,
        DateTime DeletedAt
    ) {
        public ICollection<UnitReadDto> Units { get; set; } = new List<UnitReadDto>();
        public ICollection<AssetReadDTO> Assets { get; set; } = new List<AssetReadDTO>();
    };

    public record ForceListUpdateDto(
        string Name,
        string Faction,
        AllegianceType Allegiance,
        int MaxDp,
        int MaxSp,
        int CurrentDp,
        int CurrentSp
    );
    public record ForceListAddUnitDto(Guid UnitId);
    public record ForceListRemoveUnitDto(Guid UnitId);
}
