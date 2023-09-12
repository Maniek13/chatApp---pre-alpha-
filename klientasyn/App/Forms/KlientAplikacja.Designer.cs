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
            this.MessageField = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.MessagesBox = new System.Windows.Forms.TextBox();
            this.ContactsBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // MessageField
            // 
            this.MessageField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MessageField.Location = new System.Drawing.Point(2, 654);
            this.MessageField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MessageField.Name = "MessageField";
            this.MessageField.Size = new System.Drawing.Size(751, 23);
            this.MessageField.TabIndex = 3;
            // 
            // SendBtn
            // 
            this.SendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SendBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SendBtn.FlatAppearance.BorderSize = 0;
            this.SendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SendBtn.Location = new System.Drawing.Point(761, 641);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(117, 37);
            this.SendBtn.TabIndex = 4;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = false;
            this.SendBtn.Click += new System.EventHandler(this.SendMsg_BtnClick);
            // 
            // MessagesBox
            // 
            this.MessagesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagesBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MessagesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessagesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MessagesBox.ForeColor = System.Drawing.Color.Black;
            this.MessagesBox.Location = new System.Drawing.Point(2, 2);
            this.MessagesBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MessagesBox.MaxLength = 0;
            this.MessagesBox.Multiline = true;
            this.MessagesBox.Name = "MessagesBox";
            this.MessagesBox.ReadOnly = true;
            this.MessagesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessagesBox.Size = new System.Drawing.Size(876, 629);
            this.MessagesBox.TabIndex = 7;
            // 
            // ContactsBox
            // 
            this.ContactsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContactsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ContactsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ContactsBox.FormattingEnabled = true;
            this.ContactsBox.ItemHeight = 25;
            this.ContactsBox.Location = new System.Drawing.Point(904, 2);
            this.ContactsBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ContactsBox.Name = "ContactsBox";
            this.ContactsBox.Size = new System.Drawing.Size(307, 675);
            this.ContactsBox.Sorted = true;
            this.ContactsBox.TabIndex = 9;
            this.ContactsBox.SelectedIndexChanged += new System.EventHandler(this.Contacts_SelectedIndexChanged);
            this.ContactsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Contacts_MouseDoubleClick);
            // 
            // KlientAplikacja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1230, 692);
            this.Controls.Add(this.ContactsBox);
            this.Controls.Add(this.MessagesBox);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.MessageField);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KlientAplikacja";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox MessageField;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.ListBox ContactsBox;
        public System.Windows.Forms.TextBox MessagesBox;
    }
}