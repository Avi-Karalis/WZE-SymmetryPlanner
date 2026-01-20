using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public enum RoleType : byte {
        User = 0,
        Admin = 1,
        SuperAdmin = 2
    }
}
