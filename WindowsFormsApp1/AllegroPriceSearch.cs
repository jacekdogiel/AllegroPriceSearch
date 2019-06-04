using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            DisplayNumberOfAuctions();
            CreateLinkCells();
            MarkClientAuctions();
            auctionListGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            auctionListGrid.Columns[3].Visible = false;
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
                linkCell.Value = row.Cells[2].Value;
                row.Cells[2] = linkCell;
                row.Cells[2].Tag = row.Cells[2].Value;
                row.Cells[2].Value = "Link";
            }
        }

        private void MarkClientAuctions()
        {
            foreach (DataGridViewRow row in auctionListGrid.Rows)
            {
                if ((bool)row.Cells[3].Value == true)
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void auctionListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
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
