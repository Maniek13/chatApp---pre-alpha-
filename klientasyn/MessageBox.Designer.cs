
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
            this.ChatWindow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(533, 382);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextToSend
            // 
            this.TextToSend.Location = new System.Drawing.Point(51, 385);
            this.TextToSend.Name = "TextToSend";
            this.TextToSend.Size = new System.Drawing.Size(476, 20);
            this.TextToSend.TabIndex = 1;
            this.TextToSend.TextChanged += new System.EventHandler(this.TextToSend_TextChanged);
            // 
            // ChatWindow
            // 
            this.ChatWindow.Location = new System.Drawing.Point(51, 48);
            this.ChatWindow.Multiline = true;
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.Size = new System.Drawing.Size(557, 331);
            this.ChatWindow.TabIndex = 2;
            this.ChatWindow.TextChanged += new System.EventHandler(this.ChatWindow_TextChanged);
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 450);
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
        private System.Windows.Forms.TextBox ChatWindow;
    }
}