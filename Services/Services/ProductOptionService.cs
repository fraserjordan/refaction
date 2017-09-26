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
    public class ProductOptionService : IProductOptionService
    {
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly ILoggerService _loggerService;

        public ProductOptionService(ILoggerService loggerService, IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
            _loggerService = loggerService;
        }
        public DataResponseModel GetSingleProductOption(Guid productOptionId)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                ProductOptionEntity entity = _productOptionRepository.GetSingle(productOptionId);
                model.Data = Mapper.Map<ProductOptionServiceModel>(entity);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to retrieve the product option with id: {productOptionId}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel GetProductOptionsForProduct(Guid productId)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                List<ProductOptionEntity> entity = _productOptionRepository.GetForProduct(productId);
                model.Data = Mapper.Map<List<ProductOptionServiceModel>>(entity);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to retrieve the product option with id: {productId}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel SaveProductOption(ProductOptionServiceModel productOption)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                ProductOptionEntity entity = Mapper.Map<ProductOptionEntity>(productOption);
                _productOptionRepository.Save(entity);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to save the product option with id: {productOption.Id}";

                //Setup data response model with error info
                model.Success = false;
                model.ErrorMessage = errorMessage;

                //Log error
                _loggerService.LogError(errorMessage, exception);
            }
            return model;
        }

        public DataResponseModel DeleteProductOption(Guid productOptionId)
        {
            DataResponseModel model = new DataResponseModel();
            try
            {
                _productOptionRepository.Delete(productOptionId);
            }
            catch (Exception exception)
            {
                string errorMessage = $"There was an error trying to delete the product option with id: {productOptionId}";

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
