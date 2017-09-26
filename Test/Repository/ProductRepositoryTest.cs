using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entities;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Repositories;
using Test.Helpers;

namespace Test.Repository
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private readonly IProductRepository _productRepository;
        private static Guid _productId;

        public ProductRepositoryTest()
        {
            TestDatabaseHelper databaseHelper = new TestDatabaseHelper();
            SqlConnection conn = databaseHelper.NewTestConnection();
            _productRepository = new ProductRepository(conn);
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

        [TestMethod]
        public void TestCreateProduct()
        {
            ProductEntity entity = CreateProduct();

            _productRepository.Save(entity);

            ProductEntity retrievedEntity = _productRepository.GetSingle(entity.Id);

            Assert.IsNotNull(retrievedEntity.Id);
            Assert.AreEqual(entity.Name, retrievedEntity.Name);
            Assert.AreEqual(entity.Description, retrievedEntity.Description);
            Assert.AreEqual(entity.Price, retrievedEntity.Price);
            Assert.AreEqual(entity.DeliveryPrice, retrievedEntity.DeliveryPrice);

            _productRepository.Delete(entity.Id);
        }

        [TestMethod]
        public void TestEditProduct()
        {
            ProductEntity entity = CreateProduct();

            entity.Name = "Samsung galaxy";
            entity.Description = "Product";
            entity.Price = 399.99m;
            entity.DeliveryPrice = 9.99m;

            _productRepository.Save(entity);

            ProductEntity editedEntity = _productRepository.GetSingle(entity.Id);

            Assert.AreEqual(editedEntity.Name, "Samsung galaxy");
            Assert.AreEqual(editedEntity.Description, "Product");
            Assert.AreEqual(editedEntity.Price, 399.99m);
            Assert.AreEqual(editedEntity.DeliveryPrice, 9.99m);
            Assert.AreEqual(editedEntity.Id, entity.Id);

            _productRepository.Delete(entity.Id);
        }

        [TestMethod]
        public void TestGetSingleProductByName()
        {
            CreateProduct();

            ProductEntity entity = _productRepository.GetSingle("Test Samsung galaxy");

            Assert.IsNotNull(entity);

            _productRepository.Delete(entity.Id);
        }

        [TestMethod]
        public void TestGetSingleProductById()
        {
            ProductEntity entity = CreateProduct();

            entity = _productRepository.GetSingle(entity.Id);

            Assert.IsNotNull(entity);

            _productRepository.Delete(entity.Id);
        }

        [TestMethod]
        public void TestRetrieveAllProducts()
        {
            ProductEntity entity = CreateProduct();

            List<ProductEntity> entitites = _productRepository.GetAll();

            Assert.IsNotNull(entitites);
            Assert.AreEqual(entitites.Count, 1);

            _productRepository.Delete(entity.Id);
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            ProductEntity entity = CreateProduct();

            Assert.IsNotNull(entity.Id);

            _productRepository.Delete(entity.Id);

            entity = _productRepository.GetSingle(entity.Id);

            Assert.IsNull(entity);
        }
    }
}
