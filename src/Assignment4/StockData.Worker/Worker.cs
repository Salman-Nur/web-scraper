using Microsoft.Extensions.Configuration;
using StockData.Application.Features.WebScraping.Services;
using System.Data;
using System.Net;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICompanyManagementService _companyManagementService;
        private readonly IConfiguration _configuration;
        private readonly IScraperService _scraperService;

        public Worker(ILogger<Worker> logger, ICompanyManagementService 
            companyManagementService, IConfiguration configuration, IScraperService scraperService)
        {
            _logger = logger;
            _companyManagementService = companyManagementService;
            _configuration = configuration;
            _scraperService = scraperService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        var status = await _scraperService.BringStatusAsync("https://www.dse.com.bd/latest_share_price_scroll_l.php");
                        if (status)
                        {
                            await _companyManagementService.InsertDatabaseAsync();
                            _logger.LogInformation($"All stock data parsing is complete at: {DateTime.Now}");
                        }
                        else
                        {
                            _logger.LogInformation("Market closed");
                        }
                    }
                }
                catch (WebException)
                {
                    _logger.LogError("internet connection is unstable");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in the worker service.");
                }
                finally
                {
                    var minute = _configuration.GetValue<int>("Delay:Minute");
                    await Task.Delay(TimeSpan.FromMinutes(minute), stoppingToken);
                }
            }
        }
    }
}
