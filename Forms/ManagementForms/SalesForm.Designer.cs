namespace Pokladna.Forms
{
    partial class SalesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesForm));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listViewWithScrollBar1 = new Pokladna.Forms.Controls.ListViewWithScrollBar();
            this.numberHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paymentHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "Comma sepated value|*.csv";
            // 
            // listViewWithScrollBar1
            // 
            this.listViewWithScrollBar1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberHeader,
            this.dateHeader,
            this.timeHeader,
            this.priceHeader,
            this.paymentHeader,
            this.userHeader});
            this.listViewWithScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWithScrollBar1.FullRowSelect = true;
            this.listViewWithScrollBar1.GridLines = true;
            this.listViewWithScrollBar1.HideSelection = false;
            this.listViewWithScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.listViewWithScrollBar1.Margin = new System.Windows.Forms.Padding(4);
            this.listViewWithScrollBar1.Name = "listViewWithScrollBar1";
            this.listViewWithScrollBar1.Size = new System.Drawing.Size(719, 552);
            this.listViewWithScrollBar1.TabIndex = 0;
            this.listViewWithScrollBar1.UseCompatibleStateImageBehavior = false;
            this.listViewWithScrollBar1.View = System.Windows.Forms.View.Details;
            // 
            // numberHeader
            // 
            this.numberHeader.Text = "Číslo";
            // 
            // dateHeader
            // 
            this.dateHeader.Text = "Datum";
            this.dateHeader.Width = 95;
            // 
            // timeHeader
            // 
            this.timeHeader.Text = "Čas";
            // 
            // priceHeader
            // 
            this.priceHeader.Text = "Cena";
            this.priceHeader.Width = 73;
            // 
            // paymentHeader
            // 
            this.paymentHeader.Text = "Platba";
            this.paymentHeader.Width = 148;
            // 
            // userHeader
            // 
            this.userHeader.Text = "Uživatel";
            this.userHeader.Width = 160;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 552);
            this.ControlBox = false;
            this.Controls.Add(this.listViewWithScrollBar1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "SalesForm";
            this.Text = "Výpis transakcí";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Pokladna.Forms.Controls.ListViewWithScrollBar listViewWithScrollBar1;
        private System.Windows.Forms.ColumnHeader dateHeader;
        private System.Windows.Forms.ColumnHeader priceHeader;
        private System.Windows.Forms.ColumnHeader paymentHeader;
        private System.Windows.Forms.ColumnHeader userHeader;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColumnHeader numberHeader;
        private System.Windows.Forms.ColumnHeader timeHeader;
    }
}