namespace Klient
{
    partial class KlientLogowanie
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.Login = new System.Windows.Forms.TextBox();
            this.hasło = new System.Windows.Forms.TextBox();
            this.zaloguj = new System.Windows.Forms.Button();
            this.Zarejestruj = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(125, 109);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(100, 20);
            this.Login.TabIndex = 0;
            this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged);
            // 
            // hasło
            // 
            this.hasło.Location = new System.Drawing.Point(384, 163);
            this.hasło.Name = "hasło";
            this.hasło.Size = new System.Drawing.Size(100, 20);
            this.hasło.TabIndex = 1;
            this.hasło.TextChanged += new System.EventHandler(this.hasło_TextChanged);
            // 
            // zaloguj
            // 
            this.zaloguj.Location = new System.Drawing.Point(630, 105);
            this.zaloguj.Name = "zaloguj";
            this.zaloguj.Size = new System.Drawing.Size(75, 23);
            this.zaloguj.TabIndex = 2;
            this.zaloguj.Text = "zaloguj";
            this.zaloguj.UseVisualStyleBackColor = true;
            this.zaloguj.Click += new System.EventHandler(this.Zaloguj_Click);
            // 
            // Zarejestruj
            // 
            this.Zarejestruj.Location = new System.Drawing.Point(630, 181);
            this.Zarejestruj.Name = "Zarejestruj";
            this.Zarejestruj.Size = new System.Drawing.Size(75, 23);
            this.Zarejestruj.TabIndex = 3;
            this.Zarejestruj.Text = "zarejestruj";
            this.Zarejestruj.UseVisualStyleBackColor = true;
            this.Zarejestruj.Click += new System.EventHandler(this.Zarejestruj_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "dalej";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // KlientLogowanie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Zarejestruj);
            this.Controls.Add(this.zaloguj);
            this.Controls.Add(this.hasło);
            this.Controls.Add(this.Login);
            this.Name = "KlientLogowanie";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.KlientLogowanie_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox hasło;
        private System.Windows.Forms.Button zaloguj;
        private System.Windows.Forms.Button Zarejestruj;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

