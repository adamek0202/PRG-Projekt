namespace Projekt
{
    partial class MainForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Menu 1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Menu 2", System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1 = new System.Windows.Forms.ListView();
            this.Produkt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cena = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mnozstvi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.zingerButton = new System.Windows.Forms.Button();
            this.zingerDoubleButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.zingerDoubleMenuButton = new System.Windows.Forms.Button();
            this.sumLabel = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.zingerMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Produkt,
            this.Cena,
            this.Mnozstvi});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            listViewGroup3.Header = "Menu 1";
            listViewGroup3.Name = "listViewGroup1";
            listViewGroup4.Header = "Menu 2";
            listViewGroup4.Name = "listViewGroup2";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(13, 13);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(331, 717);
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(332, 40);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Produkt
            // 
            this.Produkt.DisplayIndex = 1;
            this.Produkt.Text = "Produkt";
            this.Produkt.Width = 219;
            // 
            // Cena
            // 
            this.Cena.DisplayIndex = 2;
            this.Cena.Tag = "Cena";
            this.Cena.Text = "Cena";
            this.Cena.Width = 85;
            // 
            // Mnozstvi
            // 
            this.Mnozstvi.DisplayIndex = 0;
            this.Mnozstvi.Text = "";
            this.Mnozstvi.Width = 27;
            // 
            // zingerButton
            // 
            this.zingerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerButton.Location = new System.Drawing.Point(351, 13);
            this.zingerButton.Name = "zingerButton";
            this.zingerButton.Size = new System.Drawing.Size(96, 85);
            this.zingerButton.TabIndex = 1;
            this.zingerButton.Text = "Zinger";
            this.zingerButton.UseVisualStyleBackColor = false;
            this.zingerButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // zingerDoubleButton
            // 
            this.zingerDoubleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerDoubleButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerDoubleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerDoubleButton.Location = new System.Drawing.Point(453, 13);
            this.zingerDoubleButton.Name = "zingerDoubleButton";
            this.zingerDoubleButton.Size = new System.Drawing.Size(96, 85);
            this.zingerDoubleButton.TabIndex = 1;
            this.zingerDoubleButton.Text = "Zinger\r\nDouble";
            this.zingerDoubleButton.UseVisualStyleBackColor = false;
            this.zingerDoubleButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.Red;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.ForeColor = System.Drawing.Color.White;
            this.RemoveButton.Location = new System.Drawing.Point(351, 698);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(93, 59);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Storno\r\npoložky";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // zingerDoubleMenuButton
            // 
            this.zingerDoubleMenuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerDoubleMenuButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerDoubleMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerDoubleMenuButton.Location = new System.Drawing.Point(453, 104);
            this.zingerDoubleMenuButton.Name = "zingerDoubleMenuButton";
            this.zingerDoubleMenuButton.Size = new System.Drawing.Size(96, 85);
            this.zingerDoubleMenuButton.TabIndex = 1;
            this.zingerDoubleMenuButton.Text = "Zinger\r\nDouble\r\nMenu";
            this.zingerDoubleMenuButton.UseVisualStyleBackColor = false;
            this.zingerDoubleMenuButton.Click += new System.EventHandler(this.Button4_Click);
            // 
            // sumLabel
            // 
            this.sumLabel.AutoSize = true;
            this.sumLabel.Location = new System.Drawing.Point(12, 734);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(62, 18);
            this.sumLabel.TabIndex = 3;
            this.sumLabel.Text = "Celkem";
            // 
            // downButton
            // 
            this.downButton.BackColor = System.Drawing.Color.Black;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downButton.ForeColor = System.Drawing.Color.White;
            this.downButton.Location = new System.Drawing.Point(351, 633);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(93, 59);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "Dolu";
            this.downButton.UseVisualStyleBackColor = false;
            this.downButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // upButton
            // 
            this.upButton.BackColor = System.Drawing.Color.Black;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upButton.ForeColor = System.Drawing.Color.White;
            this.upButton.Location = new System.Drawing.Point(351, 568);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(93, 59);
            this.upButton.TabIndex = 2;
            this.upButton.Text = "Nahoru";
            this.upButton.UseVisualStyleBackColor = false;
            this.upButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.Red;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(450, 698);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(93, 59);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Storno účtu";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.BackColor = System.Drawing.Color.Yellow;
            this.ReplaceButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ReplaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceButton.ForeColor = System.Drawing.Color.Black;
            this.ReplaceButton.Location = new System.Drawing.Point(450, 633);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(93, 59);
            this.ReplaceButton.TabIndex = 2;
            this.ReplaceButton.Text = "Výměna";
            this.ReplaceButton.UseVisualStyleBackColor = false;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(351, 195);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 85);
            this.button3.TabIndex = 1;
            this.button3.Text = "Hranolky malé";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(453, 195);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 85);
            this.button5.TabIndex = 1;
            this.button5.Text = "Hranolky velké";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // zingerMenuButton
            // 
            this.zingerMenuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerMenuButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerMenuButton.Location = new System.Drawing.Point(351, 104);
            this.zingerMenuButton.Name = "zingerMenuButton";
            this.zingerMenuButton.Size = new System.Drawing.Size(96, 85);
            this.zingerMenuButton.TabIndex = 1;
            this.zingerMenuButton.Text = "Zinger\r\nMenu";
            this.zingerMenuButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 766);
            this.Controls.Add(this.sumLabel);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.zingerMenuButton);
            this.Controls.Add(this.zingerDoubleMenuButton);
            this.Controls.Add(this.zingerDoubleButton);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.zingerButton);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Pokladna";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Produkt;
        private System.Windows.Forms.ColumnHeader Cena;
        private System.Windows.Forms.Button zingerButton;
        private System.Windows.Forms.Button zingerDoubleButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button zingerDoubleMenuButton;
        private System.Windows.Forms.Label sumLabel;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.ColumnHeader Mnozstvi;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ReplaceButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button zingerMenuButton;
    }
}

