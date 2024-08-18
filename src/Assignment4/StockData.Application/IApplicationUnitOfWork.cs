using StockData.Domain;
using StockData.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IStockPriceRepository StockPriceRepository { get; }
    }
}
