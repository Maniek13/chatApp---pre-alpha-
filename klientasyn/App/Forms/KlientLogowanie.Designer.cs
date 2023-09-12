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
            this.LoginField = new System.Windows.Forms.TextBox();
            this.PasswordField = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginField
            // 
            this.LoginField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoginField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoginField.Location = new System.Drawing.Point(442, 218);
            this.LoginField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LoginField.Name = "LoginField";
            this.LoginField.Size = new System.Drawing.Size(150, 23);
            this.LoginField.TabIndex = 0;
            this.LoginField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PasswordField
            // 
            this.PasswordField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PasswordField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PasswordField.Location = new System.Drawing.Point(606, 218);
            this.PasswordField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PasswordField.Name = "PasswordField";
            this.PasswordField.Size = new System.Drawing.Size(150, 23);
            this.PasswordField.TabIndex = 1;
            this.PasswordField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PasswordField.UseSystemPasswordChar = true;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LoginBtn.FlatAppearance.BorderSize = 0;
            this.LoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginBtn.Location = new System.Drawing.Point(542, 251);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(112, 35);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "log in";
            this.LoginBtn.UseVisualStyleBackColor = false;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RegisterBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RegisterBtn.FlatAppearance.BorderSize = 0;
            this.RegisterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterBtn.Location = new System.Drawing.Point(542, 295);
            this.RegisterBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(112, 35);
            this.RegisterBtn.TabIndex = 3;
            this.RegisterBtn.Text = "register";
            this.RegisterBtn.UseVisualStyleBackColor = false;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.StatusBox.Location = new System.Drawing.Point(18, 643);
            this.StatusBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(1164, 23);
            this.StatusBox.TabIndex = 5;
            // 
            // LoginLabel
            // 
            this.LoginLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.Location = new System.Drawing.Point(438, 194);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(154, 20);
            this.LoginLabel.TabIndex = 6;
            this.LoginLabel.Text = "Login";
            this.LoginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PasswordLabel.Location = new System.Drawing.Point(606, 194);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(150, 20);
            this.PasswordLabel.TabIndex = 7;
            this.PasswordLabel.Text = "Password";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KlientLogowanie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.PasswordField);
            this.Controls.Add(this.LoginField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KlientLogowanie";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LoginField;
        private System.Windows.Forms.TextBox PasswordField;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
    }
}

