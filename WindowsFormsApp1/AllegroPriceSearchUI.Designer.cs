namespace AllegroPriceSearch
{
    partial class AllegroPriceSearchUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.partNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listedAuctionsCount = new System.Windows.Forms.Label();
            this.auctionListGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.auctionListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // partNumber
            // 
            this.partNumber.Location = new System.Drawing.Point(12, 25);
            this.partNumber.Name = "partNumber";
            this.partNumber.Size = new System.Drawing.Size(262, 20);
            this.partNumber.TabIndex = 3;
            this.partNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.partNumber_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Wpisz numer części i wciśnij Enter:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listedAuctionsCount
            // 
            this.listedAuctionsCount.AutoSize = true;
            this.listedAuctionsCount.Location = new System.Drawing.Point(10, 52);
            this.listedAuctionsCount.Name = "listedAuctionsCount";
            this.listedAuctionsCount.Size = new System.Drawing.Size(134, 13);
            this.listedAuctionsCount.TabIndex = 5;
            this.listedAuctionsCount.Text = "Ilość wystawionych części:";
            // 
            // auctionListGrid
            // 
            this.auctionListGrid.AllowUserToAddRows = false;
            this.auctionListGrid.AllowUserToDeleteRows = false;
            this.auctionListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.auctionListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.auctionListGrid.Location = new System.Drawing.Point(12, 72);
            this.auctionListGrid.MultiSelect = false;
            this.auctionListGrid.Name = "auctionListGrid";
            this.auctionListGrid.ReadOnly = true;
            this.auctionListGrid.Size = new System.Drawing.Size(563, 304);
            this.auctionListGrid.TabIndex = 6;
            this.auctionListGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.auctionListGrid_CellContentClick);
            this.auctionListGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.auctionListGrid_CellContentDoubleClick);
            // 
            // AllegroPriceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 388);
            this.Controls.Add(this.auctionListGrid);
            this.Controls.Add(this.listedAuctionsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.partNumber);
            this.Name = "AllegroPriceSearch";
            this.Text = "Wyszukiwarka opisów i cen Allegro";
            ((System.ComponentModel.ISupportInitialize)(this.auctionListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox partNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label listedAuctionsCount;
        private System.Windows.Forms.DataGridView auctionListGrid;
    }
}

