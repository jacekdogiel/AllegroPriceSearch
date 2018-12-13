namespace WindowsFormsApp1
{
    partial class Form1
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
            this.strButton = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.RichTextBox();
            this.partNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // strButton
            // 
            this.strButton.Location = new System.Drawing.Point(280, 23);
            this.strButton.Name = "strButton";
            this.strButton.Size = new System.Drawing.Size(75, 23);
            this.strButton.TabIndex = 0;
            this.strButton.Text = "Szukaj";
            this.strButton.UseVisualStyleBackColor = true;
            this.strButton.Click += new System.EventHandler(this.strButton_Click);
            // 
            // results
            // 
            this.results.Location = new System.Drawing.Point(12, 72);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(343, 304);
            this.results.TabIndex = 1;
            this.results.Text = "";
            this.results.MouseDown += new System.Windows.Forms.MouseEventHandler(this.results_MouseDown);
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
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Wpisz numer części:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ilość wystawionych części:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 388);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.partNumber);
            this.Controls.Add(this.results);
            this.Controls.Add(this.strButton);
            this.Name = "Form1";
            this.Text = "Wyszukiwarka opisów i cen Allegro";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button strButton;
        private System.Windows.Forms.RichTextBox results;
        private System.Windows.Forms.TextBox partNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label2;
    }
}

