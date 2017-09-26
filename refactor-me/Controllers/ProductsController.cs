using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.ViewModels;
using AutoMapper;
using Service.Interfaces;
using Service.Models;
using Service.Services;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [Route]
        [HttpGet]
        public DataResponseModel GetAll()
        {
            DataResponseModel model = _productService.GetAllProducts();
            model.Data = Mapper.Map<List<ProductViewModel>>(model.Data);
            return model;
        }

        [Route("search")]
        [Route]
        [HttpGet]
        public DataResponseModel SearchByName(string name)
        {
            DataResponseModel model = _productService.SearchProducts(name);
            model.Data = Mapper.Map<List<ProductViewModel>>(model.Data);
            return model;
        }

        [Route("{id}")]
        [HttpGet]
        public DataResponseModel GetProduct(Guid id)
        {
            DataResponseModel model = _productService.GetSingleProduct(id);
            model.Data = Mapper.Map<ProductViewModel>(model.Data);
            return model;
        }

        [Route]
        [HttpPost]
        [Route("create")]
        public DataResponseModel Create(ProductViewModel product)
        {
            ProductServiceModel serviceModel = Mapper.Map<ProductServiceModel>(product);
            DataResponseModel model = _productService.SaveProduct(serviceModel);
            return model;
        }

        [Route("update")]
        [HttpPut]
        public DataResponseModel Update(ProductViewModel product)
        {
            ProductServiceModel serviceModel = Mapper.Map<ProductServiceModel>(product);
            DataResponseModel model = _productService.SaveProduct(serviceModel);
            return model;
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public DataResponseModel Delete(Guid id)
        {
            DataResponseModel model = _productService.DeleteProduct(id);
            return model;
        }

        [Route("{id}/options")]
        [HttpGet]
        public DataResponseModel GetOptions(Guid id)
        {
            DataResponseModel model = _productOptionService.GetProductOptionsForProduct(id);
            model.Data = Mapper.Map<List<ProductOptionViewModel>>(model.Data);
            return model;
        }

        [Route("options/{id}")]
        [HttpGet]
        public DataResponseModel GetOption(Guid id)
        {
            DataResponseModel model = _productOptionService.GetSingleProductOption(id);
            model.Data = Mapper.Map<ProductOptionViewModel>(model.Data);
            return model;
        }

        [Route("{id}/options/create")]
        [HttpPost]
        public DataResponseModel CreateOption(Guid id, ProductOptionViewModel productOption)
        {
            productOption.ProductId = id;
            ProductOptionServiceModel serviceModel = Mapper.Map<ProductOptionServiceModel>(productOption);
            DataResponseModel model = _productOptionService.SaveProductOption(serviceModel);
            return model;
        }

        [Route("options/update")]
        [HttpPut]
        public DataResponseModel UpdateOption(ProductOptionViewModel productOption)
        {
            ProductOptionServiceModel serviceModel = Mapper.Map<ProductOptionServiceModel>(productOption);
            DataResponseModel model = _productOptionService.SaveProductOption(serviceModel);
            return model;
        }

        [Route("options/delete/{id}")]
        [HttpDelete]
        public DataResponseModel DeleteOption(Guid id)
        {
            DataResponseModel model = _productOptionService.DeleteProductOption(id);
            return model;
        }
    }
}
