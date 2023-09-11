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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LoginField = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Login.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Login.Location = new System.Drawing.Point(295, 142);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(100, 15);
            this.Login.TabIndex = 0;
            this.Login.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hasło
            // 
            this.hasło.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hasło.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.hasło.Location = new System.Drawing.Point(404, 142);
            this.hasło.Name = "hasło";
            this.hasło.Size = new System.Drawing.Size(100, 15);
            this.hasło.TabIndex = 1;
            this.hasło.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hasło.UseSystemPasswordChar = true;
            // 
            // zaloguj
            // 
            this.zaloguj.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.zaloguj.FlatAppearance.BorderSize = 0;
            this.zaloguj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zaloguj.Location = new System.Drawing.Point(361, 163);
            this.zaloguj.Name = "zaloguj";
            this.zaloguj.Size = new System.Drawing.Size(75, 23);
            this.zaloguj.TabIndex = 2;
            this.zaloguj.Text = "zaloguj";
            this.zaloguj.UseVisualStyleBackColor = false;
            this.zaloguj.Click += new System.EventHandler(this.Zaloguj_Click);
            // 
            // Zarejestruj
            // 
            this.Zarejestruj.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Zarejestruj.FlatAppearance.BorderSize = 0;
            this.Zarejestruj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Zarejestruj.Location = new System.Drawing.Point(361, 192);
            this.Zarejestruj.Name = "Zarejestruj";
            this.Zarejestruj.Size = new System.Drawing.Size(75, 23);
            this.Zarejestruj.TabIndex = 3;
            this.Zarejestruj.Text = "zarejestruj";
            this.Zarejestruj.UseVisualStyleBackColor = false;
            this.Zarejestruj.Click += new System.EventHandler(this.Zarejestruj_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(12, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 15);
            this.textBox1.TabIndex = 5;
            // 
            // LoginField
            // 
            this.LoginField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginField.Location = new System.Drawing.Point(292, 126);
            this.LoginField.Name = "LoginField";
            this.LoginField.Size = new System.Drawing.Size(103, 13);
            this.LoginField.TabIndex = 6;
            this.LoginField.Text = "Login";
            this.LoginField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(404, 126);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(100, 13);
            this.PasswordLabel.TabIndex = 7;
            this.PasswordLabel.Text = "Password";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KlientLogowanie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginField);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Zarejestruj);
            this.Controls.Add(this.zaloguj);
            this.Controls.Add(this.hasło);
            this.Controls.Add(this.Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "KlientLogowanie";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox hasło;
        private System.Windows.Forms.Button zaloguj;
        private System.Windows.Forms.Button Zarejestruj;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LoginField;
        private System.Windows.Forms.Label PasswordLabel;
    }
}

