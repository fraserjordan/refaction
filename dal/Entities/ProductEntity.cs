using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
        public List<ProductOptionEntity> Options { get; set; }

    }
}
