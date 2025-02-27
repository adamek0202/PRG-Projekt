﻿namespace Projekt.Forms
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dateStripButton = new System.Windows.Forms.ToolStripButton();
            this.filterButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.uživatelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.platbaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listViewWithScrollBar1 = new Pokladna.ListViewWithScrollBar();
            this.numberHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paymentHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateStripButton,
            this.filterButton,
            this.toolStripSeparator1,
            this.saveToolStripButton,
            this.toolStripSeparator2,
            this.printToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(719, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dateStripButton
            // 
            this.dateStripButton.Image = global::Pokladna.Properties.Resources.calendar;
            this.dateStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dateStripButton.Name = "dateStripButton";
            this.dateStripButton.Size = new System.Drawing.Size(71, 22);
            this.dateStripButton.Text = "Datum*";
            this.dateStripButton.Click += new System.EventHandler(this.dateStripButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uživatelToolStripMenuItem,
            this.platbaToolStripMenuItem});
            this.filterButton.Image = ((System.Drawing.Image)(resources.GetObject("filterButton.Image")));
            this.filterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(83, 22);
            this.filterButton.Text = "Filtrovat";
            // 
            // uživatelToolStripMenuItem
            // 
            this.uživatelToolStripMenuItem.Name = "uživatelToolStripMenuItem";
            this.uživatelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uživatelToolStripMenuItem.Text = "Uživatel*";
            this.uživatelToolStripMenuItem.Click += new System.EventHandler(this.uživatelToolStripMenuItem_Click);
            // 
            // platbaToolStripMenuItem
            // 
            this.platbaToolStripMenuItem.Name = "platbaToolStripMenuItem";
            this.platbaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.platbaToolStripMenuItem.Text = "Druh platby*";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(88, 22);
            this.saveToolStripButton.Text = "Uložit CSV";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "Tisk";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
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
            this.listViewWithScrollBar1.Location = new System.Drawing.Point(0, 25);
            this.listViewWithScrollBar1.Margin = new System.Windows.Forms.Padding(4);
            this.listViewWithScrollBar1.Name = "listViewWithScrollBar1";
            this.listViewWithScrollBar1.Size = new System.Drawing.Size(719, 527);
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
            this.Controls.Add(this.listViewWithScrollBar1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "SalesForm";
            this.Text = "Výpis transakcí";
            this.TopMost = true;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Pokladna.ListViewWithScrollBar listViewWithScrollBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ColumnHeader dateHeader;
        private System.Windows.Forms.ColumnHeader priceHeader;
        private System.Windows.Forms.ColumnHeader paymentHeader;
        private System.Windows.Forms.ColumnHeader userHeader;
        private System.Windows.Forms.ToolStripButton dateStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ColumnHeader numberHeader;
        private System.Windows.Forms.ToolStripDropDownButton filterButton;
        private System.Windows.Forms.ToolStripMenuItem uživatelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem platbaToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader timeHeader;
    }
}