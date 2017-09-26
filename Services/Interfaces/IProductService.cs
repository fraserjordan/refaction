using System;
using System.Collections.Generic;
using Repository.Entities;
using Service.Models;

namespace Service.Interfaces
{
    public interface IProductService
    {
        DataResponseModel SearchProducts(string productName);
        DataResponseModel GetAllProducts();
        DataResponseModel GetSingleProduct(Guid productId);

        DataResponseModel SaveProduct(ProductServiceModel product);

        DataResponseModel DeleteProduct(Guid productId);
    }
}
