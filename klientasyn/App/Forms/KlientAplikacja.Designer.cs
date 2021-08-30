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
            this.Dodaj = new System.Windows.Forms.Button();
            this.Kontakty = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Wiadomosc
            // 
            this.Wiadomosc.Location = new System.Drawing.Point(1, 343);
            this.Wiadomosc.Multiline = true;
            this.Wiadomosc.Name = "Wiadomosc";
            this.Wiadomosc.Size = new System.Drawing.Size(548, 29);
            this.Wiadomosc.TabIndex = 3;
            this.Wiadomosc.TextChanged += new System.EventHandler(this.Wiadomosc_TextChanged);
            // 
            // Wyślij
            // 
            this.Wyślij.Location = new System.Drawing.Point(555, 343);
            this.Wyślij.Name = "Wyślij";
            this.Wyślij.Size = new System.Drawing.Size(78, 29);
            this.Wyślij.TabIndex = 4;
            this.Wyślij.Text = "Wyślij";
            this.Wyślij.UseVisualStyleBackColor = true;
            this.Wyślij.Click += new System.EventHandler(this.Wyślij_Click);
            // 
            // Komunikaty
            // 
            this.Komunikaty.Location = new System.Drawing.Point(1, 1);
            this.Komunikaty.Multiline = true;
            this.Komunikaty.Name = "Komunikaty";
            this.Komunikaty.ReadOnly = true;
            this.Komunikaty.Size = new System.Drawing.Size(632, 336);
            this.Komunikaty.TabIndex = 7;
            this.Komunikaty.TextChanged += new System.EventHandler(this.Komunikaty_TextChanged);
            // 
            // Dodaj
            // 
            this.Dodaj.Location = new System.Drawing.Point(654, 343);
            this.Dodaj.Name = "Dodaj";
            this.Dodaj.Size = new System.Drawing.Size(146, 29);
            this.Dodaj.TabIndex = 8;
            this.Dodaj.Text = "Dodaj użytkownika";
            this.Dodaj.UseVisualStyleBackColor = true;
            this.Dodaj.Click += new System.EventHandler(this.Dodaj_Click);
            // 
            // Kontakty
            // 
            this.Kontakty.FormattingEnabled = true;
            this.Kontakty.Location = new System.Drawing.Point(654, 8);
            this.Kontakty.Name = "Kontakty";
            this.Kontakty.Size = new System.Drawing.Size(146, 329);
            this.Kontakty.TabIndex = 9;
            this.Kontakty.SelectedIndexChanged += new System.EventHandler(this.Kontakty_SelectedIndexChanged_1);
            this.Kontakty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Kontakty_MouseDoubleClick);
            // 
            // KlientAplikacja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Kontakty);
            this.Controls.Add(this.Dodaj);
            this.Controls.Add(this.Komunikaty);
            this.Controls.Add(this.Wyślij);
            this.Controls.Add(this.Wiadomosc);
            this.Name = "KlientAplikacja";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Wiadomosc;
        private System.Windows.Forms.Button Wyślij;
        private System.Windows.Forms.Button Dodaj;
        private System.Windows.Forms.ListBox Kontakty;
        public System.Windows.Forms.TextBox Komunikaty;
    }
}