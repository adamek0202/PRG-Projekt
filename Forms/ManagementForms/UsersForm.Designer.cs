namespace Pokladna.Forms.ManagementForms
{
    partial class UsersForm
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
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.surnameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.positionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewWithScrollBar1
            // 
            this.listViewWithScrollBar1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.codeHeader,
            this.nameHeader,
            this.surnameHeader,
            this.positionHeader});
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
            this.listViewWithScrollBar1.Size = new System.Drawing.Size(835, 540);
            this.listViewWithScrollBar1.TabIndex = 0;
            this.listViewWithScrollBar1.UseCompatibleStateImageBehavior = false;
            this.listViewWithScrollBar1.View = System.Windows.Forms.View.Details;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Jméno";
            this.nameHeader.Width = 150;
            // 
            // surnameHeader
            // 
            this.surnameHeader.Text = "Příjmení";
            this.surnameHeader.Width = 150;
            // 
            // codeHeader
            // 
            this.codeHeader.Text = "Kód";
            this.codeHeader.Width = 70;
            // 
            // positionHeader
            // 
            this.positionHeader.Text = "Pozice";
            this.positionHeader.Width = 100;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 540);
            this.ControlBox = false;
            this.Controls.Add(this.listViewWithScrollBar1);
            this.Name = "UsersForm";
            this.Text = "Uživatelé";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ListViewWithScrollBar listViewWithScrollBar1;
        private System.Windows.Forms.ColumnHeader codeHeader;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader surnameHeader;
        private System.Windows.Forms.ColumnHeader positionHeader;
    }
}