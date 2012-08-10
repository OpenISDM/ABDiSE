namespace WindowsFormsApplication1
{
    partial class MainWindow
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.SimulationTimer = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.listBoxAgentType = new System.Windows.Forms.ListBox();
            this.textBox_K01 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_V01 = new System.Windows.Forms.TextBox();
            this.textBox_V02 = new System.Windows.Forms.TextBox();
            this.textBox_K02 = new System.Windows.Forms.TextBox();
            this.textBox_V03 = new System.Windows.Forms.TextBox();
            this.textBox_K03 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_C0X = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_C0Y = new System.Windows.Forms.TextBox();
            this.textBox_C1Y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_C1X = new System.Windows.Forms.TextBox();
            this.textBox_C2Y = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_C2X = new System.Windows.Forms.TextBox();
            this.textBox_C3Y = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_C3X = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.listBoxAgentList = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelAgentProperties = new System.Windows.Forms.Label();
            this.listBoxConsole = new System.Windows.Forms.ListBox();
            this.textBox_V04 = new System.Windows.Forms.TextBox();
            this.textBox_K04 = new System.Windows.Forms.TextBox();
            this.textBox_V05 = new System.Windows.Forms.TextBox();
            this.textBox_K05 = new System.Windows.Forms.TextBox();
            this.textBox_V06 = new System.Windows.Forms.TextBox();
            this.textBox_K06 = new System.Windows.Forms.TextBox();
            this.listBoxAgentControl = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.listBoxJoinedAgentList = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gMapExplorer = new GMap.NET.WindowsForms.GMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCreate.Location = new System.Drawing.Point(135, 390);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(81, 101);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Create New Agent";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(240, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 400);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStart.Location = new System.Drawing.Point(858, 338);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(118, 45);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start Simulate";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonPause.Location = new System.Drawing.Point(858, 385);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(118, 29);
            this.buttonPause.TabIndex = 4;
            this.buttonPause.Text = "Pause Simulate";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // SimulationTimer
            // 
            this.SimulationTimer.Interval = 1000;
            this.SimulationTimer.Tick += new System.EventHandler(this.SimulationTimer_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(858, 303);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(118, 29);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // listBoxAgentType
            // 
            this.listBoxAgentType.FormattingEnabled = true;
            this.listBoxAgentType.ItemHeight = 12;
            this.listBoxAgentType.Location = new System.Drawing.Point(12, 60);
            this.listBoxAgentType.Name = "listBoxAgentType";
            this.listBoxAgentType.Size = new System.Drawing.Size(135, 100);
            this.listBoxAgentType.TabIndex = 6;
            this.listBoxAgentType.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentType_SelectedIndexChanged);
            // 
            // textBox_K01
            // 
            this.textBox_K01.Location = new System.Drawing.Point(9, 187);
            this.textBox_K01.Name = "textBox_K01";
            this.textBox_K01.Size = new System.Drawing.Size(88, 22);
            this.textBox_K01.TabIndex = 7;
            this.textBox_K01.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(9, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "New Agent Properties";
            // 
            // textBox_V01
            // 
            this.textBox_V01.Location = new System.Drawing.Point(103, 187);
            this.textBox_V01.Name = "textBox_V01";
            this.textBox_V01.Size = new System.Drawing.Size(113, 22);
            this.textBox_V01.TabIndex = 10;
            // 
            // textBox_V02
            // 
            this.textBox_V02.Location = new System.Drawing.Point(103, 215);
            this.textBox_V02.Name = "textBox_V02";
            this.textBox_V02.Size = new System.Drawing.Size(113, 22);
            this.textBox_V02.TabIndex = 12;
            // 
            // textBox_K02
            // 
            this.textBox_K02.Location = new System.Drawing.Point(9, 215);
            this.textBox_K02.Name = "textBox_K02";
            this.textBox_K02.Size = new System.Drawing.Size(88, 22);
            this.textBox_K02.TabIndex = 11;
            this.textBox_K02.Text = "Year";
            // 
            // textBox_V03
            // 
            this.textBox_V03.Location = new System.Drawing.Point(103, 243);
            this.textBox_V03.Name = "textBox_V03";
            this.textBox_V03.Size = new System.Drawing.Size(113, 22);
            this.textBox_V03.TabIndex = 14;
            // 
            // textBox_K03
            // 
            this.textBox_K03.Location = new System.Drawing.Point(9, 243);
            this.textBox_K03.Name = "textBox_K03";
            this.textBox_K03.Size = new System.Drawing.Size(88, 22);
            this.textBox_K03.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(9, 352);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "Agent Coordinate";
            // 
            // textBox_C0X
            // 
            this.textBox_C0X.Location = new System.Drawing.Point(56, 390);
            this.textBox_C0X.Name = "textBox_C0X";
            this.textBox_C0X.Size = new System.Drawing.Size(30, 22);
            this.textBox_C0X.TabIndex = 16;
            this.textBox_C0X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(885, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 42);
            this.label4.TabIndex = 18;
            this.label4.Text = "Simulation\r\nSpeed";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(26, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "[0]";
            // 
            // textBox_C0Y
            // 
            this.textBox_C0Y.Location = new System.Drawing.Point(92, 390);
            this.textBox_C0Y.Name = "textBox_C0Y";
            this.textBox_C0Y.Size = new System.Drawing.Size(30, 22);
            this.textBox_C0Y.TabIndex = 20;
            this.textBox_C0Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // textBox_C1Y
            // 
            this.textBox_C1Y.Location = new System.Drawing.Point(92, 418);
            this.textBox_C1Y.Name = "textBox_C1Y";
            this.textBox_C1Y.Size = new System.Drawing.Size(30, 22);
            this.textBox_C1Y.TabIndex = 23;
            this.textBox_C1Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(26, 418);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "[1]";
            // 
            // textBox_C1X
            // 
            this.textBox_C1X.Location = new System.Drawing.Point(56, 418);
            this.textBox_C1X.Name = "textBox_C1X";
            this.textBox_C1X.Size = new System.Drawing.Size(30, 22);
            this.textBox_C1X.TabIndex = 21;
            this.textBox_C1X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // textBox_C2Y
            // 
            this.textBox_C2Y.Location = new System.Drawing.Point(92, 446);
            this.textBox_C2Y.Name = "textBox_C2Y";
            this.textBox_C2Y.Size = new System.Drawing.Size(30, 22);
            this.textBox_C2Y.TabIndex = 26;
            this.textBox_C2Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(25, 446);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "[2]";
            // 
            // textBox_C2X
            // 
            this.textBox_C2X.Location = new System.Drawing.Point(56, 446);
            this.textBox_C2X.Name = "textBox_C2X";
            this.textBox_C2X.Size = new System.Drawing.Size(30, 22);
            this.textBox_C2X.TabIndex = 24;
            this.textBox_C2X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // textBox_C3Y
            // 
            this.textBox_C3Y.Location = new System.Drawing.Point(92, 474);
            this.textBox_C3Y.Name = "textBox_C3Y";
            this.textBox_C3Y.Size = new System.Drawing.Size(30, 22);
            this.textBox_C3Y.TabIndex = 29;
            this.textBox_C3Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(25, 474);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "[3]";
            // 
            // textBox_C3X
            // 
            this.textBox_C3X.Location = new System.Drawing.Point(56, 474);
            this.textBox_C3X.Name = "textBox_C3X";
            this.textBox_C3X.Size = new System.Drawing.Size(30, 22);
            this.textBox_C3X.TabIndex = 27;
            this.textBox_C3X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label9.Location = new System.Drawing.Point(646, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "AgentList";
            // 
            // listBoxAgentList
            // 
            this.listBoxAgentList.FormattingEnabled = true;
            this.listBoxAgentList.ItemHeight = 12;
            this.listBoxAgentList.Location = new System.Drawing.Point(650, 33);
            this.listBoxAgentList.Name = "listBoxAgentList";
            this.listBoxAgentList.Size = new System.Drawing.Size(157, 124);
            this.listBoxAgentList.TabIndex = 31;
            this.listBoxAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentList_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(646, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 21);
            this.label10.TabIndex = 32;
            this.label10.Text = "Agent Properties";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label11.Location = new System.Drawing.Point(11, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 21);
            this.label11.TabIndex = 33;
            this.label11.Text = "Create Agent";
            // 
            // labelAgentProperties
            // 
            this.labelAgentProperties.AutoSize = true;
            this.labelAgentProperties.Font = new System.Drawing.Font("細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelAgentProperties.Location = new System.Drawing.Point(653, 190);
            this.labelAgentProperties.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelAgentProperties.Name = "labelAgentProperties";
            this.labelAgentProperties.Size = new System.Drawing.Size(95, 12);
            this.labelAgentProperties.TabIndex = 34;
            this.labelAgentProperties.Text = "properties here";
            // 
            // listBoxConsole
            // 
            this.listBoxConsole.FormattingEnabled = true;
            this.listBoxConsole.ItemHeight = 12;
            this.listBoxConsole.Items.AddRange(new object[] {
            "This is my Console"});
            this.listBoxConsole.Location = new System.Drawing.Point(240, 420);
            this.listBoxConsole.Name = "listBoxConsole";
            this.listBoxConsole.Size = new System.Drawing.Size(736, 76);
            this.listBoxConsole.TabIndex = 35;
            // 
            // textBox_V04
            // 
            this.textBox_V04.Location = new System.Drawing.Point(103, 271);
            this.textBox_V04.Name = "textBox_V04";
            this.textBox_V04.Size = new System.Drawing.Size(113, 22);
            this.textBox_V04.TabIndex = 37;
            // 
            // textBox_K04
            // 
            this.textBox_K04.Location = new System.Drawing.Point(9, 271);
            this.textBox_K04.Name = "textBox_K04";
            this.textBox_K04.Size = new System.Drawing.Size(88, 22);
            this.textBox_K04.TabIndex = 36;
            // 
            // textBox_V05
            // 
            this.textBox_V05.Location = new System.Drawing.Point(103, 299);
            this.textBox_V05.Name = "textBox_V05";
            this.textBox_V05.Size = new System.Drawing.Size(113, 22);
            this.textBox_V05.TabIndex = 39;
            // 
            // textBox_K05
            // 
            this.textBox_K05.Location = new System.Drawing.Point(9, 299);
            this.textBox_K05.Name = "textBox_K05";
            this.textBox_K05.Size = new System.Drawing.Size(88, 22);
            this.textBox_K05.TabIndex = 38;
            // 
            // textBox_V06
            // 
            this.textBox_V06.Location = new System.Drawing.Point(103, 327);
            this.textBox_V06.Name = "textBox_V06";
            this.textBox_V06.Size = new System.Drawing.Size(113, 22);
            this.textBox_V06.TabIndex = 41;
            // 
            // textBox_K06
            // 
            this.textBox_K06.Location = new System.Drawing.Point(9, 327);
            this.textBox_K06.Name = "textBox_K06";
            this.textBox_K06.Size = new System.Drawing.Size(88, 22);
            this.textBox_K06.TabIndex = 40;
            // 
            // listBoxAgentControl
            // 
            this.listBoxAgentControl.FormattingEnabled = true;
            this.listBoxAgentControl.ItemHeight = 12;
            this.listBoxAgentControl.Location = new System.Drawing.Point(153, 60);
            this.listBoxAgentControl.Name = "listBoxAgentControl";
            this.listBoxAgentControl.Size = new System.Drawing.Size(63, 100);
            this.listBoxAgentControl.TabIndex = 42;
            this.listBoxAgentControl.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentControl_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(149, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 21);
            this.label12.TabIndex = 43;
            this.label12.Text = "Ctrl";
            // 
            // listBoxJoinedAgentList
            // 
            this.listBoxJoinedAgentList.FormattingEnabled = true;
            this.listBoxJoinedAgentList.ItemHeight = 12;
            this.listBoxJoinedAgentList.Location = new System.Drawing.Point(825, 33);
            this.listBoxJoinedAgentList.Name = "listBoxJoinedAgentList";
            this.listBoxJoinedAgentList.Size = new System.Drawing.Size(151, 124);
            this.listBoxJoinedAgentList.TabIndex = 44;
            this.listBoxJoinedAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxJoinedAgentList_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label13.Location = new System.Drawing.Point(821, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 21);
            this.label13.TabIndex = 45;
            this.label13.Text = "JoinedAgentList";
            // 
            // gMapExplorer
            // 
            this.gMapExplorer.Bearing = 0F;
            this.gMapExplorer.CanDragMap = true;
            this.gMapExplorer.GrayScaleMode = false;
            this.gMapExplorer.LevelsKeepInMemmory = 5;
            this.gMapExplorer.Location = new System.Drawing.Point(992, 12);
            this.gMapExplorer.MarkersEnabled = true;
            this.gMapExplorer.MaxZoom = 2;
            this.gMapExplorer.MinZoom = 2;
            this.gMapExplorer.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapExplorer.Name = "gMapExplorer";
            this.gMapExplorer.NegativeMode = false;
            this.gMapExplorer.PolygonsEnabled = true;
            this.gMapExplorer.RetryLoadTile = 0;
            this.gMapExplorer.RoutesEnabled = true;
            this.gMapExplorer.ShowTileGridLines = false;
            this.gMapExplorer.Size = new System.Drawing.Size(303, 484);
            this.gMapExplorer.TabIndex = 46;
            this.gMapExplorer.Zoom = 0D;
            this.gMapExplorer.Load += new System.EventHandler(this.gMapExplorer_Load);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 502);
            this.Controls.Add(this.gMapExplorer);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.listBoxJoinedAgentList);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBoxConsole);
            this.Controls.Add(this.listBoxAgentType);
            this.Controls.Add(this.listBoxAgentControl);
            this.Controls.Add(this.labelAgentProperties);
            this.Controls.Add(this.textBox_K01);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_V06);
            this.Controls.Add(this.listBoxAgentList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_K06);
            this.Controls.Add(this.textBox_C3Y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_V05);
            this.Controls.Add(this.textBox_C3X);
            this.Controls.Add(this.textBox_V01);
            this.Controls.Add(this.textBox_C2Y);
            this.Controls.Add(this.textBox_K05);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_K02);
            this.Controls.Add(this.textBox_C2X);
            this.Controls.Add(this.textBox_V04);
            this.Controls.Add(this.textBox_C1Y);
            this.Controls.Add(this.textBox_V02);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_K04);
            this.Controls.Add(this.textBox_C1X);
            this.Controls.Add(this.textBox_K03);
            this.Controls.Add(this.textBox_C0Y);
            this.Controls.Add(this.textBox_V03);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_C0X);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Agent-Based Model 2D";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        public System.Windows.Forms.Timer SimulationTimer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ListBox listBoxAgentType;
        private System.Windows.Forms.TextBox textBox_K01;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_V01;
        private System.Windows.Forms.TextBox textBox_V02;
        private System.Windows.Forms.TextBox textBox_K02;
        private System.Windows.Forms.TextBox textBox_V03;
        private System.Windows.Forms.TextBox textBox_K03;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_C0X;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_C0Y;
        private System.Windows.Forms.TextBox textBox_C1Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_C1X;
        private System.Windows.Forms.TextBox textBox_C2Y;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_C2X;
        private System.Windows.Forms.TextBox textBox_C3Y;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_C3X;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listBoxAgentList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelAgentProperties;
        private System.Windows.Forms.ListBox listBoxConsole;
        private System.Windows.Forms.TextBox textBox_V04;
        private System.Windows.Forms.TextBox textBox_K04;
        private System.Windows.Forms.TextBox textBox_V05;
        private System.Windows.Forms.TextBox textBox_K05;
        private System.Windows.Forms.TextBox textBox_V06;
        private System.Windows.Forms.TextBox textBox_K06;
        private System.Windows.Forms.ListBox listBoxAgentControl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox listBoxJoinedAgentList;
        private System.Windows.Forms.Label label13;
        private GMap.NET.WindowsForms.GMapControl gMapExplorer;
    }
}

