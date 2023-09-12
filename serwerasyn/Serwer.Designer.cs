namespace serwer
{
    partial class Serwer
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
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StartBtn.FlatAppearance.BorderSize = 0;
            this.StartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartBtn.Location = new System.Drawing.Point(330, 187);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(112, 32);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.Start_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StopBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopBtn.Location = new System.Drawing.Point(728, 187);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(112, 32);
            this.StopBtn.TabIndex = 1;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.Stop_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusBox.Location = new System.Drawing.Point(450, 187);
            this.StatusBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(270, 26);
            this.StatusBox.TabIndex = 2;
            // 
            // Serwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Serwer";
            this.Text = "Stop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.TextBox StatusBox;
    }
}

