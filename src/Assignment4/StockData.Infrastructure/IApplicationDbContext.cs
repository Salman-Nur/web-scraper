using Microsoft.EntityFrameworkCore;
using StockData.Domain.Entities;

namespace StockData.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Company { get; set; }
    }
}