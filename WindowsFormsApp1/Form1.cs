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
using System.Configuration ;
using System.Net.Http;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //nr czesci
        string part;
        string loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
        DataTable table = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        //funkcja do szukania po nacisnieciu ENTER
        private void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Thread t = new Thread(() => Start());
                t.Start();
                part = partNumber.Text;
            }
        }

        //wklejanie tekstu ze schowka
        private void partNumber_MouseDown(object sender, MouseEventArgs e)
        {
            partNumber.Text = Clipboard.GetText();
        }

        void Start()
        {
            dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Clear()));
            table.Reset();
            //pobranie danych z aukcji allegro
            string url = "https://allegro.pl/kategoria/motoryzacja?string=" + part + "&order=p&bmatch=baseline-cl-n-aut-1-3-1123";
            //pobranie aukcji z  danego konta allegro VAG24
            string url2 = "https://allegro.pl/uzytkownik/"+loginAllegro+ "?string=" + part + "&order=m&bmatch=cl-n-eng-global-uni-1-3-1130";
            string source = getSource(url);
            string source2 = getSource(url2);

            //pobranie kodu html
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);

            HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            doc2.LoadHtml(source2);

            //pobranie tytułów aukcji i cen

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//h2[@class='ebc9be2  ']/a");
            HtmlNodeCollection nodes2 = doc.DocumentNode.SelectNodes("//span[@class='_611a83b']");
            HtmlNodeCollection countParts2 = doc2.DocumentNode.SelectNodes("//h2[@class='ebc9be2  ']");

            if (nodes != null)
            {
                
                table.Columns.Add("Opis");
                table.Columns.Add("Cena");
                table.Columns.Add("Link");
                for (int i = 0; i < nodes.Count - 1; i++)
                {
                    if (nodes[i].InnerText.ToLower().Contains(part.ToLower()))
                    {
                        table.Rows.Add(nodes[i].InnerText, nodes2[i].InnerText, nodes[i].Attributes["href"].Value);
                        dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Add(nodes[i].InnerText, nodes2[i].InnerText, nodes[i].Attributes["href"].Value)));
                    }

                }
                
                dataGridView1.Invoke(new Action(() => dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)));
            }
            
            if (nodes != null)
            {
                label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + dataGridView1.RowCount.ToString()));
                nodes.Clear();
            }

            if (countParts2 != null & nodes != null)
            {
                //wyswietlenie ilosci aukcji danego konta z danym nr czesci
                label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + dataGridView1.RowCount.ToString() + "       Nasze aukcje:"+ countParts2.Count.ToString()));
                countParts2.Clear();
            }
            part = "";
        }

        //funkcja do pobierania dancyh
        public static string getSource(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Błąd połączenia");
                return "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ((DataGridView)sender)[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewLinkCell))
            {
                string url = Convert.ToString(table.Rows[e.RowIndex]["Link"]);
                Process.Start(url);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
