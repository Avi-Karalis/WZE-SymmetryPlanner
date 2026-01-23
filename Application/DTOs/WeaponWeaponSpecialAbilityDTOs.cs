
namespace Application.DTOs;
public class WeaponSpecialAbilityReadDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ValueX { get; set; }
    public string? ValueY { get; set; }
    public string Description { get; set; }
}

public class WeaponSpecialAbilityCreateDto {
    public string Name { get; set; }
    public string? ValueX { get; set; }
    public string? ValueY { get; set; }
    public string Description { get; set; }
}

public class WeaponSpecialAbilityUpdateDto : WeaponSpecialAbilityCreateDto { }
