
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
            this.Send = new System.Windows.Forms.Button();
            this.TextToSend = new System.Windows.Forms.TextBox();
            this.ContactName = new System.Windows.Forms.Label();
            this.ChatWindow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Send
            // 
            this.Send.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Send.FlatAppearance.BorderSize = 0;
            this.Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Send.Location = new System.Drawing.Point(533, 385);
            this.Send.Margin = new System.Windows.Forms.Padding(0);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 28);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = false;
            this.Send.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextToSend
            // 
            this.TextToSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextToSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TextToSend.Location = new System.Drawing.Point(51, 398);
            this.TextToSend.Name = "TextToSend";
            this.TextToSend.Size = new System.Drawing.Size(476, 15);
            this.TextToSend.TabIndex = 1;
            this.TextToSend.TextChanged += new System.EventHandler(this.TextToSend_TextChanged);
            // 
            // ContactName
            // 
            this.ContactName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ContactName.Location = new System.Drawing.Point(51, 9);
            this.ContactName.Name = "ContactName";
            this.ContactName.Size = new System.Drawing.Size(557, 23);
            this.ContactName.TabIndex = 3;
            this.ContactName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContactName.Click += new System.EventHandler(this.ContactName_Click);
            // 
            // ChatWindow
            // 
            this.ChatWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ChatWindow.Location = new System.Drawing.Point(51, 34);
            this.ChatWindow.Multiline = true;
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.ReadOnly = true;
            this.ChatWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatWindow.Size = new System.Drawing.Size(557, 345);
            this.ChatWindow.TabIndex = 2;
            this.ChatWindow.TextChanged += new System.EventHandler(this.ChatWindow_TextChanged);
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(663, 450);
            this.Controls.Add(this.ContactName);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.TextToSend);
            this.Controls.Add(this.Send);
            this.Name = "MessageBox";
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.MessageBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox TextToSend;
        private System.Windows.Forms.Label ContactName;
        private System.Windows.Forms.TextBox ChatWindow;
    }
}