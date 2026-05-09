using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces{
    public interface IAssetService : IGenericService<Asset, AssetReadDTO, AssetCreateDTO, AssetUpdateDTO>{
        Task<IEnumerable<AssetReadDTO>> GetAllByFactionAsync(string faction);
    }
}
