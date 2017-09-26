using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;


namespace refactor_me
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProductRepository>()
                   .As<IProductRepository>()
                   .InstancePerDependency();

            builder.RegisterType<ProductOptionRepository>()
                   .As<IProductOptionRepository>()
                   .InstancePerDependency();

            builder.RegisterType<ProductService>()
                   .As<IProductService>()
                   .InstancePerDependency();

            builder.RegisterType<ProductOptionService>()
                   .As<IProductOptionService>()
                   .InstancePerDependency();

            builder.RegisterType<LoggerService>()
                   .As<ILoggerService>()
                   .InstancePerDependency();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}  
