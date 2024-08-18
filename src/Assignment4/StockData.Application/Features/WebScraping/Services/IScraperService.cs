namespace StockData.Application.Features.WebScraping.Services
{
    public interface IScraperService
    {
        Task<bool> BringStatusAsync(string url);
        Task<List<List<string>>> BringInformationAsync(string url);
    }
}
