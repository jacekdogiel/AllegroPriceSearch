using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllegroPriceSearch
{
    public static class AllegroConnection
    {
        private static HtmlNodeCollection auctionTitles { get; set; }
        private static HtmlNodeCollection auctionPrices { get; set; }
        private static HtmlNodeCollection clientAuctions { get; set; }
        private static HtmlNodeCollection allAuctions { get; set; }


        private static string loginAllegro { get; set; }
        private static string titleClass { get; set; }
        private static string priceClass { get; set; }
        private static string allAuctionClass { get; set; }

        private static string url { get; set; }
        private static string urlWithLogin { get; set; }


        public static List<Record> GetAuctionRecords(string part)
        {
            AuctionBrowse(part);
            return CreateRecords(part);
        }

        private static List<Record> CreateRecords(string part)
        {
            var lstRecords = new List<Record>();
            if (auctionTitles != null)
            {
                for (int i = 0; i <= auctionTitles.Count - 1; i++)
                {
                    if (auctionTitles[i].InnerText.ToLower().Contains(part.ToLower()))
                    {
                        var record = new Record();
                        record.Title = auctionTitles[i].InnerText;
                        record.Price = auctionPrices[i].InnerText;
                        record.Link = auctionTitles[i].Attributes["href"].Value;
                        CheckClientAuctions(i, record);
                        lstRecords.Add(record);
                    }

                }
            }

            return lstRecords;
        }

        private static void CheckClientAuctions(int index, Record record)
        {
            if (clientAuctions != null)
                foreach (var auction in clientAuctions)
                {
                    if (auctionTitles[index].Attributes["href"].Value == auction.Attributes["href"].Value)
                    {
                        record.IsClientAuction = true;
                    }
                }
        }

        private static HtmlNodeCollection GetDocumentNodes(string url, string xpath)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var source = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                    var document = new HtmlDocument();
                    document.LoadHtml(source);

                    return document.DocumentNode.SelectNodes(xpath);
                }
                catch (Exception ex)
                {
                    throw new AllegroException("Could not load data from Allegro", ex);
                }
            }
        }

        private static void AuctionBrowse(string part)
        {
            GetConfiguration(part);

            try
            {
                ClearAuctions();
                SelectAuctionNodes();
            }
            catch (Exception ex)
            {
                throw new AllegroException("Could not load nodes from Allegro", ex);
            }
        }
        private static void GetConfiguration(string part)
        {
            loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
            titleClass = ConfigurationManager.AppSettings["titleClass"];
            priceClass = ConfigurationManager.AppSettings["priceClass"];
            allAuctionClass = ConfigurationManager.AppSettings["allAuctionClass"];

            url = ConfigurationManager.AppSettings["url"] + part + "&order=p";
            urlWithLogin = ConfigurationManager.AppSettings["urlWithLogin"] + loginAllegro + "?string=" + part + "&order=p";
        }

        private static void SelectAuctionNodes()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var task1 = Task.Run(() => allAuctions = GetDocumentNodes(url, $"//div[@class='{allAuctionClass}']"));
            var task2 = Task.Run(() => clientAuctions = GetDocumentNodes(urlWithLogin, $"//h2[@class='{titleClass}']/a"));

            Task.WaitAll(task1, task2);

            if (allAuctions != null)
                foreach (var node in allAuctions)
                {
                    auctionTitles = node.SelectNodes($"//h2[@class='{titleClass}']/a");
                    auctionPrices = node.SelectNodes($"//span[@class='{priceClass}']");
                }
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;
        }

        private static void ClearAuctions()
        {
            auctionTitles = null;
            auctionPrices = null;
        }
    }
}
