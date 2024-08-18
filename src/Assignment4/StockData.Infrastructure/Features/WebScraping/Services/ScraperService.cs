
using HtmlAgilityPack;
using StockData.Application.Features.WebScraping.Services;

namespace StockData.Infrastructure.Features.WebScraping.Services
{
    public class ScraperService : IScraperService
    {
        static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        public async Task<bool> BringStatusAsync(string url)
        {
            bool status = true;
            HtmlDocument doc = GetDocument(url);
            var str = doc.DocumentNode.SelectSingleNode(xpath: 
                "//span[@class='time' and contains(.,'Market Status:')]/span[@class='green']/b")
                .InnerText;

            if (str == "Closed")
            {
                status = false;
            }

            return status;
        }

        public async Task<List<List<string>>> BringInformationAsync(string url)
        {
            HtmlDocument doc = GetDocument(url);
            List<List<string>> MainResult = new List<List<string>>();

            var tableNode = doc.DocumentNode.SelectSingleNode(xpath:
                "//table[@class='table table-bordered background-white shares-table fixedHeader']");

            var totalRow = tableNode.SelectNodes(xpath: ".//tr").Count;
            int row = 1;
            int[] col = new[] {2, 3, 4, 5, 6, 7, 8, 9, 10, 11};

            for (int i = row; i < totalRow - 1; i++)
            {
                List<string> result = new List<string>();
                for (int j = 0; j < col.Length; j++)
                {
                    HtmlNode node = tableNode.SelectSingleNode($".//tr[{i}]/td[{col[j]}]");
                    var str = node.InnerText.Trim();
                    if (str.Contains("--"))
                    {
                        str = string.Empty;
                    }
                    result.Add(str);
                }
                MainResult.Add(result);
            }

            return MainResult;
        }
    }
}
