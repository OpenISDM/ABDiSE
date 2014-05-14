/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    GUI.cs

    SVN $Revision: $

    Abstract:     GUI "MainWindow" (C# windows form) of ABDiSE
                  controls GUI and ABDiSE
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;

using ABDiSE;
using ABDiSE.Model.AgentClasses;
using ABDiSE.Controller;
using ABDiSE.Model;

using System.Diagnostics;
using System.Threading;


namespace ABDiSE.View
{

    #region 2.0 GUI

    /* 
     * public partial class MainWindow : Form
     * 
     * Description:
     *      MainWindow is the GUI of ABDiSE, controls gMap, listboxes, etc.
     *      
     */
    public partial class MainWindow : Form
    {
        // pointer to controller
        CoreController CoreController;

        //GMap.NET controls
        GMapOverlay OverlayMouse;
        GMapOverlay OverlayAgents;

        //GMap markers selection controls
        GMapMarker CurrentMouseOnMarker = null;
        GMapMarker SelectedMarker = null;

        #region BackgroundWorker

        //background worker
        private BackgroundWorker bw;

        private enum workerType { stepSim, createTPTest, stepsSim};

        private void initBackgroundWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int argument = (int)e.Argument;

            switch (argument)
            {
                
                case (int)workerType.stepsSim:
                    //stepsSimulation();
                    break;

                case (int)workerType.stepSim:
                    stepSimulation();
                    break;

                case (int)workerType.createTPTest:
                    createSTPTest();
                    break;
                    
                default:
                    MessageBox.Show("bw_DoWork switch type error");
                    break;
            }

            if ((bw.CancellationPending == true))
            {
                    e.Cancel = true;
            }

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar_steps.Value = e.ProgressPercentage;
            Console.WriteLine(e.ProgressPercentage.ToString());
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {
                Console.WriteLine("bw 取消!");
            }

            else if (!(e.Error == null))
            {
                Console.WriteLine("bw Error: " + e.Error.Message);
            }

            else
            {
                Console.WriteLine("bw 完成!");
            }

            button_StepSim.Enabled = true;

        }

        #endregion 

        /* 
         * public MainWindow(God god)
         * 
         * Description:
         *      Constructor of MainWindow.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public MainWindow(CoreController CC)
        {
            // assign pointer
            this.CoreController = CC;

            //C# windows form initilizing
            InitializeComponent();

            // pause button disabled
            button_Pause.Enabled = false;


            RefreshAgentTypeOptions();

            //init ToolTip
            setToolTips();

        }

        #region gMapExplorer

        /* 
         * private void gMapExplorer_Load(object sender, EventArgs e)
         * 
         * Description:
         *      load of gMapExplorer, for use of GMap.NET initialization
         *      (GMapExplorer is the map in the center of MainWindow)
         *      
         * Arguments:     
         *      sender - refers to the object that invoked the event
         *      e - the Event Argument of the object, contains the event data.
         * Return Value:
         *      void
         */
        private void gMapExplorer_Load(object sender, EventArgs e)
        { 

            //initialize map center (hsinchu)
            gMapExplorer.Position = new PointLatLng(24.797332, 120.995304);

            //configuration
            GMapProvider.Language = LanguageType.ChineseTraditional;
            gMapExplorer.MapProvider = GMapProviders.OpenStreetMap;
            gMapExplorer.MinZoom = 3;
            gMapExplorer.MaxZoom = 18;
            gMapExplorer.Zoom = 17;
            gMapExplorer.Manager.Mode = AccessMode.ServerAndCache;
            gMapExplorer.MarkersEnabled = true;
            //gMapExplorer.Dock = DockStyle.Fill;
            
            //label text
            label_LatLng.Text = "Lng : " + gMapExplorer.Position.Lng.ToString() 
                + "   Lat : " + gMapExplorer.Position.Lat.ToString();

            // GMap overlay init
            OverlayMouse = new GMapOverlay(gMapExplorer, "OverlayMouse");
            OverlayAgents = new GMapOverlay(gMapExplorer, "OverlayAgents");

            gMapExplorer.Overlays.Add(OverlayMouse);
            gMapExplorer.Overlays.Add(OverlayAgents);

            //gMapExplorer.DragButton = MouseButtons.Left;

            //cursor init
            Cursor.Current = Cursors.WaitCursor;
            var current = new PointLatLng(gMapExplorer.Position.Lat, 
                gMapExplorer.Position.Lng);

            //double click cursor (current marker)
            var currentMark = new GMap.NET.WindowsForms.Markers.
                GMapMarkerGoogleGreen(current);

            Cursor.Current = Cursors.Default;

            //controls of GMapExplorer
            gMapExplorer.MouseDoubleClick += new MouseEventHandler
                (gMapExplorer_MouseDoubleClick);
            gMapExplorer.OnMarkerEnter += new MarkerEnter
                (gMapExplorer_OnMarkerEnter);
            gMapExplorer.OnMarkerLeave += new MarkerLeave
                (gMapExplorer_OnMarkerLeave);
            gMapExplorer.MouseUp += new MouseEventHandler
                (gMapExplorer_MouseUp);
            gMapExplorer.MouseDown += new MouseEventHandler
                (gMapExplorer_OnMouseDown);
        }

        /* 
         * private void gMapExplorer_MouseDoubleClick
         *  (object sender, MouseEventArgs e)
         * 
         * Description:
         *      mouse double click event of gMapExplorer, 
         *      create marker (looks like cross sign +)
         *      
         * Arguments:     
         *      sender - refers to the object that invoked the event
         *      e - the Event Argument of the object, contains the event data.
         * Return Value:
         *      void
         */
        private void gMapExplorer_MouseDoubleClick
            (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.WaitCursor;
                
                //set position 
                PointLatLng latLng = gMapExplorer.FromLocalToLatLng(e.X, e.Y);

                // current position
                var current = new PointLatLng
                    (Math.Abs(latLng.Lat), latLng.Lng);

                var currentMark = new 
                    GMap.NET.WindowsForms.Markers.GMapMarkerCross(current);

                //refresh overlays and markers
                gMapExplorer.MarkersEnabled = false;
                OverlayMouse.Markers.Clear();

                //gMapExplorer.Overlays.Clear();
                OverlayMouse.Markers.Add(currentMark);

                gMapExplorer.MarkersEnabled = true;
                Cursor.Current = Cursors.Hand;

                //update label infomation
                label_LatLng.Text =
                    "Lng : " + latLng.Lng.ToString()
                    + "   Lat : " + latLng.Lat.ToString();

                // auto fill value to "create agent" gui part
                textBox_AgentLng.Text = latLng.Lng.ToString();
                textBox_AgentLat.Text = latLng.Lat.ToString();
            }
        }

        /* 
         * private void gMapExplorer_OnMouseDown
         *   (object sender, MouseEventArgs e)
         * 
         * Description:
         *      on mouse down event of gMapExplorer, 
         *      for selecting markers, and searching which agent it belongs to.
         *      
         * Arguments:     
         *      sender - refers to the object that invoked the event
         *      e - the Event Argument of the object, contains the event data.
         * Return Value:
         *      void
         */
        private void gMapExplorer_OnMouseDown(object sender, MouseEventArgs e)
        {
            // your mouse must be on certain marker
            if (CurrentMouseOnMarker != null)
            {
                // select this marker
                SelectedMarker = CurrentMouseOnMarker;
                GMapMarkerCircle marker = (GMapMarkerCircle)SelectedMarker;

                // cancel all marker's selection 
                CoreController.DeselectMarkers();

                // select this one
                marker.IsSelected = true;

                //search target agent according to this marker
                for (int ii = 0; ii <= CoreController.God.AgentNumber; ii++)
                {
                    Agent target = CoreController.God.WorldAgentList[ii];
                    
                    if (target == null)
                        continue;
                    
                    //found!
                    if (target.Marker.Equals(marker))
                    {
                        //find index in listBoxAgentList, and highlight it
                        for (int jj = 0; jj < listBoxAgentList.Items.Count; 
                            jj++)
                        {
                            string name = listBoxAgentList.Items[jj].
                                ToString();
                            if (name.Equals(target.AgentProperties["Name"].
                                ToString()))
                            {
                                listBoxAgentList.SelectedIndex = jj;
                                return;
                            }
                        }

                        //find index in listBox(Joined)AgentList, and highlight it
                        for (int jj = 0; jj < listBoxJoinedAgentList.Items.Count;
                            jj++)
                        {
                            string name = listBoxJoinedAgentList.Items[jj].
                                ToString();
                            if (name.Equals(target.AgentProperties["Name"].
                                ToString()))
                            {
                                listBoxJoinedAgentList.SelectedIndex = jj;
                                return;
                            }
                        }
                    }
                }

                
            }          
        }

        /* 
         * private void gMapExplorer_OnMarkerEnter(GMapMarker m)
         * 
         * Description:
         *      on marker enter event of gMapExplorer, 
         *      for detecting mouse status about markers.
         *      
         * Arguments:     
         *      m - the marker which is entered
         * Return Value:
         *      void
         */
        private void gMapExplorer_OnMarkerEnter(GMapMarker m)
        {
            // correct class of marker
            if (m is GMapMarkerCircle)
            {
                // current marker pointer points to it
                CurrentMouseOnMarker = m;

                GMapMarkerCircle circle = (GMapMarkerCircle)m;
                
                // change diameter
                circle.CircleDiameter = 
                    (int)CircleDiameterTypes.IntActiveDiameter;

            }       
            // update label status text
            label_MouseStatus.Text = "OnMarkerEnter";
        }

        /* 
         * private void gMapExplorer_OnMarkerLeave(GMapMarker m)
         * 
         * Description:
         *      on marker leave event of gMapExplorer, 
         *      for detecting mouse status about markers.
         *      
         * Arguments:     
         *      m - the marker mouse leaves
         * Return Value:
         *      void
         */
        private void gMapExplorer_OnMarkerLeave(GMapMarker m)
        {
            // correct class of marker
            if (m is GMapMarkerCircle)
            {
                // current marker pointer points to null
                CurrentMouseOnMarker = null;

                GMapMarkerCircle circle = (GMapMarkerCircle)m;
                
                // change diameter
                circle.CircleDiameter = 
                    (int)CircleDiameterTypes.IntPassiveDiameter;

            }
            // update label status text
            label_MouseStatus.Text = "OnMarkerLeave";
        }

        /* 
         * private void gMapExplorer_MouseUp(object sender, MouseEventArgs e)
         * 
         * Description:
         *      mouse up event of gMapExplorer, 
         *      for detecting mouse status.
         *      
         * Arguments:     
         *      sender - refers to the object that invoked the event
         *      e - the Event Argument of the object, contains the event data.
         * Return Value:
         *      void
         */
        private void gMapExplorer_MouseUp(object sender, MouseEventArgs e)
        {
            // update label status text
            label_MouseStatus.Text = "MouseUp";
        }
        
        #endregion

        

        /* 
         * public void RefreshGMapMarkers()
         * 
         * Description:
         *      This method redraws all markers on gMapExplorer. 
         *      use for refreshing gMapExplorer's agents and joined agents
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        public void RefreshGMapMarkers()
        {
            // refresh overlays and markers
            // initialize
            gMapExplorer.MarkersEnabled = false;
            OverlayMouse.Markers.Clear();
            OverlayAgents.Markers.Clear();
            gMapExplorer.Overlays.Clear();

            // add each agent
            for (int ii = 0; ii <= CoreController.God.AgentNumber; ii++)
            {
                Agent currentAgent = CoreController.God.WorldAgentList[ii];

                if (currentAgent == null)
                    continue;

                // add new target marker to GMap
                OverlayAgents.Markers.Add(currentAgent.Marker);
                

            }

           

            gMapExplorer.Overlays.Add(OverlayMouse);
            gMapExplorer.Overlays.Add(OverlayAgents);
            gMapExplorer.MarkersEnabled = true;

        }


        /* 
        * public void RefreshAgentTypeOptions()
        * 
        * Description:
        *      This method fills agent types in GUI.
        *      (according to DLL functions)          
        *      
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        public void RefreshAgentTypeOptions() 
        {
            //agent list box configuration 
            listBoxAgentType.SelectionMode = SelectionMode.One;

            //dynamically add agent types in .dll
            for (int ii = 0; ii < CoreController.Classes.Count; ii++)
            {
                string agentType = CoreController.Classes[ii].ToString();


                if (agentType.Contains("ABDiSE.Model.AgentClasses."))
                {
                    agentType = agentType.Remove(0, "ABDiSE.Model.AgentClasses.".Length);

                }

                agentType = agentType.ToUpper();

                listBoxAgentType.Items.Add(agentType);

            }
        
        }

        /* 
        * public void RefreshAgentDataOptions()
        * 
        * Description:
        *      This method fills agent types in GUI.
        *      (according to list box index)          
        *      use for refreshing "create agent" status if index is changed.
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        public void RefreshAgentDataOptions()
        {
            // current selected index
            int index = listBoxAgentType.SelectedIndex;

            //index do not exist
            if (index < 0)
                return;

            // current agent type
            string type = listBoxAgentType.Items[index].ToString();

            //initialize
            listBoxAgentControl.Items.Clear();

                
            if(type!= "")
            {
                for (int ii = 0; ii < CoreController.ConfigStrings[index].SubTypes.Count; ii++)
                {
                    string subTypes = CoreController.ConfigStrings[index].SubTypes[ii].AgentSubType;

                    if (subTypes.Contains("ABDiSE.Model.AgentClasses."))
                    {
                        subTypes = subTypes.Remove(0, "ABDiSE.Model.AgentClasses.".Length);

                    }
                    listBoxAgentControl.Items.Add(subTypes);

                }
            }
            else
            {
                listBoxAgentControl.Items.Add("NULL error");
                listBoxAgentControl.Items.Add("ADD");
                listBoxAgentControl.Items.Add("OPETIONS");
                listBoxAgentControl.Items.Add("IN");
                listBoxAgentControl.Items.Add("RefreshAgent");
                listBoxAgentControl.Items.Add("DataOptions()");
            }   

        }

        /* 
        * public void RefreshAgentData()
        * 
        * Description:
        *      This method fills agent details in GUI.
        *      (according to list box index)          
        *      use for refreshing "create agent" status if index is changed.
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        public void RefreshAgentData()
        {
            // initiallize with blank 
            textBox_K01.Text = "";
            textBox_V01.Text = "";
            textBox_K02.Text = "";
            textBox_V02.Text = "";
            textBox_K03.Text = "";
            textBox_V03.Text = "";
            textBox_K04.Text = "";
            textBox_V04.Text = "";
            textBox_K05.Text = "";
            textBox_V05.Text = "";
            textBox_K06.Text = "";
            textBox_V06.Text = "";

            int index = listBoxAgentType.SelectedIndex;
            //index do not exist
            if (index < 0) 
                return;

            //string type = listBoxAgentType.Items[index].ToString();

            int index2 = listBoxAgentControl.SelectedIndex;
            //do not exist
            if (index2 < 0)
                return;
                       
          
            // fill properties in UI
            if (CoreController.ConfigStrings[index].Keys.Count>0){
                textBox_K01.Text = CoreController.ConfigStrings[index].Keys[0];
                textBox_V01.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[0];
            }
            if (CoreController.ConfigStrings[index].Keys.Count>1){
                textBox_K02.Text = CoreController.ConfigStrings[index].Keys[1];
                textBox_V02.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[1];
            }
            if (CoreController.ConfigStrings[index].Keys.Count>2){
                textBox_K03.Text = CoreController.ConfigStrings[index].Keys[2];
                textBox_V03.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[2];
            }
            if (CoreController.ConfigStrings[index].Keys.Count>3){
                textBox_K04.Text = CoreController.ConfigStrings[index].Keys[3];
                textBox_V04.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[3];
            }
            if (CoreController.ConfigStrings[index].Keys.Count>4){
                textBox_K05.Text = CoreController.ConfigStrings[index].Keys[4];
                textBox_V05.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[4];
            }
            if (CoreController.ConfigStrings[index].Keys.Count>5){
                textBox_K06.Text = CoreController.ConfigStrings[index].Keys[5];
                textBox_V06.Text = CoreController.ConfigStrings[index].SubTypes[index2].Values[5];
            }

        }

        /* 
        * public void RefreshAgentList()
        * 
        * Description:
        *      This method update/refresh agent list in GUI.
        *      (according to God.WorldAgentList )          
        *      use for refreshing after agent creation
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        public void RefreshAgentList()
        {

            //world agent list box
            listBoxAgentList.Items.Clear();

            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++ )
            {
                Agent target = CoreController.God.WorldAgentList[ii];

                if (target == null)
                    continue;

                if (target.AgentProperties.ContainsKey("Name") && target.IsJoinedAgent == false)
                {
                    listBoxAgentList.Items.Add(
                        CoreController.God.WorldAgentList[ii].AgentProperties["Name"]);
                    
                }
            }

            // clear selected markers
            CoreController.DeselectMarkers();
            //for refresh markers
            RefreshGMapMarkers();

        }

        /* 
        * public void RefreshJoinedAgentList()
        * 
        * Description:
        *      This method update/refresh joined agent list in GUI.
        *      (according to God.WorldJoinedAgentList )          
        *      use for refreshing after joined agent creation
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        public void RefreshJoinedAgentList()
        {

            //world joind agent list box
            listBoxJoinedAgentList.Items.Clear();
            int ii;

            for (ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {
                Agent target = CoreController.God.WorldAgentList[ii];

                if (target == null)
                    continue;

                if (target.AgentProperties.ContainsKey("Name") 
                    && target.IsJoinedAgent)
                {
                    listBoxJoinedAgentList.Items.Add(
                        target.AgentProperties["Name"]);

                }
            }

            // clear selected markers
            CoreController.DeselectMarkers();
            //for refresh markers
            RefreshGMapMarkers();
        }

        delegate void SetTextCallback(string text);

       

        /* 
        * public void CreateAgent()
        * 
        * Description:
        *      This method uses data in GUI to call God.create(...)
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        //this method let UI can call God.create (internal)
        public MethodReturnResults CreateAgent()
        {
            // current position in gMap
            PointLatLng LatLng = new PointLatLng();

            // input PointLatLng null
            if (textBox_AgentLat.Text == "" || textBox_AgentLng.Text == "")
                return MethodReturnResults.FAILED;

            LatLng.Lat = double.Parse(textBox_AgentLat.Text);
            LatLng.Lng = double.Parse(textBox_AgentLng.Text);


            Dictionary<string, string> Properties = 
                new Dictionary<string, string>();

            // check if null
            if (!Properties.ContainsKey(textBox_K01.Text) && 
                textBox_K01.Text!="")
                Properties.Add(textBox_K01.Text, textBox_V01.Text);
            if (!Properties.ContainsKey(textBox_K02.Text) &&
                textBox_K02.Text != "")
                Properties.Add(textBox_K02.Text, textBox_V02.Text);
            if (!Properties.ContainsKey(textBox_K03.Text) &&
                textBox_K03.Text != "")
                Properties.Add(textBox_K03.Text, textBox_V03.Text);
            if (!Properties.ContainsKey(textBox_K04.Text) &&
                textBox_K04.Text != "")
                Properties.Add(textBox_K04.Text, textBox_V04.Text);
            if (!Properties.ContainsKey(textBox_K05.Text) &&
                textBox_K05.Text != "")
                Properties.Add(textBox_K05.Text, textBox_V05.Text);
            if (!Properties.ContainsKey(textBox_K06.Text) &&
                textBox_K06.Text != "")
                Properties.Add(textBox_K06.Text, textBox_V06.Text);


            int currentIndex = listBoxAgentType.SelectedIndex;

            //no selection
            if (currentIndex < 0)
                return MethodReturnResults.FAILED;

            //string agentClass = listBoxAgentType.Items[currentIndex].ToString();

            //create arguments
            /*Object args = new Object[] 
                { CoreController, Properties, LatLng, CoreController.God.WorldEnvironmentList[0] };
            */
            string agentClass = CoreController.ConfigStrings[currentIndex].ClassFullName;

            // call DLL to create agent instance
            // ***important
            CoreController.CreateDLLInstance(
                agentClass, 
                CoreController, 
                Properties, 
                LatLng, 
                CoreController.God.WorldEnvironmentList[0]
                );


            return MethodReturnResults.FAILED;
        }

        /* 
        * private void buttonCreate_Click(object sender, EventArgs e)
        * 
        * Description:
        *      click event of button "create agent" in GUI.
        *      use for "create agent".
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void buttonCreate_Click(object sender, EventArgs e)
        {

            CreateAgent();
            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();

            textBox_AgentLat.Text = "";
            textBox_AgentLng.Text = "";
        }

        /* 
        * private void buttonStart_Click(object sender, EventArgs e)
        * 
        * Description:
        *      click event of button "start simulation" in GUI.
        *      use for starting timer.
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void buttonStart_Click(object sender, EventArgs e)
        {
            button_Pause.Enabled = true;
            button_SimStart.Enabled = false;

            CoreController.EnableMarkerAnimation();

            
            //TODO: improve it
            //StartSimulationSteps();
            //progressBar_steps.Maximum = (int)numericUpDown_SimSteps.Value;
            //bw.RunWorkerAsync(workerType.stepsSim);


            //button_SimStart.Enabled = true;
            //CoreController.DisableMarkerAnimation();
        }

        /* 
        * private void buttonPause_Click(object sender, EventArgs e)
        * 
        * Description:
        *      click event of button "puase simulation" in GUI.
        *      use for stoping timer.
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void buttonPause_Click(object sender, EventArgs e)
        {
            
            button_Pause.Enabled = false;
            button_SimStart.Enabled = true;

            //CoreController.STP.EndPool();

            CoreController.DisableMarkerAnimation();
            
        }

        

        /* 
        * private void MainWindow_Load(object sender, EventArgs e)
        * 
        * Description:
        *      load event of MainWindow in GUI.
        *      use for initializing.
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void MainWindow_Load(object sender, EventArgs e)
        {
            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();

            initBackgroundWorker();

            //TODO: temperarly disable steps simulation
            //TODO: on
            button_SimStart.Enabled = true;
        }


        /* 
        * private void textBox_C0X_KeyPress
        *   (object sender, KeyPressEventArgs e)
        * 
        * Description:
        *      key press event of textBox_C0X in GUI.
        *      use for inputing only digits.
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void textBox_C0X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /* 
        * private void listBoxAgentList_SelectedIndexChanged
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      SelectedIndexChanged event of listBoxAgentList in GUI.
        *      for searching agent (by name) in GUI.
        *      every name of agent is unique.      
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void listBoxAgentList_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            int index = listBoxAgentList.SelectedIndex;

            if (index < 0)
                return;

            string name =  listBoxAgentList.Items[index].ToString();
            /*
            MyPrintf(string.Format(
                "selectedIndexChanged: AgentList[{0}]", name)
                );
            */

            Agent target = null;

            // searching world agent list
            for(int ii=0; ii< CoreController.God.AgentNumber; ii++)
            {
                if (CoreController.God.WorldAgentList[ii] == null)
                    continue;

                if (!CoreController.God.WorldAgentList[ii].AgentProperties.ContainsKey("Name"))
                    continue;

                
                if (name.Equals(
                    CoreController.God.WorldAgentList[ii].AgentProperties["Name"].ToString()))
                {
                    //found!
                    target = CoreController.God.WorldAgentList[ii];

                    //clear markers
                    CoreController.DeselectMarkers();
                    //select what we found
                    target.Marker.IsSelected = true;
                    //refresh GUI
                    RefreshGMapMarkers();

                    break;
                }
            }

            //not found
            if (target == null)
                return;
            
            // updating label text
            label_AgentProperties.Text = string.Format(
                "{0}\n{1}\n\nIsActivated:{2}\nIsDead:{3}\n\n", 
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead);

            //display properties
            foreach (KeyValuePair<string, string> item
                in target.AgentProperties)
            {

                label_AgentProperties.Text += 
                    string.Format("{0}\n  {1}\n\n", item.Key, item.Value);
            }

            //MyPrintf(string.Format("Text : {0}", Text));

            //clear another listbox
            listBoxJoinedAgentList.ClearSelected();

        }

        /* 
        * private void listBoxAgentType_SelectedIndexChanged
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      SelectedIndexChanged event of listBoxAgentType in GUI.
        *      for filling data of selected agent type.    
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void listBoxAgentType_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            RefreshAgentDataOptions();
            RefreshAgentData();

        }

        /* 
        * private void listBoxAgentControl_SelectedIndexChanged
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      SelectedIndexChanged event of listBoxAgentControl in GUI.
        *      for filling data of selected agent type and class.    
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void listBoxAgentControl_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            RefreshAgentData();
        }

        /* 
        * private void listBoxJoinedAgentList_SelectedIndexChanged
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      SelectedIndexChanged event of listBoxJoinedAgentList in GUI.
        *      for searching joined agent (by name) in GUI.
        *      every name of agent is unique.   
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void listBoxJoinedAgentList_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            int index = listBoxJoinedAgentList.SelectedIndex;

            if (index < 0)
                return;

            string name = listBoxJoinedAgentList.Items[index].ToString();

            Agent target = null;

            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {
                target = CoreController.God.WorldAgentList[ii];

                if (target == null)
                    continue;

                if (!target.
                    AgentProperties.ContainsKey("Name"))
                    continue;

                if (name.Equals(
                    target.
                    AgentProperties["Name"].ToString()))
                {
                    // found target
                    
                    //clear markers
                    CoreController.DeselectMarkers();
                    //select what we found
                    target.Marker.IsSelected = true;
                    //refresh GUI
                    RefreshGMapMarkers();

                    break;
                }
            }

            //target not found
            if (target == null)
                return;

            // update label text
            label_AgentProperties.Text = string.Format(
                "({0}\n{1})\n\nIsActivated:{2}\nIsDead:{3}\n",
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead);

            foreach (KeyValuePair<string, string> item
                in target.AgentProperties)
            {

                label_AgentProperties.Text +=
                    string.Format(" {0}\n {1}\n\n", item.Key, item.Value);
            }

            //clear another listbox
            listBoxAgentList.ClearSelected();
        }


        private Object ClearDeadAgentLock = new Object();


        /*  
        * private void SimulationTimer_Tick
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      tick event of SimulationTimer.
        *      (time-driven version)
        *      
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            
        }



        //TODO: priory thread queue
        /*  
        * public void StartSimulationSteps()
        * 
        * Description:
        *      run simulation
        *      this method simulate certain steps of all the agents/joined agents
        *      1. start thread pool
        *      2. for(steps)
        *          queue workitems
        *      3. end pool
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        public void stepsSimulation()
        {

            //init
            CoreController.StartThreadPool(
                (int)numericUpDown_STPThreadsNum.Value,
                (int)numericUpDown_STPIdleTime.Value,
                (int)numericUpDown_STPExecuteTime.Value

                );

            //step N : stage 2

            for (int count = 0; count < 
                (int)numericUpDown_SimSteps.Value; count++ )
            {
                bw.ReportProgress(count);

                CoreController.God.CurrentStep++;
                /*label_GodCurrentStep.Text = 
                    CoreController.God.CurrentStep.ToString();
                */
                //queue joined agent 
                if (CoreController.God.AgentCount > 0)
                {
                    /*// One event is used for each object
                    ManualResetEvent[] doneEvents =
                        new ManualResetEvent[God.JoinedAgentNumber];*/
                    Console.WriteLine
                        ("launching Joined Agent tasks..");


                    Agent joinedAgent = null;

                    //every joined agent runs update() itself
                    // put workitems into threadpool
                    for (int ii = 0; 
                        ii < CoreController.God.AgentNumber; ii++)
                    {

                        joinedAgent = CoreController.God.WorldAgentList[ii];
                        
                        if (joinedAgent != null && joinedAgent.IsJoinedAgent)
                        {
                            //if need update
                            if (joinedAgent.CurrentStep <= 
                                CoreController.God.CurrentStep)
                            {
                                //TODO
                                /*
                                //update
                                CoreController.STP.QueueUserWorkItem
                                    (joinedAgent.ThreadPoolCallback, ii.ToString());
                                 * */
                                //joinedAgent.update();
                            }
                            else
                            {
                                //wait until next step
                                continue;
                            }

                            /*doneEvents[ii] = new ManualResetEvent(false);
                            // do self update check
                            joinedAgent.SetDoneEvent(doneEvents[ii]);*/



                        }
                        else
                        {
                            //doneEvents[ii] = new ManualResetEvent(true);
                            continue;
                        }
                    }
                    // Wait for all threads in pool to calculation...
                    //WaitHandle.WaitAll(doneEvents);
                    /*Console.WriteLine
                        ("after WaitAll: All calculations are complete.");*/
                }

                /*
                //barrier
                while (God.SimpleTP.WorkitemNumber > 0)
                {
                    //TODO: change this while 0 busy waiting
                }
                */

                //queue agent
                if (CoreController.God.AgentCount > 0)
                {
                    // One event is used for each object
                    /*ManualResetEvent[] doneEvents =
                        new ManualResetEvent[God.AgentNumber];*/
                    Console.WriteLine
                        ("launching {0} Agent tasks..", CoreController.God.AgentNumber);

                    Agent agent = null;

                    //every agent runs update() itself
                    for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
                    {
                        agent = CoreController.God.WorldAgentList[ii];
                        if (agent != null && agent.IsJoinedAgent != true)
                        {

                            //if need update
                            if (agent.CurrentStep <= 
                                CoreController.God.CurrentStep)
                            {
                                //TODO
                                // do self update check
                                /*
                                CoreController.STP.QueueUserWorkItem
                                    (agent.ThreadPoolCallback, ii.ToString());
                                */
                                // check whole world agents attachment
                                // TODO: search for nearby environments(grids) only 
                                //God.CheckAgentAttachment(agent);


                            }
                            else
                            {
                                //wait until next step
                                continue;
                            }


                        }
                        else
                            continue;

                    }
                }

                /*
                //barrier
                while (God.SimpleTP.WorkitemNumber > 0)
                {
                    //TODO: change this while 0 busy waiting
                }
                */
                lock (ClearDeadAgentLock)
                {
                    // clear IsDead == true
                    CoreController.God.ClearDeadAgent();
                    // refresh GUI
                    //RefreshGMapMarkers();
                    RefreshAgentList();
                    RefreshJoinedAgentList();

                    
                }

                //set delay time
                //Thread.Sleep((int)numericUpDown_SimDelay.Value);
            }


            CoreController.STP.EndPool();           

        }


        /*  
        * private void buttonCreateTP_Click
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      click event for buttonCreateTP
        *      this method queue test workitems into new SimpleThreadPool     
        *      
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void buttonCreateTP_Click(object sender, EventArgs e)
        {
            //disable this button before this method complete
            button_CreateTP.Enabled = false;

            //CreateSTPTest();
            //int argu = (int)workerType.createTPTest;
            bw.RunWorkerAsync(workerType.createTPTest);

            button_CreateTP.Enabled = true;
        }


        /*  
        * private void CreateSTPTest()
        * 
        * Description:
        *      click event for buttonCreateTP
        *      this method queue test workitems into new SimpleThreadPool     
        *      
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        private void createSTPTest()
        {
            CoreController.StartThreadPool(
                (int)numericUpDown_STPThreadsNum.Value,
                (int)numericUpDown_STPIdleTime.Value,
                (int)numericUpDown_STPExecuteTime.Value
                
                );


            //queue workitems
            //continuously queue workitems
            int Workitems = (int)numericUpDown_STPWorkitemNum.Value;

            for (int count = 1; count <= Workitems; count++)
            {
                ///threadPool.QueueUserWorkItem(
                //    new WaitCallback(SimpleThreadPool.ShowMessage),
                //    string.Format("WorkItem[{0}]", count));
                CoreController.STP.QueueUserWorkItem(
                    new WaitCallback(ABDiSE.Controller.ThreadPool.SimpleThreadPool.ShowMessage),
                    count.ToString());

                //delayed queue time
                //TODO: change sleep location
                //Thread.Sleep((int)numericUpDown_STPQueueDelay.Value);

            }
            Console.WriteLine("STP: all workitems already in queue");
            Console.WriteLine("STP: SimpleThreadPool Started and all workitems are already in queue");

            CoreController.STP.EndPool();
        }



        /*  
        * private void button_EndPool_Click
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      click event for button_EndPool
        *      this method simply calls EndPool     
        *      
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void button_EndPool_Click(object sender, EventArgs e)
        {
            CoreController.STP.EndPool(false);
        }

        /*  
        * private void button_EndPool_Click
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      click event for button_EndPool
        *      this method simply calls CancelPool     
        *      
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void button_CancelPool_Click(object sender, EventArgs e)
        {
            CoreController.STP.EndPool(true);
        }

        /*  
        * private void button_StepSim_Click
        *   (object sender, EventArgs e)
        * 
        * Description:
        *      click event for button_StepSim
        *      this method simulate one step of all the agents/joined agents
        *      1. start thread pool
        *      2. queue workitems
        *      3. end pool
        *      
        * Arguments:     
        *      sender - refers to the object that invoked the event
        *      e - the Event Argument of the object, contains the event data.
        * Return Value:
        *      void
        */
        private void button_StepSim_Click(object sender, EventArgs e)
        {
            //disable this button until
            button_StepSim.Enabled = false;


            if (bw.IsBusy == false)
            {
                //stepSimulation(); 
                bw.RunWorkerAsync(workerType.stepSim);
            }
            else
            {
                MessageBox.Show("background worker is busy, please try again");
                button_StepSim.Enabled = true;
                return;
            }

            lock (ClearDeadAgentLock)
            {
                // clear IsDead == true
                CoreController.God.ClearDeadAgent();
                // refresh GUI
                //RefreshGMapMarkers();
                RefreshAgentList();
                RefreshJoinedAgentList();
                RefreshGMapMarkers();

                CoreController.God.CurrentStep++;

                //update label
                label_GodCurrentStep.Text = CoreController.God.CurrentStep.ToString();


                //Console.WriteLine("God.CurrentStep = " + God.CurrentStep);

            }

            //enable this button
            button_StepSim.Enabled = true;
        }


        /*  
        * private void stepSimulation() 
        * 
        * Description:
        *      click event for button_StepSim
        *      this method simulate one step of all the agents/joined agents
        *      1. start thread pool
        *      2. queue workitems
        *      3. end pool
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        private void stepSimulation() 
        {

            //start new round
            CoreController.StartThreadPool(
                (int)numericUpDown_STPThreadsNum.Value,
                (int)numericUpDown_STPIdleTime.Value,
                (int)numericUpDown_STPExecuteTime.Value

                );

            //step N : stage 2


            /*// One event is used for each object
            ManualResetEvent[] doneEvents =
                new ManualResetEvent[God.JoinedAgentNumber];*/


            Console.WriteLine
                ("Step[" + CoreController.God.CurrentStep +
                "] launching {0} Agent tasks..", CoreController.God.AgentNumber);
            //compute Joined Agents

            Agent joinedAgent = null;
            
            //every joined agent runs update() itself
            // put workitems into threadpool
            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {

                joinedAgent = CoreController.God.WorldAgentList[ii];
                if (joinedAgent != null && joinedAgent.IsJoinedAgent)
                {
                    //if need update
                    if (joinedAgent.CurrentStep <= CoreController.God.CurrentStep)
                    {
                        //update
                        
                        CoreController.STP.QueueUserWorkItem
                            (joinedAgent.ThreadPoolCallback, ii.ToString());
                        
                    }
                    else
                    {
                        //wait until next step
                        continue;
                    }

                    //doneEvents[ii] = new ManualResetEvent(false);
                    // do self update check
                    //joinedAgent.SetDoneEvent(doneEvents[ii]);*/



                }
                else
                {
                    //doneEvents[ii] = new ManualResetEvent(true);
                    continue;
                }
            }
            // Wait for all threads in pool to calculation...
            
            //WaitHandle.WaitAll(doneEvents);

            /*Console.WriteLine
                ("after WaitAll: All calculations are complete.");*/

            //TODO: barrier between two stages

            // compute agent

            // One event is used for each object
            /*ManualResetEvent[] doneEvents =
                new ManualResetEvent[God.AgentNumber];*/


            Agent agent = null;

            //every agent runs update() itself
            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {
                agent = CoreController.God.WorldAgentList[ii];
                if (agent != null)
                {

                    //if need update
                    if (agent.CurrentStep <= CoreController.God.CurrentStep)
                    {
                        //TODO
                        
                        // do self update
                        CoreController.STP.QueueUserWorkItem
                            (agent.ThreadPoolCallback, ii.ToString());

                            
                        // check whole world agents attachment
                        // TODO: search for nearby environments(grids) only 
                        //CoreController.God.CheckAgentAttachment(agent);
                            
                            
                    }
                    else
                    {
                        //wait until next step
                        continue;
                    }


                }
                else
                    continue;

            }


            CoreController.STP.EndPool();



        
        }

        /*  
        * private void setToolTips()
        * 
        * Description:
        *      this method sets default tooltip for certain control units
        *      for example: label , textbox, etc.
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
        */
        private void setToolTips()
        {
            ToolTip label_AgentCoordTitleTip = new ToolTip();
            label_AgentCoordTitleTip.SetToolTip(
                label_AgentCoordTitle, 
                "雙擊地圖兩下以自動取得LatLng資訊"
                );

        }

        private void environmentRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            ABDiSE.Model.Environment targetEnv;
            targetEnv = CoreController.God.WorldEnvironmentList[0];


            if (targetEnv.EnvProperties.ContainsKey("Weather"))
            {
                targetEnv.EnvProperties.Remove("Weather");

            }

            if (radioButton_Cloudy.Checked)
            {
                targetEnv.EnvProperties.Add("Weather", "Cloudy");
            }

            if (radioButton_Rainy.Checked)
            {
                targetEnv.EnvProperties.Add("Weather", "Rainy"); 
            }

            if (radioButton_Sunny.Checked)
            {
                targetEnv.EnvProperties.Add("Weather", "Sunny"); 
            }


            label_EnvironmentProperties.Text =
                CoreController.God.WorldEnvironmentList[0].
                    CreateEnvironmentPropertiesString();
        }

        private void environmentTextBoxes_TextChanged(object sender, EventArgs e)
        {
            double windDir = 0;
            double.TryParse(
                textBox_Env_WindDirection.Text,
                out windDir
                );
            //TODO: check 
            CoreController.God.WorldEnvironmentList[0].EnvProperties["WindDirection"] = windDir.ToString();

            double windSpeed = 0;
            double.TryParse(
                textBox_Env_WindSpeed.Text,
                out windSpeed
                );
            //TODO: check 
            CoreController.God.WorldEnvironmentList[0].EnvProperties["WindSpeed"] = windSpeed.ToString();

            double rainFall = 0;
            double.TryParse(
                textBox_Env_RainFall.Text,
                out rainFall
                );
            //TODO: check 
            CoreController.God.WorldEnvironmentList[0].EnvProperties["RainFall"] = windSpeed.ToString();

            //display number 0 environment
            label_EnvironmentProperties.Text =
                CoreController.God.WorldEnvironmentList[0].CreateEnvironmentPropertiesString();
            
        }

    }

    #endregion
    

    #region GMAP CUSTOM MARKER

    /*  
    * public class GMapMarkerCircle : GMap.NET.WindowsForms.GMapMarker
    *   
    * 
    * Description:
    *      this class extends GMapMarker
    *      use for drawing custom circle marker      
    *      
    *      reuse internet open source code, few edit by Lightorz
    *      
    */

    public class GMapMarkerCircle : GMap.NET.WindowsForms.GMapMarker
    {

        #region Properties

        // animation mode
        public bool IsAnimated = false;

        // selected marker will show additional square
        public bool IsSelected = false;

        // The pen for the outer circle
        public Pen OuterPen { get; set; }

        // The brush for the inner circle
        public Brush InnerBrush { get; set; }

        // The brush for the Text
        public Brush TextBrush { get; set; }

        // The font for the text
        public Font TextFont { get; set; }

        // The text to display inside of the marker 
        public String Text { get; set; }

        private int diameter = 10;

        public int SelectedOffset = 15;

        // need to load images
        public Image[] CustomImages;

        public int ImageX, ImageY;

        public bool IsSquare = true;
        public bool IsCircle = false;

        //random images index
        public Random RandomSeed;

        // The size of the circle
        public int CircleDiameter
        {
            get
            {
                return this.diameter;
            }
            set
            {
                diameter = value;
                this.Size = new System.Drawing.Size(diameter, diameter);
                Offset = new System.Drawing.Point
                    (-Size.Width / 2, -Size.Height / 2);
            }
        }
        #endregion

        public GMapMarkerCircle(PointLatLng p)
            : base(p)
        {
            OuterPen = new Pen(Color.Black, 2);
            InnerBrush = new SolidBrush(Color.White);

            CircleDiameter = (int)CircleDiameterTypes.IntPassiveDiameter;

            this.TextFont = new Font("Arial", (int)(diameter / 2));
            this.TextBrush = Brushes.Black;
            Offset = new System.Drawing.Point
                (-Size.Width / 2, -Size.Height / 2);

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of 
        /// the whole circle</param>
        /// <param name="text">The text in the marker.</param>
        public GMapMarkerCircle
            (PointLatLng p, Pen outer, Brush inner, int diam, String text)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = new Font("Arial", (int)(diameter / 2));
            this.TextBrush = Brushes.Black;
            Offset = new System.Drawing.Point
                (-Size.Width / 2, -Size.Height / 2);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The LatLongPoint of the marker.</param>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of 
        /// the whole circle</param>
        /// <param name="textBrush">The brush for the text.</param>
        public GMapMarkerCircle(PointLatLng p, Pen outer, Brush inner, 
            int diam, String text, Brush textBrush)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = new Font("Arial", (int)(diameter / 2));
            this.TextBrush = textBrush;
            Offset = new System.Drawing.Point
                (-Size.Width / 2, -Size.Height / 2);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The LatLongPoint of the marker.</param>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of 
        /// the whole circle</param>
        /// <param name="textBrush">The brush for the text.</param>
        public GMapMarkerCircle(PointLatLng p, Pen outer, Brush inner, 
            int diam, String text, Brush textBrush, Font textFont)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = textFont;
            this.TextBrush = textBrush;
            Offset = new System.Drawing.Point
                (-Size.Width / 2, -Size.Height / 2);
        }

        /// <summary>
        /// Render a circle
        /// </summary>
        /// <param name="g"></param>
        public override void OnRender(Graphics g)
        {

            #region old onrender
            /*
            if (this.IsSmoking)
            {
                Image smoke;
                if (IsAnimated)
                {
                    //randomly pick one image from SmokeImages[]
                    //int randomIndex = seed.Next(0, 1);
                    //smoke = SmokeImages[randomIndex];
                    smoke = SmokeImages[0];
                }
                else
                {
                    smoke = SmokeImages[0];
                }
                g.DrawImageUnscaled(smoke, LocalPosition.X - smoke.Width / 3,
                    LocalPosition.Y - smoke.Height / 2);       
            }
            else if (this.IsBurning)
            {
                Image fire;
                if (IsAnimated)
                {
                    //randomly pick one image from FireImages[]
                    int randomIndex = seed.Next(0, 2);
                    fire = FireImages[randomIndex];
                }
                else
                {
                    fire = FireImages[0];
                }
                //g.DrawImageUnscaled(fire, LocalPosition.X - fire.Width / 2,
                //    LocalPosition.Y - fire.Height / 2);
                g.DrawImageUnscaled(fire, LocalPosition.X - fire.Width / 6,
                    LocalPosition.Y - fire.Height / 5);
            }
            else if (this.IsBuilding)
            {
                g.FillRectangle(InnerBrush, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                g.DrawRectangle(OuterPen, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                
            }
            else if (this.IsBuildingCinder)
            {
                g.FillRectangle(InnerBrush, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                g.DrawRectangle(OuterPen, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));
            }
            else if(this.IsTreeCinder)
            {
                g.FillEllipse(InnerBrush, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                g.DrawEllipse(OuterPen, new Rectangle
                    (LocalPosition.X, LocalPosition.Y, diameter, diameter));

            }
            else if(this.IsFlooding){

                Image flood = FloodImages[2];

                g.DrawImageUnscaled(flood, LocalPosition.X - flood.Width / 6,
                    LocalPosition.Y - flood.Height / 6);       
            }
            else if(this.IsWater){

                Image water = FloodImages[2];

                g.DrawImageUnscaled(water, LocalPosition.X - water.Width / 6,
                    LocalPosition.Y - water.Height / 6);       
            }
             * */
            #endregion  
            
            if (this.CustomImages == null)
            {
                if(IsSquare)
                {
                    g.FillRectangle(InnerBrush, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                    g.DrawRectangle(OuterPen, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                }
                else if(IsCircle)
                {
                    g.FillEllipse(InnerBrush, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                    g.DrawEllipse(OuterPen, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));

                }
                else
                {
                    g.FillEllipse(InnerBrush, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                    g.DrawEllipse(OuterPen, new Rectangle
                        (LocalPosition.X, LocalPosition.Y, diameter, diameter));
                }

            }
            else 
            {
                if (this.IsAnimated)
                {

                    int randomIndex = RandomSeed.Next(0, CustomImages.Length);
                    Image randomImage = CustomImages[randomIndex];

                    g.DrawImageUnscaled(
                        randomImage, 
                        LocalPosition.X + ImageX,
                        LocalPosition.Y + ImageY
                    );
                    
                    
                }
                else //not animated
                {
                    //int randomIndex = seed.Next(0, CustomImages.Length);
                    Image randomImage = CustomImages[0];

                    g.DrawImageUnscaled(
                        randomImage,
                        LocalPosition.X + ImageX,
                        LocalPosition.Y + ImageY
                    );


                }
            }

            if (this.IsSelected)
            {

                g.DrawRectangle(OuterPen, new Rectangle
                    (LocalPosition.X - SelectedOffset / 2,
                    LocalPosition.Y - SelectedOffset / 2,
                    diameter + SelectedOffset, diameter + SelectedOffset));

            }

            if (!String.IsNullOrEmpty(this.Text))
            {
                SizeF sizeOfString = g.MeasureString
                    (this.Text, this.TextFont);
                int x = (LocalPosition.X + diameter / 2) - 
                    (int)(sizeOfString.Width / 2);
                int y = (LocalPosition.Y + diameter / 2) - 
                    (int)(sizeOfString.Height / 2);
                g.DrawString(this.Text, this.TextFont, 
                    this.TextBrush, x, y);
            }
        }


    }

    #endregion


}
