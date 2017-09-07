namespace voteApp
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
            this.lblLines = new System.Windows.Forms.Label();
            this.lblStringSend = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.PortName = "COM9";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(39, 158);
            this.textBoxInput.MaxLength = 40;
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(504, 165);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(574, 158);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(212, 65);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Skicka!";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(98, 49);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(445, 33);
            this.comboBoxPorts.TabIndex = 2;
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Location = new System.Drawing.Point(473, 359);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(70, 25);
            this.lblLines.TabIndex = 3;
            this.lblLines.Text = "label1";
            // 
            // lblStringSend
            // 
            this.lblStringSend.AutoSize = true;
            this.lblStringSend.Location = new System.Drawing.Point(34, 347);
            this.lblStringSend.Name = "lblStringSend";
            this.lblStringSend.Size = new System.Drawing.Size(70, 25);
            this.lblStringSend.TabIndex = 4;
            this.lblStringSend.Text = "label1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(574, 256);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(212, 67);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Rensa";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ange fråga:";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(898, 158);
            this.textBoxData.Multiline = true;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(368, 515);
            this.textBoxData.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(893, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 835);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblStringSend);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBoxInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "voteMachine App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label lblStringSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.Label label3;
    }
}

