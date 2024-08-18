using Autofac;
using StockData.Application.Features.WebScraping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyManagementService>().As<ICompanyManagementService>()
                .InstancePerLifetimeScope();
        }
    }
}
