using HtmlAgilityPack;
using System;
using System.Net;
using System.Net.Http;

namespace WindowsFormsApp1
{
    class Connection
    {
        public static HtmlNodeCollection GetAllegroNodes(string url, string xpath)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var source = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                var document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(source);
                HtmlNodeCollection node = document.DocumentNode.SelectNodes(xpath);
                return node;
            }
            catch (Exception ex)
            {
                throw new AllegroException("Could not load data from Allegro",ex);
            }
        }


    }
}
