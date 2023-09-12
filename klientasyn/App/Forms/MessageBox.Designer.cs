
namespace Klient
{
    partial class MessageBox
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
            this.SendBtn = new System.Windows.Forms.Button();
            this.TextToSendField = new System.Windows.Forms.TextBox();
            this.ContactName = new System.Windows.Forms.Label();
            this.ChatWindow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SendBtn.FlatAppearance.BorderSize = 0;
            this.SendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SendBtn.Location = new System.Drawing.Point(800, 592);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(0);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(112, 43);
            this.SendBtn.TabIndex = 0;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = false;
            this.SendBtn.Click += new System.EventHandler(this.SendMessage_BtnClick);
            // 
            // TextToSendField
            // 
            this.TextToSendField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextToSendField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextToSendField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TextToSendField.Location = new System.Drawing.Point(76, 612);
            this.TextToSendField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextToSendField.Name = "TextToSendField";
            this.TextToSendField.Size = new System.Drawing.Size(714, 23);
            this.TextToSendField.TabIndex = 1;
            // 
            // ContactName
            // 
            this.ContactName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ContactName.Location = new System.Drawing.Point(76, 14);
            this.ContactName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ContactName.Name = "ContactName";
            this.ContactName.Size = new System.Drawing.Size(836, 35);
            this.ContactName.TabIndex = 3;
            this.ContactName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChatWindow
            // 
            this.ChatWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatWindow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ChatWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ChatWindow.ForeColor = System.Drawing.Color.Black;
            this.ChatWindow.Location = new System.Drawing.Point(76, 52);
            this.ChatWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChatWindow.MaxLength = 0;
            this.ChatWindow.Multiline = true;
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.ReadOnly = true;
            this.ChatWindow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ChatWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatWindow.Size = new System.Drawing.Size(836, 531);
            this.ChatWindow.TabIndex = 2;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(994, 692);
            this.Controls.Add(this.ContactName);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.TextToSendField);
            this.Controls.Add(this.SendBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MessageBox";
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.MessageBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.TextBox TextToSendField;
        private System.Windows.Forms.Label ContactName;
        private System.Windows.Forms.TextBox ChatWindow;
    }
}