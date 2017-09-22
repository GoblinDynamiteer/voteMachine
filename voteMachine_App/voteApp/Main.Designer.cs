﻿namespace voteApp
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStatus = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblRedVotes = new System.Windows.Forms.Label();
            this.lblGreenVotes = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblVoteOptGreen = new System.Windows.Forms.Label();
            this.lblVoteOptRed = new System.Windows.Forms.Label();
            this.textBoxGreenOpt = new System.Windows.Forms.TextBox();
            this.textBoxRedOpt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblComOpen = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.PortName = "COM7";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(20, 91);
            this.textBoxInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxInput.MaxLength = 200;
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(139, 61);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(182, 91);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Skicka!";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(49, 25);
            this.comboBoxPorts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(110, 21);
            this.comboBoxPorts.TabIndex = 2;
            this.comboBoxPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxPorts_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(182, 129);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Rensa";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fråga:";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(21, 247);
            this.textBoxData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxData.Multiline = true;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.ReadOnly = true;
            this.textBoxData.Size = new System.Drawing.Size(236, 139);
            this.textBoxData.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 227);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status";
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(262, 217);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(27, 23);
            this.btnStatus.TabIndex = 10;
            this.btnStatus.Text = "?";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::voteApp.Properties.Resources.vote_circle_red;
            this.pictureBox1.Location = new System.Drawing.Point(420, 137);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::voteApp.Properties.Resources.vote_circle_green;
            this.pictureBox2.Location = new System.Drawing.Point(314, 137);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 104);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // lblRedVotes
            // 
            this.lblRedVotes.AutoSize = true;
            this.lblRedVotes.BackColor = System.Drawing.Color.Red;
            this.lblRedVotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedVotes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblRedVotes.Location = new System.Drawing.Point(453, 174);
            this.lblRedVotes.Name = "lblRedVotes";
            this.lblRedVotes.Size = new System.Drawing.Size(32, 33);
            this.lblRedVotes.TabIndex = 14;
            this.lblRedVotes.Text = "0";
            this.lblRedVotes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenVotes
            // 
            this.lblGreenVotes.AutoSize = true;
            this.lblGreenVotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblGreenVotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblGreenVotes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGreenVotes.Location = new System.Drawing.Point(346, 174);
            this.lblGreenVotes.Name = "lblGreenVotes";
            this.lblGreenVotes.Size = new System.Drawing.Size(32, 33);
            this.lblGreenVotes.TabIndex = 13;
            this.lblGreenVotes.Text = "0";
            this.lblGreenVotes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(309, 25);
            this.lblQuestion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(71, 78);
            this.lblQuestion.TabIndex = 18;
            this.lblQuestion.Text = "RAD1\r\nRAD2\r\nRAD3";
            // 
            // lblVoteOptGreen
            // 
            this.lblVoteOptGreen.AutoSize = true;
            this.lblVoteOptGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoteOptGreen.Location = new System.Drawing.Point(311, 114);
            this.lblVoteOptGreen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVoteOptGreen.Name = "lblVoteOptGreen";
            this.lblVoteOptGreen.Size = new System.Drawing.Size(49, 18);
            this.lblVoteOptGreen.TabIndex = 19;
            this.lblVoteOptGreen.Text = "Green";
            // 
            // lblVoteOptRed
            // 
            this.lblVoteOptRed.AutoSize = true;
            this.lblVoteOptRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoteOptRed.Location = new System.Drawing.Point(420, 114);
            this.lblVoteOptRed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVoteOptRed.Name = "lblVoteOptRed";
            this.lblVoteOptRed.Size = new System.Drawing.Size(39, 20);
            this.lblVoteOptRed.TabIndex = 20;
            this.lblVoteOptRed.Text = "Red";
            // 
            // textBoxGreenOpt
            // 
            this.textBoxGreenOpt.Location = new System.Drawing.Point(20, 178);
            this.textBoxGreenOpt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxGreenOpt.MaxLength = 8;
            this.textBoxGreenOpt.Name = "textBoxGreenOpt";
            this.textBoxGreenOpt.Size = new System.Drawing.Size(59, 20);
            this.textBoxGreenOpt.TabIndex = 21;
            this.textBoxGreenOpt.TextChanged += new System.EventHandler(this.textBoxGreenOpt_TextChanged);
            // 
            // textBoxRedOpt
            // 
            this.textBoxRedOpt.Location = new System.Drawing.Point(100, 178);
            this.textBoxRedOpt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxRedOpt.MaxLength = 8;
            this.textBoxRedOpt.Name = "textBoxRedOpt";
            this.textBoxRedOpt.Size = new System.Drawing.Size(59, 20);
            this.textBoxRedOpt.TabIndex = 22;
            this.textBoxRedOpt.TextChanged += new System.EventHandler(this.textBoxRedOpt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Grönt val";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Rött val";
            // 
            // lblComOpen
            // 
            this.lblComOpen.Location = new System.Drawing.Point(182, 25);
            this.lblComOpen.Name = "lblComOpen";
            this.lblComOpen.Size = new System.Drawing.Size(75, 21);
            this.lblComOpen.TabIndex = 25;
            this.lblComOpen.Text = "Öppna";
            this.lblComOpen.UseVisualStyleBackColor = true;
            // 
            // btnWeb
            // 
            this.btnWeb.Location = new System.Drawing.Point(182, 217);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(75, 23);
            this.btnWeb.TabIndex = 26;
            this.btnWeb.Text = "Web";
            this.btnWeb.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 428);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.lblComOpen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRedOpt);
            this.Controls.Add(this.textBoxGreenOpt);
            this.Controls.Add(this.lblVoteOptRed);
            this.Controls.Add(this.lblVoteOptGreen);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblGreenVotes);
            this.Controls.Add(this.lblRedVotes);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBoxInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMain";
            this.Text = "voteMachine App";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblRedVotes;
        private System.Windows.Forms.Label lblGreenVotes;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblVoteOptGreen;
        private System.Windows.Forms.Label lblVoteOptRed;
        private System.Windows.Forms.TextBox textBoxGreenOpt;
        private System.Windows.Forms.TextBox textBoxRedOpt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button lblComOpen;
        private System.Windows.Forms.Button btnWeb;
    }
}

