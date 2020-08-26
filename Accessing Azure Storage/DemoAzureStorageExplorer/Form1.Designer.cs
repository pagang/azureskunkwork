namespace DemoAzureStorageExplorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReadLog = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.ListBox();
            this.btnWebApi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadLog
            // 
            this.btnReadLog.Location = new System.Drawing.Point(12, 12);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(144, 23);
            this.btnReadLog.TabIndex = 0;
            this.btnReadLog.Text = "Access log via dll";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.btnReadLog_Click);
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.FormattingEnabled = true;
            this.logBox.ItemHeight = 15;
            this.logBox.Location = new System.Drawing.Point(12, 41);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(776, 394);
            this.logBox.TabIndex = 1;
            // 
            // btnWebApi
            // 
            this.btnWebApi.Location = new System.Drawing.Point(178, 12);
            this.btnWebApi.Name = "btnWebApi";
            this.btnWebApi.Size = new System.Drawing.Size(154, 23);
            this.btnWebApi.TabIndex = 2;
            this.btnWebApi.Text = "Access log via web API";
            this.btnWebApi.UseVisualStyleBackColor = true;
            this.btnWebApi.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnWebApi);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.btnReadLog);
            this.Name = "Form1";
            this.Text = "Azure Table Storage Testbed";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.Button btnWebApi;
    }
}

