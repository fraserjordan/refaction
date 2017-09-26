using System;
using Repository.Entities;
using Service.Models;

namespace Service.Interfaces
{
    public interface IProductOptionService
    {
        DataResponseModel GetSingleProductOption(Guid productOptionId);
        DataResponseModel GetProductOptionsForProduct(Guid productId);

        DataResponseModel SaveProductOption(ProductOptionServiceModel productOption);

        DataResponseModel DeleteProductOption(Guid productOptionId);
    }
}
