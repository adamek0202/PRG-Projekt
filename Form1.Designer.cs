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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Menu 1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Menu 2", System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1 = new System.Windows.Forms.ListView();
            this.Produkt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cena = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mnozstvi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.zingerButton = new System.Windows.Forms.Button();
            this.zingerDoubleButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.mZingerDoubleButton = new System.Windows.Forms.Button();
            this.sumLabel = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.mZingerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.SteelBlue;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Produkt,
            this.Cena,
            this.Mnozstvi});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            listViewGroup1.Header = "Menu 1";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Menu 2";
            listViewGroup2.Name = "listViewGroup2";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(13, 36);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(331, 694);
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
            this.zingerButton.FlatAppearance.BorderSize = 0;
            this.zingerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerButton.Location = new System.Drawing.Point(351, 103);
            this.zingerButton.Name = "zingerButton";
            this.zingerButton.Size = new System.Drawing.Size(96, 85);
            this.zingerButton.TabIndex = 1;
            this.zingerButton.Tag = "100";
            this.zingerButton.Text = "Zinger";
            this.zingerButton.UseVisualStyleBackColor = false;
            this.zingerButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // zingerDoubleButton
            // 
            this.zingerDoubleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerDoubleButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerDoubleButton.FlatAppearance.BorderSize = 0;
            this.zingerDoubleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerDoubleButton.Location = new System.Drawing.Point(453, 103);
            this.zingerDoubleButton.Name = "zingerDoubleButton";
            this.zingerDoubleButton.Size = new System.Drawing.Size(96, 85);
            this.zingerDoubleButton.TabIndex = 1;
            this.zingerDoubleButton.Tag = "101";
            this.zingerDoubleButton.Text = "Zinger\r\nDouble";
            this.zingerDoubleButton.UseVisualStyleBackColor = false;
            this.zingerDoubleButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.Red;
            this.RemoveButton.FlatAppearance.BorderSize = 0;
            this.RemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.RemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.ForeColor = System.Drawing.Color.White;
            this.RemoveButton.Location = new System.Drawing.Point(450, 656);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(93, 98);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Storno\r\npoložky";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // mZingerDoubleButton
            // 
            this.mZingerDoubleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mZingerDoubleButton.FlatAppearance.BorderSize = 0;
            this.mZingerDoubleButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mZingerDoubleButton.Location = new System.Drawing.Point(453, 12);
            this.mZingerDoubleButton.Name = "mZingerDoubleButton";
            this.mZingerDoubleButton.Size = new System.Drawing.Size(96, 85);
            this.mZingerDoubleButton.TabIndex = 1;
            this.mZingerDoubleButton.Text = "Zinger\r\nDouble\r\nMenu";
            this.mZingerDoubleButton.UseVisualStyleBackColor = false;
            this.mZingerDoubleButton.Click += new System.EventHandler(this.Button4_Click);
            // 
            // sumLabel
            // 
            this.sumLabel.AutoSize = true;
            this.sumLabel.ForeColor = System.Drawing.Color.White;
            this.sumLabel.Location = new System.Drawing.Point(12, 734);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(76, 23);
            this.sumLabel.TabIndex = 3;
            this.sumLabel.Text = "Celkem";
            // 
            // downButton
            // 
            this.downButton.BackColor = System.Drawing.Color.Black;
            this.downButton.FlatAppearance.BorderSize = 0;
            this.downButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.downButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downButton.ForeColor = System.Drawing.Color.White;
            this.downButton.Location = new System.Drawing.Point(351, 630);
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
            this.upButton.FlatAppearance.BorderSize = 0;
            this.upButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.upButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upButton.ForeColor = System.Drawing.Color.White;
            this.upButton.Location = new System.Drawing.Point(351, 565);
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
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(351, 695);
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
            this.ReplaceButton.FlatAppearance.BorderSize = 0;
            this.ReplaceButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.ReplaceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.ReplaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceButton.ForeColor = System.Drawing.Color.Black;
            this.ReplaceButton.Location = new System.Drawing.Point(450, 565);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(93, 85);
            this.ReplaceButton.TabIndex = 2;
            this.ReplaceButton.Text = "Výměna";
            this.ReplaceButton.UseVisualStyleBackColor = false;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(657, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 85);
            this.button3.TabIndex = 1;
            this.button3.Text = "Twister\r\nMenu\r\nSýr";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(555, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 85);
            this.button5.TabIndex = 1;
            this.button5.Text = "Qurito Menu";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // mZingerButton
            // 
            this.mZingerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mZingerButton.FlatAppearance.BorderSize = 0;
            this.mZingerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mZingerButton.Location = new System.Drawing.Point(351, 12);
            this.mZingerButton.Name = "mZingerButton";
            this.mZingerButton.Size = new System.Drawing.Size(96, 85);
            this.mZingerButton.TabIndex = 1;
            this.mZingerButton.Text = "Zinger\r\nMenu";
            this.mZingerButton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(759, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 85);
            this.button1.TabIndex = 1;
            this.button1.Text = "Twister\r\nMenu\r\nSlanina";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(861, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 85);
            this.button2.TabIndex = 1;
            this.button2.Text = "Twister\r\nMenu\r\nSlanina";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(963, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 85);
            this.button4.TabIndex = 1;
            this.button4.Text = "Classic\r\nMenu";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(1065, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 85);
            this.button6.TabIndex = 1;
            this.button6.Text = "Hot Wings\r\nMenu";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button7.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(1167, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(96, 85);
            this.button7.TabIndex = 1;
            this.button7.Text = "Detske\r\nMenu";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button8.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(1269, 13);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(96, 85);
            this.button8.TabIndex = 1;
            this.button8.Text = "Strips\r\nMenu";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button9.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Location = new System.Drawing.Point(1371, 13);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(96, 85);
            this.button9.TabIndex = 1;
            this.button9.Text = "Texas\r\nGrander\r\nMenu";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button10.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(555, 103);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(96, 85);
            this.button10.TabIndex = 1;
            this.button10.Tag = "101";
            this.button10.Text = "Zinger\r\nDouble";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button2_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button11.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Location = new System.Drawing.Point(657, 103);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(96, 85);
            this.button11.TabIndex = 1;
            this.button11.Tag = "101";
            this.button11.Text = "Zinger\r\nDouble";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button2_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button12.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Location = new System.Drawing.Point(549, 695);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(105, 59);
            this.button12.TabIndex = 2;
            this.button12.Text = "Kupóny";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button13.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.ForeColor = System.Drawing.Color.Black;
            this.button13.Location = new System.Drawing.Point(660, 695);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(105, 59);
            this.button13.TabIndex = 2;
            this.button13.Text = "Manažer\r\nFunkce";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pokladní: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1478, 766);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sumLabel);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.mZingerButton);
            this.Controls.Add(this.mZingerDoubleButton);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.zingerDoubleButton);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.zingerButton);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private System.Windows.Forms.Button mZingerDoubleButton;
        private System.Windows.Forms.Label sumLabel;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.ColumnHeader Mnozstvi;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ReplaceButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button mZingerButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label1;
    }
}

