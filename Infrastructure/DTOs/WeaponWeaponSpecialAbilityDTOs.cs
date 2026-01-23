
namespace Infrastructure.DTOs {
    public record WeaponSpecialAbilityReadDto(
        Guid Id,
        string Name,
        string? ValueX,
        string? ValueY,
        string Description
    );
    public record WeaponSpecialAbilityCreateDto(
    string Name,
    string? ValueX,
    string? ValueY,
    string Description
);
    public record WeaponSpecialAbilityUpdateDto(
    Guid Id,
    string Name,
    string? ValueX,
    string? ValueY,
    string Description
);

}
