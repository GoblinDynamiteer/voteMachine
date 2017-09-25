namespace voteApp
{
    partial class WebForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebForm));
            this.tbMac = new System.Windows.Forms.TextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.btnGetDevices = new System.Windows.Forms.Button();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.lblWebLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(501, 493);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(233, 31);
            this.tbMac.TabIndex = 7;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(501, 448);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(233, 31);
            this.tbIp.TabIndex = 6;
            // 
            // btnGetDevices
            // 
            this.btnGetDevices.Location = new System.Drawing.Point(36, 448);
            this.btnGetDevices.Name = "btnGetDevices";
            this.btnGetDevices.Size = new System.Drawing.Size(233, 76);
            this.btnGetDevices.TabIndex = 5;
            this.btnGetDevices.Text = "Get Devices";
            this.btnGetDevices.UseVisualStyleBackColor = true;
            this.btnGetDevices.Click += new System.EventHandler(this.btnGetDevices_Click);
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.ItemHeight = 25;
            this.listBoxDevices.Location = new System.Drawing.Point(36, 24);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(698, 404);
            this.listBoxDevices.TabIndex = 4;
            this.listBoxDevices.SelectedIndexChanged += new System.EventHandler(this.listBoxDevices_SelectedIndexChanged);
            // 
            // lblWebLink
            // 
            this.lblWebLink.AutoSize = true;
            this.lblWebLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebLink.Location = new System.Drawing.Point(31, 558);
            this.lblWebLink.Name = "lblWebLink";
            this.lblWebLink.Size = new System.Drawing.Size(130, 51);
            this.lblWebLink.TabIndex = 9;
            this.lblWebLink.TabStop = true;
            this.lblWebLink.Text = "http://";
            this.lblWebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblWebLink_LinkClicked);
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 663);
            this.Controls.Add(this.lblWebLink);
            this.Controls.Add(this.tbMac);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.btnGetDevices);
            this.Controls.Add(this.listBoxDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WebForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMac;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Button btnGetDevices;
        private System.Windows.Forms.ListBox listBoxDevices;
        private System.Windows.Forms.LinkLabel lblWebLink;
    }
}