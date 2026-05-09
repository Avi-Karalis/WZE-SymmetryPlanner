using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs {
    public class AssetReadDTO {
        public Guid Id { get; set; }
        public string Faction { get; set; }
        public string Name { get; set; }
        public sbyte DpCost { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    public class AssetCreateDTO {
        public string Faction { get; set; }
        public string Name { get; set; }
        public sbyte DpCost { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class AssetUpdateDTO : AssetCreateDTO {

    }
}
