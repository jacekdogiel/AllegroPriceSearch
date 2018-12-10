using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.Net;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string part;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void strButton_Click(object sender, EventArgs e)
        {
            //nowy watek zeby nie blokowac interfejsu
            Thread t = new Thread(()=>Start());
            t.Start();
            part = partNumber.Text;
            results.Text = "";
        }
        void Start()
        {
            string url = "https://allegro.pl/listing?string=" + part + "&order=p&bmatch=cl-n-eng-global-uni-1-3-1130";
            string source = getSource(url);
            if (source == "") return;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//span[@class='ecb7eff'] | //h2[@class='_4462670  ']");

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        results.Text = results.Text + node.InnerText + Environment.NewLine;
                    }
                   ));
                }
            }
        }

        string getSource(string url)
        {
            try
            {
                WebClient mywebClient = new WebClient();
                mywebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");
                byte[] myDataBuffer = mywebClient.DownloadData(url);
                string source = Encoding.ASCII.GetString(myDataBuffer);

                return source;
            }
            catch(Exception ex)
            {
                return "";
            }
            return "";
        }
    }
}
