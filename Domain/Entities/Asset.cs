using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities {
    [Table("Assets")]
    public class Asset : BaseEntity {
        public string? Faction { get; set; }
        public string Name { get; set; }
        public sbyte DpCost { get; set; }
        public string Description { get; set; } = string.Empty;// General = humanity, Necrotech = Dark Legion
        public sbyte Status { get; set; } = 0; // 0 public, 1 testing


        public Asset(string faction, string name, sbyte dpCost, string description) {
            Faction = faction;
            Name = name;
            DpCost = dpCost;
            Description = description;
        }

        public Asset(string faction, string name, sbyte dpCost, string description, sbyte status) {
            Faction = faction;
            Name = name;
            DpCost = dpCost;
            Description = description;
            Status = status;
        }
    }


}
