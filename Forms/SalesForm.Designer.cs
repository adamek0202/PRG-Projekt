namespace Projekt.Forms
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "24.12.2024",
            "250Kč",
            "Stravenková karta",
            "Květa prosová"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesForm));
            this.listViewWithScrollBar1 = new Pokladna.ListViewWithScrollBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.uložitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tiskToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paymentHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewWithScrollBar1
            // 
            this.listViewWithScrollBar1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateHeader,
            this.priceHeader,
            this.paymentHeader,
            this.userHeader});
            this.listViewWithScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWithScrollBar1.FullRowSelect = true;
            this.listViewWithScrollBar1.GridLines = true;
            this.listViewWithScrollBar1.HideSelection = false;
            this.listViewWithScrollBar1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewWithScrollBar1.Location = new System.Drawing.Point(0, 25);
            this.listViewWithScrollBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewWithScrollBar1.Name = "listViewWithScrollBar1";
            this.listViewWithScrollBar1.Size = new System.Drawing.Size(719, 527);
            this.listViewWithScrollBar1.TabIndex = 0;
            this.listViewWithScrollBar1.UseCompatibleStateImageBehavior = false;
            this.listViewWithScrollBar1.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uložitToolStripButton,
            this.tiskToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(719, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // uložitToolStripButton
            // 
            this.uložitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("uložitToolStripButton.Image")));
            this.uložitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uložitToolStripButton.Name = "uložitToolStripButton";
            this.uložitToolStripButton.Size = new System.Drawing.Size(66, 22);
            this.uložitToolStripButton.Text = "Uložit*";
            this.uložitToolStripButton.Click += new System.EventHandler(this.uložitToolStripButton_Click);
            // 
            // tiskToolStripButton
            // 
            this.tiskToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("tiskToolStripButton.Image")));
            this.tiskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiskToolStripButton.Name = "tiskToolStripButton";
            this.tiskToolStripButton.Size = new System.Drawing.Size(55, 22);
            this.tiskToolStripButton.Text = "Tisk*";
            // 
            // dateHeader
            // 
            this.dateHeader.Text = "Datum";
            this.dateHeader.Width = 95;
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
            this.userHeader.Width = 128;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 552);
            this.Controls.Add(this.listViewWithScrollBar1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SalesForm";
            this.ShowIcon = false;
            this.Text = "Výpis transakcí";
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Pokladna.ListViewWithScrollBar listViewWithScrollBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton uložitToolStripButton;
        private System.Windows.Forms.ToolStripButton tiskToolStripButton;
        private System.Windows.Forms.ColumnHeader dateHeader;
        private System.Windows.Forms.ColumnHeader priceHeader;
        private System.Windows.Forms.ColumnHeader paymentHeader;
        private System.Windows.Forms.ColumnHeader userHeader;
    }
}