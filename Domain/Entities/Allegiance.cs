using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;
public enum AllegianceType : byte {
    AgentsOfLight = 0,
    ServantsOfDarkness = 1
}

public class Allegiance : BaseEntity {
    public AllegianceType Type { get; set; }

    // Optional: convenience property
    [NotMapped]
    public string Name => Type switch {
        AllegianceType.AgentsOfLight => "Agents of Light",
        AllegianceType.ServantsOfDarkness => "Servants of Darkness",
        _ => "Unknown"
    };
}