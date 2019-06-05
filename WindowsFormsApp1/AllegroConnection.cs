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
        private static HtmlNodeCollection auctionTitles { get; set; }
        private static HtmlNodeCollection auctionPrices { get; set; }
        private static HtmlNodeCollection clientAuctions { get; set; }

        private static string loginAllegro;
        private static string titleClass;
        private static string priceClass;
        private static string allAuctionClass;

        private static string url;
        private static string urlWithLogin;


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
            var allAuctions = GetDocumentNodes(url, $"//div[@class='{allAuctionClass}']");
            if (allAuctions != null)
                foreach (var node in allAuctions)
                {
                    auctionTitles = node.SelectNodes($"//h2[@class='{titleClass}']/a");
                    auctionPrices = node.SelectNodes($"//span[@class='{priceClass}']");
                }
            clientAuctions = GetDocumentNodes(urlWithLogin, $"//h2[@class='{titleClass}']/a");
        }

        private static void ClearAuctions()
        {
            auctionTitles = null;
            auctionPrices = null;
        }
    }
}
