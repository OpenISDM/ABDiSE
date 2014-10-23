/** 
 *  @file GUI.cs
 *  ABDiSE GUI is the main window in the OpenISDM ABDiSE project.
 *  It handles form controls (button, listbox, label, etc...) and GMap.NET controls.
 *  This componet interacts with ABDiSE CoreController.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *  Version:
 *
 *      2.0
 *
 *  Abstract:
 *
 *      ABDiSE GUI.cs is a c# windows form in the OpenISDM ABDiSE project.
 *      It handles form controls (button, listbox, label, etc...) and GMap.NET controls.
 *      This componet interacts with ABDiSE CoreController.
 *
 *  Authors:  
 *
 *      Tzu-Liang Hsu, Lightorz@gmail.com
 *
 *  License:
 *
 *      GPL 3.0 This file is subject to the terms and conditions defined 
 *      in file 'COPYING.txt', which is part of this source code package.
 *
 *  Major Revision History:
 *
 *      2014/5/28: version 2.0 alpha
 *      2014/7/2:  edit comments for doxygen
 *      2014/8/28: edit comments
 *
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


    /**
     *  MainWindow is the GUI of ABDiSE, controls GMap.NET, listboxes, buttons, etc.
     *  The background worker is also included in this class.
     */
    public partial class MainWindow : Form
    {
        //
        /// pointer to controller
        //
        CoreController CoreController;

        //
        /// GMap.NET controls
        //
        GMapOverlay OverlayMouse;
        GMapOverlay OverlayAgents;

        //
        /// GMap markers selection controls
        //
        GMapMarker CurrentMouseOnMarker = null;
        GMapMarker SelectedMarker = null;

        #region BackgroundWorker

        //
        /// background worker
        //
        private BackgroundWorker bw;

        private enum workerType { stepSim, createTPTest, stepsSim};


        /**
         *  This method initialize the background worker in ABDiSE.
         *  Because GUI should not do the heavy computaion or simulation.
         */
        private void initBackgroundWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += 
                new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += 
                new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }


        /**
         *  This method will be called by the background worker in ABDiSE.
         *  It executes different methods according to different arguments.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the DoWorkEventArgs of the object, contains the event data. 
         */
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


        /**
         *  This method handles progress in background worker.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void bw_ProgressChanged
            (object sender, ProgressChangedEventArgs e)
        {
            progressBar_steps.Value = e.ProgressPercentage;
            Console.WriteLine(e.ProgressPercentage.ToString());
        }


        /**
         *  This method handles insturctions after background worker completed.
         *  
         *  @param sender - refers to the object that invoked the event.
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void bw_RunWorkerCompleted
            (object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {
                Console.WriteLine("bw cancelled!");
            }

            else if (!(e.Error == null))
            {
                Console.WriteLine("bw Error: " + e.Error.Message);
            }

            else
            {
                Console.WriteLine("bw completed!");
            }

            //
            //after background worker is finished
            //
            RefreshAgentList();
            RefreshJoinedAgentList();
            RefreshGMapMarkers();

            button_StepSim.Enabled = true;

        }

        #endregion 


        /**
         *  This is the constructor of MainWindow. It initializes necessary objects for GUI.
         * 
         *  @param CC - pointer of existing core controller (of ABDiSE)
         */
        public MainWindow(CoreController CC)
        {
            //
            // assign pointer
            //
            this.CoreController = CC;

            //
            // C# windows form initilizing
            //
            InitializeComponent();

            //
            // pause button disabled
            //
            button_Pause.Enabled = false;


            RefreshAgentTypeOptions();

            //
            // init ToolTip
            //
            setToolTips();

            RefreshEnvTextboxes();
        }

        #region gMapExplorer


        /**
         *  This is the load method of GMap Explorer.
         *  (GMapExplorer is the map in the center of MainWindow)
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void gMapExplorer_Load(object sender, EventArgs e)
        { 
            //
            // initialize map center (hsinchu)
            //
            gMapExplorer.Position = new PointLatLng(24.797332, 120.995304);

            //
            // configuration
            //
            GMapProvider.Language = LanguageType.ChineseTraditional;
            gMapExplorer.MapProvider = GMapProviders.BingMap;
            gMapExplorer.MinZoom = 1;
            gMapExplorer.MaxZoom = 18;
            gMapExplorer.Zoom = 17;
            gMapExplorer.Manager.Mode = AccessMode.ServerAndCache;
            gMapExplorer.MarkersEnabled = true;
            //gMapExplorer.Dock = DockStyle.Fill;

            loadMapProvidersForComboBox();
            
            //
            // label text
            //
            label_LatLng.Text = "Lng : " + gMapExplorer.Position.Lng.ToString() 
                + "   Lat : " + gMapExplorer.Position.Lat.ToString();

            //
            // GMap overlay init
            //
            OverlayMouse = new GMapOverlay(gMapExplorer, "OverlayMouse");
            OverlayAgents = new GMapOverlay(gMapExplorer, "OverlayAgents");

            gMapExplorer.Overlays.Add(OverlayMouse);
            gMapExplorer.Overlays.Add(OverlayAgents);

            //gMapExplorer.DragButton = MouseButtons.Left;

            //
            // cursor initialization
            //
            Cursor.Current = Cursors.WaitCursor;
            var current = new PointLatLng(gMapExplorer.Position.Lat, 
                gMapExplorer.Position.Lng);

            //
            // double click cursor (current marker)
            //
            var currentMark = new GMap.NET.WindowsForms.Markers.
                GMapMarkerGoogleGreen(current);

            Cursor.Current = Cursors.Default;

            //
            // controls of GMapExplorer
            //
            gMapExplorer.MouseDoubleClick += new MouseEventHandler
                (gMapExplorer_MouseDoubleClick);
            gMapExplorer.OnMarkerEnter += new MarkerEnter
                (gMapExplorer_OnMarkerEnter);
            gMapExplorer.OnMarkerLeave += new MarkerLeave
                (gMapExplorer_OnMarkerLeave);
            //gMapExplorer.MouseUp += new MouseEventHandler
            //    (gMapExplorer_MouseUp);
            gMapExplorer.MouseDown += new MouseEventHandler
                (gMapExplorer_OnMouseDown);
            gMapExplorer.MouseMove += new MouseEventHandler
                (gMapExplorer_MouseMove);
        }


        /**
         *  Mouse double click event of gMapExplorer, 
         *  Create marker (looks like plus sign +)
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data. 
         */
        private void gMapExplorer_MouseDoubleClick
            (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.WaitCursor;
                
                //
                // set position 
                //
                PointLatLng latLng = gMapExplorer.FromLocalToLatLng(e.X, e.Y);

                //
                // current position
                //
                var current = new PointLatLng
                    (Math.Abs(latLng.Lat), latLng.Lng);

                var currentMark = new 
                    GMap.NET.WindowsForms.Markers.GMapMarkerCross(current);

                //
                // refresh overlays and markers
                //
                gMapExplorer.MarkersEnabled = false;
                OverlayMouse.Markers.Clear();

                //gMapExplorer.Overlays.Clear();
                OverlayMouse.Markers.Add(currentMark);

                gMapExplorer.MarkersEnabled = true;
                Cursor.Current = Cursors.Hand;

                //
                // update label infomation
                //
                label_LatLng.Text =
                    "Lng : " + latLng.Lng.ToString()
                    + "   Lat : " + latLng.Lat.ToString();

                //
                // auto fill value to "create agent" gui part
                //
                textBox_AgentLng.Text = latLng.Lng.ToString();
                textBox_AgentLat.Text = latLng.Lat.ToString();
            }
        }


        /**
         *  On mouse down event of gMapExplorer, 
         *  For selecting markers, and searching which agent it belongs to.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void gMapExplorer_OnMouseDown(object sender, MouseEventArgs e)
        {
            //
            // your mouse must be on certain marker
            //
            if (CurrentMouseOnMarker != null)
            {
                //
                // select this marker
                //
                SelectedMarker = CurrentMouseOnMarker;
                GMapMarkerCircle marker = (GMapMarkerCircle)SelectedMarker;

                //
                // cancel all marker's selection 
                //
                CoreController.DeselectMarkers();

                //
                // select this one
                //
                marker.IsSelected = true;

                //
                // search target agent according to this marker
                //
                for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
                {
                    Agent target = CoreController.God.WorldAgentList[ii];
                    
                    if (target == null)
                        continue;
                    
                    //
                    // found!
                    //
                    if (target.Marker.Equals(marker))
                    {
                        //
                        // find index in listBoxAgentList, and highlight it
                        //
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

                        //
                        // find index in listBox(Joined)AgentList
                        // and highlight it
                        //
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

        /**
         *  On marker enter event of gMapExplorer, 
         *  for detecting mouse status about markers.
         *  
         *  @param m - the marker which is entered by mouse
         */
        private void gMapExplorer_OnMarkerEnter(GMapMarker m)
        {
            //
            // correct class of marker
            //
            if (m is GMapMarkerCircle)
            {
                //
                // current marker pointer points to it
                //
                CurrentMouseOnMarker = m;

                GMapMarkerCircle circle = (GMapMarkerCircle)m;
                
                //
                // change diameter
                //
                circle.CircleDiameter = 
                    (int)CircleDiameterTypes.IntActiveDiameter;

            } 
      
        }


        /**
         *  On marker leave event of gMapExplorer, 
         *  for detecting mouse status about markers.
         *  
         *  @param m - the marker which mouse leaves
         */
        private void gMapExplorer_OnMarkerLeave(GMapMarker m)
        {
            //
            // correct class of marker
            //
            if (m is GMapMarkerCircle)
            {
                //
                // current marker pointer points to null
                //
                CurrentMouseOnMarker = null;

                GMapMarkerCircle circle = (GMapMarkerCircle)m;
                
                //
                // change diameter
                //
                circle.CircleDiameter = 
                    (int)CircleDiameterTypes.IntPassiveDiameter;

            }

        }


        /**
         *  Mouse up event of gMapExplorer, 
         *  for detecting mouse status.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void gMapExplorer_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // update label status text
            //
        }

        private void gMapExplorer_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng latLng = gMapExplorer.FromLocalToLatLng(e.X, e.Y);

            //
            // update label infomation
            //
            label_LatLng.Text =
                "Lng : " + latLng.Lng.ToString()
                + "   Lat : " + latLng.Lat.ToString();

            label_MapZoom.Text = "MapZoomLevel: " + gMapExplorer.Zoom.ToString();

        }
        
        #endregion

        /**
         *  This method redraws all markers on gMapExplorer. 
         *  Use for refreshing gMapExplorer's agents
         */
        public void RefreshGMapMarkers()
        {
            //
            // refresh overlays and markers
            // initialize
            //
            gMapExplorer.MarkersEnabled = false;
            OverlayMouse.Markers.Clear();
            OverlayAgents.Markers.Clear();
            gMapExplorer.Overlays.Clear();

            //
            // add (draw) each agent
            //
            for (int ii = 0; ii <= CoreController.God.AgentNumber; ii++)
            {
                Agent currentAgent = CoreController.God.WorldAgentList[ii];

                if (currentAgent == null)
                    continue;

                //
                // add new target marker to GMap
                //
                OverlayAgents.Markers.Add(currentAgent.Marker);
                

            }

            gMapExplorer.Overlays.Add(OverlayMouse);
            gMapExplorer.Overlays.Add(OverlayAgents);
            gMapExplorer.MarkersEnabled = true;

        }

        /**
         *  This method fills agent types in GUI.
         *  (according to DLL functions)  
         */
        public void RefreshAgentTypeOptions() 
        {
            //
            //agent list box configuration 
            //
            listBoxAgentType.SelectionMode = SelectionMode.One;

            listBoxAgentType.Items.Clear();

            //
            //dynamically add agent types in .dll
            //
            for (int ii = 0; ii < CoreController.SelectedClasses.Count; ii++)
            {
                string agentType = CoreController.SelectedClasses[ii].ToString();


                if (agentType.Contains("ABDiSE.Model.AgentClasses."))
                {
                    agentType = agentType.Remove(0, "ABDiSE.Model.AgentClasses.".Length);

                }

                //agentType = agentType.ToUpper();

                listBoxAgentType.Items.Add(agentType);

            }
        
        }
     
        /**
         *  Transforms index according to user's selection.
         *  
         *  @param index wrong index before transform
         *  
         *  @return correct index of target parameter
         */ 
        public int AgentTypeIndexTransform(int index) 
        {
            string type = listBoxAgentType.Items[index].ToString();
            string fullName = "ABDiSE.Model.AgentClasses.";
            fullName += type;

            int correctIndex = -1;
            for (int ii = 0; ii < CoreController.Classes.Count; ii++)
            {
                //
                // find the correct index of 
                // selected item(type)
                //
                if (fullName.Equals(CoreController.Classes[ii].ToString()))
                {
                    correctIndex = ii;
                    break;
                }

            }

            return correctIndex;
        }

        /**
         *  This method fills agent data in GUI,
         *  according to list box agent type.
         *  Use for refreshing "create agent" status if index is changed.  
         */
        public void RefreshAgentDataOptions()
        {
            //
            // current selected index (may be wrong)
            //
            int index = listBoxAgentType.SelectedIndex;

            //
            //index do not exist
            //
            if (index < 0)
            {
                return;
            }

            //
            // index transform
            //
            int correctIndex = AgentTypeIndexTransform(index);            

            //
            // initialize
            //
            listBoxAgentControl.Items.Clear();

                
            if(correctIndex >= 0)
            {
                for (int ii = 0; 
                    ii < CoreController.ConfigStrings[correctIndex].SubTypes.Count; 
                    ii++)
                {
                    string subTypes = 
                        CoreController.ConfigStrings[correctIndex].SubTypes[ii].AgentSubType;

                    if (subTypes.Contains("ABDiSE.Model.AgentClasses."))
                    {
                        subTypes = 
                            subTypes.Remove(0, "ABDiSE.Model.AgentClasses.".Length);

                    }
                    listBoxAgentControl.Items.Add(subTypes);

                }
            }
            else
            {
                listBoxAgentControl.Items.Add("NULL error");
                listBoxAgentControl.Items.Add("check");
                listBoxAgentControl.Items.Add("OPETIONS");
                listBoxAgentControl.Items.Add("IN");
                listBoxAgentControl.Items.Add("agent dll code");
                
            }   

        }

        /**
         *  This method fills agent details in GUI.
         *  (according to ConfigStrings)          
         *  use for refreshing "create agent" status if index is changed.
         */
        public void RefreshAgentData()
        {
            //
            // initiallize with blank 
            //
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
            
            //
            // index do not exist
            //
            if (index < 0) 
                return;

            int index1 = AgentTypeIndexTransform(index);

            int index2 = listBoxAgentControl.SelectedIndex;

            //
            // do not exist
            //
            if (index2 < 0 || index1 < 0)
                return;
                       
            //
            // fill properties in UI
            //
            if (CoreController.ConfigStrings[index1].Keys.Count > 0)
            {
                textBox_K01.Text =
                    CoreController.ConfigStrings[index1].Keys[0];
                textBox_V01.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[0];
            }
            if (CoreController.ConfigStrings[index1].Keys.Count > 1)
            {
                textBox_K02.Text =
                    CoreController.ConfigStrings[index1].Keys[1];
                textBox_V02.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[1];
            }
            if (CoreController.ConfigStrings[index1].Keys.Count > 2)
            {
                textBox_K03.Text =
                    CoreController.ConfigStrings[index1].Keys[2];
                textBox_V03.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[2];
            }
            if (CoreController.ConfigStrings[index1].Keys.Count > 3)
            {
                textBox_K04.Text =
                    CoreController.ConfigStrings[index1].Keys[3];
                textBox_V04.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[3];
            }
            if (CoreController.ConfigStrings[index1].Keys.Count > 4)
            {
                textBox_K05.Text =
                    CoreController.ConfigStrings[index1].Keys[4];
                textBox_V05.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[4];
            }
            if (CoreController.ConfigStrings[index1].Keys.Count > 5)
            {
                textBox_K06.Text =
                    CoreController.ConfigStrings[index1].Keys[5];
                textBox_V06.Text =
                    CoreController.ConfigStrings[index1].SubTypes[index2].Values[5];
            }

        }

        /**
         *  This method update/refresh agent list in GUI (right side).
         *  (according to God.WorldAgentList )          
         *  Use for refreshing after agent creation or deletion
         */
        public void RefreshAgentList()
        {
            //
            //world agent list box
            //
            listBoxAgentList.Items.Clear();

            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++ )
            {
                Agent target = CoreController.God.WorldAgentList[ii];

                if (target == null)
                    continue;

                if (target.AgentProperties.ContainsKey("Name") &&
                    target.IsJoinedAgent == false)
                {
                    listBoxAgentList.Items.Add(
                        CoreController.God.WorldAgentList[ii].AgentProperties["Name"]);
                    
                }
            }

            //
            // clear selected markers
            //
            CoreController.DeselectMarkers();

            //
            //for refresh markers
            //
            RefreshGMapMarkers();

        }

        /**
         *  This method update/refresh joined agent list in GUI.
         *  (according to God.WorldAgentList )          
         *  Use for refreshing after joined agent creation or deletion
         */
        public void RefreshJoinedAgentList()
        {
            //
            //world joind agent list box
            //
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

            //
            // clear selected markers
            //
            CoreController.DeselectMarkers();

            //
            //for refresh markers
            //
            RefreshGMapMarkers();
        }

        /**
         * CreateAgent() uses data in GUI(left side) to call CoreController, 
         * and then calls constructor in agent dll dynamically.
         * 
         * This method is Dynamic Loading DLL Controller (DLC)
         * 
         * @return succeed of fail.
         */
        public MethodReturnResults CreateAgent()
        {
            //
            // current position in gMap
            //
            PointLatLng LatLng = new PointLatLng();

            //
            // input PointLatLng null
            //
            if (textBox_AgentLat.Text == "" || textBox_AgentLng.Text == "")
                return MethodReturnResults.FAIL;

            LatLng.Lat = double.Parse(textBox_AgentLat.Text);
            LatLng.Lng = double.Parse(textBox_AgentLng.Text);


            Dictionary<string, string> Properties = 
                new Dictionary<string, string>();

            //
            // check if null
            //
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

            //
            // no selection
            //
            if (currentIndex < 0)
                return MethodReturnResults.FAIL;

            int newIndex = AgentTypeIndexTransform(currentIndex);

            if (newIndex < 0)
                return MethodReturnResults.FAIL;

            //
            // create agent instance
            //
            string agentClass = CoreController.ConfigStrings[newIndex].ClassFullName;

            //
            // call DLL to create agent instance
            // ***important
            //
            /*
            CoreController.CreateDLLInstance(
                agentClass, 
                CoreController, 
                Properties, 
                LatLng, 
                CoreController.God.WorldEnvironmentList[0]
                );
            */
            CoreController.God.Create(
                agentClass, 
                CoreController, 
                Properties, 
                LatLng, 
                CoreController.God.WorldEnvironmentList[0]
                );

            return MethodReturnResults.SUCCEED;
        }

        /**
         *  Click event of button "create agent" in GUI.
         *  Use for "create agent".
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void buttonCreate_Click(object sender, EventArgs e)
        {

            CreateAgent();
            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();

            //
            // clear lat and lng info
            //
            textBox_AgentLat.Text = "";
            textBox_AgentLng.Text = "";
        }
        
        /**
         *  Click event of button "start simulation" in GUI.
         *  Use for starting timer.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
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

        /**
         *  Click event of button "puase simulation" in GUI.
         *  Use for stoping timer.
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.
         */
        private void buttonPause_Click(object sender, EventArgs e)
        {
            
            button_Pause.Enabled = false;
            button_SimStart.Enabled = true;

            //CoreController.STP.EndPool();

            CoreController.DisableMarkerAnimation();
            
        }
     
        /**
         *  Load event of MainWindow in GUI.
         *  Use for initializing.
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data. 
         */
        private void MainWindow_Load(object sender, EventArgs e)
        {

            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();

            initBackgroundWorker();

            button_SimStart.Enabled = true;
        }

        /**
         *  Key press event of textBox_C0X in GUI.
         *  This function limits user who can only input digits.
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data. 
         */
        private void textBox_C0X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /**
         *  SelectedIndexChanged event of listBoxAgentList in GUI.
         *  For searching agent (by name) in GUI.
         *  Every name of agent should be unique. 
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.  
         */
        private void listBoxAgentList_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            int index = listBoxAgentList.SelectedIndex;

            if (index < 0)
                return;

            string name =  listBoxAgentList.Items[index].ToString();

            Agent target = null;

            //
            // search world agent list
            //
            for(int ii=0; ii< CoreController.God.AgentNumber; ii++)
            {
                if (CoreController.God.WorldAgentList[ii] == null)
                    continue;

                if (!CoreController.God.WorldAgentList[ii].AgentProperties.ContainsKey("Name"))
                    continue;

                
                if (name.Equals(
                    CoreController.God.WorldAgentList[ii].AgentProperties["Name"].ToString()))
                {
                    //
                    // found target
                    //
                    target = CoreController.God.WorldAgentList[ii];

                    //
                    // clear markers
                    //
                    CoreController.DeselectMarkers();
                    
                    target.Marker.IsSelected = true;
                    RefreshGMapMarkers();

                    break;
                }
            }

            //
            // target not found
            //
            if (target == null)
                return;
            
            //
            // updating label text
            //
            label_AgentProperties.Text = string.Format(
                "{0}\n{1}\n\nIsActivated:{2}\nIsDead:{3}\nCurrentStep:{4}\n", 
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead, target.CurrentStep);

            //
            // display properties
            //
            foreach (KeyValuePair<string, string> item
                in target.AgentProperties)
            {

                label_AgentProperties.Text += 
                    string.Format("{0} :  \t{1}\n", item.Key, item.Value);
            }

            //
            //clear another listbox
            //
            listBoxJoinedAgentList.ClearSelected();

        }

        /**
         *  SelectedIndexChanged event of listBoxAgentType in GUI.
         *  For filling data of selected agent type.   
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.   
         */
        private void listBoxAgentType_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            RefreshAgentDataOptions();
            RefreshAgentData();

        }

        /**
         *  SelectedIndexChanged event of listBoxAgentControl in GUI.
         *  For filling data of selected agent type and class. 
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void listBoxAgentControl_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            RefreshAgentData();
        }

        /**
         *  SelectedIndexChanged event of listBoxJoinedAgentList in GUI.
         *  For searching joined agent (by name) in GUI.
         *  Every name of agent should be unique.  
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.   
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
                "{0}\n{1}\n\nIsActivated:{2}\nIsDead:{3}\nCurrentStep:{4}\n",
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead, target.CurrentStep);

            foreach (KeyValuePair<string, string> item
                in target.AgentProperties)
            {

                label_AgentProperties.Text +=
                    string.Format("{0} : \t{1}\n", item.Key, item.Value);
            }

            //clear another listbox
            listBoxAgentList.ClearSelected();
        }

        //
        /// agent lock for multi-thread version
        //
        private Object ClearDeadAgentLock = new Object();


        /**
         *  Click event for buttonCreateTP.
         *  This method queue test workitems into new SimpleThreadPool.
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void buttonCreateTP_Click(object sender, EventArgs e)
        {
            //
            // disable this button before this method complete
            //
            button_CreateTP.Enabled = false;


            // 
            // call background worker
            //
            bw.RunWorkerAsync(workerType.createTPTest);

            button_CreateTP.Enabled = true;
        }


        /**
         *  This method queues test workitems into new SimpleThreadPool 
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
                    new WaitCallback(ABDiSE.Controller.ThreadPool.SimpleThreadPool.ShowTestMessage),
                    count.ToString());

                //delayed queue time
                //TODO: change sleep location
                //Thread.Sleep((int)numericUpDown_STPQueueDelay.Value);

            }
            Console.WriteLine("STP: all workitems already in queue");
            Console.WriteLine("STP: SimpleThreadPool Started and all workitems are already in queue");

            CoreController.STP.EndPool();
        }


        /**
         *  click event for button_EndPool
         *  this method calls EndPool 
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_EndPool_Click(object sender, EventArgs e)
        {
            CoreController.STP.EndPool(false);
        }


        /**
         *  click event for button_CancelPool_Click
         *  this method calls CancelPool
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_CancelPool_Click(object sender, EventArgs e)
        {
            CoreController.STP.EndPool(true);
        }

        /**
         *  click event for button_StepSim
         *  this method simulates one step of all the agents
         *       1. start thread pool
         *       2. queue workitems
         *       3. end pool
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_StepSim_Click(object sender, EventArgs e)
        {
            //
            // disable this button until STP completed
            //
            button_StepSim.Enabled = false;


            if (bw.IsBusy == false)
            {
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

                CoreController.God.ClearDeadAgent();

                //RefreshGMapMarkers();
                RefreshAgentList();
                RefreshJoinedAgentList();
                RefreshGMapMarkers();

                CoreController.God.CurrentStep++;

                label_GodCurrentStep.Text = CoreController.God.CurrentStep.ToString();


            }

            //enable this button
            button_StepSim.Enabled = true;
        }

        /**
         *  this method simulates one step of all the agents
         *  called by background worker
         *  
         *       1. start thread pool
         *       2. queue workitems
         *       3. end pool
         *    
         */
        private void stepSimulation() 
        {
            //
            // start new round (one step) of simulation
            //
            CoreController.StartThreadPool(
                (int)numericUpDown_STPThreadsNum.Value,
                (int)numericUpDown_STPIdleTime.Value,
                (int)numericUpDown_STPExecuteTime.Value

                );

            //
            //step N : stage 2
            //

            Console.WriteLine
                ("Step[" + CoreController.God.CurrentStep +
                "] launching {0} Agent tasks..", CoreController.God.AgentNumber);
            //compute Joined Agents

            Agent joinedAgent = null;
            
            //
            // every joined agent runs update() by itself
            // put workitems into threadpool
            //
            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {

                joinedAgent = CoreController.God.WorldAgentList[ii];

                //
                // check IsJoinedAgent property
                //
                if (joinedAgent != null && joinedAgent.IsJoinedAgent)
                {
                    //
                    // if need update
                    //
                    if (joinedAgent.CurrentStep < CoreController.God.CurrentStep)
                    {
                        //
                        //update
                        //
                        CoreController.STP.QueueUserWorkItem
                            (joinedAgent.ThreadPoolCallback, ii.ToString());
                        
                    }
                    else
                    {
                        //
                        //wait until next step
                        //
                        continue;
                    }

                }
                else
                {
                    continue;
                }
            }
            //
            // Wait for all threads in pool to calculation...
            //


            //TODO: barrier between two stages

            //
            // compute agent
            //

            Agent agent = null;

            //
            // every agent runs update() by itself
            //
            for (int ii = 0; ii < CoreController.God.AgentNumber; ii++)
            {
                agent = CoreController.God.WorldAgentList[ii];

                //
                // check IsJoinedAgent is false
                //
                if (agent != null && agent.IsJoinedAgent != true)
                {
                    //
                    // if need update
                    //
                    if (agent.CurrentStep < CoreController.God.CurrentStep)
                    {
                        //
                        // do self update
                        //
                        CoreController.STP.QueueUserWorkItem
                            (agent.ThreadPoolCallback, ii.ToString());

                    }
                    else
                    {
                        //
                        // wait until next step
                        //
                        continue;
                    }
                }
                else
                {
                    //
                    // check next agent
                    //
                    continue;
                }

            }


            CoreController.STP.EndPool();

            CoreController.God.ClearDeadAgent();
        
        }

        /**
         *  this method sets default tooltip for certain control units
         *  for example: label , textbox, etc.
         */
        private void setToolTips()
        {
            ToolTip label_AgentCoordTitleTip = new ToolTip();
            label_AgentCoordTitleTip.SetToolTip(
                gMapExplorer, 
                "雙擊地圖兩下以自動取得LatLng資訊\nDouble click to fill Lat and Lng data\n" + 
                "右鍵拖曳地圖以移動\nRight click and drag to move map"
                );

        }




        /**
         *  This method calls a new form for selecting needed DLL (agent types)
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_SelectDLL_Click(object sender, EventArgs e)
        {
            SelectDLLForm newForm = new SelectDLLForm(CoreController);


            newForm.LoadDLLClasses(CoreController.Classes);
            newForm.ShowDialog(this);

            newForm.Dispose();

            //
            // refresh agent types (according to selected dlls)
            //
            RefreshAgentTypeOptions();
        }

        /**
         *  save current experiment into XML files
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_SaveExperiment_Click(object sender, EventArgs e)
        {
            CoreController.XMLController.Save();
        }

        /**
         *  load experiment from existing xml files
         * 
         *  @param sender - refers to the object that invoked the event
         *  @param e - the Event Argument of the object, contains the event data.    
         */
        private void button_LoadExperiment_Click(object sender, EventArgs e)
        {
            DialogResult result = 
                MessageBox.Show("Loading agents will overwrite current AgentList", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                CoreController.XMLController.Load();

                //refresh gui
                RefreshAgentList();
                RefreshJoinedAgentList();
                RefreshGMapMarkers();
                RefreshAgentData();
                textBox_AgentLat.Text = "";
                textBox_AgentLng.Text = "";

                //update god's current step from one loaded agent
                CoreController.God.CurrentStep = CoreController.God.WorldAgentList[0].CurrentStep;
                label_GodCurrentStep.Text = CoreController.God.CurrentStep.ToString();

                //move map explorer
                if (CoreController.God.WorldAgentList[0] != null)
                {

                    gMapExplorer.Position = CoreController.God.WorldAgentList[0].LatLng;
                }
                else
                {
                    gMapExplorer.Position = new PointLatLng(24.797332, 120.995304);
                }
                gMapExplorer.Zoom = 17;

            }
           
            
        }


        private void comboBox_MapProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox_MapProvider.SelectedItem.ToString()){

                case "BingHybirdMap":
                    gMapExplorer.MapProvider = GMapProviders.BingHybridMap;
                    break;
                case "BingMap":
                    gMapExplorer.MapProvider = GMapProviders.BingMap;
                    break;
                case "BingSatelliteMap":
                    gMapExplorer.MapProvider = GMapProviders.BingSatelliteMap;
                    break;

                case "ArcGIS_StreetMap_World_2D_Map":
                    gMapExplorer.MapProvider = GMapProviders.ArcGIS_StreetMap_World_2D_Map;
                    break;
                case "ArcGIS_World_Physical_Map":
                    gMapExplorer.MapProvider = GMapProviders.ArcGIS_World_Physical_Map;
                    break;
                case "GoogleMap":
                    gMapExplorer.MapProvider = GMapProviders.GoogleMap;
                    break;

                case "OpenCycleMap":
                    gMapExplorer.MapProvider = GMapProviders.OpenCycleMap;
                    break;
                case "OpenStreetMap":
                    gMapExplorer.MapProvider = GMapProviders.OpenStreetMap;
                    break;
                case "OpenStreetOsm":
                    gMapExplorer.MapProvider = GMapProviders.OpenStreetOsm;
                    break;

            }

            //gMapExplorer.Refresh();
        }

        private void loadMapProvidersForComboBox()
        {
            comboBox_MapProvider.Items.Clear();
            comboBox_MapProvider.Items.Add("BingMap");
            comboBox_MapProvider.Items.Add("BingHybirdMap");
            comboBox_MapProvider.Items.Add("BingSatelliteMap");
            comboBox_MapProvider.Items.Add("ArcGIS_StreetMap_World_2D_Map");
            comboBox_MapProvider.Items.Add("ArcGIS_World_Physical_Map");
            comboBox_MapProvider.Items.Add("GoogleMap");
            comboBox_MapProvider.Items.Add("OpenCycleMap");
            comboBox_MapProvider.Items.Add("OpenStreetMap");
            comboBox_MapProvider.Items.Add("OpenStreetOsm");

        }

        private Size smallSize;
        private Size fullSize;
        private Point smallButtonLocation;
        private Point mapLocation;

        private void button_FullScreen_Click(object sender, EventArgs e)
        {
            if (smallSize.Width == 0)
                smallSize = gMapExplorer.Size;
            if (fullSize.Width == 0)
                //fullSize = new Size(1059, 735);
                fullSize = this.Size;
            if (smallButtonLocation.X ==0)
                smallButtonLocation = button_FullScreen.Location;
            if (mapLocation.X == 0)
                mapLocation = gMapExplorer.Location;

            // small size
            if (gMapExplorer.Size == smallSize) 
            {
                
                gMapExplorer.Size = fullSize;
                gMapExplorer.Location = new Point(0, 0);
                button_FullScreen.Text = "Back";
                button_FullScreen.Location = new Point(10, 10);
            }
            else //full screen size
            {
                
                gMapExplorer.Size = smallSize;
                gMapExplorer.Location = new Point(323, 12);

                button_FullScreen.Text = "FullScreen";
                button_FullScreen.Location = smallButtonLocation;
            }

        }

        public void RefreshEnvTextboxes()
        { 
            //pointer to the only one environment
            Dictionary<string, string> envProperties =  
                CoreController.God.WorldEnvironmentList[0].EnvProperties;

            int count = envProperties.Count;
            
            if(count > 0){
                textBox_ek1.Text = envProperties.ElementAt(0).Key.ToString();
                textBox_ev1.Text = envProperties.ElementAt(0).Value.ToString();
            }
            if (count > 1)
            {
                textBox_ek2.Text = envProperties.ElementAt(1).Key.ToString();
                textBox_ev2.Text = envProperties.ElementAt(1).Value.ToString();
            }
            if (count > 2)
            {
                textBox_ek3.Text = envProperties.ElementAt(2).Key.ToString();
                textBox_ev3.Text = envProperties.ElementAt(2).Value.ToString();
            }
            if (count > 3)
            {
                textBox_ek4.Text = envProperties.ElementAt(3).Key.ToString();
                textBox_ev4.Text = envProperties.ElementAt(3).Value.ToString();
            }
            if (count > 4)
            {
                textBox_ek5.Text = envProperties.ElementAt(4).Key.ToString();
                textBox_ev5.Text = envProperties.ElementAt(4).Value.ToString();
            }
            if (count > 5)
            {
                textBox_ek6.Text = envProperties.ElementAt(5).Key.ToString();
                textBox_ev6.Text = envProperties.ElementAt(5).Value.ToString();
            }
            if (count > 6)
            {
                textBox_ek7.Text = envProperties.ElementAt(6).Key.ToString();
                textBox_ev7.Text = envProperties.ElementAt(6).Value.ToString();
            }
            if (count > 7)
            {
                textBox_ek8.Text = envProperties.ElementAt(7).Key.ToString();
                textBox_ev8.Text = envProperties.ElementAt(7).Value.ToString();
            }
            if (count >8 )
            {
                textBox_ek9.Text = envProperties.ElementAt(8).Key.ToString();
                textBox_ev9.Text = envProperties.ElementAt(8).Value.ToString();
            }
        
        }

        public void SaveEnvTextboxes()
        {
            Dictionary<string, string> newProperties = 
                new Dictionary<string,string>();

            if (!newProperties.ContainsKey(textBox_ek1.Text) &&
                textBox_ek1.Text != "")
                newProperties.Add(textBox_ek1.Text, textBox_ev1.Text);
            if (!newProperties.ContainsKey(textBox_ek2.Text) &&
                textBox_ek2.Text != "")
                newProperties.Add(textBox_ek2.Text, textBox_ev2.Text);
            if (!newProperties.ContainsKey(textBox_ek3.Text) &&
                textBox_ek3.Text != "")
                newProperties.Add(textBox_ek3.Text, textBox_ev3.Text);
            if (!newProperties.ContainsKey(textBox_ek4.Text) &&
                textBox_ek4.Text != "")
                newProperties.Add(textBox_ek4.Text, textBox_ev4.Text);
            if (!newProperties.ContainsKey(textBox_ek5.Text) &&
                textBox_ek5.Text != "")
                newProperties.Add(textBox_ek5.Text, textBox_ev5.Text);
            if (!newProperties.ContainsKey(textBox_ek6.Text) &&
                textBox_ek6.Text != "")
                newProperties.Add(textBox_ek6.Text, textBox_ev6.Text);
            if (!newProperties.ContainsKey(textBox_ek7.Text) &&
                textBox_ek7.Text != "")
                newProperties.Add(textBox_ek7.Text, textBox_ev7.Text);
            if (!newProperties.ContainsKey(textBox_ek8.Text) &&
                textBox_ek8.Text != "")
                newProperties.Add(textBox_ek8.Text, textBox_ev8.Text);
            if (!newProperties.ContainsKey(textBox_ek9.Text) &&
                textBox_ek9.Text != "")
                newProperties.Add(textBox_ek9.Text, textBox_ev9.Text);

            //replace
            CoreController.God.WorldEnvironmentList[0].EnvProperties = 
                newProperties;

        }

        private void button_saveEnvData_Click(object sender, EventArgs e)
        {
            SaveEnvTextboxes();
        }



    }

    

    #region GMAP CUSTOM MARKER


    /**
     *  This class extends GMapMarker
     *  used for drawing custom circle marker      
     *      
     *  reuse internet open source code, edit by Lightorz
     */
    public class GMapMarkerCircle : GMap.NET.WindowsForms.GMapMarker
    {

        #region Properties

        //
        /// animation mode
        //
        public bool IsAnimated = false;

        //
        /// selected marker will show additional square
        //
        public bool IsSelected = false;

        //
        /// The pen for the outer circle
        //
        public Pen OuterPen { get; set; }

        //
        /// The brush for the inner circle
        //
        public Brush InnerBrush { get; set; }

        //
        /// The brush for the Text
        //
        public Brush TextBrush { get; set; }

        //
        /// The font for the text
        //
        public Font TextFont { get; set; }

        //
        /// The text to display inside of the marker 
        //
        public String Text { get; set; }

        private int diameter = 10;

        public int SelectedOffset = 15;

        //
        /// need to load images
        //
        public Image[] CustomImages;

        public int ImageX, ImageY;

        public bool IsSquare = true;
        public bool IsCircle = false;

        //
        ///random images index
        //
        public Random RandomSeed;

        //
        /// The size of the circle
        //
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

        /**
         *  Constructor of GMapMarkerCircle (1 param)
         *  
         *  @param p point in lat and lng format
         */
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


        /**
         *  Constructor of GMapMarkerCircle  (5 params)
         *  
         *  @param p  point in lat and lng format
         *  @param outer  The pen for the outer ring
         *  @param inner The brush for the inner circle
         *  @param diam The diameter in pixel of the whole circle
         *  @param text The text in the marker
         */
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


        /**
         *  Constructor of GMapMarkerCircle  (6 params)
         *  
         *  @param p  point in lat and lng format
         *  @param outer  The pen for the outer ring
         *  @param inner The brush for the inner circle
         *  @param diam The diameter in pixel of the whole circle
         *  @param text The text in the marker
         *  @param textBrush The brush for the text
         */
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



        /**
         *  Main method to render a circle
         *  
         *  @param g Graphics
         */
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
