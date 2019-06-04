using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AllegroPriceSearch : Form
    {
        string part;
        public List<Record> records { get; set; }

        public AllegroPriceSearch()
        {
            InitializeComponent();
        }

        private async void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                part = partNumber.Text;
                await Task.Run(() => records = AllegroConnection.GetAuctionRecords(part));
                DisplayData();
            }
        }

        private void DisplayData()
        {
            auctionListGrid.DataSource = records;
            listedAuctionsCount.Text = "Ilość wystawionych części:" + auctionListGrid.RowCount.ToString();
            part = "";
        }

        private void MarkClientAuctions()
        {
            //foreach (var auction in clientAuctions)
            //{
            //    foreach (DataGridViewRow row in auctionListGrid.Rows)
            //    {
            //        if (linksTable.Rows[row.Index]["Link"].ToString() == auction.Attributes["href"].Value.ToString())
            //            row.DefaultCellStyle.ForeColor = Color.Blue;
            //    }
            //}
        }

        private void auctionListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                Process.Start(Convert.ToString(auctionListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
            }
        }

        private void auctionListGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                Clipboard.SetText(auctionListGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void partNumber_MouseDown(object sender, MouseEventArgs e)
        {
            //partNumber.Text = Clipboard.GetText();
        }
    }
}
