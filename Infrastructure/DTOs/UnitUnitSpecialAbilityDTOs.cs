
namespace Infrastructure.DTOs {
    public record UnitSpecialAbilityReadDto(
        Guid Id,
        string Name,
        string? ValueX,
        string? ValueY,
        string Description
    );
    public record UnitSpecialAbilityCreateDto(
        string Name,
        string? ValueX,
        string? ValueY,
        string Description
    );
    public record UnitSpecialAbilityUpdateDto(
        Guid Id,
        string Name,
        string? ValueX,
        string? ValueY,
        string Description
    );

}
