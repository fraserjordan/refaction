using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Repository.Entities;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Repositories;
using Test.Helpers;

namespace Test.Repository
{
    [TestClass]
    public class ProductOptionRepositoryTest
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly Guid _productGuid;
        private readonly Guid _productOptionGuid;

        public ProductOptionRepositoryTest()
        {
            TestDatabaseHelper databaseHelper = new TestDatabaseHelper();
            SqlConnection conn = databaseHelper.NewTestConnection();
            _productRepository = new ProductRepository(conn);
            _productOptionRepository = new ProductOptionRepository(conn);
        }

        public ProductEntity CreateProduct()
        {
            ProductEntity productEntity = new ProductEntity()
            {
                Name = "Test samsung galaxy",
                Description = "Test product",
                Price = 450.99m,
                DeliveryPrice = 14.99m
            };
            _productRepository.Save(productEntity);
            return productEntity;
        }

        public ProductOptionEntity CreateProductOption(Guid productId)
        {
            ProductOptionEntity productOptionEntity = new ProductOptionEntity()
            {
                Name = "Gold",
                Description = "Gold color",
                ProductId = productId
            };
            _productOptionRepository.Save(productOptionEntity);
            return productOptionEntity;
        }

        [TestMethod]
        public void TestCreateProductOption()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);
            Assert.IsNotNull(productEntity.Id);

            ProductOptionEntity retrievedProductOptionEntity = _productOptionRepository.GetSingle(productOptionEntity.Id);

            Assert.IsNotNull(retrievedProductOptionEntity.Id);
            Assert.AreEqual(productOptionEntity.Name, retrievedProductOptionEntity.Name);
            Assert.AreEqual(productOptionEntity.Description, retrievedProductOptionEntity.Description);
            Assert.AreEqual(productOptionEntity.ProductId, retrievedProductOptionEntity.ProductId);

            _productRepository.Delete(productEntity.Id);
            _productOptionRepository.Delete(productOptionEntity.Id);
        }

        [TestMethod]
        public void TestGetSingleProductOptionByName()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);

            ProductOptionEntity entity = _productOptionRepository.GetSingle("Gold");

            Assert.IsNotNull(entity);

            _productRepository.Delete(productEntity.Id);
            _productOptionRepository.Delete(productOptionEntity.Id);
        }

        [TestMethod]
        public void TestGetSingleProductOptionById()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);

            ProductOptionEntity entity = _productOptionRepository.GetSingle(productOptionEntity.Id);

            Assert.IsNotNull(entity);

            _productRepository.Delete(productEntity.Id);
            _productOptionRepository.Delete(productOptionEntity.Id);
        }

        [TestMethod]
        public void TestRetrieveProductOptionsByProduct()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);

            List<ProductOptionEntity> entitites = _productOptionRepository.GetForProduct(productEntity.Id);

            Assert.IsNotNull(entitites);
            Assert.AreEqual(entitites.Count, 1);

            _productRepository.Delete(productEntity.Id);
            _productOptionRepository.Delete(productOptionEntity.Id);
        }

        [TestMethod]
        public void TestEditProductOption()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);

            productOptionEntity.Name = "Test";
            productOptionEntity.Description = "Edit";

            _productOptionRepository.Save(productOptionEntity);

            ProductOptionEntity editedEntity = _productOptionRepository.GetSingle(productOptionEntity.Id);

            Assert.AreEqual(editedEntity.Name, "Test");
            Assert.AreEqual(editedEntity.Description, "Edit");

            _productOptionRepository.Delete(productOptionEntity.Id);
        }

        [TestMethod]
        public void TestDeleteProductOption()
        {
            ProductEntity productEntity = CreateProduct();
            ProductOptionEntity productOptionEntity = CreateProductOption(productEntity.Id);

            ProductOptionEntity entity = _productOptionRepository.GetSingle(productOptionEntity.Id);

            Assert.IsNotNull(entity);

            _productOptionRepository.Delete(productOptionEntity.Id);

            entity = _productOptionRepository.GetSingle(productOptionEntity.Id);

            Assert.IsNull(entity);

            // Delete product
            _productRepository.Delete(productEntity.Id);
            _productOptionRepository.Delete(productOptionEntity.Id);
        }
    }
}
