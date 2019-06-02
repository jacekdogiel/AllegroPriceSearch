using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string part;
        HtmlAgilityPack.HtmlNodeCollection allAuctions;
        HtmlAgilityPack.HtmlNodeCollection auctionPrices;
        HtmlAgilityPack.HtmlNodeCollection clientAuctions;
        DataTable linksTable;

        public Form1()
        {
            InitializeComponent();
            linksTable = new DataTable();
        }

        private async void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                part = partNumber.Text;
                await Task.Run(() => AllegroAuctionBrowse());
                DisplayData();
            }
        }

        private void AllegroAuctionBrowse()
        {
            var loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
            var titleClass = ConfigurationManager.AppSettings["titleClass"];
            var priceClass = ConfigurationManager.AppSettings["priceClass"];

            var url = "https://allegro.pl/kategoria/motoryzacja?string=" + part + "&order=p";
            var urlWithAccount = "https://allegro.pl/uzytkownik/" + loginAllegro + "?string=" + part + "&order=p";

            try
            {
                allAuctions = Connection.GetAllegroNodes(url, $"//h2[@class='{titleClass}']/a");
                auctionPrices = Connection.GetAllegroNodes(url, $"//span[@class='{priceClass}']");
                clientAuctions = Connection.GetAllegroNodes(urlWithAccount, $"//h2[@class='{titleClass}']/a");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayData()
        {
            auctionListGrid.Rows.Clear();
            linksTable.Reset();
            if (allAuctions != null)
            {
                linksTable.Columns.Add("Link");
                for (int i = 0; i <= allAuctions.Count - 1; i++)
                {
                    if (allAuctions[i].InnerText.ToLower().Contains(part.ToLower()))
                    {
                        linksTable.Rows.Add(allAuctions[i].Attributes["href"].Value);
                        auctionListGrid.Rows.Add(allAuctions[i].InnerText,
                                               auctionPrices[i].InnerText,
                                               allAuctions[i].Attributes["href"].Value);
                    }

                }
                listedAuctionsCount.Text = "Ilość wystawionych części:" + auctionListGrid.RowCount.ToString();
                if (clientAuctions != null)
                {
                    MarkClientAuctions();
                }
            }
            part = "";
        }

        private void MarkClientAuctions()
        {
            for (int i = 0; i <= clientAuctions.Count - 1; i++)
            {
                foreach (DataGridViewRow row in auctionListGrid.Rows)
                {
                    if (linksTable.Rows[row.Index]["Link"].ToString() == clientAuctions[i].Attributes["href"].Value.ToString())
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            listedAuctionsCount.Text = "Ilość wystawionych części:" + auctionListGrid.RowCount.ToString()
            + "      Nasze aukcje:" + clientAuctions.Count.ToString();
        }

        private void auctionListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ((DataGridView)sender)[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewLinkCell))
            {
                Process.Start(Convert.ToString(linksTable.Rows[e.RowIndex]["Link"]));
            }
        }

        private void auctionListGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(auctionListGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void partNumber_MouseDown(object sender, MouseEventArgs e)
        {
            //partNumber.Text = Clipboard.GetText();
        }
    }
}
