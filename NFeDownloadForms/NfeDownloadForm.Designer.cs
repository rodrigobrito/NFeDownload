namespace NFeDownloadForms
{
    partial class NfeDownloadForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nfeTextBox = new System.Windows.Forms.TextBox();
            this.captchaPictureBox = new System.Windows.Forms.PictureBox();
            this.captchaTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N.º NF-e:";
            // 
            // nfeTextBox
            // 
            this.nfeTextBox.Location = new System.Drawing.Point(210, 25);
            this.nfeTextBox.Name = "nfeTextBox";
            this.nfeTextBox.Size = new System.Drawing.Size(276, 20);
            this.nfeTextBox.TabIndex = 1;
            this.nfeTextBox.Text = "35140904736972000280550010000623341920165384";
            // 
            // captchaPictureBox
            // 
            this.captchaPictureBox.BackColor = System.Drawing.Color.White;
            this.captchaPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.captchaPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.captchaPictureBox.Location = new System.Drawing.Point(12, 8);
            this.captchaPictureBox.Name = "captchaPictureBox";
            this.captchaPictureBox.Size = new System.Drawing.Size(192, 120);
            this.captchaPictureBox.TabIndex = 2;
            this.captchaPictureBox.TabStop = false;
            // 
            // captchaTextBox
            // 
            this.captchaTextBox.Location = new System.Drawing.Point(210, 70);
            this.captchaTextBox.Name = "captchaTextBox";
            this.captchaTextBox.Size = new System.Drawing.Size(276, 20);
            this.captchaTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Captcha:";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(411, 105);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Enviar";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButtonOnClick);
            // 
            // NfeDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 140);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.captchaTextBox);
            this.Controls.Add(this.captchaPictureBox);
            this.Controls.Add(this.nfeTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NfeDownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NF-e Download";
            this.Load += new System.EventHandler(this.NfeDownloadForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.captchaPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nfeTextBox;
        private System.Windows.Forms.PictureBox captchaPictureBox;
        private System.Windows.Forms.TextBox captchaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendButton;
    }
}

