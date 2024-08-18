using Microsoft.EntityFrameworkCore;
using StockData.Application;
using StockData.Domain.Repositories;
using StockData.Infrastructure;
using StockData.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository CompanyRepository { get; private set; }
        public IStockPriceRepository StockPriceRepository { get; private set; }

        public ApplicationUnitOfWork(ICompanyRepository companyRepository, 
            IStockPriceRepository stockPriceRepository, 
            IApplicationDbContext dbContext) : base((DbContext)dbContext)
        {
            CompanyRepository = companyRepository;
            StockPriceRepository = stockPriceRepository;
        }
    }
}
