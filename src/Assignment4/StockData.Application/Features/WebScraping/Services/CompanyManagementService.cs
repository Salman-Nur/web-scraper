using StockData.Domain.Entities;
using StockData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StockData.Application.Features.WebScraping.Services
{
    public class CompanyManagementService : ICompanyManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IScraperService _scraperService;
        public CompanyManagementService(IApplicationUnitOfWork unitOfWork,
            IScraperService scraperService)
        {
            _unitOfWork = unitOfWork;
            _scraperService = scraperService;
        }

        public async Task InsertDatabaseAsync()
        {
            var result = await _scraperService.BringInformationAsync("https://www.dse.com.bd/latest_share_price_scroll_l.php");

            foreach (var item in result)
            {
                var tradeCode = item[0];
                var list = await _unitOfWork.CompanyRepository.GetAllAsync();
                Company existingCompany = list.FirstOrDefault(c => c.TradeCode == tradeCode);

                if (existingCompany is null)
                {
                    await InsertCompanyAsync(tradeCode, item);
                }
                else
                {
                    await InsertPriceAsync(existingCompany, item);
                }
            }
        }

        private async Task InsertCompanyAsync(string tradeCode, List<string> item)
        {
            var company = new Company()
            {
                TradeCode = tradeCode
            };

            await _unitOfWork.CompanyRepository.AddAsync(company);
            await _unitOfWork.SaveAsync();

            await InsertPriceAsync(company, item);
        }

        private async Task InsertPriceAsync(Company existingCompany, List<string> item)
        {
            int i = 1;

            var prices = new StockPrice()
            {
                LastTradingPrice = double.Parse(item[i]),
                High = double.Parse(item[++i]),
                Low = double.Parse(item[++i]),
                ClosePrice = double.Parse(item[++i]),
                YesterdayClosePrice = double.Parse(item[++i]),
                Change = item[++i],
                Trade = double.Parse(item[++i]),
                Value = double.Parse(item[++i]),
                Volume = double.Parse(item[++i]),
                CompanyId = existingCompany.Id
            };

            await _unitOfWork.StockPriceRepository.AddAsync(prices);
            await _unitOfWork.SaveAsync();
        }
    }    
}
