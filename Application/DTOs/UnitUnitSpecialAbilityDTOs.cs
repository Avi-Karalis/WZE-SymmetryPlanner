
namespace Application.DTOs;
public class UnitSpecialAbilityReadDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ValueX { get; set; }
    public string? ValueY { get; set; }
    public string Description { get; set; }
}

public class UnitSpecialAbilityCreateDto {
    public string Name { get; set; }
    public string? ValueX { get; set; }
    public string? ValueY { get; set; }
    public string Description { get; set; }
}

public class UnitSpecialAbilityUpdateDto : UnitSpecialAbilityCreateDto { }


