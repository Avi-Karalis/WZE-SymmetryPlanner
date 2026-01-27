using Domain.Entities;


namespace Application.DTOs {
    public class ForceListCreateDto(
        string Name,
        string Faction,
        int MaxDp,
        AllegianceType Allegiance,
        Guid UserId
    );

    public class ForceListReadDto(
        string Name,
        string Faction,
        Allegiance Allegiance,
        ICollection<Unit> Units ,
        int MaxDp,
        int MaxSp ,
        int CurrentDp,
        int CurrentSp,
        Guid UserId,
        User User
    );
    public class ForceListUpdateDto(
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

    public class AddUnitDto(Guid UnitId);
    public class RemoveUnitDto(Guid UnitId);
}
