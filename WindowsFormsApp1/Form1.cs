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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //nr czesci
        string part;
        string loginAllegro = ConfigurationManager.AppSettings["loginAllegro"];
        public Form1()
        {
            InitializeComponent();
        }

        private void strButton_Click(object sender, EventArgs e)
        {
            //czyszczenie textboxa
            results.Clear();
            //nowy watek zeby nie blokowac interfejsu
            Thread t = new Thread(()=>Start());
            t.Start();
            //pobranie nr czesci
            part = partNumber.Text;
            
        }
        void Start()
        {
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
            HtmlNodeCollection countParts = doc.DocumentNode.SelectNodes("//span[@class='_611a83b']");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//span[@class='_611a83b'] | //h2[@class='ebc9be2  ']");
            HtmlNodeCollection countParts2 = doc2.DocumentNode.SelectNodes("//h2[@class='ebc9be2  ']");

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        //wyswietlenie tytułów aukcji i cen
                        results.Text = results.Text + node.InnerText + Environment.NewLine;
                    }
                   ));
                }
            }
            else { results.Invoke(new Action(() => results.Text = "Nie ma takiej części")); }

            if (nodes != null)
            {
                label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + countParts.Count.ToString()));
            }

            if (countParts2 != null & nodes != null)
            {
                //wyswietlenie ilosci aukcji danego konta z danym nr czesci
                label2.Invoke(new Action(() => label2.Text = "Ilość wystawionych części:" + countParts.Count.ToString()+"       Nasze aukcje:"+ countParts2.Count.ToString()));
            }

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

        private void results_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Select All");
                menuItem.Click += new EventHandler(SelectAll);
                contextMenu.MenuItems.Add(menuItem);
                results.ContextMenu = contextMenu;
            }
        }

        void CutAction(object sender, EventArgs e)
        {
            results.Cut();
        }

        void CopyAction(object sender, EventArgs e)
        {
            Clipboard.SetText(results.SelectedText);
        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                results.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }

        void SelectAll(object sender, EventArgs e)
        {
            results.SelectAll();
            results.Focus();
        }

        //funkcja do szukania po nacisnieciu ENTER
        private void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                results.Clear();
                Thread t = new Thread(() => Start());
                t.Start();
                part = partNumber.Text;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
