namespace ABDiSE.View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.button_Create = new System.Windows.Forms.Button();
            this.button_SimStart = new System.Windows.Forms.Button();
            this.button_Pause = new System.Windows.Forms.Button();
            this.numericUpDown_SimDelay = new System.Windows.Forms.NumericUpDown();
            this.listBoxAgentType = new System.Windows.Forms.ListBox();
            this.textBox_K01 = new System.Windows.Forms.TextBox();
            this.label_AgentTypeTitle = new System.Windows.Forms.Label();
            this.label_AgentPropertiesTitle = new System.Windows.Forms.Label();
            this.textBox_V01 = new System.Windows.Forms.TextBox();
            this.textBox_V02 = new System.Windows.Forms.TextBox();
            this.textBox_K02 = new System.Windows.Forms.TextBox();
            this.textBox_V03 = new System.Windows.Forms.TextBox();
            this.textBox_K03 = new System.Windows.Forms.TextBox();
            this.label_AgentCoordTitle = new System.Windows.Forms.Label();
            this.textBox_AgentLat = new System.Windows.Forms.TextBox();
            this.label_CurrentLatTitle = new System.Windows.Forms.Label();
            this.textBox_AgentLng = new System.Windows.Forms.TextBox();
            this.label_AgentListTitle = new System.Windows.Forms.Label();
            this.listBoxAgentList = new System.Windows.Forms.ListBox();
            this.label_SelectedAgentPropertiesTitle = new System.Windows.Forms.Label();
            this.label_AgentProperties = new System.Windows.Forms.Label();
            this.textBox_V04 = new System.Windows.Forms.TextBox();
            this.textBox_K04 = new System.Windows.Forms.TextBox();
            this.textBox_V05 = new System.Windows.Forms.TextBox();
            this.textBox_K05 = new System.Windows.Forms.TextBox();
            this.textBox_V06 = new System.Windows.Forms.TextBox();
            this.textBox_K06 = new System.Windows.Forms.TextBox();
            this.listBoxAgentControl = new System.Windows.Forms.ListBox();
            this.listBoxJoinedAgentList = new System.Windows.Forms.ListBox();
            this.label_JoinedAgentListTitle = new System.Windows.Forms.Label();
            this.gMapExplorer = new GMap.NET.WindowsForms.GMapControl();
            this.label_LatLng = new System.Windows.Forms.Label();
            this.label_CurrentLngTitle = new System.Windows.Forms.Label();
            this.label_MouseStatusTitle = new System.Windows.Forms.Label();
            this.label_MouseStatus = new System.Windows.Forms.Label();
            this.button_CreateTP = new System.Windows.Forms.Button();
            this.label_ThreadNumber = new System.Windows.Forms.Label();
            this.button_CancelPool = new System.Windows.Forms.Button();
            this.button_EndPool = new System.Windows.Forms.Button();
            this.numericUpDown_STPQueueDelay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_STPWorkitemNum = new System.Windows.Forms.NumericUpDown();
            this.label_STPQueueDelayTitle = new System.Windows.Forms.Label();
            this.label_STPWorkitemTitle = new System.Windows.Forms.Label();
            this.numericUpDown_STPIdleTime = new System.Windows.Forms.NumericUpDown();
            this.label_STPIdleTimeTitle = new System.Windows.Forms.Label();
            this.numericUpDown_STPExecuteTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_STPThreadsNum = new System.Windows.Forms.NumericUpDown();
            this.label_ExecuteTime = new System.Windows.Forms.Label();
            this.button_StepSim = new System.Windows.Forms.Button();
            this.label_CurrentStepTitle = new System.Windows.Forms.Label();
            this.label_GodCurrentStep = new System.Windows.Forms.Label();
            this.label_SimDelayTitle = new System.Windows.Forms.Label();
            this.numericUpDown_SimSteps = new System.Windows.Forms.NumericUpDown();
            this.label_SimStepsTitle = new System.Windows.Forms.Label();
            this.groupBox_AgentCreation = new System.Windows.Forms.GroupBox();
            this.button_SelectDLL = new System.Windows.Forms.Button();
            this.groupBox_STP = new System.Windows.Forms.GroupBox();
            this.groupBox_SimControl = new System.Windows.Forms.GroupBox();
            this.progressBar_steps = new System.Windows.Forms.ProgressBar();
            this.groupBox_Environment = new System.Windows.Forms.GroupBox();
            this.label_EnvironmentProperties = new System.Windows.Forms.Label();
            this.textBox_Env_WindSpeed = new System.Windows.Forms.TextBox();
            this.label_EnvironmentPropertiesTitle = new System.Windows.Forms.Label();
            this.label_Env_WindSpeedTitle = new System.Windows.Forms.Label();
            this.textBox_Env_WindDirection = new System.Windows.Forms.TextBox();
            this.label_Env_WindDirTitle = new System.Windows.Forms.Label();
            this.label_Env_RainFallTitle = new System.Windows.Forms.Label();
            this.textBox_Env_RainFall = new System.Windows.Forms.TextBox();
            this.radioButton_Rainy = new System.Windows.Forms.RadioButton();
            this.radioButton_Cloudy = new System.Windows.Forms.RadioButton();
            this.radioButton_Sunny = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPQueueDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPWorkitemNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPIdleTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPExecuteTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPThreadsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimSteps)).BeginInit();
            this.groupBox_AgentCreation.SuspendLayout();
            this.groupBox_STP.SuspendLayout();
            this.groupBox_SimControl.SuspendLayout();
            this.groupBox_Environment.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Create
            // 
            this.button_Create.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Create.Location = new System.Drawing.Point(17, 39);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(88, 62);
            this.button_Create.TabIndex = 1;
            this.button_Create.Text = "Create New Agent";
            this.button_Create.UseVisualStyleBackColor = true;
            this.button_Create.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // button_SimStart
            // 
            this.button_SimStart.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SimStart.Location = new System.Drawing.Point(182, 40);
            this.button_SimStart.Name = "button_SimStart";
            this.button_SimStart.Size = new System.Drawing.Size(89, 22);
            this.button_SimStart.TabIndex = 3;
            this.button_SimStart.Text = "Sim_Start";
            this.button_SimStart.UseVisualStyleBackColor = true;
            this.button_SimStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // button_Pause
            // 
            this.button_Pause.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Pause.Location = new System.Drawing.Point(277, 39);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(75, 62);
            this.button_Pause.TabIndex = 4;
            this.button_Pause.Text = "Sim_Pause";
            this.button_Pause.UseVisualStyleBackColor = true;
            this.button_Pause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // numericUpDown_SimDelay
            // 
            this.numericUpDown_SimDelay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_SimDelay.Location = new System.Drawing.Point(370, 66);
            this.numericUpDown_SimDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_SimDelay.Name = "numericUpDown_SimDelay";
            this.numericUpDown_SimDelay.Size = new System.Drawing.Size(55, 22);
            this.numericUpDown_SimDelay.TabIndex = 5;
            this.numericUpDown_SimDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_SimDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // listBoxAgentType
            // 
            this.listBoxAgentType.FormattingEnabled = true;
            this.listBoxAgentType.ItemHeight = 12;
            this.listBoxAgentType.Location = new System.Drawing.Point(10, 33);
            this.listBoxAgentType.Name = "listBoxAgentType";
            this.listBoxAgentType.Size = new System.Drawing.Size(142, 100);
            this.listBoxAgentType.TabIndex = 6;
            this.listBoxAgentType.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentType_SelectedIndexChanged);
            // 
            // textBox_K01
            // 
            this.textBox_K01.Location = new System.Drawing.Point(12, 205);
            this.textBox_K01.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K01.Name = "textBox_K01";
            this.textBox_K01.Size = new System.Drawing.Size(141, 22);
            this.textBox_K01.TabIndex = 7;
            this.textBox_K01.Text = "Name";
            // 
            // label_AgentTypeTitle
            // 
            this.label_AgentTypeTitle.AutoSize = true;
            this.label_AgentTypeTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentTypeTitle.Location = new System.Drawing.Point(5, 18);
            this.label_AgentTypeTitle.Name = "label_AgentTypeTitle";
            this.label_AgentTypeTitle.Size = new System.Drawing.Size(70, 12);
            this.label_AgentTypeTitle.TabIndex = 8;
            this.label_AgentTypeTitle.Text = "Agent Type";
            // 
            // label_AgentPropertiesTitle
            // 
            this.label_AgentPropertiesTitle.AutoSize = true;
            this.label_AgentPropertiesTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentPropertiesTitle.Location = new System.Drawing.Point(8, 186);
            this.label_AgentPropertiesTitle.Name = "label_AgentPropertiesTitle";
            this.label_AgentPropertiesTitle.Size = new System.Drawing.Size(126, 12);
            this.label_AgentPropertiesTitle.TabIndex = 9;
            this.label_AgentPropertiesTitle.Text = "New Agent Properties";
            // 
            // textBox_V01
            // 
            this.textBox_V01.Location = new System.Drawing.Point(159, 205);
            this.textBox_V01.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V01.Name = "textBox_V01";
            this.textBox_V01.Size = new System.Drawing.Size(144, 22);
            this.textBox_V01.TabIndex = 10;
            // 
            // textBox_V02
            // 
            this.textBox_V02.Location = new System.Drawing.Point(159, 229);
            this.textBox_V02.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V02.Name = "textBox_V02";
            this.textBox_V02.Size = new System.Drawing.Size(144, 22);
            this.textBox_V02.TabIndex = 12;
            // 
            // textBox_K02
            // 
            this.textBox_K02.Location = new System.Drawing.Point(12, 229);
            this.textBox_K02.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K02.Name = "textBox_K02";
            this.textBox_K02.Size = new System.Drawing.Size(141, 22);
            this.textBox_K02.TabIndex = 11;
            this.textBox_K02.Text = "Year";
            // 
            // textBox_V03
            // 
            this.textBox_V03.Location = new System.Drawing.Point(159, 253);
            this.textBox_V03.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V03.Name = "textBox_V03";
            this.textBox_V03.Size = new System.Drawing.Size(144, 22);
            this.textBox_V03.TabIndex = 14;
            // 
            // textBox_K03
            // 
            this.textBox_K03.Location = new System.Drawing.Point(12, 253);
            this.textBox_K03.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K03.Name = "textBox_K03";
            this.textBox_K03.Size = new System.Drawing.Size(141, 22);
            this.textBox_K03.TabIndex = 13;
            // 
            // label_AgentCoordTitle
            // 
            this.label_AgentCoordTitle.AutoSize = true;
            this.label_AgentCoordTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentCoordTitle.Location = new System.Drawing.Point(7, 349);
            this.label_AgentCoordTitle.Name = "label_AgentCoordTitle";
            this.label_AgentCoordTitle.Size = new System.Drawing.Size(104, 12);
            this.label_AgentCoordTitle.TabIndex = 15;
            this.label_AgentCoordTitle.Text = "Agent Coordinate";
            // 
            // textBox_AgentLat
            // 
            this.textBox_AgentLat.Location = new System.Drawing.Point(45, 370);
            this.textBox_AgentLat.Name = "textBox_AgentLat";
            this.textBox_AgentLat.Size = new System.Drawing.Size(155, 22);
            this.textBox_AgentLat.TabIndex = 16;
            this.textBox_AgentLat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label_CurrentLatTitle
            // 
            this.label_CurrentLatTitle.AutoSize = true;
            this.label_CurrentLatTitle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_CurrentLatTitle.Location = new System.Drawing.Point(15, 370);
            this.label_CurrentLatTitle.Name = "label_CurrentLatTitle";
            this.label_CurrentLatTitle.Size = new System.Drawing.Size(27, 17);
            this.label_CurrentLatTitle.TabIndex = 19;
            this.label_CurrentLatTitle.Text = "Lat";
            // 
            // textBox_AgentLng
            // 
            this.textBox_AgentLng.Location = new System.Drawing.Point(45, 393);
            this.textBox_AgentLng.Name = "textBox_AgentLng";
            this.textBox_AgentLng.Size = new System.Drawing.Size(155, 22);
            this.textBox_AgentLng.TabIndex = 20;
            this.textBox_AgentLng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label_AgentListTitle
            // 
            this.label_AgentListTitle.AutoSize = true;
            this.label_AgentListTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentListTitle.Location = new System.Drawing.Point(779, 14);
            this.label_AgentListTitle.Name = "label_AgentListTitle";
            this.label_AgentListTitle.Size = new System.Drawing.Size(59, 12);
            this.label_AgentListTitle.TabIndex = 30;
            this.label_AgentListTitle.Text = "AgentList";
            // 
            // listBoxAgentList
            // 
            this.listBoxAgentList.FormattingEnabled = true;
            this.listBoxAgentList.HorizontalScrollbar = true;
            this.listBoxAgentList.ItemHeight = 12;
            this.listBoxAgentList.Location = new System.Drawing.Point(788, 29);
            this.listBoxAgentList.Name = "listBoxAgentList";
            this.listBoxAgentList.Size = new System.Drawing.Size(216, 88);
            this.listBoxAgentList.TabIndex = 31;
            this.listBoxAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentList_SelectedIndexChanged);
            // 
            // label_SelectedAgentPropertiesTitle
            // 
            this.label_SelectedAgentPropertiesTitle.AutoSize = true;
            this.label_SelectedAgentPropertiesTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SelectedAgentPropertiesTitle.Location = new System.Drawing.Point(776, 243);
            this.label_SelectedAgentPropertiesTitle.Name = "label_SelectedAgentPropertiesTitle";
            this.label_SelectedAgentPropertiesTitle.Size = new System.Drawing.Size(98, 12);
            this.label_SelectedAgentPropertiesTitle.TabIndex = 32;
            this.label_SelectedAgentPropertiesTitle.Text = "Agent Properties";
            // 
            // label_AgentProperties
            // 
            this.label_AgentProperties.AutoSize = true;
            this.label_AgentProperties.Location = new System.Drawing.Point(783, 264);
            this.label_AgentProperties.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_AgentProperties.Name = "label_AgentProperties";
            this.label_AgentProperties.Size = new System.Drawing.Size(82, 12);
            this.label_AgentProperties.TabIndex = 34;
            this.label_AgentProperties.Text = "Agent Properties";
            // 
            // textBox_V04
            // 
            this.textBox_V04.Location = new System.Drawing.Point(159, 277);
            this.textBox_V04.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V04.Name = "textBox_V04";
            this.textBox_V04.Size = new System.Drawing.Size(144, 22);
            this.textBox_V04.TabIndex = 37;
            // 
            // textBox_K04
            // 
            this.textBox_K04.Location = new System.Drawing.Point(12, 277);
            this.textBox_K04.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K04.Name = "textBox_K04";
            this.textBox_K04.Size = new System.Drawing.Size(141, 22);
            this.textBox_K04.TabIndex = 36;
            // 
            // textBox_V05
            // 
            this.textBox_V05.Location = new System.Drawing.Point(159, 301);
            this.textBox_V05.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V05.Name = "textBox_V05";
            this.textBox_V05.Size = new System.Drawing.Size(144, 22);
            this.textBox_V05.TabIndex = 39;
            // 
            // textBox_K05
            // 
            this.textBox_K05.Location = new System.Drawing.Point(12, 301);
            this.textBox_K05.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K05.Name = "textBox_K05";
            this.textBox_K05.Size = new System.Drawing.Size(141, 22);
            this.textBox_K05.TabIndex = 38;
            // 
            // textBox_V06
            // 
            this.textBox_V06.Location = new System.Drawing.Point(159, 325);
            this.textBox_V06.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V06.Name = "textBox_V06";
            this.textBox_V06.Size = new System.Drawing.Size(144, 22);
            this.textBox_V06.TabIndex = 41;
            // 
            // textBox_K06
            // 
            this.textBox_K06.Location = new System.Drawing.Point(12, 325);
            this.textBox_K06.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K06.Name = "textBox_K06";
            this.textBox_K06.Size = new System.Drawing.Size(141, 22);
            this.textBox_K06.TabIndex = 40;
            // 
            // listBoxAgentControl
            // 
            this.listBoxAgentControl.FormattingEnabled = true;
            this.listBoxAgentControl.ItemHeight = 12;
            this.listBoxAgentControl.Location = new System.Drawing.Point(158, 33);
            this.listBoxAgentControl.Name = "listBoxAgentControl";
            this.listBoxAgentControl.Size = new System.Drawing.Size(144, 100);
            this.listBoxAgentControl.TabIndex = 42;
            this.listBoxAgentControl.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentControl_SelectedIndexChanged);
            // 
            // listBoxJoinedAgentList
            // 
            this.listBoxJoinedAgentList.FormattingEnabled = true;
            this.listBoxJoinedAgentList.HorizontalScrollbar = true;
            this.listBoxJoinedAgentList.ItemHeight = 12;
            this.listBoxJoinedAgentList.Location = new System.Drawing.Point(788, 140);
            this.listBoxJoinedAgentList.Name = "listBoxJoinedAgentList";
            this.listBoxJoinedAgentList.Size = new System.Drawing.Size(216, 100);
            this.listBoxJoinedAgentList.TabIndex = 44;
            this.listBoxJoinedAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxJoinedAgentList_SelectedIndexChanged);
            // 
            // label_JoinedAgentListTitle
            // 
            this.label_JoinedAgentListTitle.AutoSize = true;
            this.label_JoinedAgentListTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_JoinedAgentListTitle.Location = new System.Drawing.Point(779, 120);
            this.label_JoinedAgentListTitle.Name = "label_JoinedAgentListTitle";
            this.label_JoinedAgentListTitle.Size = new System.Drawing.Size(95, 12);
            this.label_JoinedAgentListTitle.TabIndex = 45;
            this.label_JoinedAgentListTitle.Text = "JoinedAgentList";
            // 
            // gMapExplorer
            // 
            this.gMapExplorer.Bearing = 0F;
            this.gMapExplorer.CanDragMap = true;
            this.gMapExplorer.GrayScaleMode = false;
            this.gMapExplorer.LevelsKeepInMemmory = 5;
            this.gMapExplorer.Location = new System.Drawing.Point(323, 12);
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
            this.gMapExplorer.Size = new System.Drawing.Size(443, 357);
            this.gMapExplorer.TabIndex = 46;
            this.gMapExplorer.Zoom = 0D;
            this.gMapExplorer.Load += new System.EventHandler(this.gMapExplorer_Load);
            // 
            // label_LatLng
            // 
            this.label_LatLng.AutoSize = true;
            this.label_LatLng.Location = new System.Drawing.Point(321, 382);
            this.label_LatLng.Name = "label_LatLng";
            this.label_LatLng.Size = new System.Drawing.Size(61, 12);
            this.label_LatLng.TabIndex = 47;
            this.label_LatLng.Text = "labelLatLng";
            // 
            // label_CurrentLngTitle
            // 
            this.label_CurrentLngTitle.AutoSize = true;
            this.label_CurrentLngTitle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_CurrentLngTitle.Location = new System.Drawing.Point(15, 393);
            this.label_CurrentLngTitle.Name = "label_CurrentLngTitle";
            this.label_CurrentLngTitle.Size = new System.Drawing.Size(31, 17);
            this.label_CurrentLngTitle.TabIndex = 19;
            this.label_CurrentLngTitle.Text = "Lng";
            // 
            // label_MouseStatusTitle
            // 
            this.label_MouseStatusTitle.AutoSize = true;
            this.label_MouseStatusTitle.Location = new System.Drawing.Point(628, 382);
            this.label_MouseStatusTitle.Name = "label_MouseStatusTitle";
            this.label_MouseStatusTitle.Size = new System.Drawing.Size(69, 12);
            this.label_MouseStatusTitle.TabIndex = 48;
            this.label_MouseStatusTitle.Text = "Mouse Status:";
            this.label_MouseStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_MouseStatus
            // 
            this.label_MouseStatus.AutoSize = true;
            this.label_MouseStatus.Location = new System.Drawing.Point(703, 382);
            this.label_MouseStatus.Name = "label_MouseStatus";
            this.label_MouseStatus.Size = new System.Drawing.Size(63, 12);
            this.label_MouseStatus.TabIndex = 49;
            this.label_MouseStatus.Text = "mouse status";
            this.label_MouseStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_CreateTP
            // 
            this.button_CreateTP.Location = new System.Drawing.Point(84, 129);
            this.button_CreateTP.Name = "button_CreateTP";
            this.button_CreateTP.Size = new System.Drawing.Size(68, 40);
            this.button_CreateTP.TabIndex = 50;
            this.button_CreateTP.Text = "new ThreadPool";
            this.button_CreateTP.UseVisualStyleBackColor = true;
            this.button_CreateTP.Click += new System.EventHandler(this.buttonCreateTP_Click);
            // 
            // label_ThreadNumber
            // 
            this.label_ThreadNumber.AutoSize = true;
            this.label_ThreadNumber.Location = new System.Drawing.Point(8, 20);
            this.label_ThreadNumber.Name = "label_ThreadNumber";
            this.label_ThreadNumber.Size = new System.Drawing.Size(48, 12);
            this.label_ThreadNumber.TabIndex = 52;
            this.label_ThreadNumber.Text = "#Threads";
            this.label_ThreadNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_CancelPool
            // 
            this.button_CancelPool.Location = new System.Drawing.Point(84, 171);
            this.button_CancelPool.Name = "button_CancelPool";
            this.button_CancelPool.Size = new System.Drawing.Size(68, 21);
            this.button_CancelPool.TabIndex = 53;
            this.button_CancelPool.Text = "CancelPool";
            this.button_CancelPool.UseVisualStyleBackColor = true;
            this.button_CancelPool.Click += new System.EventHandler(this.button_CancelPool_Click);
            // 
            // button_EndPool
            // 
            this.button_EndPool.Location = new System.Drawing.Point(84, 194);
            this.button_EndPool.Name = "button_EndPool";
            this.button_EndPool.Size = new System.Drawing.Size(68, 22);
            this.button_EndPool.TabIndex = 53;
            this.button_EndPool.Text = "EndPool";
            this.button_EndPool.UseVisualStyleBackColor = true;
            this.button_EndPool.Click += new System.EventHandler(this.button_EndPool_Click);
            // 
            // numericUpDown_STPQueueDelay
            // 
            this.numericUpDown_STPQueueDelay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPQueueDelay.Location = new System.Drawing.Point(7, 194);
            this.numericUpDown_STPQueueDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_STPQueueDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_STPQueueDelay.Name = "numericUpDown_STPQueueDelay";
            this.numericUpDown_STPQueueDelay.Size = new System.Drawing.Size(64, 22);
            this.numericUpDown_STPQueueDelay.TabIndex = 63;
            this.numericUpDown_STPQueueDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_STPQueueDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_STPWorkitemNum
            // 
            this.numericUpDown_STPWorkitemNum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPWorkitemNum.Location = new System.Drawing.Point(7, 75);
            this.numericUpDown_STPWorkitemNum.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_STPWorkitemNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_STPWorkitemNum.Name = "numericUpDown_STPWorkitemNum";
            this.numericUpDown_STPWorkitemNum.Size = new System.Drawing.Size(64, 22);
            this.numericUpDown_STPWorkitemNum.TabIndex = 62;
            this.numericUpDown_STPWorkitemNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_STPWorkitemNum.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label_STPQueueDelayTitle
            // 
            this.label_STPQueueDelayTitle.AutoSize = true;
            this.label_STPQueueDelayTitle.Location = new System.Drawing.Point(5, 179);
            this.label_STPQueueDelayTitle.Name = "label_STPQueueDelayTitle";
            this.label_STPQueueDelayTitle.Size = new System.Drawing.Size(62, 12);
            this.label_STPQueueDelayTitle.TabIndex = 61;
            this.label_STPQueueDelayTitle.Text = "QueueDelay";
            this.label_STPQueueDelayTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_STPWorkitemTitle
            // 
            this.label_STPWorkitemTitle.AutoSize = true;
            this.label_STPWorkitemTitle.Location = new System.Drawing.Point(5, 61);
            this.label_STPWorkitemTitle.Name = "label_STPWorkitemTitle";
            this.label_STPWorkitemTitle.Size = new System.Drawing.Size(62, 12);
            this.label_STPWorkitemTitle.TabIndex = 60;
            this.label_STPWorkitemTitle.Text = "#Workitems";
            this.label_STPWorkitemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_STPIdleTime
            // 
            this.numericUpDown_STPIdleTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPIdleTime.Location = new System.Drawing.Point(7, 154);
            this.numericUpDown_STPIdleTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_STPIdleTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_STPIdleTime.Name = "numericUpDown_STPIdleTime";
            this.numericUpDown_STPIdleTime.Size = new System.Drawing.Size(64, 22);
            this.numericUpDown_STPIdleTime.TabIndex = 59;
            this.numericUpDown_STPIdleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_STPIdleTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label_STPIdleTimeTitle
            // 
            this.label_STPIdleTimeTitle.AutoSize = true;
            this.label_STPIdleTimeTitle.Location = new System.Drawing.Point(5, 139);
            this.label_STPIdleTimeTitle.Name = "label_STPIdleTimeTitle";
            this.label_STPIdleTimeTitle.Size = new System.Drawing.Size(47, 12);
            this.label_STPIdleTimeTitle.TabIndex = 58;
            this.label_STPIdleTimeTitle.Text = "IdleTime";
            this.label_STPIdleTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_STPExecuteTime
            // 
            this.numericUpDown_STPExecuteTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPExecuteTime.Location = new System.Drawing.Point(7, 114);
            this.numericUpDown_STPExecuteTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_STPExecuteTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_STPExecuteTime.Name = "numericUpDown_STPExecuteTime";
            this.numericUpDown_STPExecuteTime.Size = new System.Drawing.Size(64, 22);
            this.numericUpDown_STPExecuteTime.TabIndex = 57;
            this.numericUpDown_STPExecuteTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_STPExecuteTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_STPThreadsNum
            // 
            this.numericUpDown_STPThreadsNum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPThreadsNum.Location = new System.Drawing.Point(7, 35);
            this.numericUpDown_STPThreadsNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_STPThreadsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_STPThreadsNum.Name = "numericUpDown_STPThreadsNum";
            this.numericUpDown_STPThreadsNum.Size = new System.Drawing.Size(64, 22);
            this.numericUpDown_STPThreadsNum.TabIndex = 56;
            this.numericUpDown_STPThreadsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_STPThreadsNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label_ExecuteTime
            // 
            this.label_ExecuteTime.AutoSize = true;
            this.label_ExecuteTime.Location = new System.Drawing.Point(5, 100);
            this.label_ExecuteTime.Name = "label_ExecuteTime";
            this.label_ExecuteTime.Size = new System.Drawing.Size(71, 12);
            this.label_ExecuteTime.TabIndex = 54;
            this.label_ExecuteTime.Text = "ExecuteCount";
            this.label_ExecuteTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_StepSim
            // 
            this.button_StepSim.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_StepSim.Location = new System.Drawing.Point(114, 39);
            this.button_StepSim.Name = "button_StepSim";
            this.button_StepSim.Size = new System.Drawing.Size(62, 62);
            this.button_StepSim.TabIndex = 55;
            this.button_StepSim.Text = "Sim_Step";
            this.button_StepSim.UseVisualStyleBackColor = true;
            this.button_StepSim.Click += new System.EventHandler(this.button_StepSim_Click);
            // 
            // label_CurrentStepTitle
            // 
            this.label_CurrentStepTitle.AutoSize = true;
            this.label_CurrentStepTitle.Location = new System.Drawing.Point(16, 23);
            this.label_CurrentStepTitle.Name = "label_CurrentStepTitle";
            this.label_CurrentStepTitle.Size = new System.Drawing.Size(93, 12);
            this.label_CurrentStepTitle.TabIndex = 56;
            this.label_CurrentStepTitle.Text = "God.CurrentStep : ";
            // 
            // label_GodCurrentStep
            // 
            this.label_GodCurrentStep.AutoSize = true;
            this.label_GodCurrentStep.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_GodCurrentStep.Location = new System.Drawing.Point(115, 23);
            this.label_GodCurrentStep.Name = "label_GodCurrentStep";
            this.label_GodCurrentStep.Size = new System.Drawing.Size(83, 12);
            this.label_GodCurrentStep.TabIndex = 57;
            this.label_GodCurrentStep.Text = "CurrentStep 0";
            // 
            // label_SimDelayTitle
            // 
            this.label_SimDelayTitle.AutoSize = true;
            this.label_SimDelayTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SimDelayTitle.Location = new System.Drawing.Point(368, 45);
            this.label_SimDelayTitle.Name = "label_SimDelayTitle";
            this.label_SimDelayTitle.Size = new System.Drawing.Size(62, 12);
            this.label_SimDelayTitle.TabIndex = 58;
            this.label_SimDelayTitle.Text = "SimDelay:";
            // 
            // numericUpDown_SimSteps
            // 
            this.numericUpDown_SimSteps.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_SimSteps.Location = new System.Drawing.Point(229, 79);
            this.numericUpDown_SimSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_SimSteps.Name = "numericUpDown_SimSteps";
            this.numericUpDown_SimSteps.Size = new System.Drawing.Size(42, 22);
            this.numericUpDown_SimSteps.TabIndex = 59;
            this.numericUpDown_SimSteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_SimSteps.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label_SimStepsTitle
            // 
            this.label_SimStepsTitle.AutoSize = true;
            this.label_SimStepsTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SimStepsTitle.Location = new System.Drawing.Point(185, 83);
            this.label_SimStepsTitle.Name = "label_SimStepsTitle";
            this.label_SimStepsTitle.Size = new System.Drawing.Size(38, 12);
            this.label_SimStepsTitle.TabIndex = 60;
            this.label_SimStepsTitle.Text = "Steps:";
            // 
            // groupBox_AgentCreation
            // 
            this.groupBox_AgentCreation.Controls.Add(this.button_SelectDLL);
            this.groupBox_AgentCreation.Controls.Add(this.label_AgentTypeTitle);
            this.groupBox_AgentCreation.Controls.Add(this.label_AgentPropertiesTitle);
            this.groupBox_AgentCreation.Controls.Add(this.listBoxAgentControl);
            this.groupBox_AgentCreation.Controls.Add(this.listBoxAgentType);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V06);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V03);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K03);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K04);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V02);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V04);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K02);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K05);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V01);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_V05);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K06);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_K01);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_AgentLng);
            this.groupBox_AgentCreation.Controls.Add(this.textBox_AgentLat);
            this.groupBox_AgentCreation.Controls.Add(this.label_AgentCoordTitle);
            this.groupBox_AgentCreation.Controls.Add(this.label_CurrentLngTitle);
            this.groupBox_AgentCreation.Controls.Add(this.label_CurrentLatTitle);
            this.groupBox_AgentCreation.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_AgentCreation.Location = new System.Drawing.Point(9, 4);
            this.groupBox_AgentCreation.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox_AgentCreation.Name = "groupBox_AgentCreation";
            this.groupBox_AgentCreation.Size = new System.Drawing.Size(310, 425);
            this.groupBox_AgentCreation.TabIndex = 61;
            this.groupBox_AgentCreation.TabStop = false;
            this.groupBox_AgentCreation.Text = "New Agent Setting";
            // 
            // button_SelectDLL
            // 
            this.button_SelectDLL.Location = new System.Drawing.Point(10, 136);
            this.button_SelectDLL.Name = "button_SelectDLL";
            this.button_SelectDLL.Size = new System.Drawing.Size(142, 31);
            this.button_SelectDLL.TabIndex = 43;
            this.button_SelectDLL.Text = "Select DLL";
            this.button_SelectDLL.UseVisualStyleBackColor = true;
            this.button_SelectDLL.Click += new System.EventHandler(this.button_SelectDLL_Click);
            // 
            // groupBox_STP
            // 
            this.groupBox_STP.Controls.Add(this.numericUpDown_STPQueueDelay);
            this.groupBox_STP.Controls.Add(this.label_ThreadNumber);
            this.groupBox_STP.Controls.Add(this.numericUpDown_STPWorkitemNum);
            this.groupBox_STP.Controls.Add(this.button_CancelPool);
            this.groupBox_STP.Controls.Add(this.label_STPQueueDelayTitle);
            this.groupBox_STP.Controls.Add(this.button_CreateTP);
            this.groupBox_STP.Controls.Add(this.label_STPWorkitemTitle);
            this.groupBox_STP.Controls.Add(this.button_EndPool);
            this.groupBox_STP.Controls.Add(this.numericUpDown_STPIdleTime);
            this.groupBox_STP.Controls.Add(this.label_ExecuteTime);
            this.groupBox_STP.Controls.Add(this.label_STPIdleTimeTitle);
            this.groupBox_STP.Controls.Add(this.numericUpDown_STPThreadsNum);
            this.groupBox_STP.Controls.Add(this.numericUpDown_STPExecuteTime);
            this.groupBox_STP.Location = new System.Drawing.Point(9, 433);
            this.groupBox_STP.Name = "groupBox_STP";
            this.groupBox_STP.Size = new System.Drawing.Size(164, 227);
            this.groupBox_STP.TabIndex = 62;
            this.groupBox_STP.TabStop = false;
            this.groupBox_STP.Text = "ThreadPool Setting";
            // 
            // groupBox_SimControl
            // 
            this.groupBox_SimControl.Controls.Add(this.progressBar_steps);
            this.groupBox_SimControl.Controls.Add(this.button_Create);
            this.groupBox_SimControl.Controls.Add(this.button_SimStart);
            this.groupBox_SimControl.Controls.Add(this.button_Pause);
            this.groupBox_SimControl.Controls.Add(this.label_SimStepsTitle);
            this.groupBox_SimControl.Controls.Add(this.numericUpDown_SimDelay);
            this.groupBox_SimControl.Controls.Add(this.numericUpDown_SimSteps);
            this.groupBox_SimControl.Controls.Add(this.label_SimDelayTitle);
            this.groupBox_SimControl.Controls.Add(this.button_StepSim);
            this.groupBox_SimControl.Controls.Add(this.label_GodCurrentStep);
            this.groupBox_SimControl.Controls.Add(this.label_CurrentStepTitle);
            this.groupBox_SimControl.Location = new System.Drawing.Point(323, 420);
            this.groupBox_SimControl.Name = "groupBox_SimControl";
            this.groupBox_SimControl.Size = new System.Drawing.Size(443, 110);
            this.groupBox_SimControl.TabIndex = 63;
            this.groupBox_SimControl.TabStop = false;
            this.groupBox_SimControl.Text = "Simulation Controller";
            // 
            // progressBar_steps
            // 
            this.progressBar_steps.Location = new System.Drawing.Point(182, 66);
            this.progressBar_steps.Name = "progressBar_steps";
            this.progressBar_steps.Size = new System.Drawing.Size(89, 10);
            this.progressBar_steps.TabIndex = 61;
            // 
            // groupBox_Environment
            // 
            this.groupBox_Environment.Controls.Add(this.label_EnvironmentProperties);
            this.groupBox_Environment.Controls.Add(this.textBox_Env_WindSpeed);
            this.groupBox_Environment.Controls.Add(this.label_EnvironmentPropertiesTitle);
            this.groupBox_Environment.Controls.Add(this.label_Env_WindSpeedTitle);
            this.groupBox_Environment.Controls.Add(this.textBox_Env_WindDirection);
            this.groupBox_Environment.Controls.Add(this.label_Env_WindDirTitle);
            this.groupBox_Environment.Controls.Add(this.label_Env_RainFallTitle);
            this.groupBox_Environment.Controls.Add(this.textBox_Env_RainFall);
            this.groupBox_Environment.Controls.Add(this.radioButton_Rainy);
            this.groupBox_Environment.Controls.Add(this.radioButton_Cloudy);
            this.groupBox_Environment.Controls.Add(this.radioButton_Sunny);
            this.groupBox_Environment.Location = new System.Drawing.Point(323, 572);
            this.groupBox_Environment.Name = "groupBox_Environment";
            this.groupBox_Environment.Size = new System.Drawing.Size(443, 92);
            this.groupBox_Environment.TabIndex = 64;
            this.groupBox_Environment.TabStop = false;
            this.groupBox_Environment.Text = "Environment Controller";
            // 
            // label_EnvironmentProperties
            // 
            this.label_EnvironmentProperties.AutoSize = true;
            this.label_EnvironmentProperties.Location = new System.Drawing.Point(253, 32);
            this.label_EnvironmentProperties.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_EnvironmentProperties.Name = "label_EnvironmentProperties";
            this.label_EnvironmentProperties.Size = new System.Drawing.Size(74, 12);
            this.label_EnvironmentProperties.TabIndex = 66;
            this.label_EnvironmentProperties.Text = "properties here";
            // 
            // textBox_Env_WindSpeed
            // 
            this.textBox_Env_WindSpeed.Location = new System.Drawing.Point(158, 63);
            this.textBox_Env_WindSpeed.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_Env_WindSpeed.Name = "textBox_Env_WindSpeed";
            this.textBox_Env_WindSpeed.Size = new System.Drawing.Size(88, 22);
            this.textBox_Env_WindSpeed.TabIndex = 71;
            this.textBox_Env_WindSpeed.Text = "9.2";
            this.textBox_Env_WindSpeed.TextChanged += new System.EventHandler(this.environmentTextBoxes_TextChanged);
            // 
            // label_EnvironmentPropertiesTitle
            // 
            this.label_EnvironmentPropertiesTitle.AutoSize = true;
            this.label_EnvironmentPropertiesTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_EnvironmentPropertiesTitle.Location = new System.Drawing.Point(253, 15);
            this.label_EnvironmentPropertiesTitle.Name = "label_EnvironmentPropertiesTitle";
            this.label_EnvironmentPropertiesTitle.Size = new System.Drawing.Size(137, 12);
            this.label_EnvironmentPropertiesTitle.TabIndex = 65;
            this.label_EnvironmentPropertiesTitle.Text = "Environment Properties";
            // 
            // label_Env_WindSpeedTitle
            // 
            this.label_Env_WindSpeedTitle.AutoSize = true;
            this.label_Env_WindSpeedTitle.Location = new System.Drawing.Point(79, 66);
            this.label_Env_WindSpeedTitle.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_Env_WindSpeedTitle.Name = "label_Env_WindSpeedTitle";
            this.label_Env_WindSpeedTitle.Size = new System.Drawing.Size(62, 12);
            this.label_Env_WindSpeedTitle.TabIndex = 70;
            this.label_Env_WindSpeedTitle.Text = "Wind Speed";
            // 
            // textBox_Env_WindDirection
            // 
            this.textBox_Env_WindDirection.Location = new System.Drawing.Point(157, 41);
            this.textBox_Env_WindDirection.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_Env_WindDirection.Name = "textBox_Env_WindDirection";
            this.textBox_Env_WindDirection.Size = new System.Drawing.Size(88, 22);
            this.textBox_Env_WindDirection.TabIndex = 69;
            this.textBox_Env_WindDirection.Text = "45.1";
            this.textBox_Env_WindDirection.TextChanged += new System.EventHandler(this.environmentTextBoxes_TextChanged);
            // 
            // label_Env_WindDirTitle
            // 
            this.label_Env_WindDirTitle.AutoSize = true;
            this.label_Env_WindDirTitle.Location = new System.Drawing.Point(79, 44);
            this.label_Env_WindDirTitle.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_Env_WindDirTitle.Name = "label_Env_WindDirTitle";
            this.label_Env_WindDirTitle.Size = new System.Drawing.Size(74, 12);
            this.label_Env_WindDirTitle.TabIndex = 68;
            this.label_Env_WindDirTitle.Text = "WindDirection";
            // 
            // label_Env_RainFallTitle
            // 
            this.label_Env_RainFallTitle.AutoSize = true;
            this.label_Env_RainFallTitle.Location = new System.Drawing.Point(81, 22);
            this.label_Env_RainFallTitle.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_Env_RainFallTitle.Name = "label_Env_RainFallTitle";
            this.label_Env_RainFallTitle.Size = new System.Drawing.Size(73, 12);
            this.label_Env_RainFallTitle.TabIndex = 67;
            this.label_Env_RainFallTitle.Text = "RainFall (mm)";
            // 
            // textBox_Env_RainFall
            // 
            this.textBox_Env_RainFall.Location = new System.Drawing.Point(158, 18);
            this.textBox_Env_RainFall.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_Env_RainFall.Name = "textBox_Env_RainFall";
            this.textBox_Env_RainFall.Size = new System.Drawing.Size(88, 22);
            this.textBox_Env_RainFall.TabIndex = 14;
            this.textBox_Env_RainFall.Text = "10";
            this.textBox_Env_RainFall.TextChanged += new System.EventHandler(this.environmentTextBoxes_TextChanged);
            // 
            // radioButton_Rainy
            // 
            this.radioButton_Rainy.AutoSize = true;
            this.radioButton_Rainy.Location = new System.Drawing.Point(17, 64);
            this.radioButton_Rainy.Name = "radioButton_Rainy";
            this.radioButton_Rainy.Size = new System.Drawing.Size(51, 16);
            this.radioButton_Rainy.TabIndex = 2;
            this.radioButton_Rainy.Text = "Rainy";
            this.radioButton_Rainy.UseVisualStyleBackColor = true;
            this.radioButton_Rainy.CheckedChanged += new System.EventHandler(this.environmentRadioButtons_CheckedChanged);
            // 
            // radioButton_Cloudy
            // 
            this.radioButton_Cloudy.AutoSize = true;
            this.radioButton_Cloudy.Location = new System.Drawing.Point(17, 42);
            this.radioButton_Cloudy.Name = "radioButton_Cloudy";
            this.radioButton_Cloudy.Size = new System.Drawing.Size(58, 16);
            this.radioButton_Cloudy.TabIndex = 1;
            this.radioButton_Cloudy.Text = "Cloudy";
            this.radioButton_Cloudy.UseVisualStyleBackColor = true;
            this.radioButton_Cloudy.CheckedChanged += new System.EventHandler(this.environmentRadioButtons_CheckedChanged);
            // 
            // radioButton_Sunny
            // 
            this.radioButton_Sunny.AutoSize = true;
            this.radioButton_Sunny.Checked = true;
            this.radioButton_Sunny.Location = new System.Drawing.Point(17, 20);
            this.radioButton_Sunny.Name = "radioButton_Sunny";
            this.radioButton_Sunny.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Sunny.TabIndex = 0;
            this.radioButton_Sunny.TabStop = true;
            this.radioButton_Sunny.Text = "Sunny";
            this.radioButton_Sunny.UseVisualStyleBackColor = true;
            this.radioButton_Sunny.CheckedChanged += new System.EventHandler(this.environmentRadioButtons_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 664);
            this.Controls.Add(this.groupBox_Environment);
            this.Controls.Add(this.groupBox_SimControl);
            this.Controls.Add(this.groupBox_STP);
            this.Controls.Add(this.groupBox_AgentCreation);
            this.Controls.Add(this.label_MouseStatus);
            this.Controls.Add(this.label_MouseStatusTitle);
            this.Controls.Add(this.label_LatLng);
            this.Controls.Add(this.gMapExplorer);
            this.Controls.Add(this.label_JoinedAgentListTitle);
            this.Controls.Add(this.listBoxJoinedAgentList);
            this.Controls.Add(this.label_AgentProperties);
            this.Controls.Add(this.label_SelectedAgentPropertiesTitle);
            this.Controls.Add(this.listBoxAgentList);
            this.Controls.Add(this.label_AgentListTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Agent-Based Model 2D";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPQueueDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPWorkitemNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPIdleTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPExecuteTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPThreadsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimSteps)).EndInit();
            this.groupBox_AgentCreation.ResumeLayout(false);
            this.groupBox_AgentCreation.PerformLayout();
            this.groupBox_STP.ResumeLayout(false);
            this.groupBox_STP.PerformLayout();
            this.groupBox_SimControl.ResumeLayout(false);
            this.groupBox_SimControl.PerformLayout();
            this.groupBox_Environment.ResumeLayout(false);
            this.groupBox_Environment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.Button button_SimStart;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.NumericUpDown numericUpDown_SimDelay;
        private System.Windows.Forms.ListBox listBoxAgentType;
        private System.Windows.Forms.TextBox textBox_K01;
        private System.Windows.Forms.Label label_AgentTypeTitle;
        private System.Windows.Forms.Label label_AgentPropertiesTitle;
        private System.Windows.Forms.TextBox textBox_V01;
        private System.Windows.Forms.TextBox textBox_V02;
        private System.Windows.Forms.TextBox textBox_K02;
        private System.Windows.Forms.TextBox textBox_V03;
        private System.Windows.Forms.TextBox textBox_K03;
        private System.Windows.Forms.Label label_AgentCoordTitle;
        private System.Windows.Forms.TextBox textBox_AgentLat;
        private System.Windows.Forms.Label label_CurrentLatTitle;
        private System.Windows.Forms.TextBox textBox_AgentLng;
        private System.Windows.Forms.Label label_AgentListTitle;
        private System.Windows.Forms.ListBox listBoxAgentList;
        private System.Windows.Forms.Label label_SelectedAgentPropertiesTitle;
        private System.Windows.Forms.Label label_AgentProperties;
        private System.Windows.Forms.TextBox textBox_V04;
        private System.Windows.Forms.TextBox textBox_K04;
        private System.Windows.Forms.TextBox textBox_V05;
        private System.Windows.Forms.TextBox textBox_K05;
        private System.Windows.Forms.TextBox textBox_V06;
        private System.Windows.Forms.TextBox textBox_K06;
        private System.Windows.Forms.ListBox listBoxAgentControl;
        private System.Windows.Forms.ListBox listBoxJoinedAgentList;
        private System.Windows.Forms.Label label_JoinedAgentListTitle;
        private GMap.NET.WindowsForms.GMapControl gMapExplorer;
        private System.Windows.Forms.Label label_LatLng;
        private System.Windows.Forms.Label label_CurrentLngTitle;
        private System.Windows.Forms.Label label_MouseStatusTitle;
        private System.Windows.Forms.Label label_MouseStatus;
        private System.Windows.Forms.Button button_CreateTP;
        private System.Windows.Forms.Label label_ThreadNumber;
        private System.Windows.Forms.Button button_CancelPool;
        private System.Windows.Forms.Button button_EndPool;
        private System.Windows.Forms.Button button_StepSim;
        private System.Windows.Forms.Label label_ExecuteTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_STPExecuteTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_STPThreadsNum;
        private System.Windows.Forms.NumericUpDown numericUpDown_STPIdleTime;
        private System.Windows.Forms.Label label_STPIdleTimeTitle;
        private System.Windows.Forms.Label label_CurrentStepTitle;
        private System.Windows.Forms.Label label_GodCurrentStep;
        private System.Windows.Forms.Label label_SimDelayTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_SimSteps;
        private System.Windows.Forms.NumericUpDown numericUpDown_STPQueueDelay;
        private System.Windows.Forms.NumericUpDown numericUpDown_STPWorkitemNum;
        private System.Windows.Forms.Label label_STPQueueDelayTitle;
        private System.Windows.Forms.Label label_STPWorkitemTitle;
        private System.Windows.Forms.Label label_SimStepsTitle;
        private System.Windows.Forms.GroupBox groupBox_AgentCreation;
        private System.Windows.Forms.GroupBox groupBox_STP;
        private System.Windows.Forms.GroupBox groupBox_SimControl;
        private System.Windows.Forms.ProgressBar progressBar_steps;
        private System.Windows.Forms.GroupBox groupBox_Environment;
        private System.Windows.Forms.RadioButton radioButton_Rainy;
        private System.Windows.Forms.RadioButton radioButton_Cloudy;
        private System.Windows.Forms.RadioButton radioButton_Sunny;
        private System.Windows.Forms.Label label_EnvironmentPropertiesTitle;
        private System.Windows.Forms.Label label_EnvironmentProperties;
        private System.Windows.Forms.TextBox textBox_Env_WindDirection;
        private System.Windows.Forms.Label label_Env_WindDirTitle;
        private System.Windows.Forms.Label label_Env_RainFallTitle;
        private System.Windows.Forms.TextBox textBox_Env_RainFall;
        private System.Windows.Forms.TextBox textBox_Env_WindSpeed;
        private System.Windows.Forms.Label label_Env_WindSpeedTitle;
        private System.Windows.Forms.Button button_SelectDLL;
    }
}

