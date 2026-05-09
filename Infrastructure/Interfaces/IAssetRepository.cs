using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces {
    public interface IAssetRepository : IGenericRepository<Asset> {
        Task<IEnumerable<Asset>> GetAllByFactionAsync(string faction);
    }
}
