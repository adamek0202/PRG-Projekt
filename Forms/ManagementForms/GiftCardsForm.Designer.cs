namespace Pokladna.Forms.ManagementForms
{
    partial class GiftCardsForm
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
            this.listViewWithScrollBar1 = new Pokladna.Forms.Controls.ListViewWithScrollBar();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewWithScrollBar1
            // 
            this.listViewWithScrollBar1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewWithScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWithScrollBar1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listViewWithScrollBar1.FullRowSelect = true;
            this.listViewWithScrollBar1.GridLines = true;
            this.listViewWithScrollBar1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewWithScrollBar1.HideSelection = false;
            this.listViewWithScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.listViewWithScrollBar1.MultiSelect = false;
            this.listViewWithScrollBar1.Name = "listViewWithScrollBar1";
            this.listViewWithScrollBar1.ShowGroups = false;
            this.listViewWithScrollBar1.Size = new System.Drawing.Size(940, 579);
            this.listViewWithScrollBar1.TabIndex = 0;
            this.listViewWithScrollBar1.UseCompatibleStateImageBehavior = false;
            this.listViewWithScrollBar1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kód";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Obnos";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Vydána";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Expirace";
            this.columnHeader4.Width = 120;
            // 
            // GiftCardsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 579);
            this.ControlBox = false;
            this.Controls.Add(this.listViewWithScrollBar1);
            this.Name = "GiftCardsForm";
            this.Text = "Dárkové karty";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ListViewWithScrollBar listViewWithScrollBar1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}