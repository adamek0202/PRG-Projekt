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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "mZingerDouble",
            "210Kč",
            "2"}, -1);
            this.zingerButton = new System.Windows.Forms.Button();
            this.zingerDoubleButton = new System.Windows.Forms.Button();
            this.RemoveItemButton = new System.Windows.Forms.Button();
            this.mZingerDoubleButton = new System.Windows.Forms.Button();
            this.sumLabel = new System.Windows.Forms.Label();
            this.DownButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.CancelReceiptButton = new System.Windows.Forms.Button();
            this.ExchangeButton = new System.Windows.Forms.Button();
            this.mTwisterButton = new System.Windows.Forms.Button();
            this.mQuritoButton = new System.Windows.Forms.Button();
            this.mZingerButton = new System.Windows.Forms.Button();
            this.mTwisterBaconButton = new System.Windows.Forms.Button();
            this.mTwisterCheeseButton = new System.Windows.Forms.Button();
            this.mClassicButton = new System.Windows.Forms.Button();
            this.mHotWingButton = new System.Windows.Forms.Button();
            this.mStripsButton = new System.Windows.Forms.Button();
            this.mTexasGranderButton = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.CouponsButton = new System.Windows.Forms.Button();
            this.ManagerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView1 = new Projekt.ListViewWithScrollBar();
            this.Produkt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cena = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mnozstvi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // zingerButton
            // 
            this.zingerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerButton.FlatAppearance.BorderSize = 0;
            this.zingerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerButton.Location = new System.Drawing.Point(10, 3);
            this.zingerButton.Name = "zingerButton";
            this.zingerButton.Size = new System.Drawing.Size(96, 85);
            this.zingerButton.TabIndex = 1;
            this.zingerButton.Tag = "100";
            this.zingerButton.Text = "Zinger";
            this.zingerButton.UseVisualStyleBackColor = false;
            this.zingerButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // zingerDoubleButton
            // 
            this.zingerDoubleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.zingerDoubleButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.zingerDoubleButton.FlatAppearance.BorderSize = 0;
            this.zingerDoubleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zingerDoubleButton.Location = new System.Drawing.Point(112, 3);
            this.zingerDoubleButton.Name = "zingerDoubleButton";
            this.zingerDoubleButton.Size = new System.Drawing.Size(96, 85);
            this.zingerDoubleButton.TabIndex = 1;
            this.zingerDoubleButton.Tag = "101";
            this.zingerDoubleButton.Text = "Zinger\r\nDouble";
            this.zingerDoubleButton.UseVisualStyleBackColor = false;
            this.zingerDoubleButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // RemoveItemButton
            // 
            this.RemoveItemButton.BackColor = System.Drawing.Color.Red;
            this.RemoveItemButton.FlatAppearance.BorderSize = 0;
            this.RemoveItemButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.RemoveItemButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.RemoveItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveItemButton.ForeColor = System.Drawing.Color.White;
            this.RemoveItemButton.Location = new System.Drawing.Point(516, 973);
            this.RemoveItemButton.Name = "RemoveItemButton";
            this.RemoveItemButton.Size = new System.Drawing.Size(93, 98);
            this.RemoveItemButton.TabIndex = 0;
            this.RemoveItemButton.Text = "Storno\r\npoložky";
            this.RemoveItemButton.UseVisualStyleBackColor = false;
            this.RemoveItemButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // mZingerDoubleButton
            // 
            this.mZingerDoubleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mZingerDoubleButton.FlatAppearance.BorderSize = 0;
            this.mZingerDoubleButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerDoubleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mZingerDoubleButton.Location = new System.Drawing.Point(112, 12);
            this.mZingerDoubleButton.Name = "mZingerDoubleButton";
            this.mZingerDoubleButton.Size = new System.Drawing.Size(96, 85);
            this.mZingerDoubleButton.TabIndex = 0;
            this.mZingerDoubleButton.Text = "Zinger\r\nDouble\r\nMenu";
            this.mZingerDoubleButton.UseVisualStyleBackColor = false;
            this.mZingerDoubleButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // sumLabel
            // 
            this.sumLabel.AutoSize = true;
            this.sumLabel.ForeColor = System.Drawing.Color.White;
            this.sumLabel.Location = new System.Drawing.Point(12, 1048);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(76, 23);
            this.sumLabel.TabIndex = 0;
            this.sumLabel.Text = "Celkem";
            // 
            // DownButton
            // 
            this.DownButton.BackColor = System.Drawing.Color.Black;
            this.DownButton.FlatAppearance.BorderSize = 0;
            this.DownButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.DownButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownButton.ForeColor = System.Drawing.Color.White;
            this.DownButton.Location = new System.Drawing.Point(417, 947);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(93, 59);
            this.DownButton.TabIndex = 0;
            this.DownButton.Text = "Dolu";
            this.DownButton.UseVisualStyleBackColor = false;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.BackColor = System.Drawing.Color.Black;
            this.UpButton.FlatAppearance.BorderSize = 0;
            this.UpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.UpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpButton.ForeColor = System.Drawing.Color.White;
            this.UpButton.Location = new System.Drawing.Point(417, 882);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(93, 59);
            this.UpButton.TabIndex = 0;
            this.UpButton.Text = "Nahoru";
            this.UpButton.UseVisualStyleBackColor = false;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // CancelReceiptButton
            // 
            this.CancelReceiptButton.BackColor = System.Drawing.Color.Red;
            this.CancelReceiptButton.FlatAppearance.BorderSize = 0;
            this.CancelReceiptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelReceiptButton.ForeColor = System.Drawing.Color.White;
            this.CancelReceiptButton.Location = new System.Drawing.Point(417, 1012);
            this.CancelReceiptButton.Name = "CancelReceiptButton";
            this.CancelReceiptButton.Size = new System.Drawing.Size(93, 59);
            this.CancelReceiptButton.TabIndex = 0;
            this.CancelReceiptButton.Text = "Storno účtu";
            this.CancelReceiptButton.UseVisualStyleBackColor = false;
            this.CancelReceiptButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ExchangeButton
            // 
            this.ExchangeButton.BackColor = System.Drawing.Color.Yellow;
            this.ExchangeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ExchangeButton.FlatAppearance.BorderSize = 0;
            this.ExchangeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.ExchangeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.ExchangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExchangeButton.ForeColor = System.Drawing.Color.Black;
            this.ExchangeButton.Location = new System.Drawing.Point(615, 921);
            this.ExchangeButton.Name = "ExchangeButton";
            this.ExchangeButton.Size = new System.Drawing.Size(93, 85);
            this.ExchangeButton.TabIndex = 0;
            this.ExchangeButton.Text = "Výměna";
            this.ExchangeButton.UseVisualStyleBackColor = false;
            this.ExchangeButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // mTwisterButton
            // 
            this.mTwisterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mTwisterButton.FlatAppearance.BorderSize = 0;
            this.mTwisterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mTwisterButton.Location = new System.Drawing.Point(316, 12);
            this.mTwisterButton.Name = "mTwisterButton";
            this.mTwisterButton.Size = new System.Drawing.Size(96, 85);
            this.mTwisterButton.TabIndex = 0;
            this.mTwisterButton.Text = "Twister\r\nMenu\r\nSýr";
            this.mTwisterButton.UseVisualStyleBackColor = false;
            this.mTwisterButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mQuritoButton
            // 
            this.mQuritoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mQuritoButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mQuritoButton.FlatAppearance.BorderSize = 0;
            this.mQuritoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mQuritoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mQuritoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mQuritoButton.Location = new System.Drawing.Point(214, 12);
            this.mQuritoButton.Name = "mQuritoButton";
            this.mQuritoButton.Size = new System.Drawing.Size(96, 85);
            this.mQuritoButton.TabIndex = 0;
            this.mQuritoButton.Text = "Qurito Menu";
            this.mQuritoButton.UseVisualStyleBackColor = false;
            this.mQuritoButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mZingerButton
            // 
            this.mZingerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mZingerButton.FlatAppearance.BorderSize = 0;
            this.mZingerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mZingerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mZingerButton.Location = new System.Drawing.Point(10, 12);
            this.mZingerButton.Name = "mZingerButton";
            this.mZingerButton.Size = new System.Drawing.Size(96, 85);
            this.mZingerButton.TabIndex = 0;
            this.mZingerButton.Text = "Zinger\r\nMenu";
            this.mZingerButton.UseVisualStyleBackColor = false;
            this.mZingerButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mTwisterBaconButton
            // 
            this.mTwisterBaconButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterBaconButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mTwisterBaconButton.FlatAppearance.BorderSize = 0;
            this.mTwisterBaconButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterBaconButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterBaconButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mTwisterBaconButton.Location = new System.Drawing.Point(418, 12);
            this.mTwisterBaconButton.Name = "mTwisterBaconButton";
            this.mTwisterBaconButton.Size = new System.Drawing.Size(96, 85);
            this.mTwisterBaconButton.TabIndex = 0;
            this.mTwisterBaconButton.Text = "Twister\r\nMenu\r\nSlanina";
            this.mTwisterBaconButton.UseVisualStyleBackColor = false;
            this.mTwisterBaconButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mTwisterCheeseButton
            // 
            this.mTwisterCheeseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterCheeseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mTwisterCheeseButton.FlatAppearance.BorderSize = 0;
            this.mTwisterCheeseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterCheeseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTwisterCheeseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mTwisterCheeseButton.Location = new System.Drawing.Point(418, 104);
            this.mTwisterCheeseButton.Name = "mTwisterCheeseButton";
            this.mTwisterCheeseButton.Size = new System.Drawing.Size(96, 85);
            this.mTwisterCheeseButton.TabIndex = 0;
            this.mTwisterCheeseButton.Text = "Twister\r\nMenu\r\nSýr";
            this.mTwisterCheeseButton.UseVisualStyleBackColor = false;
            this.mTwisterCheeseButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mClassicButton
            // 
            this.mClassicButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mClassicButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mClassicButton.FlatAppearance.BorderSize = 0;
            this.mClassicButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mClassicButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mClassicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mClassicButton.Location = new System.Drawing.Point(112, 103);
            this.mClassicButton.Name = "mClassicButton";
            this.mClassicButton.Size = new System.Drawing.Size(96, 85);
            this.mClassicButton.TabIndex = 0;
            this.mClassicButton.Text = "Classic\r\nMenu";
            this.mClassicButton.UseVisualStyleBackColor = false;
            this.mClassicButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mHotWingButton
            // 
            this.mHotWingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mHotWingButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mHotWingButton.FlatAppearance.BorderSize = 0;
            this.mHotWingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mHotWingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mHotWingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mHotWingButton.Location = new System.Drawing.Point(10, 103);
            this.mHotWingButton.Name = "mHotWingButton";
            this.mHotWingButton.Size = new System.Drawing.Size(96, 85);
            this.mHotWingButton.TabIndex = 0;
            this.mHotWingButton.Text = "Hot Wings\r\nMenu";
            this.mHotWingButton.UseVisualStyleBackColor = false;
            this.mHotWingButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mStripsButton
            // 
            this.mStripsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mStripsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mStripsButton.FlatAppearance.BorderSize = 0;
            this.mStripsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mStripsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mStripsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mStripsButton.Location = new System.Drawing.Point(214, 104);
            this.mStripsButton.Name = "mStripsButton";
            this.mStripsButton.Size = new System.Drawing.Size(96, 85);
            this.mStripsButton.TabIndex = 0;
            this.mStripsButton.Text = "Strips\r\nMenu";
            this.mStripsButton.UseVisualStyleBackColor = false;
            this.mStripsButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // mTexasGranderButton
            // 
            this.mTexasGranderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTexasGranderButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mTexasGranderButton.FlatAppearance.BorderSize = 0;
            this.mTexasGranderButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTexasGranderButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mTexasGranderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mTexasGranderButton.Location = new System.Drawing.Point(316, 104);
            this.mTexasGranderButton.Name = "mTexasGranderButton";
            this.mTexasGranderButton.Size = new System.Drawing.Size(96, 85);
            this.mTexasGranderButton.TabIndex = 0;
            this.mTexasGranderButton.Text = "Texas\r\nGrander\r\nMenu";
            this.mTexasGranderButton.UseVisualStyleBackColor = false;
            this.mTexasGranderButton.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button10.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(214, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(96, 85);
            this.button10.TabIndex = 1;
            this.button10.Tag = "101";
            this.button10.Text = "Zinger\r\nDouble";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button11.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Location = new System.Drawing.Point(316, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(96, 85);
            this.button11.TabIndex = 1;
            this.button11.Tag = "101";
            this.button11.Text = "Zinger\r\nDouble";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.ItemButton_Click);
            // 
            // CouponsButton
            // 
            this.CouponsButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.CouponsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.CouponsButton.FlatAppearance.BorderSize = 0;
            this.CouponsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.CouponsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.CouponsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CouponsButton.ForeColor = System.Drawing.Color.Black;
            this.CouponsButton.Location = new System.Drawing.Point(615, 1012);
            this.CouponsButton.Name = "CouponsButton";
            this.CouponsButton.Size = new System.Drawing.Size(105, 59);
            this.CouponsButton.TabIndex = 0;
            this.CouponsButton.Text = "Kupóny";
            this.CouponsButton.UseVisualStyleBackColor = false;
            this.CouponsButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // ManagerButton
            // 
            this.ManagerButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ManagerButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ManagerButton.FlatAppearance.BorderSize = 0;
            this.ManagerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.ManagerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.ManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManagerButton.ForeColor = System.Drawing.Color.Black;
            this.ManagerButton.Location = new System.Drawing.Point(726, 1012);
            this.ManagerButton.Name = "ManagerButton";
            this.ManagerButton.Size = new System.Drawing.Size(105, 59);
            this.ManagerButton.TabIndex = 0;
            this.ManagerButton.Text = "Manažer\r\nFunkce";
            this.ManagerButton.UseVisualStyleBackColor = false;
            this.ManagerButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pokladní: ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.mQuritoButton);
            this.panel1.Controls.Add(this.mTwisterButton);
            this.panel1.Controls.Add(this.mTwisterBaconButton);
            this.panel1.Controls.Add(this.mTwisterCheeseButton);
            this.panel1.Controls.Add(this.mClassicButton);
            this.panel1.Controls.Add(this.mZingerDoubleButton);
            this.panel1.Controls.Add(this.mZingerButton);
            this.panel1.Controls.Add(this.mHotWingButton);
            this.panel1.Controls.Add(this.mStripsButton);
            this.panel1.Controls.Add(this.mTexasGranderButton);
            this.panel1.Location = new System.Drawing.Point(417, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 205);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(520, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 176);
            this.button1.TabIndex = 3;
            this.button1.Tag = "100";
            this.button1.Text = "B-Smarty";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.zingerButton);
            this.panel2.Controls.Add(this.zingerDoubleButton);
            this.panel2.Controls.Add(this.button10);
            this.panel2.Controls.Add(this.button11);
            this.panel2.Location = new System.Drawing.Point(417, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(867, 205);
            this.panel2.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Yellow;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(516, 882);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 85);
            this.button3.TabIndex = 0;
            this.button3.Text = "Konec";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Yellow;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(837, 947);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(183, 124);
            this.button4.TabIndex = 0;
            this.button4.Text = "Platba";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.SteelBlue;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Produkt,
            this.Cena,
            this.Mnozstvi});
            this.listView1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(13, 36);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(397, 1008);
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(332, 40);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Produkt
            // 
            this.Produkt.DisplayIndex = 1;
            this.Produkt.Text = "Produkt";
            this.Produkt.Width = 230;
            // 
            // Cena
            // 
            this.Cena.DisplayIndex = 2;
            this.Cena.Tag = "Cena";
            this.Cena.Text = "Cena";
            this.Cena.Width = 100;
            // 
            // Mnozstvi
            // 
            this.Mnozstvi.DisplayIndex = 0;
            this.Mnozstvi.Text = "";
            this.Mnozstvi.Width = 45;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sumLabel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.ManagerButton);
            this.Controls.Add(this.CouponsButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ExchangeButton);
            this.Controls.Add(this.CancelReceiptButton);
            this.Controls.Add(this.RemoveItemButton);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Arial", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokladna";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewWithScrollBar listView1;
        private System.Windows.Forms.ColumnHeader Produkt;
        private System.Windows.Forms.ColumnHeader Cena;
        private System.Windows.Forms.Button zingerButton;
        private System.Windows.Forms.Button zingerDoubleButton;
        private System.Windows.Forms.Button RemoveItemButton;
        private System.Windows.Forms.Button mZingerDoubleButton;
        private System.Windows.Forms.Label sumLabel;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.ColumnHeader Mnozstvi;
        private System.Windows.Forms.Button CancelReceiptButton;
        private System.Windows.Forms.Button ExchangeButton;
        private System.Windows.Forms.Button mTwisterButton;
        private System.Windows.Forms.Button mQuritoButton;
        private System.Windows.Forms.Button mZingerButton;
        private System.Windows.Forms.Button mTwisterBaconButton;
        private System.Windows.Forms.Button mTwisterCheeseButton;
        private System.Windows.Forms.Button mClassicButton;
        private System.Windows.Forms.Button mHotWingButton;
        private System.Windows.Forms.Button mStripsButton;
        private System.Windows.Forms.Button mTexasGranderButton;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button CouponsButton;
        private System.Windows.Forms.Button ManagerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

