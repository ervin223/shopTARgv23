using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using System;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IRealEstate
    {
        Task<RealEstate> Details(Guid id);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> Delete(Guid id);
        Task<RealEstate> Create(RealEstateDto dto);
    }
}
