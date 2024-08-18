using System;
using System.Collections.Generic;
using System.Text;

namespace StockData.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
