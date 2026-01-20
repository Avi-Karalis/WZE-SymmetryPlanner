using System;
using System.Collections.Generic;

namespace Domain.Entities {
    public class User : BaseEntity{

        // OAuth provider info
        public string Provider { get; set; } = "Google"; // e.g., "Google", "Microsoft"
        public string ProviderUserId { get; set; } // Google "sub" claim
        public string Email { get; set; }
        public string Name { get; set; }
        public string? PictureUrl { get; set; }

        public RoleType Role { get; set; } = RoleType.User;

        public ICollection<ForceList> ForceLists{ get; set; } = new List<ForceList>();


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

    }
}
