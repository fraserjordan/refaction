using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using AutoMapper.Configuration;
using refactor_me.ViewModels;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Models;
using Service.Services;

namespace refactor_me
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.Run();

            var config = new MapperConfigurationExpression();

            //Create all maps for AutoMapper
            config.CreateMap<ProductEntity, ProductServiceModel>();
            config.CreateMap<ProductServiceModel, ProductEntity>();
            config.CreateMap<ProductOptionEntity, ProductOptionServiceModel>();
            config.CreateMap<ProductOptionServiceModel, ProductOptionEntity>();
            config.CreateMap<ProductViewModel, ProductServiceModel>();
            config.CreateMap<ProductServiceModel, ProductViewModel>();
            config.CreateMap<ProductOptionViewModel, ProductOptionServiceModel>();
            config.CreateMap<ProductOptionServiceModel, ProductOptionViewModel>();

            // Initialize AutoMapper with the above mappings
            Mapper.Initialize(config);
        }
    }
}
