﻿using BaseModule.Interfaces;
using Bitir.Data.Model.Dtos;
using Core.Entities;
using ProductModule.Dtos;
using System.Threading.Tasks;

namespace ProductModule.Interfaces
{
    public interface IProductService<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapperListing<T>> GetSystemProducts();
        Task<bool> AddProductToVendor(AddProductToVendorRequest addProductToVendorRequest);
    }
}