namespace Klient
{
    partial class KlientAplikacja
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
            this.Wiadomosc = new System.Windows.Forms.TextBox();
            this.Wyślij = new System.Windows.Forms.Button();
            this.Komunikaty = new System.Windows.Forms.TextBox();
            this.Kontakty = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Wiadomosc
            // 
            this.Wiadomosc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Wiadomosc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Wiadomosc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Wiadomosc.Location = new System.Drawing.Point(2, 654);
            this.Wiadomosc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Wiadomosc.Name = "Wiadomosc";
            this.Wiadomosc.Size = new System.Drawing.Size(822, 23);
            this.Wiadomosc.TabIndex = 3;
            // 
            // Wyślij
            // 
            this.Wyślij.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Wyślij.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Wyślij.FlatAppearance.BorderSize = 0;
            this.Wyślij.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Wyślij.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Wyślij.Location = new System.Drawing.Point(833, 641);
            this.Wyślij.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Wyślij.Name = "Wyślij";
            this.Wyślij.Size = new System.Drawing.Size(117, 37);
            this.Wyślij.TabIndex = 4;
            this.Wyślij.Text = "Send";
            this.Wyślij.UseVisualStyleBackColor = false;
            this.Wyślij.Click += new System.EventHandler(this.Wyślij_Click);
            // 
            // Komunikaty
            // 
            this.Komunikaty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Komunikaty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Komunikaty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Komunikaty.ForeColor = System.Drawing.Color.Black;
            this.Komunikaty.Location = new System.Drawing.Point(2, 2);
            this.Komunikaty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Komunikaty.MaxLength = 0;
            this.Komunikaty.Multiline = true;
            this.Komunikaty.Name = "Komunikaty";
            this.Komunikaty.ReadOnly = true;
            this.Komunikaty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Komunikaty.Size = new System.Drawing.Size(948, 629);
            this.Komunikaty.TabIndex = 7;
            // 
            // Kontakty
            // 
            this.Kontakty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Kontakty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Kontakty.FormattingEnabled = true;
            this.Kontakty.ItemHeight = 25;
            this.Kontakty.Location = new System.Drawing.Point(980, 2);
            this.Kontakty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kontakty.Name = "Kontakty";
            this.Kontakty.Size = new System.Drawing.Size(389, 675);
            this.Kontakty.Sorted = true;
            this.Kontakty.TabIndex = 9;
            this.Kontakty.SelectedIndexChanged += new System.EventHandler(this.Kontakty_SelectedIndexChanged_1);
            this.Kontakty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Kontakty_MouseDoubleClick);
            // 
            // KlientAplikacja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1382, 692);
            this.Controls.Add(this.Kontakty);
            this.Controls.Add(this.Komunikaty);
            this.Controls.Add(this.Wyślij);
            this.Controls.Add(this.Wiadomosc);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KlientAplikacja";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Wiadomosc;
        private System.Windows.Forms.Button Wyślij;
        private System.Windows.Forms.ListBox Kontakty;
        public System.Windows.Forms.TextBox Komunikaty;
    }
}