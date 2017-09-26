using System;
using System.Collections.Generic;
using Repository.Entities;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        List<ProductEntity> Search(string productName);
        List<ProductEntity> GetAll(); 
        ProductEntity GetSingle(Guid id);
        ProductEntity GetSingle(string name);
        void Save(ProductEntity productEntity);
        void Delete(Guid productId);
    }
}
