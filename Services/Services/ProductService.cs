using System;
using System.Collections.Generic;
using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Models;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private static IProductRepository _productRepository;
        private static ILoggerService _loggerService;
        public ProductService(IProductRepository productRepository, ILoggerService loggerService)
        {
            _productRepository = productRepository;
            _loggerService = loggerService;
        }

        public DataResponseModel GetSingleProduct(Guid productOptionId)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                ProductEntity entity = _productRepository.GetSingle(productOptionId);
                model.Data = Mapper.Map<ProductServiceModel>(entity);
            }
            catch(Exception exception)
            {
                string errorMessage = $"There was an error trying to retrieve the product with id: {productOptionId}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel SaveProduct(ProductServiceModel product)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                ProductEntity entity = Mapper.Map<ProductEntity>(product);
                _productRepository.Save(entity);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to save the product with id: {product.Id}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel DeleteProduct(Guid productId)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                _productRepository.Delete(productId);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to delete the product with id: {productId}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel GetAllProducts()
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                List<ProductEntity> entity = _productRepository.GetAll();
                model.Data = Mapper.Map<List<ProductServiceModel>>(entity);
            }
            catch (Exception exception)
            {
                const string errorMessage = "There was an error trying to retrieve all products";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel SearchProducts(string productName)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                List<ProductEntity> entity = _productRepository.Search(productName);
                model.Data = Mapper.Map<List<ProductServiceModel>>(entity);
            }
            catch (Exception exception)
            {
                const string errorMessage = "There was an error trying to search all products";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }
    }
}
