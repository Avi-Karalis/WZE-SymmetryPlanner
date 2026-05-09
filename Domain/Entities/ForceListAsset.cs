using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities {
    public class ForceListAsset {
        public Guid Id { get; set; }
        public Guid ForceListId { get; set; }
        public ForceList ForceList { get; set; }

        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
