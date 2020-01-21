namespace ModbusTest
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.comboBoxDev1_PortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.comboBoxDecodeType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHis = new System.Windows.Forms.Button();
            this.comboBox_BaudRate = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            this.statusStrip1.Size = new System.Drawing.Size(502, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // comboBoxDev1_PortName
            // 
            this.comboBoxDev1_PortName.FormattingEnabled = true;
            this.comboBoxDev1_PortName.Location = new System.Drawing.Point(98, 11);
            this.comboBoxDev1_PortName.Margin = new System.Windows.Forms.Padding(5);
            this.comboBoxDev1_PortName.Name = "comboBoxDev1_PortName";
            this.comboBoxDev1_PortName.Size = new System.Drawing.Size(164, 27);
            this.comboBoxDev1_PortName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "端口号";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(98, 126);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(5);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(76, 35);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "打开";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(186, 126);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(5);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(76, 35);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(26, 209);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(164, 29);
            this.textBoxValue.TabIndex = 13;
            this.textBoxValue.Text = "115200";
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(200, 209);
            this.buttonRead.Margin = new System.Windows.Forms.Padding(5);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(76, 35);
            this.buttonRead.TabIndex = 12;
            this.buttonRead.Text = "读取";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Location = new System.Drawing.Point(286, 209);
            this.buttonWrite.Margin = new System.Windows.Forms.Padding(5);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(76, 35);
            this.buttonWrite.TabIndex = 14;
            this.buttonWrite.Text = "设置";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // comboBoxDecodeType
            // 
            this.comboBoxDecodeType.AutoCompleteCustomSource.AddRange(new string[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxDecodeType.FormattingEnabled = true;
            this.comboBoxDecodeType.Items.AddRange(new object[] {
            "0:big-endian",
            "1:little-endian",
            "2:big-endian byte swap",
            "3:little-endian byte swap"});
            this.comboBoxDecodeType.Location = new System.Drawing.Point(98, 87);
            this.comboBoxDecodeType.Margin = new System.Windows.Forms.Padding(5);
            this.comboBoxDecodeType.Name = "comboBoxDecodeType";
            this.comboBoxDecodeType.Size = new System.Drawing.Size(287, 27);
            this.comboBoxDecodeType.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "编码";
            // 
            // buttonHis
            // 
            this.buttonHis.Location = new System.Drawing.Point(395, 209);
            this.buttonHis.Margin = new System.Windows.Forms.Padding(5);
            this.buttonHis.Name = "buttonHis";
            this.buttonHis.Size = new System.Drawing.Size(93, 35);
            this.buttonHis.TabIndex = 17;
            this.buttonHis.Text = "通信历史";
            this.buttonHis.UseVisualStyleBackColor = true;
            this.buttonHis.Click += new System.EventHandler(this.buttonHis_Click);
            // 
            // comboBox_BaudRate
            // 
            this.comboBox_BaudRate.FormattingEnabled = true;
            this.comboBox_BaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBox_BaudRate.Location = new System.Drawing.Point(98, 48);
            this.comboBox_BaudRate.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox_BaudRate.Name = "comboBox_BaudRate";
            this.comboBox_BaudRate.Size = new System.Drawing.Size(164, 27);
            this.comboBox_BaudRate.TabIndex = 18;
            this.comboBox_BaudRate.Text = "9600";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 345);
            this.Controls.Add(this.comboBox_BaudRate);
            this.Controls.Add(this.buttonHis);
            this.Controls.Add(this.comboBoxDecodeType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.comboBoxDev1_PortName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModbusTest";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ComboBox comboBoxDev1_PortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.ComboBox comboBoxDecodeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHis;
        private System.Windows.Forms.ComboBox comboBox_BaudRate;
    }
}

