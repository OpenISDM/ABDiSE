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
            this.label_AgentSubtypeTitle = new System.Windows.Forms.Label();
            this.groupBox_STPTest = new System.Windows.Forms.GroupBox();
            this.groupBox_SimControl = new System.Windows.Forms.GroupBox();
            this.progressBar_steps = new System.Windows.Forms.ProgressBar();
            this.button_LoadExperiment = new System.Windows.Forms.Button();
            this.button_SaveExperiment = new System.Windows.Forms.Button();
            this.groupBox_Environment = new System.Windows.Forms.GroupBox();
            this.textBox_ek9 = new System.Windows.Forms.TextBox();
            this.textBox_ev9 = new System.Windows.Forms.TextBox();
            this.button_saveEnvData = new System.Windows.Forms.Button();
            this.textBox_ek8 = new System.Windows.Forms.TextBox();
            this.textBox_ek2 = new System.Windows.Forms.TextBox();
            this.textBox_ek3 = new System.Windows.Forms.TextBox();
            this.textBox_ek4 = new System.Windows.Forms.TextBox();
            this.textBox_ek5 = new System.Windows.Forms.TextBox();
            this.textBox_ek6 = new System.Windows.Forms.TextBox();
            this.textBox_ek7 = new System.Windows.Forms.TextBox();
            this.textBox_ek1 = new System.Windows.Forms.TextBox();
            this.textBox_ev8 = new System.Windows.Forms.TextBox();
            this.textBox_ev2 = new System.Windows.Forms.TextBox();
            this.textBox_ev3 = new System.Windows.Forms.TextBox();
            this.textBox_ev4 = new System.Windows.Forms.TextBox();
            this.textBox_ev5 = new System.Windows.Forms.TextBox();
            this.comboBox_MapProvider = new System.Windows.Forms.ComboBox();
            this.textBox_ev6 = new System.Windows.Forms.TextBox();
            this.textBox_ev7 = new System.Windows.Forms.TextBox();
            this.textBox_ev1 = new System.Windows.Forms.TextBox();
            this.label_MapZoom = new System.Windows.Forms.Label();
            this.button_FullScreen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPQueueDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPWorkitemNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPIdleTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPExecuteTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_STPThreadsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SimSteps)).BeginInit();
            this.groupBox_AgentCreation.SuspendLayout();
            this.groupBox_STPTest.SuspendLayout();
            this.groupBox_SimControl.SuspendLayout();
            this.groupBox_Environment.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Create
            // 
            this.button_Create.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Create.Location = new System.Drawing.Point(168, 380);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(136, 39);
            this.button_Create.TabIndex = 1;
            this.button_Create.Text = "Create New Agent";
            this.button_Create.UseVisualStyleBackColor = true;
            this.button_Create.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // button_SimStart
            // 
            this.button_SimStart.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SimStart.Location = new System.Drawing.Point(289, 19);
            this.button_SimStart.Name = "button_SimStart";
            this.button_SimStart.Size = new System.Drawing.Size(146, 50);
            this.button_SimStart.TabIndex = 3;
            this.button_SimStart.Text = "Simulate N Steps";
            this.button_SimStart.UseVisualStyleBackColor = true;
            this.button_SimStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // numericUpDown_SimDelay
            // 
            this.numericUpDown_SimDelay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_SimDelay.Location = new System.Drawing.Point(228, 46);
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
            this.listBoxAgentType.Location = new System.Drawing.Point(12, 33);
            this.listBoxAgentType.Name = "listBoxAgentType";
            this.listBoxAgentType.Size = new System.Drawing.Size(142, 100);
            this.listBoxAgentType.TabIndex = 6;
            this.listBoxAgentType.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentType_SelectedIndexChanged);
            // 
            // textBox_K01
            // 
            this.textBox_K01.Location = new System.Drawing.Point(12, 178);
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
            this.label_AgentTypeTitle.Location = new System.Drawing.Point(11, 18);
            this.label_AgentTypeTitle.Name = "label_AgentTypeTitle";
            this.label_AgentTypeTitle.Size = new System.Drawing.Size(70, 12);
            this.label_AgentTypeTitle.TabIndex = 8;
            this.label_AgentTypeTitle.Text = "Agent Type";
            // 
            // label_AgentPropertiesTitle
            // 
            this.label_AgentPropertiesTitle.AutoSize = true;
            this.label_AgentPropertiesTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentPropertiesTitle.Location = new System.Drawing.Point(8, 159);
            this.label_AgentPropertiesTitle.Name = "label_AgentPropertiesTitle";
            this.label_AgentPropertiesTitle.Size = new System.Drawing.Size(126, 12);
            this.label_AgentPropertiesTitle.TabIndex = 9;
            this.label_AgentPropertiesTitle.Text = "New Agent Properties";
            // 
            // textBox_V01
            // 
            this.textBox_V01.Location = new System.Drawing.Point(159, 178);
            this.textBox_V01.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V01.Name = "textBox_V01";
            this.textBox_V01.Size = new System.Drawing.Size(144, 22);
            this.textBox_V01.TabIndex = 10;
            // 
            // textBox_V02
            // 
            this.textBox_V02.Location = new System.Drawing.Point(159, 202);
            this.textBox_V02.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V02.Name = "textBox_V02";
            this.textBox_V02.Size = new System.Drawing.Size(144, 22);
            this.textBox_V02.TabIndex = 12;
            // 
            // textBox_K02
            // 
            this.textBox_K02.Location = new System.Drawing.Point(12, 202);
            this.textBox_K02.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K02.Name = "textBox_K02";
            this.textBox_K02.Size = new System.Drawing.Size(141, 22);
            this.textBox_K02.TabIndex = 11;
            // 
            // textBox_V03
            // 
            this.textBox_V03.Location = new System.Drawing.Point(159, 226);
            this.textBox_V03.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V03.Name = "textBox_V03";
            this.textBox_V03.Size = new System.Drawing.Size(144, 22);
            this.textBox_V03.TabIndex = 14;
            // 
            // textBox_K03
            // 
            this.textBox_K03.Location = new System.Drawing.Point(12, 226);
            this.textBox_K03.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K03.Name = "textBox_K03";
            this.textBox_K03.Size = new System.Drawing.Size(141, 22);
            this.textBox_K03.TabIndex = 13;
            // 
            // textBox_AgentLat
            // 
            this.textBox_AgentLat.Location = new System.Drawing.Point(121, 327);
            this.textBox_AgentLat.Name = "textBox_AgentLat";
            this.textBox_AgentLat.Size = new System.Drawing.Size(183, 22);
            this.textBox_AgentLat.TabIndex = 16;
            this.textBox_AgentLat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label_CurrentLatTitle
            // 
            this.label_CurrentLatTitle.AutoSize = true;
            this.label_CurrentLatTitle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_CurrentLatTitle.Location = new System.Drawing.Point(10, 332);
            this.label_CurrentLatTitle.Name = "label_CurrentLatTitle";
            this.label_CurrentLatTitle.Size = new System.Drawing.Size(88, 17);
            this.label_CurrentLatTitle.TabIndex = 19;
            this.label_CurrentLatTitle.Text = "Lat (Latitude)";
            // 
            // textBox_AgentLng
            // 
            this.textBox_AgentLng.Location = new System.Drawing.Point(121, 352);
            this.textBox_AgentLng.Name = "textBox_AgentLng";
            this.textBox_AgentLng.Size = new System.Drawing.Size(183, 22);
            this.textBox_AgentLng.TabIndex = 20;
            this.textBox_AgentLng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_C0X_KeyPress);
            // 
            // label_AgentListTitle
            // 
            this.label_AgentListTitle.AutoSize = true;
            this.label_AgentListTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentListTitle.Location = new System.Drawing.Point(772, 10);
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
            this.listBoxAgentList.Location = new System.Drawing.Point(772, 25);
            this.listBoxAgentList.Name = "listBoxAgentList";
            this.listBoxAgentList.Size = new System.Drawing.Size(200, 112);
            this.listBoxAgentList.TabIndex = 31;
            this.listBoxAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentList_SelectedIndexChanged);
            // 
            // label_SelectedAgentPropertiesTitle
            // 
            this.label_SelectedAgentPropertiesTitle.AutoSize = true;
            this.label_SelectedAgentPropertiesTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SelectedAgentPropertiesTitle.Location = new System.Drawing.Point(772, 277);
            this.label_SelectedAgentPropertiesTitle.Name = "label_SelectedAgentPropertiesTitle";
            this.label_SelectedAgentPropertiesTitle.Size = new System.Drawing.Size(98, 12);
            this.label_SelectedAgentPropertiesTitle.TabIndex = 32;
            this.label_SelectedAgentPropertiesTitle.Text = "Agent Properties";
            // 
            // label_AgentProperties
            // 
            this.label_AgentProperties.AutoSize = true;
            this.label_AgentProperties.Location = new System.Drawing.Point(772, 298);
            this.label_AgentProperties.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_AgentProperties.Name = "label_AgentProperties";
            this.label_AgentProperties.Size = new System.Drawing.Size(82, 12);
            this.label_AgentProperties.TabIndex = 34;
            this.label_AgentProperties.Text = "Agent Properties";
            // 
            // textBox_V04
            // 
            this.textBox_V04.Location = new System.Drawing.Point(159, 250);
            this.textBox_V04.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V04.Name = "textBox_V04";
            this.textBox_V04.Size = new System.Drawing.Size(144, 22);
            this.textBox_V04.TabIndex = 37;
            // 
            // textBox_K04
            // 
            this.textBox_K04.Location = new System.Drawing.Point(12, 250);
            this.textBox_K04.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K04.Name = "textBox_K04";
            this.textBox_K04.Size = new System.Drawing.Size(141, 22);
            this.textBox_K04.TabIndex = 36;
            // 
            // textBox_V05
            // 
            this.textBox_V05.Location = new System.Drawing.Point(159, 274);
            this.textBox_V05.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V05.Name = "textBox_V05";
            this.textBox_V05.Size = new System.Drawing.Size(144, 22);
            this.textBox_V05.TabIndex = 39;
            // 
            // textBox_K05
            // 
            this.textBox_K05.Location = new System.Drawing.Point(12, 274);
            this.textBox_K05.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K05.Name = "textBox_K05";
            this.textBox_K05.Size = new System.Drawing.Size(141, 22);
            this.textBox_K05.TabIndex = 38;
            // 
            // textBox_V06
            // 
            this.textBox_V06.Location = new System.Drawing.Point(159, 298);
            this.textBox_V06.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_V06.Name = "textBox_V06";
            this.textBox_V06.Size = new System.Drawing.Size(144, 22);
            this.textBox_V06.TabIndex = 41;
            // 
            // textBox_K06
            // 
            this.textBox_K06.Location = new System.Drawing.Point(12, 298);
            this.textBox_K06.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_K06.Name = "textBox_K06";
            this.textBox_K06.Size = new System.Drawing.Size(141, 22);
            this.textBox_K06.TabIndex = 40;
            // 
            // listBoxAgentControl
            // 
            this.listBoxAgentControl.FormattingEnabled = true;
            this.listBoxAgentControl.ItemHeight = 12;
            this.listBoxAgentControl.Location = new System.Drawing.Point(159, 33);
            this.listBoxAgentControl.Name = "listBoxAgentControl";
            this.listBoxAgentControl.Size = new System.Drawing.Size(144, 124);
            this.listBoxAgentControl.TabIndex = 42;
            this.listBoxAgentControl.SelectedIndexChanged += new System.EventHandler(this.listBoxAgentControl_SelectedIndexChanged);
            // 
            // listBoxJoinedAgentList
            // 
            this.listBoxJoinedAgentList.FormattingEnabled = true;
            this.listBoxJoinedAgentList.HorizontalScrollbar = true;
            this.listBoxJoinedAgentList.ItemHeight = 12;
            this.listBoxJoinedAgentList.Location = new System.Drawing.Point(772, 165);
            this.listBoxJoinedAgentList.Name = "listBoxJoinedAgentList";
            this.listBoxJoinedAgentList.Size = new System.Drawing.Size(200, 100);
            this.listBoxJoinedAgentList.TabIndex = 44;
            this.listBoxJoinedAgentList.SelectedIndexChanged += new System.EventHandler(this.listBoxJoinedAgentList_SelectedIndexChanged);
            // 
            // label_JoinedAgentListTitle
            // 
            this.label_JoinedAgentListTitle.AutoSize = true;
            this.label_JoinedAgentListTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_JoinedAgentListTitle.Location = new System.Drawing.Point(772, 145);
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
            this.gMapExplorer.Size = new System.Drawing.Size(443, 524);
            this.gMapExplorer.TabIndex = 46;
            this.gMapExplorer.Zoom = 0D;
            this.gMapExplorer.Load += new System.EventHandler(this.gMapExplorer_Load);
            this.gMapExplorer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMapExplorer_MouseDoubleClick);
            this.gMapExplorer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapExplorer_MouseMove);
            // 
            // label_LatLng
            // 
            this.label_LatLng.AutoSize = true;
            this.label_LatLng.Location = new System.Drawing.Point(321, 542);
            this.label_LatLng.Name = "label_LatLng";
            this.label_LatLng.Size = new System.Drawing.Size(61, 12);
            this.label_LatLng.TabIndex = 47;
            this.label_LatLng.Text = "labelLatLng";
            // 
            // label_CurrentLngTitle
            // 
            this.label_CurrentLngTitle.AutoSize = true;
            this.label_CurrentLngTitle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_CurrentLngTitle.Location = new System.Drawing.Point(9, 352);
            this.label_CurrentLngTitle.Name = "label_CurrentLngTitle";
            this.label_CurrentLngTitle.Size = new System.Drawing.Size(104, 17);
            this.label_CurrentLngTitle.TabIndex = 19;
            this.label_CurrentLngTitle.Text = "Lng (Longitude)";
            // 
            // button_CreateTP
            // 
            this.button_CreateTP.Location = new System.Drawing.Point(11, 185);
            this.button_CreateTP.Name = "button_CreateTP";
            this.button_CreateTP.Size = new System.Drawing.Size(74, 36);
            this.button_CreateTP.TabIndex = 50;
            this.button_CreateTP.Text = "Create ThreadPool";
            this.button_CreateTP.UseVisualStyleBackColor = true;
            this.button_CreateTP.Click += new System.EventHandler(this.buttonCreateTP_Click);
            // 
            // label_ThreadNumber
            // 
            this.label_ThreadNumber.AutoSize = true;
            this.label_ThreadNumber.Location = new System.Drawing.Point(22, 104);
            this.label_ThreadNumber.Name = "label_ThreadNumber";
            this.label_ThreadNumber.Size = new System.Drawing.Size(79, 12);
            this.label_ThreadNumber.TabIndex = 52;
            this.label_ThreadNumber.Text = "Thread Number";
            this.label_ThreadNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_CancelPool
            // 
            this.button_CancelPool.Location = new System.Drawing.Point(11, 219);
            this.button_CancelPool.Name = "button_CancelPool";
            this.button_CancelPool.Size = new System.Drawing.Size(74, 21);
            this.button_CancelPool.TabIndex = 53;
            this.button_CancelPool.Text = "CancelPool";
            this.button_CancelPool.UseVisualStyleBackColor = true;
            this.button_CancelPool.Click += new System.EventHandler(this.button_CancelPool_Click);
            // 
            // button_EndPool
            // 
            this.button_EndPool.Location = new System.Drawing.Point(11, 237);
            this.button_EndPool.Name = "button_EndPool";
            this.button_EndPool.Size = new System.Drawing.Size(74, 22);
            this.button_EndPool.TabIndex = 53;
            this.button_EndPool.Text = "EndPool";
            this.button_EndPool.UseVisualStyleBackColor = true;
            this.button_EndPool.Click += new System.EventHandler(this.button_EndPool_Click);
            // 
            // numericUpDown_STPQueueDelay
            // 
            this.numericUpDown_STPQueueDelay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPQueueDelay.Location = new System.Drawing.Point(11, 161);
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
            this.numericUpDown_STPQueueDelay.Size = new System.Drawing.Size(74, 22);
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
            this.numericUpDown_STPWorkitemNum.Location = new System.Drawing.Point(11, 44);
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
            this.numericUpDown_STPWorkitemNum.Size = new System.Drawing.Size(74, 22);
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
            this.label_STPQueueDelayTitle.Location = new System.Drawing.Point(11, 146);
            this.label_STPQueueDelayTitle.Name = "label_STPQueueDelayTitle";
            this.label_STPQueueDelayTitle.Size = new System.Drawing.Size(80, 12);
            this.label_STPQueueDelayTitle.TabIndex = 61;
            this.label_STPQueueDelayTitle.Text = "Queueing Delay";
            this.label_STPQueueDelayTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_STPWorkitemTitle
            // 
            this.label_STPWorkitemTitle.AutoSize = true;
            this.label_STPWorkitemTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_STPWorkitemTitle.Location = new System.Drawing.Point(11, 17);
            this.label_STPWorkitemTitle.Name = "label_STPWorkitemTitle";
            this.label_STPWorkitemTitle.Size = new System.Drawing.Size(74, 24);
            this.label_STPWorkitemTitle.TabIndex = 60;
            this.label_STPWorkitemTitle.Text = "Test Workitem\r\nNumber";
            this.label_STPWorkitemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_STPIdleTime
            // 
            this.numericUpDown_STPIdleTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPIdleTime.Location = new System.Drawing.Point(11, 121);
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
            this.numericUpDown_STPIdleTime.Size = new System.Drawing.Size(74, 22);
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
            this.label_STPIdleTimeTitle.Location = new System.Drawing.Point(11, 106);
            this.label_STPIdleTimeTitle.Name = "label_STPIdleTimeTitle";
            this.label_STPIdleTimeTitle.Size = new System.Drawing.Size(86, 12);
            this.label_STPIdleTimeTitle.TabIndex = 58;
            this.label_STPIdleTimeTitle.Text = "Thread Idle Time";
            this.label_STPIdleTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_STPExecuteTime
            // 
            this.numericUpDown_STPExecuteTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_STPExecuteTime.Location = new System.Drawing.Point(11, 81);
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
            this.numericUpDown_STPExecuteTime.Size = new System.Drawing.Size(74, 22);
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
            this.numericUpDown_STPThreadsNum.Location = new System.Drawing.Point(107, 98);
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
            this.numericUpDown_STPThreadsNum.Size = new System.Drawing.Size(34, 22);
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
            this.label_ExecuteTime.Location = new System.Drawing.Point(11, 68);
            this.label_ExecuteTime.Name = "label_ExecuteTime";
            this.label_ExecuteTime.Size = new System.Drawing.Size(83, 12);
            this.label_ExecuteTime.TabIndex = 54;
            this.label_ExecuteTime.Text = "Execution Times";
            this.label_ExecuteTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_StepSim
            // 
            this.button_StepSim.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_StepSim.Location = new System.Drawing.Point(18, 44);
            this.button_StepSim.Name = "button_StepSim";
            this.button_StepSim.Size = new System.Drawing.Size(123, 48);
            this.button_StepSim.TabIndex = 55;
            this.button_StepSim.Text = "Simulate One Step";
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
            this.label_GodCurrentStep.Location = new System.Drawing.Point(112, 23);
            this.label_GodCurrentStep.Name = "label_GodCurrentStep";
            this.label_GodCurrentStep.Size = new System.Drawing.Size(12, 12);
            this.label_GodCurrentStep.TabIndex = 57;
            this.label_GodCurrentStep.Text = "0";
            // 
            // label_SimDelayTitle
            // 
            this.label_SimDelayTitle.AutoSize = true;
            this.label_SimDelayTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SimDelayTitle.Location = new System.Drawing.Point(156, 44);
            this.label_SimDelayTitle.Name = "label_SimDelayTitle";
            this.label_SimDelayTitle.Size = new System.Drawing.Size(66, 24);
            this.label_SimDelayTitle.TabIndex = 58;
            this.label_SimDelayTitle.Text = "Step\r\nDelay (ms)";
            // 
            // numericUpDown_SimSteps
            // 
            this.numericUpDown_SimSteps.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.numericUpDown_SimSteps.Location = new System.Drawing.Point(197, 19);
            this.numericUpDown_SimSteps.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDown_SimSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_SimSteps.Name = "numericUpDown_SimSteps";
            this.numericUpDown_SimSteps.Size = new System.Drawing.Size(37, 22);
            this.numericUpDown_SimSteps.TabIndex = 59;
            this.numericUpDown_SimSteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_SimSteps.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_SimSteps.ValueChanged += new System.EventHandler(this.numericUpDown_SimSteps_ValueChanged);
            // 
            // label_SimStepsTitle
            // 
            this.label_SimStepsTitle.AutoSize = true;
            this.label_SimStepsTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SimStepsTitle.Location = new System.Drawing.Point(156, 23);
            this.label_SimStepsTitle.Name = "label_SimStepsTitle";
            this.label_SimStepsTitle.Size = new System.Drawing.Size(38, 12);
            this.label_SimStepsTitle.TabIndex = 60;
            this.label_SimStepsTitle.Text = "Steps:";
            // 
            // groupBox_AgentCreation
            // 
            this.groupBox_AgentCreation.Controls.Add(this.button_SelectDLL);
            this.groupBox_AgentCreation.Controls.Add(this.label_AgentSubtypeTitle);
            this.groupBox_AgentCreation.Controls.Add(this.button_Create);
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
            this.groupBox_AgentCreation.Controls.Add(this.label_CurrentLngTitle);
            this.groupBox_AgentCreation.Controls.Add(this.label_CurrentLatTitle);
            this.groupBox_AgentCreation.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_AgentCreation.Location = new System.Drawing.Point(9, 4);
            this.groupBox_AgentCreation.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox_AgentCreation.Name = "groupBox_AgentCreation";
            this.groupBox_AgentCreation.Size = new System.Drawing.Size(310, 425);
            this.groupBox_AgentCreation.TabIndex = 61;
            this.groupBox_AgentCreation.TabStop = false;
            this.groupBox_AgentCreation.Text = "Agent Creation";
            // 
            // button_SelectDLL
            // 
            this.button_SelectDLL.Location = new System.Drawing.Point(12, 136);
            this.button_SelectDLL.Name = "button_SelectDLL";
            this.button_SelectDLL.Size = new System.Drawing.Size(142, 21);
            this.button_SelectDLL.TabIndex = 43;
            this.button_SelectDLL.Text = "Select Agent Types (DLL)";
            this.button_SelectDLL.UseVisualStyleBackColor = true;
            this.button_SelectDLL.Click += new System.EventHandler(this.button_SelectDLL_Click);
            // 
            // label_AgentSubtypeTitle
            // 
            this.label_AgentSubtypeTitle.AutoSize = true;
            this.label_AgentSubtypeTitle.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_AgentSubtypeTitle.Location = new System.Drawing.Point(157, 18);
            this.label_AgentSubtypeTitle.Name = "label_AgentSubtypeTitle";
            this.label_AgentSubtypeTitle.Size = new System.Drawing.Size(87, 12);
            this.label_AgentSubtypeTitle.TabIndex = 8;
            this.label_AgentSubtypeTitle.Text = "Agent Subtype";
            // 
            // groupBox_STPTest
            // 
            this.groupBox_STPTest.Controls.Add(this.numericUpDown_STPQueueDelay);
            this.groupBox_STPTest.Controls.Add(this.numericUpDown_STPWorkitemNum);
            this.groupBox_STPTest.Controls.Add(this.button_CancelPool);
            this.groupBox_STPTest.Controls.Add(this.label_STPQueueDelayTitle);
            this.groupBox_STPTest.Controls.Add(this.button_CreateTP);
            this.groupBox_STPTest.Controls.Add(this.label_STPWorkitemTitle);
            this.groupBox_STPTest.Controls.Add(this.button_EndPool);
            this.groupBox_STPTest.Controls.Add(this.numericUpDown_STPIdleTime);
            this.groupBox_STPTest.Controls.Add(this.label_ExecuteTime);
            this.groupBox_STPTest.Controls.Add(this.label_STPIdleTimeTitle);
            this.groupBox_STPTest.Controls.Add(this.numericUpDown_STPExecuteTime);
            this.groupBox_STPTest.Location = new System.Drawing.Point(9, 433);
            this.groupBox_STPTest.Name = "groupBox_STPTest";
            this.groupBox_STPTest.Size = new System.Drawing.Size(121, 267);
            this.groupBox_STPTest.TabIndex = 62;
            this.groupBox_STPTest.TabStop = false;
            this.groupBox_STPTest.Text = "ThreadPool Testing";
            // 
            // groupBox_SimControl
            // 
            this.groupBox_SimControl.Controls.Add(this.numericUpDown_STPThreadsNum);
            this.groupBox_SimControl.Controls.Add(this.label_ThreadNumber);
            this.groupBox_SimControl.Controls.Add(this.progressBar_steps);
            this.groupBox_SimControl.Controls.Add(this.button_LoadExperiment);
            this.groupBox_SimControl.Controls.Add(this.button_SaveExperiment);
            this.groupBox_SimControl.Controls.Add(this.button_SimStart);
            this.groupBox_SimControl.Controls.Add(this.label_SimStepsTitle);
            this.groupBox_SimControl.Controls.Add(this.numericUpDown_SimDelay);
            this.groupBox_SimControl.Controls.Add(this.numericUpDown_SimSteps);
            this.groupBox_SimControl.Controls.Add(this.label_SimDelayTitle);
            this.groupBox_SimControl.Controls.Add(this.button_StepSim);
            this.groupBox_SimControl.Controls.Add(this.label_GodCurrentStep);
            this.groupBox_SimControl.Controls.Add(this.label_CurrentStepTitle);
            this.groupBox_SimControl.Location = new System.Drawing.Point(323, 564);
            this.groupBox_SimControl.Name = "groupBox_SimControl";
            this.groupBox_SimControl.Size = new System.Drawing.Size(443, 130);
            this.groupBox_SimControl.TabIndex = 63;
            this.groupBox_SimControl.TabStop = false;
            this.groupBox_SimControl.Text = "Simulation Controller";
            // 
            // progressBar_steps
            // 
            this.progressBar_steps.Location = new System.Drawing.Point(158, 74);
            this.progressBar_steps.Name = "progressBar_steps";
            this.progressBar_steps.Size = new System.Drawing.Size(277, 10);
            this.progressBar_steps.TabIndex = 61;
            // 
            // button_LoadExperiment
            // 
            this.button_LoadExperiment.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_LoadExperiment.Location = new System.Drawing.Point(302, 92);
            this.button_LoadExperiment.Name = "button_LoadExperiment";
            this.button_LoadExperiment.Size = new System.Drawing.Size(133, 30);
            this.button_LoadExperiment.TabIndex = 72;
            this.button_LoadExperiment.Text = "Load Experiment";
            this.button_LoadExperiment.UseVisualStyleBackColor = true;
            this.button_LoadExperiment.Click += new System.EventHandler(this.button_LoadExperiment_Click);
            // 
            // button_SaveExperiment
            // 
            this.button_SaveExperiment.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SaveExperiment.Location = new System.Drawing.Point(157, 92);
            this.button_SaveExperiment.Name = "button_SaveExperiment";
            this.button_SaveExperiment.Size = new System.Drawing.Size(139, 30);
            this.button_SaveExperiment.TabIndex = 72;
            this.button_SaveExperiment.Text = "Save Experiment";
            this.button_SaveExperiment.UseVisualStyleBackColor = true;
            this.button_SaveExperiment.Click += new System.EventHandler(this.button_SaveExperiment_Click);
            // 
            // groupBox_Environment
            // 
            this.groupBox_Environment.Controls.Add(this.textBox_ek9);
            this.groupBox_Environment.Controls.Add(this.textBox_ev9);
            this.groupBox_Environment.Controls.Add(this.button_saveEnvData);
            this.groupBox_Environment.Controls.Add(this.textBox_ek8);
            this.groupBox_Environment.Controls.Add(this.textBox_ek2);
            this.groupBox_Environment.Controls.Add(this.textBox_ek3);
            this.groupBox_Environment.Controls.Add(this.textBox_ek4);
            this.groupBox_Environment.Controls.Add(this.textBox_ek5);
            this.groupBox_Environment.Controls.Add(this.textBox_ek6);
            this.groupBox_Environment.Controls.Add(this.textBox_ek7);
            this.groupBox_Environment.Controls.Add(this.textBox_ek1);
            this.groupBox_Environment.Controls.Add(this.textBox_ev8);
            this.groupBox_Environment.Controls.Add(this.textBox_ev2);
            this.groupBox_Environment.Controls.Add(this.textBox_ev3);
            this.groupBox_Environment.Controls.Add(this.textBox_ev4);
            this.groupBox_Environment.Controls.Add(this.textBox_ev5);
            this.groupBox_Environment.Controls.Add(this.comboBox_MapProvider);
            this.groupBox_Environment.Controls.Add(this.textBox_ev6);
            this.groupBox_Environment.Controls.Add(this.textBox_ev7);
            this.groupBox_Environment.Controls.Add(this.textBox_ev1);
            this.groupBox_Environment.Location = new System.Drawing.Point(130, 433);
            this.groupBox_Environment.Name = "groupBox_Environment";
            this.groupBox_Environment.Size = new System.Drawing.Size(182, 267);
            this.groupBox_Environment.TabIndex = 64;
            this.groupBox_Environment.TabStop = false;
            this.groupBox_Environment.Text = "Environment Controller";
            // 
            // textBox_ek9
            // 
            this.textBox_ek9.Location = new System.Drawing.Point(6, 214);
            this.textBox_ek9.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek9.Name = "textBox_ek9";
            this.textBox_ek9.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek9.TabIndex = 87;
            this.textBox_ek9.Text = "key";
            // 
            // textBox_ev9
            // 
            this.textBox_ev9.Location = new System.Drawing.Point(109, 214);
            this.textBox_ev9.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev9.Name = "textBox_ev9";
            this.textBox_ev9.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev9.TabIndex = 86;
            this.textBox_ev9.Text = "0";
            // 
            // button_saveEnvData
            // 
            this.button_saveEnvData.Location = new System.Drawing.Point(38, 240);
            this.button_saveEnvData.Name = "button_saveEnvData";
            this.button_saveEnvData.Size = new System.Drawing.Size(138, 21);
            this.button_saveEnvData.TabIndex = 64;
            this.button_saveEnvData.Text = "Save to Environment";
            this.button_saveEnvData.UseVisualStyleBackColor = true;
            this.button_saveEnvData.Click += new System.EventHandler(this.button_saveEnvData_Click);
            // 
            // textBox_ek8
            // 
            this.textBox_ek8.Location = new System.Drawing.Point(6, 194);
            this.textBox_ek8.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek8.Name = "textBox_ek8";
            this.textBox_ek8.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek8.TabIndex = 85;
            this.textBox_ek8.Text = "key";
            // 
            // textBox_ek2
            // 
            this.textBox_ek2.Location = new System.Drawing.Point(6, 62);
            this.textBox_ek2.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek2.Name = "textBox_ek2";
            this.textBox_ek2.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek2.TabIndex = 84;
            this.textBox_ek2.Text = "key";
            // 
            // textBox_ek3
            // 
            this.textBox_ek3.Location = new System.Drawing.Point(6, 84);
            this.textBox_ek3.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek3.Name = "textBox_ek3";
            this.textBox_ek3.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek3.TabIndex = 83;
            this.textBox_ek3.Text = "key";
            // 
            // textBox_ek4
            // 
            this.textBox_ek4.Location = new System.Drawing.Point(6, 106);
            this.textBox_ek4.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek4.Name = "textBox_ek4";
            this.textBox_ek4.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek4.TabIndex = 82;
            this.textBox_ek4.Text = "key";
            // 
            // textBox_ek5
            // 
            this.textBox_ek5.Location = new System.Drawing.Point(6, 128);
            this.textBox_ek5.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek5.Name = "textBox_ek5";
            this.textBox_ek5.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek5.TabIndex = 81;
            this.textBox_ek5.Text = "key";
            // 
            // textBox_ek6
            // 
            this.textBox_ek6.Location = new System.Drawing.Point(6, 150);
            this.textBox_ek6.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek6.Name = "textBox_ek6";
            this.textBox_ek6.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek6.TabIndex = 80;
            this.textBox_ek6.Text = "key";
            // 
            // textBox_ek7
            // 
            this.textBox_ek7.Location = new System.Drawing.Point(6, 172);
            this.textBox_ek7.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek7.Name = "textBox_ek7";
            this.textBox_ek7.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek7.TabIndex = 79;
            this.textBox_ek7.Text = "key";
            // 
            // textBox_ek1
            // 
            this.textBox_ek1.Location = new System.Drawing.Point(6, 40);
            this.textBox_ek1.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ek1.Name = "textBox_ek1";
            this.textBox_ek1.Size = new System.Drawing.Size(101, 22);
            this.textBox_ek1.TabIndex = 78;
            this.textBox_ek1.Text = "key";
            // 
            // textBox_ev8
            // 
            this.textBox_ev8.Location = new System.Drawing.Point(109, 194);
            this.textBox_ev8.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev8.Name = "textBox_ev8";
            this.textBox_ev8.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev8.TabIndex = 77;
            this.textBox_ev8.Text = "0";
            // 
            // textBox_ev2
            // 
            this.textBox_ev2.Location = new System.Drawing.Point(109, 62);
            this.textBox_ev2.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev2.Name = "textBox_ev2";
            this.textBox_ev2.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev2.TabIndex = 76;
            this.textBox_ev2.Text = "0";
            // 
            // textBox_ev3
            // 
            this.textBox_ev3.Location = new System.Drawing.Point(109, 84);
            this.textBox_ev3.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev3.Name = "textBox_ev3";
            this.textBox_ev3.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev3.TabIndex = 75;
            this.textBox_ev3.Text = "0";
            // 
            // textBox_ev4
            // 
            this.textBox_ev4.Location = new System.Drawing.Point(109, 106);
            this.textBox_ev4.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev4.Name = "textBox_ev4";
            this.textBox_ev4.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev4.TabIndex = 74;
            this.textBox_ev4.Text = "0";
            // 
            // textBox_ev5
            // 
            this.textBox_ev5.Location = new System.Drawing.Point(109, 128);
            this.textBox_ev5.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev5.Name = "textBox_ev5";
            this.textBox_ev5.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev5.TabIndex = 73;
            this.textBox_ev5.Text = "0";
            // 
            // comboBox_MapProvider
            // 
            this.comboBox_MapProvider.FormattingEnabled = true;
            this.comboBox_MapProvider.Location = new System.Drawing.Point(6, 18);
            this.comboBox_MapProvider.Name = "comboBox_MapProvider";
            this.comboBox_MapProvider.Size = new System.Drawing.Size(170, 20);
            this.comboBox_MapProvider.TabIndex = 72;
            this.comboBox_MapProvider.Text = "GMapProvider";
            this.comboBox_MapProvider.SelectedIndexChanged += new System.EventHandler(this.comboBox_MapProvider_SelectedIndexChanged);
            // 
            // textBox_ev6
            // 
            this.textBox_ev6.Location = new System.Drawing.Point(109, 150);
            this.textBox_ev6.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev6.Name = "textBox_ev6";
            this.textBox_ev6.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev6.TabIndex = 71;
            this.textBox_ev6.Text = "0";
            // 
            // textBox_ev7
            // 
            this.textBox_ev7.Location = new System.Drawing.Point(109, 172);
            this.textBox_ev7.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev7.Name = "textBox_ev7";
            this.textBox_ev7.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev7.TabIndex = 69;
            this.textBox_ev7.Text = "0";
            // 
            // textBox_ev1
            // 
            this.textBox_ev1.Location = new System.Drawing.Point(109, 40);
            this.textBox_ev1.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ev1.Name = "textBox_ev1";
            this.textBox_ev1.Size = new System.Drawing.Size(67, 22);
            this.textBox_ev1.TabIndex = 14;
            this.textBox_ev1.Text = "0";
            // 
            // label_MapZoom
            // 
            this.label_MapZoom.AutoSize = true;
            this.label_MapZoom.Location = new System.Drawing.Point(599, 542);
            this.label_MapZoom.Name = "label_MapZoom";
            this.label_MapZoom.Size = new System.Drawing.Size(60, 12);
            this.label_MapZoom.TabIndex = 67;
            this.label_MapZoom.Text = "MapZoom: ";
            // 
            // button_FullScreen
            // 
            this.button_FullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FullScreen.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_FullScreen.Location = new System.Drawing.Point(701, 539);
            this.button_FullScreen.Name = "button_FullScreen";
            this.button_FullScreen.Size = new System.Drawing.Size(65, 19);
            this.button_FullScreen.TabIndex = 68;
            this.button_FullScreen.Text = "FullScreen";
            this.button_FullScreen.UseVisualStyleBackColor = true;
            this.button_FullScreen.Click += new System.EventHandler(this.button_FullScreen_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 703);
            this.Controls.Add(this.button_FullScreen);
            this.Controls.Add(this.gMapExplorer);
            this.Controls.Add(this.label_MapZoom);
            this.Controls.Add(this.groupBox_Environment);
            this.Controls.Add(this.groupBox_SimControl);
            this.Controls.Add(this.groupBox_STPTest);
            this.Controls.Add(this.groupBox_AgentCreation);
            this.Controls.Add(this.label_LatLng);
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
            this.Text = "ABDiSE - An Agent-Based Disaster Simulation Environment";
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
            this.groupBox_STPTest.ResumeLayout(false);
            this.groupBox_STPTest.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox_STPTest;
        private System.Windows.Forms.GroupBox groupBox_SimControl;
        private System.Windows.Forms.ProgressBar progressBar_steps;
        private System.Windows.Forms.GroupBox groupBox_Environment;
        private System.Windows.Forms.TextBox textBox_ev7;
        private System.Windows.Forms.TextBox textBox_ev1;
        private System.Windows.Forms.TextBox textBox_ev6;
        private System.Windows.Forms.Button button_SelectDLL;
        private System.Windows.Forms.Button button_SaveExperiment;
        private System.Windows.Forms.Button button_LoadExperiment;
        private System.Windows.Forms.Label label_AgentSubtypeTitle;
        private System.Windows.Forms.Label label_MapZoom;
        private System.Windows.Forms.ComboBox comboBox_MapProvider;
        private System.Windows.Forms.Button button_FullScreen;
        private System.Windows.Forms.TextBox textBox_ek8;
        private System.Windows.Forms.TextBox textBox_ek2;
        private System.Windows.Forms.TextBox textBox_ek3;
        private System.Windows.Forms.TextBox textBox_ek4;
        private System.Windows.Forms.TextBox textBox_ek5;
        private System.Windows.Forms.TextBox textBox_ek6;
        private System.Windows.Forms.TextBox textBox_ek7;
        private System.Windows.Forms.TextBox textBox_ek1;
        private System.Windows.Forms.TextBox textBox_ev8;
        private System.Windows.Forms.TextBox textBox_ev2;
        private System.Windows.Forms.TextBox textBox_ev3;
        private System.Windows.Forms.TextBox textBox_ev4;
        private System.Windows.Forms.TextBox textBox_ev5;
        private System.Windows.Forms.Button button_saveEnvData;
        private System.Windows.Forms.TextBox textBox_ek9;
        private System.Windows.Forms.TextBox textBox_ev9;
    }
}

