using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllegroPriceSearch
{
    public partial class AllegroPriceSearchUI : Form
    {
        private string part { get; set; }
        private Stopwatch stopwatch { get; set; }
        private List<Record> records { get; set; }

        public AllegroPriceSearchUI()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
        }

        private async void partNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                stopwatch.Start();
                part = partNumber.Text;
                await Task.Run(() => records = AllegroConnection.GetAuctionRecords(part));
                stopwatch.Stop();
                var elapsed = stopwatch.Elapsed;
                DisplayData();
            }
        }

        private void DisplayData()
        {

            auctionListGrid.DataSource = records;
            DisplayNumberOfAuctions();
            CreateLinkCells();
            MarkClientAuctions();
            auctionListGrid.Columns["Link"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            auctionListGrid.Columns["IsClientAuction"].Visible = false;
            part = "";

        }

        private void DisplayNumberOfAuctions()
        {
            listedAuctionsCount.Text = "Ilość wystawionych części:" + records.Count().ToString()
                + "         Nasze aukcje:" + records.Where(r => r.IsClientAuction == true).Count().ToString();
        }

        private void CreateLinkCells()
        {
            foreach (DataGridViewRow row in auctionListGrid.Rows)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                linkCell.Value = row.Cells["Link"].Value;
                row.Cells["Link"] = linkCell;
                row.Cells["Link"].Tag = row.Cells[2].Value;
                row.Cells["Link"].Value = "Link";
            }
        }

        private void MarkClientAuctions()
        {
            foreach (DataGridViewRow row in auctionListGrid.Rows)
            {
                if ((bool)row.Cells["IsClientAuction"].Value == true)
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void auctionListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == auctionListGrid.Columns["Link"].Index)
            {
                Process.Start(Convert.ToString(auctionListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString()));
            }
        }

        private void auctionListGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                Clipboard.SetText(auctionListGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
        }
    }
}
