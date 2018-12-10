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
            this.strButton = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.RichTextBox();
            this.partNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // strButton
            // 
            this.strButton.Location = new System.Drawing.Point(362, 12);
            this.strButton.Name = "strButton";
            this.strButton.Size = new System.Drawing.Size(75, 23);
            this.strButton.TabIndex = 0;
            this.strButton.Text = "Szukaj";
            this.strButton.UseVisualStyleBackColor = true;
            this.strButton.Click += new System.EventHandler(this.strButton_Click);
            // 
            // results
            // 
            this.results.Location = new System.Drawing.Point(94, 54);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(694, 384);
            this.results.TabIndex = 1;
            this.results.Text = "";
            this.results.MouseDown += new System.Windows.Forms.MouseEventHandler(this.results_MouseDown);
            // 
            // partNumber
            // 
            this.partNumber.Location = new System.Drawing.Point(94, 14);
            this.partNumber.Name = "partNumber";
            this.partNumber.Size = new System.Drawing.Size(262, 20);
            this.partNumber.TabIndex = 3;
            this.partNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.partNumber_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.partNumber);
            this.Controls.Add(this.results);
            this.Controls.Add(this.strButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button strButton;
        private System.Windows.Forms.RichTextBox results;
        private System.Windows.Forms.TextBox partNumber;
    }
}

