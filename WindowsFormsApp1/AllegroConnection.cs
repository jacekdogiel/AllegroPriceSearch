using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace WindowsFormsApp1
{
    public static class AllegroConnection
    {
        private static HtmlNodeCollection allAuctions { get; set; }
        private static HtmlNodeCollection auctionPrices { get; set; }
        private static HtmlNodeCollection clientAuctions { get; set; }

        public static List<Record> GetAuctionRecords(string part)
        {
            AllegroAuctionBrowse(part);
            var lstRecords = new List<Record>();
            for (int i = 0; i <= allAuctions.Count - 1; i++)
            {
                if (allAuctions[i].InnerText.ToLower().Contains(part.ToLower()))
                {
                    Record record = new Record();
                    record.Title = allAuctions[i].InnerText;
                    record.Price = auctionPrices[i].InnerText;
                    record.Link = allAuctions[i].Attributes["href"].Value;

                    foreach (var auction in clientAuctions)
                    {
                        if (allAuctions[i].Attributes["href"].Value == auction.Attributes["href"].Value)
                        {
                            record.IsClientAuction = true;
                        }
                    }
                    lstRecords.Add(record);
                }

            }
            return lstRecords;
        }

        private static HtmlNodeCollection GetAllegroNodes(string url, string xpath)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var source = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                var document = new HtmlDocument();
                document.LoadHtml(source);

                return document.DocumentNode.SelectNodes(xpath);
            }
            catch (Exception ex)
            {
                throw new AllegroException("Could not load data from Allegro", ex);
            }
        }
        private static void AllegroAuctionBrowse(string part)
        {
            var loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
            var titleClass = ConfigurationManager.AppSettings["titleClass"];
            var priceClass = ConfigurationManager.AppSettings["priceClass"];

            var url = ConfigurationManager.AppSettings["url"] + part + "&order=p";
            var urlWithLogin = ConfigurationManager.AppSettings["urlWithLogin"] + loginAllegro + "?string=" + part + "&order=p";

            try
            {
                allAuctions = GetAllegroNodes(url, $"//h2[@class='{titleClass}']/a");
                auctionPrices = GetAllegroNodes(url, $"//span[@class='{priceClass}']");
                clientAuctions = GetAllegroNodes(urlWithLogin, $"//h2[@class='{titleClass}']/a");
            }
            catch (Exception ex)
            {
                throw new AllegroException("Could not load nodes from Allegro", ex);
            }
        }
    }
}
