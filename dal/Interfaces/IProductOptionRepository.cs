using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;

namespace Repository.Interfaces
{
    public interface IProductOptionRepository
    {
        ProductOptionEntity GetSingle(Guid id);
        ProductOptionEntity GetSingle(string name);
        List<ProductOptionEntity> GetForProduct(Guid productId);
        void Save(ProductOptionEntity productOptionEntity);
        void Delete(Guid id);
    }
}
