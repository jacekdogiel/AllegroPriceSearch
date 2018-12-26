using System;
using System.Data;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //nr czesci
        string part;
        string loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
        DataTable linksTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        //start wyszukiwania po wcisnieciu ENTER
        private void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Thread t = new Thread(() => AllegroAuctionBrowse());
                t.Start();
                part = partNumber.Text;
            }
        }

        void AllegroAuctionBrowse()
        {
            dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Clear()));
            linksTable.Reset();
            //pobranie danych z aukcji allegro
            string url = "https://allegro.pl/kategoria/motoryzacja?string=" + part + "&order=p";
            //pobranie aukcji z  danego konta allegro VAG24
            string url2 = "https://allegro.pl/uzytkownik/"+loginAllegro+ "?string=" + part + "&order=p";

            try
            {
                var allAuctions = Connection.GetAllegroNodes(url,"//h2[@class='ebc9be2  ']/a");
                var auctionPrices = Connection.GetAllegroNodes(url,"//span[@class='_611a83b']");
                var clientAuctions = Connection.GetAllegroNodes(url2, "//h2[@class='ebc9be2  ']/a");

                if (allAuctions != null)
                {
                    linksTable.Columns.Add("Link");
                    for (int i = 0; i <= allAuctions.Count - 1; i++)
                    {
                        if (allAuctions[i].InnerText.ToLower().Contains(part.ToLower()))
                        {
                            linksTable.Rows.Add(allAuctions[i].Attributes["href"].Value);
                            dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Add(allAuctions[i].InnerText, auctionPrices[i].InnerText, allAuctions[i].Attributes["href"].Value)));
                        }

                    }
                    if (clientAuctions != null)
                    {
                        for (int i = 0; i <= clientAuctions.Count - 1; i++)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (linksTable.Rows[row.Index]["Link"].ToString() == clientAuctions[i].Attributes["href"].Value.ToString())
                                    row.DefaultCellStyle.ForeColor = Color.Blue;
                            }
                        }
                    }
                }
                    label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + dataGridView1.RowCount.ToString()));

                    if (clientAuctions != null & dataGridView1.Rows.Count != 0)
                    {
                        //wyswietlenie ilosci aukcji danego konta z danym nr czesci
                        label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + dataGridView1.RowCount.ToString() + "       Nasze aukcje:" + clientAuctions.Count.ToString()));
                    }
                part = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ((DataGridView)sender)[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewLinkCell))
            {
                Process.Start(Convert.ToString(linksTable.Rows[e.RowIndex]["Link"]));
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        //wklejanie tekstu ze schowka
        private void partNumber_MouseDown(object sender, MouseEventArgs e)
        {
            partNumber.Text = Clipboard.GetText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
