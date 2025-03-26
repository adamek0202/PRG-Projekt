namespace Pokladna
{
    partial class Keypad
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

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.kRemoveButton = new System.Windows.Forms.Button();
            this.kDualZeroButton = new System.Windows.Forms.Button();
            this.kZeroButton = new System.Windows.Forms.Button();
            this.kNineButton = new System.Windows.Forms.Button();
            this.kEightButton = new System.Windows.Forms.Button();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.kSevenButton = new System.Windows.Forms.Button();
            this.kSixButton = new System.Windows.Forms.Button();
            this.kFiveButton = new System.Windows.Forms.Button();
            this.kFourButton = new System.Windows.Forms.Button();
            this.kThreeButton = new System.Windows.Forms.Button();
            this.kTwoButton = new System.Windows.Forms.Button();
            this.kOneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kRemoveButton
            // 
            this.kRemoveButton.BackColor = System.Drawing.Color.Lime;
            this.kRemoveButton.FlatAppearance.BorderSize = 0;
            this.kRemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.kRemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.kRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kRemoveButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.kRemoveButton.Location = new System.Drawing.Point(200, 332);
            this.kRemoveButton.Margin = new System.Windows.Forms.Padding(4);
            this.kRemoveButton.Name = "kRemoveButton";
            this.kRemoveButton.Size = new System.Drawing.Size(90, 81);
            this.kRemoveButton.TabIndex = 4;
            this.kRemoveButton.Text = "OK";
            this.kRemoveButton.UseVisualStyleBackColor = false;
            this.kRemoveButton.Click += new System.EventHandler(this.kRemoveButton_Click);
            // 
            // kDualZeroButton
            // 
            this.kDualZeroButton.BackColor = System.Drawing.Color.Red;
            this.kDualZeroButton.FlatAppearance.BorderSize = 0;
            this.kDualZeroButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.kDualZeroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.kDualZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kDualZeroButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kDualZeroButton.ForeColor = System.Drawing.Color.White;
            this.kDualZeroButton.Location = new System.Drawing.Point(4, 332);
            this.kDualZeroButton.Margin = new System.Windows.Forms.Padding(4);
            this.kDualZeroButton.Name = "kDualZeroButton";
            this.kDualZeroButton.Size = new System.Drawing.Size(90, 81);
            this.kDualZeroButton.TabIndex = 4;
            this.kDualZeroButton.Tag = "00";
            this.kDualZeroButton.Text = "C";
            this.kDualZeroButton.UseVisualStyleBackColor = false;
            this.kDualZeroButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kZeroButton.Location = new System.Drawing.Point(102, 332);
            this.kZeroButton.Margin = new System.Windows.Forms.Padding(4);
            this.kZeroButton.Name = "kZeroButton";
            this.kZeroButton.Size = new System.Drawing.Size(90, 81);
            this.kZeroButton.TabIndex = 4;
            this.kZeroButton.Tag = "0";
            this.kZeroButton.Text = "0";
            this.kZeroButton.UseVisualStyleBackColor = false;
            this.kZeroButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kNineButton.Location = new System.Drawing.Point(200, 243);
            this.kNineButton.Margin = new System.Windows.Forms.Padding(4);
            this.kNineButton.Name = "kNineButton";
            this.kNineButton.Size = new System.Drawing.Size(90, 81);
            this.kNineButton.TabIndex = 4;
            this.kNineButton.Tag = "9";
            this.kNineButton.Text = "9";
            this.kNineButton.UseVisualStyleBackColor = false;
            this.kNineButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kEightButton.Location = new System.Drawing.Point(102, 243);
            this.kEightButton.Margin = new System.Windows.Forms.Padding(4);
            this.kEightButton.Name = "kEightButton";
            this.kEightButton.Size = new System.Drawing.Size(90, 81);
            this.kEightButton.TabIndex = 4;
            this.kEightButton.Tag = "8";
            this.kEightButton.Text = "8";
            this.kEightButton.UseVisualStyleBackColor = false;
            this.kEightButton.Click += new System.EventHandler(this.kOneButton_Click);
            // 
            // numberTextBox
            // 
            this.numberTextBox.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numberTextBox.Location = new System.Drawing.Point(3, 5);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(287, 44);
            this.numberTextBox.TabIndex = 11;
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
            this.kSevenButton.Location = new System.Drawing.Point(4, 243);
            this.kSevenButton.Margin = new System.Windows.Forms.Padding(4);
            this.kSevenButton.Name = "kSevenButton";
            this.kSevenButton.Size = new System.Drawing.Size(90, 81);
            this.kSevenButton.TabIndex = 4;
            this.kSevenButton.Tag = "7";
            this.kSevenButton.Text = "7";
            this.kSevenButton.UseVisualStyleBackColor = false;
            this.kSevenButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kSixButton.Location = new System.Drawing.Point(200, 154);
            this.kSixButton.Margin = new System.Windows.Forms.Padding(4);
            this.kSixButton.Name = "kSixButton";
            this.kSixButton.Size = new System.Drawing.Size(90, 81);
            this.kSixButton.TabIndex = 4;
            this.kSixButton.Tag = "6";
            this.kSixButton.Text = "6";
            this.kSixButton.UseVisualStyleBackColor = false;
            this.kSixButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kFiveButton.Location = new System.Drawing.Point(102, 154);
            this.kFiveButton.Margin = new System.Windows.Forms.Padding(4);
            this.kFiveButton.Name = "kFiveButton";
            this.kFiveButton.Size = new System.Drawing.Size(90, 81);
            this.kFiveButton.TabIndex = 4;
            this.kFiveButton.Tag = "5";
            this.kFiveButton.Text = "5";
            this.kFiveButton.UseVisualStyleBackColor = false;
            this.kFiveButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kFourButton.Location = new System.Drawing.Point(4, 154);
            this.kFourButton.Margin = new System.Windows.Forms.Padding(4);
            this.kFourButton.Name = "kFourButton";
            this.kFourButton.Size = new System.Drawing.Size(90, 81);
            this.kFourButton.TabIndex = 3;
            this.kFourButton.Tag = "4";
            this.kFourButton.Text = "4";
            this.kFourButton.UseVisualStyleBackColor = false;
            this.kFourButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kThreeButton.Location = new System.Drawing.Point(200, 65);
            this.kThreeButton.Margin = new System.Windows.Forms.Padding(4);
            this.kThreeButton.Name = "kThreeButton";
            this.kThreeButton.Size = new System.Drawing.Size(90, 81);
            this.kThreeButton.TabIndex = 2;
            this.kThreeButton.Tag = "3";
            this.kThreeButton.Text = "3";
            this.kThreeButton.UseVisualStyleBackColor = false;
            this.kThreeButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kTwoButton.Location = new System.Drawing.Point(102, 65);
            this.kTwoButton.Margin = new System.Windows.Forms.Padding(4);
            this.kTwoButton.Name = "kTwoButton";
            this.kTwoButton.Size = new System.Drawing.Size(90, 81);
            this.kTwoButton.TabIndex = 0;
            this.kTwoButton.Tag = "2";
            this.kTwoButton.Text = "2";
            this.kTwoButton.UseVisualStyleBackColor = false;
            this.kTwoButton.Click += new System.EventHandler(this.kOneButton_Click);
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
            this.kOneButton.Location = new System.Drawing.Point(4, 65);
            this.kOneButton.Margin = new System.Windows.Forms.Padding(4);
            this.kOneButton.Name = "kOneButton";
            this.kOneButton.Size = new System.Drawing.Size(90, 81);
            this.kOneButton.TabIndex = 1;
            this.kOneButton.Tag = "1";
            this.kOneButton.Text = "1";
            this.kOneButton.UseVisualStyleBackColor = false;
            this.kOneButton.Click += new System.EventHandler(this.kOneButton_Click);
            // 
            // Keypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.Controls.Add(this.kRemoveButton);
            this.Controls.Add(this.kDualZeroButton);
            this.Controls.Add(this.kOneButton);
            this.Controls.Add(this.kZeroButton);
            this.Controls.Add(this.kTwoButton);
            this.Controls.Add(this.kNineButton);
            this.Controls.Add(this.kThreeButton);
            this.Controls.Add(this.kEightButton);
            this.Controls.Add(this.kFourButton);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.kFiveButton);
            this.Controls.Add(this.kSevenButton);
            this.Controls.Add(this.kSixButton);
            this.Name = "Keypad";
            this.Size = new System.Drawing.Size(297, 420);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button kRemoveButton;
        private System.Windows.Forms.Button kDualZeroButton;
        private System.Windows.Forms.Button kZeroButton;
        private System.Windows.Forms.Button kNineButton;
        private System.Windows.Forms.Button kEightButton;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Button kSevenButton;
        private System.Windows.Forms.Button kSixButton;
        private System.Windows.Forms.Button kFiveButton;
        private System.Windows.Forms.Button kFourButton;
        private System.Windows.Forms.Button kThreeButton;
        private System.Windows.Forms.Button kTwoButton;
        private System.Windows.Forms.Button kOneButton;
    }
}
