namespace Projekt.Forms
{
    partial class PaymentForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Menu 1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Menu 2", System.Windows.Forms.HorizontalAlignment.Left);
            this.keypadPanel = new System.Windows.Forms.Panel();
            this.kRemoveButton = new System.Windows.Forms.Button();
            this.kDualZeroButton = new System.Windows.Forms.Button();
            this.kZeroButton = new System.Windows.Forms.Button();
            this.kNineButton = new System.Windows.Forms.Button();
            this.kEightButton = new System.Windows.Forms.Button();
            this.PayedTextBox = new System.Windows.Forms.TextBox();
            this.kSevenButton = new System.Windows.Forms.Button();
            this.kSixButton = new System.Windows.Forms.Button();
            this.kFiveButton = new System.Windows.Forms.Button();
            this.kFourButton = new System.Windows.Forms.Button();
            this.kThreeButton = new System.Windows.Forms.Button();
            this.kTwoButton = new System.Windows.Forms.Button();
            this.kOneButton = new System.Windows.Forms.Button();
            this.cardButton = new System.Windows.Forms.Button();
            this.foodCardButton = new System.Windows.Forms.Button();
            this.PluxeeButton = new System.Windows.Forms.Button();
            this.ChequeButton = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.giftCardButton = new System.Windows.Forms.Button();
            this.discountButton = new System.Windows.Forms.Button();
            this.cashButton = new System.Windows.Forms.Button();
            this.exactCashButton = new System.Windows.Forms.Button();
            this.sumLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.listView1 = new Projekt.ListViewWithScrollBar();
            this.Produkt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cena = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mnozstvi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.keypadPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // keypadPanel
            // 
            this.keypadPanel.Controls.Add(this.kRemoveButton);
            this.keypadPanel.Controls.Add(this.kDualZeroButton);
            this.keypadPanel.Controls.Add(this.kZeroButton);
            this.keypadPanel.Controls.Add(this.kNineButton);
            this.keypadPanel.Controls.Add(this.kEightButton);
            this.keypadPanel.Controls.Add(this.PayedTextBox);
            this.keypadPanel.Controls.Add(this.kSevenButton);
            this.keypadPanel.Controls.Add(this.kSixButton);
            this.keypadPanel.Controls.Add(this.kFiveButton);
            this.keypadPanel.Controls.Add(this.kFourButton);
            this.keypadPanel.Controls.Add(this.kThreeButton);
            this.keypadPanel.Controls.Add(this.kTwoButton);
            this.keypadPanel.Controls.Add(this.kOneButton);
            this.keypadPanel.Location = new System.Drawing.Point(477, 312);
            this.keypadPanel.Margin = new System.Windows.Forms.Padding(4);
            this.keypadPanel.Name = "keypadPanel";
            this.keypadPanel.Size = new System.Drawing.Size(293, 431);
            this.keypadPanel.TabIndex = 0;
            // 
            // kRemoveButton
            // 
            this.kRemoveButton.BackColor = System.Drawing.Color.Black;
            this.kRemoveButton.FlatAppearance.BorderSize = 0;
            this.kRemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kRemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kRemoveButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kRemoveButton.ForeColor = System.Drawing.Color.White;
            this.kRemoveButton.Location = new System.Drawing.Point(200, 344);
            this.kRemoveButton.Margin = new System.Windows.Forms.Padding(4);
            this.kRemoveButton.Name = "kRemoveButton";
            this.kRemoveButton.Size = new System.Drawing.Size(90, 81);
            this.kRemoveButton.TabIndex = 4;
            this.kRemoveButton.Text = "←";
            this.kRemoveButton.UseVisualStyleBackColor = false;
            this.kRemoveButton.Click += new System.EventHandler(this.KRemoveButton_Click);
            // 
            // kDualZeroButton
            // 
            this.kDualZeroButton.BackColor = System.Drawing.Color.Black;
            this.kDualZeroButton.FlatAppearance.BorderSize = 0;
            this.kDualZeroButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kDualZeroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kDualZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kDualZeroButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kDualZeroButton.ForeColor = System.Drawing.Color.White;
            this.kDualZeroButton.Location = new System.Drawing.Point(4, 344);
            this.kDualZeroButton.Margin = new System.Windows.Forms.Padding(4);
            this.kDualZeroButton.Name = "kDualZeroButton";
            this.kDualZeroButton.Size = new System.Drawing.Size(90, 81);
            this.kDualZeroButton.TabIndex = 4;
            this.kDualZeroButton.Tag = "00";
            this.kDualZeroButton.Text = "00";
            this.kDualZeroButton.UseVisualStyleBackColor = false;
            this.kDualZeroButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kZeroButton
            // 
            this.kZeroButton.BackColor = System.Drawing.Color.Black;
            this.kZeroButton.FlatAppearance.BorderSize = 0;
            this.kZeroButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kZeroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kZeroButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kZeroButton.ForeColor = System.Drawing.Color.White;
            this.kZeroButton.Location = new System.Drawing.Point(102, 344);
            this.kZeroButton.Margin = new System.Windows.Forms.Padding(4);
            this.kZeroButton.Name = "kZeroButton";
            this.kZeroButton.Size = new System.Drawing.Size(90, 81);
            this.kZeroButton.TabIndex = 4;
            this.kZeroButton.Tag = "0";
            this.kZeroButton.Text = "0";
            this.kZeroButton.UseVisualStyleBackColor = false;
            this.kZeroButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kNineButton
            // 
            this.kNineButton.BackColor = System.Drawing.Color.Black;
            this.kNineButton.FlatAppearance.BorderSize = 0;
            this.kNineButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kNineButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kNineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kNineButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kNineButton.ForeColor = System.Drawing.Color.White;
            this.kNineButton.Location = new System.Drawing.Point(200, 255);
            this.kNineButton.Margin = new System.Windows.Forms.Padding(4);
            this.kNineButton.Name = "kNineButton";
            this.kNineButton.Size = new System.Drawing.Size(90, 81);
            this.kNineButton.TabIndex = 4;
            this.kNineButton.Tag = "9";
            this.kNineButton.Text = "9";
            this.kNineButton.UseVisualStyleBackColor = false;
            this.kNineButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kEightButton
            // 
            this.kEightButton.BackColor = System.Drawing.Color.Black;
            this.kEightButton.FlatAppearance.BorderSize = 0;
            this.kEightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kEightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kEightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kEightButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kEightButton.ForeColor = System.Drawing.Color.White;
            this.kEightButton.Location = new System.Drawing.Point(102, 255);
            this.kEightButton.Margin = new System.Windows.Forms.Padding(4);
            this.kEightButton.Name = "kEightButton";
            this.kEightButton.Size = new System.Drawing.Size(90, 81);
            this.kEightButton.TabIndex = 4;
            this.kEightButton.Tag = "8";
            this.kEightButton.Text = "8";
            this.kEightButton.UseVisualStyleBackColor = false;
            this.kEightButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // PayedTextBox
            // 
            this.PayedTextBox.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PayedTextBox.Location = new System.Drawing.Point(3, 17);
            this.PayedTextBox.Name = "PayedTextBox";
            this.PayedTextBox.Size = new System.Drawing.Size(287, 44);
            this.PayedTextBox.TabIndex = 11;
            // 
            // kSevenButton
            // 
            this.kSevenButton.BackColor = System.Drawing.Color.Black;
            this.kSevenButton.FlatAppearance.BorderSize = 0;
            this.kSevenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kSevenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kSevenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kSevenButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kSevenButton.ForeColor = System.Drawing.Color.White;
            this.kSevenButton.Location = new System.Drawing.Point(4, 255);
            this.kSevenButton.Margin = new System.Windows.Forms.Padding(4);
            this.kSevenButton.Name = "kSevenButton";
            this.kSevenButton.Size = new System.Drawing.Size(90, 81);
            this.kSevenButton.TabIndex = 4;
            this.kSevenButton.Tag = "7";
            this.kSevenButton.Text = "7";
            this.kSevenButton.UseVisualStyleBackColor = false;
            this.kSevenButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kSixButton
            // 
            this.kSixButton.BackColor = System.Drawing.Color.Black;
            this.kSixButton.FlatAppearance.BorderSize = 0;
            this.kSixButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kSixButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kSixButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kSixButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kSixButton.ForeColor = System.Drawing.Color.White;
            this.kSixButton.Location = new System.Drawing.Point(200, 166);
            this.kSixButton.Margin = new System.Windows.Forms.Padding(4);
            this.kSixButton.Name = "kSixButton";
            this.kSixButton.Size = new System.Drawing.Size(90, 81);
            this.kSixButton.TabIndex = 4;
            this.kSixButton.Tag = "6";
            this.kSixButton.Text = "6";
            this.kSixButton.UseVisualStyleBackColor = false;
            this.kSixButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kFiveButton
            // 
            this.kFiveButton.BackColor = System.Drawing.Color.Black;
            this.kFiveButton.FlatAppearance.BorderSize = 0;
            this.kFiveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kFiveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kFiveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kFiveButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kFiveButton.ForeColor = System.Drawing.Color.White;
            this.kFiveButton.Location = new System.Drawing.Point(102, 166);
            this.kFiveButton.Margin = new System.Windows.Forms.Padding(4);
            this.kFiveButton.Name = "kFiveButton";
            this.kFiveButton.Size = new System.Drawing.Size(90, 81);
            this.kFiveButton.TabIndex = 4;
            this.kFiveButton.Tag = "5";
            this.kFiveButton.Text = "5";
            this.kFiveButton.UseVisualStyleBackColor = false;
            this.kFiveButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kFourButton
            // 
            this.kFourButton.BackColor = System.Drawing.Color.Black;
            this.kFourButton.FlatAppearance.BorderSize = 0;
            this.kFourButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kFourButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kFourButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kFourButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kFourButton.ForeColor = System.Drawing.Color.White;
            this.kFourButton.Location = new System.Drawing.Point(4, 166);
            this.kFourButton.Margin = new System.Windows.Forms.Padding(4);
            this.kFourButton.Name = "kFourButton";
            this.kFourButton.Size = new System.Drawing.Size(90, 81);
            this.kFourButton.TabIndex = 3;
            this.kFourButton.Tag = "4";
            this.kFourButton.Text = "4";
            this.kFourButton.UseVisualStyleBackColor = false;
            this.kFourButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kThreeButton
            // 
            this.kThreeButton.BackColor = System.Drawing.Color.Black;
            this.kThreeButton.FlatAppearance.BorderSize = 0;
            this.kThreeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kThreeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kThreeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kThreeButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kThreeButton.ForeColor = System.Drawing.Color.White;
            this.kThreeButton.Location = new System.Drawing.Point(200, 77);
            this.kThreeButton.Margin = new System.Windows.Forms.Padding(4);
            this.kThreeButton.Name = "kThreeButton";
            this.kThreeButton.Size = new System.Drawing.Size(90, 81);
            this.kThreeButton.TabIndex = 2;
            this.kThreeButton.Tag = "3";
            this.kThreeButton.Text = "3";
            this.kThreeButton.UseVisualStyleBackColor = false;
            this.kThreeButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kTwoButton
            // 
            this.kTwoButton.BackColor = System.Drawing.Color.Black;
            this.kTwoButton.FlatAppearance.BorderSize = 0;
            this.kTwoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kTwoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kTwoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kTwoButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kTwoButton.ForeColor = System.Drawing.Color.White;
            this.kTwoButton.Location = new System.Drawing.Point(102, 77);
            this.kTwoButton.Margin = new System.Windows.Forms.Padding(4);
            this.kTwoButton.Name = "kTwoButton";
            this.kTwoButton.Size = new System.Drawing.Size(90, 81);
            this.kTwoButton.TabIndex = 0;
            this.kTwoButton.Tag = "2";
            this.kTwoButton.Text = "2";
            this.kTwoButton.UseVisualStyleBackColor = false;
            this.kTwoButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // kOneButton
            // 
            this.kOneButton.BackColor = System.Drawing.Color.Black;
            this.kOneButton.FlatAppearance.BorderSize = 0;
            this.kOneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.kOneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.kOneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kOneButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kOneButton.ForeColor = System.Drawing.Color.White;
            this.kOneButton.Location = new System.Drawing.Point(4, 77);
            this.kOneButton.Margin = new System.Windows.Forms.Padding(4);
            this.kOneButton.Name = "kOneButton";
            this.kOneButton.Size = new System.Drawing.Size(90, 81);
            this.kOneButton.TabIndex = 1;
            this.kOneButton.Tag = "1";
            this.kOneButton.Text = "1";
            this.kOneButton.UseVisualStyleBackColor = false;
            this.kOneButton.Click += new System.EventHandler(this.KeypadButton_Click);
            // 
            // cardButton
            // 
            this.cardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.cardButton.FlatAppearance.BorderSize = 0;
            this.cardButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.cardButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.cardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cardButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cardButton.ForeColor = System.Drawing.Color.Black;
            this.cardButton.Location = new System.Drawing.Point(15, 149);
            this.cardButton.Margin = new System.Windows.Forms.Padding(4);
            this.cardButton.Name = "cardButton";
            this.cardButton.Size = new System.Drawing.Size(169, 126);
            this.cardButton.TabIndex = 4;
            this.cardButton.Text = "Platební karta";
            this.cardButton.UseVisualStyleBackColor = false;
            this.cardButton.Click += new System.EventHandler(this.CardButton_Click);
            // 
            // foodCardButton
            // 
            this.foodCardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.foodCardButton.FlatAppearance.BorderSize = 0;
            this.foodCardButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.foodCardButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.foodCardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.foodCardButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.foodCardButton.ForeColor = System.Drawing.Color.Black;
            this.foodCardButton.Location = new System.Drawing.Point(15, 283);
            this.foodCardButton.Margin = new System.Windows.Forms.Padding(4);
            this.foodCardButton.Name = "foodCardButton";
            this.foodCardButton.Size = new System.Drawing.Size(169, 126);
            this.foodCardButton.TabIndex = 4;
            this.foodCardButton.Tag = "FoodCard";
            this.foodCardButton.Text = "Stravenková karta";
            this.foodCardButton.UseVisualStyleBackColor = false;
            this.foodCardButton.Click += new System.EventHandler(this.CardButton_Click);
            // 
            // PluxeeButton
            // 
            this.PluxeeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.PluxeeButton.FlatAppearance.BorderSize = 0;
            this.PluxeeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.PluxeeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.PluxeeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PluxeeButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PluxeeButton.ForeColor = System.Drawing.Color.Black;
            this.PluxeeButton.Location = new System.Drawing.Point(192, 149);
            this.PluxeeButton.Margin = new System.Windows.Forms.Padding(4);
            this.PluxeeButton.Name = "PluxeeButton";
            this.PluxeeButton.Size = new System.Drawing.Size(145, 59);
            this.PluxeeButton.TabIndex = 4;
            this.PluxeeButton.Text = "Pluxee*";
            this.PluxeeButton.UseVisualStyleBackColor = false;
            // 
            // ChequeButton
            // 
            this.ChequeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.ChequeButton.FlatAppearance.BorderSize = 0;
            this.ChequeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.ChequeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.ChequeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChequeButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ChequeButton.ForeColor = System.Drawing.Color.Black;
            this.ChequeButton.Location = new System.Drawing.Point(192, 216);
            this.ChequeButton.Margin = new System.Windows.Forms.Padding(4);
            this.ChequeButton.Name = "ChequeButton";
            this.ChequeButton.Size = new System.Drawing.Size(145, 59);
            this.ChequeButton.TabIndex = 5;
            this.ChequeButton.Text = "Cheque*";
            this.ChequeButton.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.button16.FlatAppearance.BorderSize = 0;
            this.button16.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.button16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button16.ForeColor = System.Drawing.Color.Black;
            this.button16.Location = new System.Drawing.Point(192, 283);
            this.button16.Margin = new System.Windows.Forms.Padding(4);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(145, 59);
            this.button16.TabIndex = 6;
            this.button16.Text = "Ticket Restaurant*";
            this.button16.UseVisualStyleBackColor = false;
            // 
            // giftCardButton
            // 
            this.giftCardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.giftCardButton.FlatAppearance.BorderSize = 0;
            this.giftCardButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.giftCardButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.giftCardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.giftCardButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.giftCardButton.ForeColor = System.Drawing.Color.Black;
            this.giftCardButton.Location = new System.Drawing.Point(191, 82);
            this.giftCardButton.Margin = new System.Windows.Forms.Padding(4);
            this.giftCardButton.Name = "giftCardButton";
            this.giftCardButton.Size = new System.Drawing.Size(145, 59);
            this.giftCardButton.TabIndex = 7;
            this.giftCardButton.Text = "Dárková karta*";
            this.giftCardButton.UseVisualStyleBackColor = false;
            // 
            // discountButton
            // 
            this.discountButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.discountButton.FlatAppearance.BorderSize = 0;
            this.discountButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.discountButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.discountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.discountButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.discountButton.ForeColor = System.Drawing.Color.Black;
            this.discountButton.Location = new System.Drawing.Point(15, 82);
            this.discountButton.Margin = new System.Windows.Forms.Padding(4);
            this.discountButton.Name = "discountButton";
            this.discountButton.Size = new System.Drawing.Size(169, 59);
            this.discountButton.TabIndex = 8;
            this.discountButton.Text = "Sleva (%)";
            this.discountButton.UseVisualStyleBackColor = false;
            this.discountButton.Click += new System.EventHandler(this.discountButton_Click);
            // 
            // cashButton
            // 
            this.cashButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.cashButton.FlatAppearance.BorderSize = 0;
            this.cashButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.cashButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.cashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cashButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cashButton.ForeColor = System.Drawing.Color.Black;
            this.cashButton.Location = new System.Drawing.Point(14, 540);
            this.cashButton.Margin = new System.Windows.Forms.Padding(4);
            this.cashButton.Name = "cashButton";
            this.cashButton.Size = new System.Drawing.Size(169, 126);
            this.cashButton.TabIndex = 9;
            this.cashButton.Text = "Hotovost";
            this.cashButton.UseVisualStyleBackColor = false;
            this.cashButton.Click += new System.EventHandler(this.CashButton_Click);
            // 
            // exactCashButton
            // 
            this.exactCashButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(129)))), ((int)(((byte)(0)))));
            this.exactCashButton.FlatAppearance.BorderSize = 0;
            this.exactCashButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.exactCashButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.exactCashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exactCashButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exactCashButton.ForeColor = System.Drawing.Color.Black;
            this.exactCashButton.Location = new System.Drawing.Point(191, 540);
            this.exactCashButton.Margin = new System.Windows.Forms.Padding(4);
            this.exactCashButton.Name = "exactCashButton";
            this.exactCashButton.Size = new System.Drawing.Size(169, 126);
            this.exactCashButton.TabIndex = 10;
            this.exactCashButton.Text = "Hotovost\r\npřesně\r\n";
            this.exactCashButton.UseVisualStyleBackColor = false;
            this.exactCashButton.Click += new System.EventHandler(this.ExactCashButton_Click);
            // 
            // sumLabel
            // 
            this.sumLabel.AutoSize = true;
            this.sumLabel.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sumLabel.ForeColor = System.Drawing.Color.White;
            this.sumLabel.Location = new System.Drawing.Point(12, 9);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(166, 26);
            this.sumLabel.TabIndex = 12;
            this.sumLabel.Text = "Celkem: xxx Kč";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.giftCardButton);
            this.panel2.Controls.Add(this.discountButton);
            this.panel2.Controls.Add(this.foodCardButton);
            this.panel2.Controls.Add(this.cardButton);
            this.panel2.Controls.Add(this.exactCashButton);
            this.panel2.Controls.Add(this.PluxeeButton);
            this.panel2.Controls.Add(this.cashButton);
            this.panel2.Controls.Add(this.ChequeButton);
            this.panel2.Controls.Add(this.button16);
            this.panel2.Location = new System.Drawing.Point(787, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(371, 677);
            this.panel2.TabIndex = 14;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(1165, 644);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(140, 99);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "X";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.Button22_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
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
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(13, 46);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(441, 688);
            this.listView1.TabIndex = 13;
            this.listView1.TileSize = new System.Drawing.Size(332, 40);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Produkt
            // 
            this.Produkt.DisplayIndex = 1;
            this.Produkt.Text = "Produkt";
            this.Produkt.Width = 270;
            // 
            // Cena
            // 
            this.Cena.DisplayIndex = 2;
            this.Cena.Tag = "Cena";
            this.Cena.Text = "Cena";
            this.Cena.Width = 105;
            // 
            // Mnozstvi
            // 
            this.Mnozstvi.DisplayIndex = 0;
            this.Mnozstvi.Text = "";
            this.Mnozstvi.Width = 45;
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(1318, 755);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.sumLabel);
            this.Controls.Add(this.keypadPanel);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1324, 761);
            this.Name = "PaymentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Platba";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.keypadPanel.ResumeLayout(false);
            this.keypadPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel keypadPanel;
        private System.Windows.Forms.Button kTwoButton;
        private System.Windows.Forms.Button kZeroButton;
        private System.Windows.Forms.Button kNineButton;
        private System.Windows.Forms.Button kEightButton;
        private System.Windows.Forms.Button kSevenButton;
        private System.Windows.Forms.Button kSixButton;
        private System.Windows.Forms.Button kFiveButton;
        private System.Windows.Forms.Button kFourButton;
        private System.Windows.Forms.Button kThreeButton;
        private System.Windows.Forms.Button kOneButton;
        private System.Windows.Forms.Button cardButton;
        private System.Windows.Forms.Button foodCardButton;
        private System.Windows.Forms.Button PluxeeButton;
        private System.Windows.Forms.Button ChequeButton;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button giftCardButton;
        private System.Windows.Forms.Button discountButton;
        private System.Windows.Forms.Button cashButton;
        private System.Windows.Forms.Button exactCashButton;
        private System.Windows.Forms.TextBox PayedTextBox;
        private System.Windows.Forms.Label sumLabel;
        private ListViewWithScrollBar listView1;
        private System.Windows.Forms.ColumnHeader Produkt;
        private System.Windows.Forms.ColumnHeader Cena;
        private System.Windows.Forms.ColumnHeader Mnozstvi;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button kRemoveButton;
        private System.Windows.Forms.Button kDualZeroButton;
        private System.Windows.Forms.Button cancelButton;
        private System.IO.Ports.SerialPort serialPort1;
    }
}