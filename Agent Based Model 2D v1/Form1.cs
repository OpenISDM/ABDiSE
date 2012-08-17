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



namespace AgentBasedModel
{

    #region MAIN WINDOW UI

    public partial class MainWindow : Form
    {

        //Graphics g;
        //Pen FirePen, BuildingPen, TreePen, SmokePen;
        Random Seed = new Random();
        God God;

        //GMAP
        GMapOverlay OverlayOne;
        GMapOverlay OverlayAgents;
        //String contry;

        public MainWindow(God god)
        {

            this.God = god;

            InitializeComponent();

            // pause disabled
            buttonPause.Enabled = false;

            //agent list box config 
            listBoxAgentType.SelectionMode = SelectionMode.One;

            listBoxAgentType.Items.Add("FIRE");
            listBoxAgentType.Items.Add("SMOKE");
            listBoxAgentType.Items.Add("TREE");
            listBoxAgentType.Items.Add("BUILDING");

            listBoxAgentControl.Items.Add("00");
            listBoxAgentControl.Items.Add("01");
            listBoxAgentControl.Items.Add("02");
            listBoxAgentControl.Items.Add("03");

            //Console initialize
            listBoxConsole.Items.Clear();

        }

        //GMap.NET
        private void gMapExplorer_Load(object sender, EventArgs e)
        { 
            //init map
            gMapExplorer.Position = new PointLatLng(24.797332, 120.995304);


            GMapProvider.Language = LanguageType.ChineseTraditional;
            gMapExplorer.MapProvider = GMapProviders.YahooMap;
            gMapExplorer.MinZoom = 3;
            gMapExplorer.MaxZoom = 17;
            gMapExplorer.Zoom = 11;
            gMapExplorer.Manager.Mode = AccessMode.ServerAndCache;
            gMapExplorer.MarkersEnabled = true;
            //gMapExplorer.Dock = DockStyle.Fill;
            

            labelLatLng.Text = "Lng : " + gMapExplorer.Position.Lng.ToString() 
                + "   Lat : " + gMapExplorer.Position.Lat.ToString();

            OverlayOne = new GMapOverlay(gMapExplorer, "OverlayOne");
            OverlayAgents = new GMapOverlay(gMapExplorer, "OverlayAgents");
            /*
            overlayOne.Markers.Add(new 
                GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new
                    PointLatLng(24.797332, 120.995304)));
            */
            gMapExplorer.Overlays.Add(OverlayOne);
            gMapExplorer.Overlays.Add(OverlayAgents);

            //gMapExplorer.DragButton = MouseButtons.Left;

            Cursor.Current = Cursors.WaitCursor;
            var current = new PointLatLng(gMapExplorer.Position.Lat, 
                gMapExplorer.Position.Lng);

            var currentMark = new GMap.NET.WindowsForms.Markers.
                GMapMarkerGoogleGreen(current);

            Cursor.Current = Cursors.Default;
            gMapExplorer.MouseDoubleClick += new MouseEventHandler
                (gMapExplorer_MouseDoubleClick);
        }

        void gMapExplorer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.WaitCursor;

                PointLatLng latLng = gMapExplorer.FromLocalToLatLng(e.X, e.Y);

                var current = new PointLatLng
                    (Math.Abs(latLng.Lat), latLng.Lng);

                var currentMark = new 
                    GMap.NET.WindowsForms.Markers.GMapMarkerCross(current);

                //refresh overlays and markers
                gMapExplorer.MarkersEnabled = false;
                OverlayOne.Markers.Clear();
                //gMapExplorer.Overlays.Clear();
                OverlayOne.Markers.Add(currentMark);
                //gMapExplorer.Overlays.Add(OverlayOne);
                gMapExplorer.MarkersEnabled = true;
                Cursor.Current = Cursors.Hand;

                labelLatLng.Text =
                    "Lng : " + latLng.Lng.ToString()
                    + "   Lat : " + latLng.Lat.ToString();

                textBox_AgentLng.Text = latLng.Lng.ToString();
                textBox_AgentLat.Text = latLng.Lat.ToString();
            }
        }

        // Refresh gMapExplorer's agents
        
        public void RefreshGMapMarkers()
        {
            
            // refresh overlays and markers
            // initialize
            gMapExplorer.MarkersEnabled = false;
            OverlayOne.Markers.Clear();
            OverlayAgents.Markers.Clear();
            gMapExplorer.Overlays.Clear();

            // add each agent
            for (int ii = 0; ii <= God.AgentCount; ii++ )
            {
                Agent CurrentAgent = God.WorldAgentList[ii];

                if (CurrentAgent == null)
                    continue;

                if (CurrentAgent.AttachableObjectAgentType
                    != AttachableObjectAgentTypes.NULL)
                {
                    switch (CurrentAgent.AttachableObjectAgentType)
                    {
                        case AttachableObjectAgentTypes.BUILDING:
                            var BuildingMarker =
                                new GMap.NET.WindowsForms.
                                    Markers.GMapMarkerGoogleGreen
                                    (CurrentAgent.LatLng);

                            OverlayAgents.Markers.Add(BuildingMarker);

                            break;
                        case AttachableObjectAgentTypes.TREE:
                            var TreeMarker =
                                new GMap.NET.WindowsForms.
                                    Markers.GMapMarkerGoogleGreen
                                    (CurrentAgent.LatLng);

                            OverlayAgents.Markers.Add(TreeMarker);
                            break;
                        default:
                            break;
                    }
                }
                else if (CurrentAgent.NaturalElementAgentType
                   != NaturalElementAgentTypes.NULL)
                {
                    switch (CurrentAgent.NaturalElementAgentType)
                    {
                        case NaturalElementAgentTypes.FIRE:
                            var FireMarker =
                                new GMap.NET.WindowsForms.
                                    Markers.GMapMarkerGoogleRed
                                    (CurrentAgent.LatLng);

                            OverlayAgents.Markers.Add(FireMarker);
                            break;
                        case NaturalElementAgentTypes.SMOKE:
                            var SmokeMarker =
                                new GMap.NET.WindowsForms.
                                    Markers.GMapMarkerGoogleRed
                                    (CurrentAgent.LatLng);

                            OverlayAgents.Markers.Add(SmokeMarker);
                            break;
                        default:
                            break;
                    }
                }
            }//end of for-loop

            gMapExplorer.Overlays.Add(OverlayOne);
            gMapExplorer.Overlays.Add(OverlayAgents);
            gMapExplorer.MarkersEnabled = true;

        }

        public void RefreshAgentData()
        {
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

            string type = listBoxAgentType.Items[index].ToString();

            int index2 = listBoxAgentControl.SelectedIndex;
            //do not exist
            if (index2 < 0)
                return;


            string ctrl = listBoxAgentControl.Items[index2].ToString();
            
            int ctrlInt = int.Parse(ctrl);

            /*
            MyPrintf(string.Format(
                "current list box agentType index:{0} tostring:{1}",
                index, type)
                );
            */

            Dictionary<string, string> EnterProperties =
                new Dictionary<string, string>();

            switch (type)
            {
                case "FIRE":
                    EnterProperties = 
                        new AgentDataXML(God).ReturnFireProperties(ctrlInt);
                    break;

                case "SMOKE":
                    EnterProperties =
                        new AgentDataXML(God).ReturnSmokeProperties(ctrlInt);
                    break;

                case "BUILDING":
                    EnterProperties =
                        new AgentDataXML(God).ReturnBuildingProperties(ctrlInt);
                    break;

                case "TREE":
                    EnterProperties =
                        new AgentDataXML(God).ReturnTreeProperties(ctrlInt);
                    break;

                default:
                    break;

            }

            // fill properties in UI
            if (EnterProperties.Count>0){
                textBox_K01.Text = EnterProperties.ElementAt(0).Key;
                textBox_V01.Text = EnterProperties.ElementAt(0).Value;
            }
            if (EnterProperties.Count>1){
                textBox_K02.Text = EnterProperties.ElementAt(1).Key;
                textBox_V02.Text = EnterProperties.ElementAt(1).Value;
            }
            if (EnterProperties.Count>2){
                textBox_K03.Text = EnterProperties.ElementAt(2).Key;
                textBox_V03.Text = EnterProperties.ElementAt(2).Value;
            }
            if (EnterProperties.Count>3){
                textBox_K04.Text = EnterProperties.ElementAt(3).Key;
                textBox_V04.Text = EnterProperties.ElementAt(3).Value;
            }
            if (EnterProperties.Count > 4){
                textBox_K05.Text = EnterProperties.ElementAt(4).Key;
                textBox_V05.Text = EnterProperties.ElementAt(4).Value;
            }
            if (EnterProperties.Count>5){
                textBox_K06.Text = EnterProperties.ElementAt(5).Key;
                textBox_V06.Text = EnterProperties.ElementAt(5).Value;
            }
        }

        public void RefreshAgentList()
        {
            MyPrintf("RefreshAgentList");

            //world agent list box
            listBoxAgentList.Items.Clear();
            int ii;
            string output;
            for (ii = 0; ii <= God.AgentCount; ii++ )
            {
                if (God.WorldAgentList[ii] == null)
                    continue;

                if (God.WorldAgentList[ii].Properties.ContainsKey("Name")){
                    listBoxAgentList.Items.Add(
                        God.WorldAgentList[ii].Properties["Name"]);
                    
                    output = string.Format("Current name '{0}'", 
                        God.WorldAgentList[ii].Properties["Name"]);
                }
            }
            //output = string.Format("Stop in ii = {0}", ii);
            //MyPrintf(output);


            //for testing Markers
            RefreshGMapMarkers();

        }

        public void RefreshJoinedAgentList()
        {
            //MyPrintf("RefreshAgentList");
            //world agent list box
            listBoxJoinedAgentList.Items.Clear();
            int ii;
            string output;

            for (ii = 0; ii <= God.JoinedAgentNumber; ii++)
            {
                if (God.WorldJoinedAgentList[ii] == null)
                    continue;

                if (God.WorldJoinedAgentList[ii].Properties.ContainsKey("Name"))
                {
                    listBoxJoinedAgentList.Items.Add(
                        God.WorldJoinedAgentList[ii].Properties["Name"]);

                    output = string.Format("Current name '{0}'",
                        God.WorldJoinedAgentList[ii].Properties["Name"]);
                }
            }
            //output = string.Format("Stop in ii = {0}", ii);
            //MyPrintf(output);
        }

        public void MyPrintf(string String)
        {
            listBoxConsole.Items.Add(String);
            listBoxConsole.TopIndex = listBoxConsole.Items.Count-1;
        }


        //this method let UI can call God.create (internal)
        public void CreateAgent()
        {

            PointLatLng LatLng = new PointLatLng();

            LatLng.Lat = double.Parse(textBox_AgentLat.Text);
            LatLng.Lng = double.Parse(textBox_AgentLng.Text);


            Dictionary<string, string> Properties = 
                new Dictionary<string, string>();

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
                return;

            switch (listBoxAgentType.Items[currentIndex].ToString())
            {
                    
                case "FIRE":

                    God.create(NaturalElementAgentTypes.FIRE,
                        Properties,
                        LatLng,
                        God.WorldEnvironmentList[0]
                        );

                    
                    //draw it
                    //g.DrawRectangle(FirePen, Target);

                    break;

                case "SMOKE":
                    God.create(
                        NaturalElementAgentTypes.SMOKE,
                        Properties,
                        LatLng,
                        God.WorldEnvironmentList[0]
                        );

                    //g.DrawRectangle(SmokePen, Target);
                    break;

                case "TREE":
                    God.create(
                        AttachableObjectAgentTypes.TREE,
                        Properties,
                        LatLng,
                        God.WorldEnvironmentList[0]     
                        );

                    //g.DrawRectangle(TreePen, Target);
                    break;

                case "BUILDING":
                    God.create(
                        AttachableObjectAgentTypes.BUILDING,
                        Properties,
                        LatLng,
                        God.WorldEnvironmentList[0]
                        );

                    //g.DrawRectangle(BuildingPen, Target);
                    break;

                default:
                    break;

            }
            

        }


        private void buttonCreate_Click(object sender, EventArgs e)
        {

            CreateAgent();
            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();
      
        }





        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonPause.Enabled = true;
            buttonStart.Enabled = false;

            SimulationTimer.Start();
            
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            buttonPause.Enabled = false;
            buttonStart.Enabled = true;

            SimulationTimer.Stop();
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {
            RefreshAgentData();
            RefreshAgentList();
            RefreshJoinedAgentList();
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SimulationTimer.Interval = (int)(numericUpDown1.Value * 1000);
        }


        private void textBox_C0X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

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

            for(int ii=0; ii<God.AgentNumber; ii++)
            {
                if (God.WorldAgentList[ii] == null)
                    continue;

                /*if(God.WorldAgentList[ii].Properties.ContainsKey("Name"))
                    MyPrintf(string.Format(
                        "name:{0}, list:{1}", 
                        name, 
                 * God.WorldAgentList[ii].Properties["Name"].ToString()
                    ));
                */
                if (name.Equals(
                    God.WorldAgentList[ii].Properties["Name"].ToString())){
                    target = God.WorldAgentList[ii];
                    //MyPrintf("target found!");
                    break;
                }
            }
            
            labelAgentProperties.Text = string.Format(
                "({0}, {1})\n\nIsActivated:{2}\nIsDead:{3}\n", 
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead);



            foreach (KeyValuePair<string, string> item
                in target.Properties)
            {

                labelAgentProperties.Text += 
                    string.Format("{0}::{1}\n", item.Key, item.Value);
            }

            //MyPrintf(string.Format("Text : {0}", Text));

            //clear another listbox
            listBoxJoinedAgentList.ClearSelected();

        }

        private void listBoxAgentType_SelectedIndexChanged(
            object sender, EventArgs e)
        {

            RefreshAgentData();

        }

        private void listBoxAgentControl_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            RefreshAgentData();
        }

        private void listBoxJoinedAgentList_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            int index = listBoxJoinedAgentList.SelectedIndex;

            if (index < 0)
                return;

            string name = listBoxJoinedAgentList.Items[index].ToString();
            /*
            MyPrintf(string.Format(
                "selectedIndexChanged: AgentList[{0}]", name)
                );
            */

            JoinedAgent target = null;

            for (int ii = 0; ii < God.WorldJoinedAgentList.Length; ii++)
            {
                if (God.WorldJoinedAgentList[ii] == null)
                    break;

                /*if(God.WorldAgentList[ii].Properties.ContainsKey("Name"))
                    MyPrintf(string.Format(
                        "name:{0}, list:{1}", 
                        name, 
                 * God.WorldAgentList[ii].Properties["Name"].ToString()
                    ));
                */
                if (name.Equals(
                    God.WorldJoinedAgentList[ii].Properties["Name"].ToString()))
                {
                    target = God.WorldJoinedAgentList[ii];
                    //MyPrintf("target found!");
                    break;
                }
            }

            labelAgentProperties.Text = string.Format(
                "({0}, {1})\n\nIsActivated:{2}\nIsDead:{3}\n",
                target.LatLng.Lat, target.LatLng.Lng,
                target.IsActivated, target.IsDead);



            foreach (KeyValuePair<string, string> item
                in target.Properties)
            {

                labelAgentProperties.Text +=
                    string.Format("{0}::{1}\n", item.Key, item.Value);
            }

            //clear another listbox
            listBoxAgentList.ClearSelected();
        }







    }

    #endregion

    #region DEFINATIONS

    // version 3
    public enum NaturalElementAgentTypes
    {
        //nature agents
        FIRE, SMOKE, FUILD,

        //it belongs to the AttachableObjectAgentTypes
        NULL,

        //end of types
        MAXIMUM_AGENT

    }
    public enum AttachableObjectAgentTypes
    {

        //others
        TREE, BUILDING, HUMAN, ANIMAL, CAR, SHIP,

        //it belongs to the NaturalElementAgentTypes
        NULL,

        //end of types
        MAXIMUM_AGENT

    }

    /*
     * Class Map
     * 
     * (temp data structure)
     * The simulation world environment is the set of grids.
     * I'll try to make our own environment with non-uniform grids (or some better ideas).
     * 
     * */
    /*
    public class Map
    {

        public Map()
        {

        }


    }
     * */

    #endregion

    #region COORDINATE STRUCT




    /*
     * struct CoordinateStruct
     * 
     * this struct have multiple levels
     * level 0: one grid, simple x, y
     * level 1: grid with 1/4 size (0,0) (0,1) (1,0) (1,1)
     * level 2: grid with 1/4 size .....
     */
    public struct CoordinateStruct
    {
        public int X, Y;

    }


    public class CoordinateCreater
    {
        //c# windows applicaiton
        MainWindow MainWindow;

        public void SetMainWindow(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public static CoordinateStruct[] CreateRaondomCoordinate(
            int CoordinateMaxLevel)
        {

            //System.Threading.Thread.Sleep(1000);
            Random seed = new Random();
            int random = seed.Next();
            
            CoordinateStruct[] TargetCoordinte;

            // create correct position coordinate
            if (CoordinateMaxLevel > 0){
                TargetCoordinte = new CoordinateStruct[CoordinateMaxLevel];
            }else{
                TargetCoordinte = null;
                //error!
            }

            
            for (int ii = 0; ii < CoordinateMaxLevel; ii++)
            {
                
                if (ii < 1){
                    TargetCoordinte[ii].X = seed.Next(0, 255);
                    TargetCoordinte[ii].Y = seed.Next(0, 255);
                }else{
                    TargetCoordinte[ii].X = seed.Next(0, 2);
                    TargetCoordinte[ii].Y = seed.Next(0, 2);
                }
            }

            return TargetCoordinte;
        }
    }

    /*
     * class CoordinateSystem
     * 
     * this system have multiple levels
     * level 0: one grid, simple x, y
     * level 1: grid with 1/4 size
     */
    /*
    public class CoordinateSystem
    {
        public int X, Y;

        public CoordinateSystem(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }


    }
    */

    /*
     * 
     * Class Environment
     * 
     * this class can create “target environment”
     * according to custom class Map, or other GIS data format
     * 
     * A local environment of a region in the world is a set of parameters that affects the objects in the region
     * 
     */

    #endregion

    #region AGNET DEFAULT DATA

    public class AgentDataXML
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        God God;

        public AgentDataXML(God God)
        {
            this.God = God;
        }

        public Dictionary<string, string> ReturnFireProperties(int ControlFlag)
        {
            //initialize
            AgentProperties.Clear();
            AgentProperties.Add("Name", string.Format(
                "Default Fire - 0{0}", God.AgentNumber+1));

            switch (ControlFlag)
            {
                case 0:
                    AgentProperties.Add("FireClass", "A");
                    AgentProperties.Add("FireLevel", "0");
                    AgentProperties.Add("FireType", "Minor");
                    AgentProperties.Add("FireProperty01", "Control Flag 00");
                    AgentProperties.Add("FireProperty02", "Control Flag 00");
                    break;
                case 1:
                    AgentProperties.Add("FireClass", "B");
                    AgentProperties.Add("FireLevel", "5");
                    AgentProperties.Add("FireType", "Strong");
                    AgentProperties.Add("FireProperty01", "Control Flag 01");
                    AgentProperties.Add("FireProperty02", "Control Flag 01");
                    break;
                case 2:
                    AgentProperties.Add("FireClass", "C");
                    AgentProperties.Add("FireLevel", "10");
                    AgentProperties.Add("FireType", "Great");
                    AgentProperties.Add("FireProperty01", "Control Flag 02");
                    AgentProperties.Add("FireProperty02", "Control Flag 02");
                    break;
                case 3:
                    AgentProperties.Add("FireClass", "D");
                    AgentProperties.Add("FireLevel", "20");
                    AgentProperties.Add("FireType", "Ultra");
                    AgentProperties.Add("FireProperty01", "Control Flag 03");
                    AgentProperties.Add("FireProperty02", "Control Flag 03");
                    break;
                default:
                    AgentProperties.Add("FireClass", "");
                    AgentProperties.Add("FireLevel", "");
                    AgentProperties.Add("FireType", "");
                    AgentProperties.Add("FireProperty01", "");
                    AgentProperties.Add("FireProperty02", "");
                    break;
            }
            return AgentProperties;
        }

        // data from http://www.servpro.com/fire_smoke
        public Dictionary<string, string> ReturnSmokeProperties(int ControlFlag)
        {
            //initialize
            AgentProperties.Clear();
            AgentProperties.Add("Name", string.Format(
                "Default Smoke - 0{0}", God.AgentNumber+1));
            switch (ControlFlag)
            {
                case 0:
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Wet Smoke Residues");
                    AgentProperties.Add("SmokeProperty01", "Control Flag 00");
                    break;
                case 1:
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Dry Smoke Residues");
                    AgentProperties.Add("SmokeProperty01", "Control Flag 01");
                    break;
                case 2:
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Protein Residues");
                    AgentProperties.Add("SmokeProperty01", "Control Flag 02");
                    break;
                case 3:
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Fuel Oil Soot");
                    AgentProperties.Add("SmokeProperty01", "Control Flag 03");
                    break;
                default:
                    AgentProperties.Add("SmokeLevel", "");
                    AgentProperties.Add("SmokeType", "");
                    AgentProperties.Add("SmokeProperty01", "");
                    break;
            }
            return AgentProperties;
        }


        public Dictionary<string, string> ReturnTreeProperties(int ControlFlag)
        {
            //initialize
            AgentProperties.Clear();
            AgentProperties.Add("Name", string.Format(
                "Default Tree - 0{0}", God.AgentNumber+1));
            switch (ControlFlag)
            {
                case 0:
                    AgentProperties.Add("TreeType", "Pine Tree");
                    AgentProperties.Add("Height", "10.0");
                    AgentProperties.Add("Age", "20");
                    AgentProperties.Add("TreeProperty01", "Control Flag 00");
                    break;
                case 1:
                    AgentProperties.Add("TreeType", "Pine Tree");
                    AgentProperties.Add("Height", "20.0");
                    AgentProperties.Add("Age", "20");
                    AgentProperties.Add("TreeProperty01", "Control Flag 01");
                    break;
                case 2:
                    AgentProperties.Add("TreeType", "Pine Tree");
                    AgentProperties.Add("Height", "30.0");
                    AgentProperties.Add("Age", "20");
                    AgentProperties.Add("TreeProperty01", "Control Flag 02");
                    break;
                default:
                    AgentProperties.Add("TreeType", "");
                    AgentProperties.Add("Height", "");
                    AgentProperties.Add("Age", "");
                    AgentProperties.Add("TreeProperty01", "");
                    break;
            }
            return AgentProperties;
        }
        public Dictionary<string, string> ReturnBuildingProperties(
            int ControlFlag)
        {
            //initialize
            AgentProperties.Clear();
            AgentProperties.Add("Name", string.Format(
                "Default Building - 0{0}", God.AgentNumber+1));
            switch (ControlFlag)
            {
                case 0:
                    AgentProperties.Add("BuildingType", "Single-Story House");
                    AgentProperties.Add("Floor", "1");
                    AgentProperties.Add("BuiltYear", "5");
                    AgentProperties.Add("BuildingProperty01", "Control Flag 00");
                    break;
                case 1:
                    AgentProperties.Add("BuildingType", "Villa");
                    AgentProperties.Add("Floor", "3");
                    AgentProperties.Add("BuiltYear", "5");
                    AgentProperties.Add("BuildingProperty01", "Control Flag 01");
                    break;
                case 2:
                    AgentProperties.Add("BuildingType", "Edifice");
                    AgentProperties.Add("Floor", "30");
                    AgentProperties.Add("BuiltYear", "5");
                    AgentProperties.Add("BuildingProperty01", "Control Flag 02");
                    break;
                default:
                    AgentProperties.Add("BuildingType", "");
                    AgentProperties.Add("Floor", "");
                    AgentProperties.Add("BuiltYear", "");
                    AgentProperties.Add("BuildingProperty01", "");
                    break;
            }
            return AgentProperties;
        }

    }


    #endregion

    #region ENVIRONMENT

    public class Environment
    {

        //c# windows applicaiton
        MainWindow MainWindow;


        // reference of agents, recorded by environment
  	    // this list is used for searching
        // agents in this list are NOT created by environment, just references
        public Agent[] RefAgentList;
        public int[] RefAgentID;

        // average altitude of this local environment
        public double AvgAltitude { get; set; }

        // rain fall data
        public double RainFall { get; set; }

        // wind speed data
        public double WindSpeed { get; set; }
        
        // wind direction data, maybe 0 ~ 360 degree
        public double WindDirection { get; set; }

        // local air temperature
        public double Temperature { get; set; }

        // local citizen's and animal's population 
        public int[] Population { get; set; }

        // historical data dictionary
        public Dictionary<string, string> HistoricalData =
            new Dictionary<string, string>();


        public void SetMainWindow(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public Environment(
            int MaxNumberOfAgents, double AvgAltitude, 
            double RainFall, double WindSpeed, 
            double WindDirection, double Temperature, int Population
            )
        {
            this.RefAgentList = new Agent[MaxNumberOfAgents];
            this.RefAgentID = new int[MaxNumberOfAgents];
            this.AvgAltitude = AvgAltitude;
            this.RainFall = RainFall;
            this.WindSpeed = WindSpeed;
            this.WindDirection = WindDirection;
            this.Temperature = Temperature;
            this.Population = new int[10];
            this.Population[0] = Population;

            

        }

        // update during time period
         
        /*
        public void updateData(...)
        {
            this.RainFall = ...;
        }
        */

        // get all data of local environment
        public void DisplayAllData()
        {
           

            MainWindow.MyPrintf("-DisplayAllData");
            string s = string.Format(
                "Display Some Data: Population[0]:" + Population[0]);

            MainWindow.MyPrintf(s);

            // return all data
            //return EnvironmentDataStruct;
        }
       
    }
    #endregion

    #region GOD AND MAIN

    /*
     * Class God
     * 
     * Each Simulation experiment is set up through this entity. 
     * 
     * 
     */
    public class God
    {
        //c# windows applicaiton
        public MainWindow MainWindow;

        public Environment[] WorldEnvironmentList;
        // reference of agents, recorded by God
  	    // this list is used for recording ALL agents in the simulation world
        public Agent[] WorldAgentList;
        public JoinedAgent[] WorldJoinedAgentList;

        // for naming new Agent (Maximum array index)
        public int AgentNumber = 0;
        public int JoinedAgentNumber = 0;

        //real agent count
        public int AgentCount = 0;
        public int JoinedAgentCount = 0;

        public Dictionary <string, string> properties =
            new Dictionary<string, string>();

        private int MaximumEnvironments = 100;
        private int MaximumAgents = 1000;

        private God()
        {
            //assign pointers
            MainWindow = new MainWindow(this);

            this.WorldEnvironmentList = new Environment[MaximumEnvironments];
            this.WorldAgentList = new Agent[MaximumAgents];
            this.WorldJoinedAgentList = new JoinedAgent[MaximumAgents];
        }


        //add target environment to god's list
        public void AddToEnvironmentList(Environment En)
        {

            for (int ii = 0; ii < WorldEnvironmentList.Length; ii++)
            {
                if (WorldEnvironmentList[ii] != null)
                {
                    WorldEnvironmentList[ii] = En;
                    break;
                }
            }

        }

        public void ClearDeadAgent()
        {

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                //clear dead agent
                if (WorldAgentList[ii] != null &&
                    WorldAgentList[ii].IsDead == true)
                {
                    WorldAgentList[ii] = null;
                    MainWindow.MyPrintf(
                        string.Format("Clear Dead Agent #{0}", ii));
                    // agent number -1
                    AgentCount--;
                }
            }

            for (int ii = 0; ii < JoinedAgentNumber; ii++)
            {
                //clear dead agent
                if (WorldJoinedAgentList[ii] != null &&
                    WorldJoinedAgentList[ii].IsDead == true)
                {
                    WorldJoinedAgentList[ii] = null;
                    MainWindow.MyPrintf(
                        string.Format("Clear Dead JoinedAgent #{0}", ii));
                    // agent number -1
                    JoinedAgentCount--;
                }
            }

        }

        //add target agent to god's list
        public void AddToAgentList(Agent TargetAgent)
        {
            // agent number +1
            AgentNumber++;
            AgentCount++;

            MainWindow.MyPrintf(string.Format(
                    "-AddToAgentList({0})", TargetAgent.Properties["Name"]));

            // way 1: very simple and stupid way: 
            //        search first null slot and replace it

            for (int ii = 0; ii < AgentCount; ii++)
            {

                if (WorldAgentList[ii] == null)
                {
                    WorldAgentList[ii] = TargetAgent;

                    break;
                }
            }


            //windows form
            MainWindow.RefreshAgentList();

        }

        //add target agent to god's list
        public void AddToJoinedAgentList(JoinedAgent TargetAgent)
        {
            JoinedAgentNumber++;
            JoinedAgentCount++;

            MainWindow.MyPrintf(string.Format(
                    "-AddToJoinedAgentList({0})", 
                    TargetAgent.Properties["Name"]));

            // way 1: very simple and stupid way: 
            //        search first null slot and replace it

            for (int ii = 0; ii < JoinedAgentNumber; ii++)
            {

                if (WorldJoinedAgentList[ii] == null)
                {
                    WorldJoinedAgentList[ii] = TargetAgent;

                    break;
                }
            }


            //windows form
            MainWindow.RefreshJoinedAgentList();

        }

        // let the agent checks if there is suitable agent to attach
        public void CheckAgentAttachment(Agent TargetAgent)
        {
            int Result = -1;
            for (int ii = 0; ii < AgentNumber; ii++)
            {
                if (WorldAgentList[ii] != null)
                {
                    Result = TargetAgent.Attach(WorldAgentList[ii]);
                    if (Result != -1)
                        break;
                }
                else
                    continue;
            }

            switch (Result)
            {
                case -1:
                    break;
                case 0:
                    //Attach succeeds
                    ClearDeadAgent();
                    MainWindow.RefreshAgentList();
                    MainWindow.RefreshJoinedAgentList();
                    break;

            }

        }

        // Main
        [STAThread]
        static void Main(string[] args)
        {

            // initialize UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // initialize God
            God god = new God();

            //Application.Run(MainWindow);
            god.MainWindow.MyPrintf(
                "Welcome to Agent-Based Model Simulation World!");

            //initialize default Environment
            Environment HsinchuCity = 
                new Environment(10, 20.0, 10.0, 9.2, 45.1, 31.3, 422554);

            //add to global environments
            god.AddToEnvironmentList(HsinchuCity);

            //set main window for UI
            HsinchuCity.SetMainWindow(god.MainWindow);

            //start main window UI
            god.MainWindow.MyPrintf("\n:::::::::Application.Run::::::::");
            Application.Run(god.MainWindow);

        }


        //( internal	 存取能力限制於同一組件內)
        //組件，Assembly，為編譯後產物，副檔名為dll或exe。
        internal int create(
            NaturalElementAgentTypes agentType, 
            Dictionary<string, string> properties,
            PointLatLng latLng, 
            Environment targetEnvironment
            )
        {
            switch (agentType)
            {
                case NaturalElementAgentTypes.FIRE:
                    // create and initialize new fire
                    // assign correct properties

                    Agent newFire = 
                        new Agent(this, agentType, properties, 
                            latLng, targetEnvironment);
                    
                    //force activated
                    activate(newFire);

                    CheckAgentAttachment(newFire);
                    break;

                case NaturalElementAgentTypes.SMOKE:
                    Agent newSmoke = 
                        new Agent(this, agentType, properties, 
                            latLng, targetEnvironment);

                    //force activated
                    activate(newSmoke);

                    CheckAgentAttachment(newSmoke);
                    break;


                default:
                    //unknown agnet type: error
                    return -1;

            }
            //succeed 
            return 0;
        }


        //( internal	 存取能力限制於同一組件內)
        //組件，Assembly，為編譯後產物，副檔名為dll或exe。
        internal int create(
            AttachableObjectAgentTypes agentType, 
            Dictionary<string, string> properties,
            PointLatLng latLng, 
            Environment targetEnvironment
            )
        {

            switch (agentType)
            {
                case AttachableObjectAgentTypes.BUILDING:

                    Agent newBuilding= 
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newBuilding);
                    break;

                case AttachableObjectAgentTypes.TREE:
                    Agent newTree = 
                        new Agent(this, agentType, properties, 
                            latLng, targetEnvironment);

                    //force activated
                    activate(newTree);
                    break;

                default:
                    //unknown agnet type: error
                    return -1;
            }
            //succeed 
            return 0;
        }

        //Activate: make specified objects active.
        private int activate(Agent target)
        {

            //if(...)
            target.IsActivated = true;
            // and do other things

            //succeed or fail method status
            return 0;
        } 

        //Affect(or Assign): change environment parameters 
        //      and attributes of objects in non-causal ways.
        private int affect(Agent target, Dictionary<string, string> controls)
        {
            //if(controls.ooo)
            //  target.OOO = XXX;
            
            //succeed or fail method status
            return 0;

        }
        //Control: cause an object to change behavior/state in ways not 
        //      defined by object’s own behavior-change methods. 
        private int control(Agent target, Dictionary<string, string> controls)
        {
            //if(controls.ooo)
            //  target.OOO = XXX;
            //succeed or fail method status
            return 0; 
        }
    }
    #endregion

    #region AGENTS
    /*
     * Class Agent
     * 
     * making this class general
     * Methods do behave differently depending on agent types.
     * 
     * 
     */ 
    public class Agent
    {
        //God pointer
        God God;

        public Dictionary<string, string> Properties;


        //v3 types 
        public NaturalElementAgentTypes NaturalElementAgentType;
        public AttachableObjectAgentTypes AttachableObjectAgentType;

        public bool IsDead; 

        public bool IsActivated;

        public PointLatLng LatLng;

        //public CoordinateSystem Coordinate;
        
        public CoordinateStruct[] Coordinate;

        public Environment MyEnvironment;

        // judge/determine collision between Agent A and B
        // return status int
        public int AgentCollision(Agent TargetAgent)
        {
            int CollisionResult = -1;

            double difference = 0.00000001;
            
            //check if [X Y Level 0]
            if (Math.Abs(TargetAgent.LatLng.Lng - this.LatLng.Lng) 
                < difference
                && Math.Abs(TargetAgent.LatLng.Lat - this.LatLng.Lat) 
                < difference)
            {
                // return status: level 0 collision
                CollisionResult = 0;

                if (Math.Abs(TargetAgent.LatLng.Lng - this.LatLng.Lng)
                < difference * 0.1 &&
                    Math.Abs(TargetAgent.LatLng.Lat - this.LatLng.Lat)
                < difference * 0.1)
                {
                    // return status: level 1 collision
                    CollisionResult = 1;
                }

                
            }


            return CollisionResult;
        }


        //v3 
        public Agent(
            God God,
            NaturalElementAgentTypes AgentType, 
            Dictionary<string, string> Properties,
            PointLatLng LatLng, 
            Environment AgentEnvironment
            )
        {
            this.God = God;
            God.MainWindow.MyPrintf(
                    "-constructing NaturalElementAgentTypes V3 Agent");

            this.NaturalElementAgentType = AgentType;
            this.AttachableObjectAgentType = 
                AttachableObjectAgentTypes.NULL;

            IsDead = false;
            IsActivated = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.Properties = Properties;
            
            switch (AgentType)
            {
                //specify every type of agents
                case NaturalElementAgentTypes.FIRE:

                    break;
                case NaturalElementAgentTypes.SMOKE:

                    break;

            }

            God.AddToAgentList(this);

        }
        //v3
        public Agent(
            God God,
            AttachableObjectAgentTypes AgentType, 
            Dictionary<string, string> Properties,
            PointLatLng LatLng, 
            Environment AgentEnvironment
            )
        {

            this.God = God;
            God.MainWindow.MyPrintf(
                "-constructing AttachableObjectAgentTypes V3 Agent");

            this.AttachableObjectAgentType = AgentType;
            this.NaturalElementAgentType =
                NaturalElementAgentTypes.NULL;

            IsDead = false;
            IsActivated = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.Properties = Properties;


            switch (AgentType)
            {
                //specify every type of agents
                case AttachableObjectAgentTypes.BUILDING:

                    break;
                case AttachableObjectAgentTypes.TREE:

                    break;

            }

            God.AddToAgentList(this);

        }

        // A attach B => A.Attach(B)
        // B attached by A => B.AttachedBy(A)
	    public int Attach(Agent AgentB)
	    {
            // attaches to a target if conditions are true
            // Agent A,B itself disappears,
            // create new joined agent
            // ex: becomes agent: target (joined with) fire

            //not activated
            if (this.IsActivated == false || AgentB.IsActivated == false)
                return -1;

            //two types condition
            if ((this.NaturalElementAgentType 
                == NaturalElementAgentTypes.NULL) 
                || (AgentB.AttachableObjectAgentType 
                == AttachableObjectAgentTypes.NULL))
                return -1;

            //if collision
            if (AgentB.AgentCollision(this) >= 0)
            {
                God.MainWindow.MyPrintf(
                    string.Format("Attach Collision! {0} attaches{1}",
                    this.Properties["Name"].ToString(), 
                    AgentB.Properties["Name"].ToString())
                    );

                God.MainWindow.MyPrintf(
                    string.Format("Agent {0} is dead",
                    this.Properties["Name"].ToString()));

                //call attachedby and create new joinedAgent
                AgentB.AttachedBy(this);

                //wait to be disposed(free)
                this.IsActivated = false;
                this.IsDead = true;
                



                //succeed method status
                return 0;
            }
            else
            {
                //nothing happens
                return -1;
            }

        }

        // A attach B => A.Attach(B)
        // B attached by A => B.AttachedBy(A)
        public int AttachedBy(Agent AgentA)
        {

            God.MainWindow.MyPrintf(
                string.Format("{1} Attached By {0}",
                AgentA.Properties["Name"].ToString(),
                this.Properties["Name"].ToString())
                );

            switch(AgentA.NaturalElementAgentType)
            {
                case NaturalElementAgentTypes.FIRE:
                    switch (this.AttachableObjectAgentType)
                    {
                        //possible cases
                        case AttachableObjectAgentTypes.TREE:
                        case AttachableObjectAgentTypes.BUILDING:

                            //create new joined agent
                            JoinedAgent NewJoinedAgent = new JoinedAgent(
                                God,
                                AgentA.NaturalElementAgentType,
                                this.AttachableObjectAgentType,
                                AgentA.Properties, this.Properties,
                                LatLng, MyEnvironment
                                );

                            this.IsActivated = false;
                            this.IsDead = true;

                            
                            break;
                        // impossible cases
                        case AttachableObjectAgentTypes.CAR:
                            //do nothing
                            break;
                    }
                    break;

                case NaturalElementAgentTypes.SMOKE:
                    switch (this.AttachableObjectAgentType)
                    {
                        //possible cases
                        case AttachableObjectAgentTypes.BUILDING:

                            //create new joined agent
                            JoinedAgent NewJoinedAgent = new JoinedAgent(
                                God,
                                AgentA.NaturalElementAgentType,
                                this.AttachableObjectAgentType,
                                AgentA.Properties, this.Properties,
                                LatLng, MyEnvironment
                                );

                            //agent A and B disappers
                            this.IsActivated = false;
                            this.IsDead = true;
                            
                            
                            break;

                        //impossible cases
                        case AttachableObjectAgentTypes.TREE:
                        case AttachableObjectAgentTypes.CAR:
                            break;

                    }
                    break;

                default:
                    break;
            }

            // free itself
            this.IsActivated = false;
            this.IsDead = true;

            return 0;
        }//end of attachedby

        public int EnlargeCoordinateLevel(int TargetLevel)
        {
            string test = "";
            test = String.Format(
                "EnlargeCoordinateLevel({0}) original {1}", 
                TargetLevel, Coordinate.Length);

            God.MainWindow.MyPrintf(test);

            // if bigger
            if (TargetLevel > Coordinate.Length){
                CoordinateStruct[] LargerCoordinateStruct = 
                    new CoordinateStruct[TargetLevel];
                Coordinate.CopyTo(LargerCoordinateStruct, 0);

                foreach (var e in Coordinate)
                {
                    Console.WriteLine(
                        "({0}, {1})", 
                        e.X.ToString(), e.Y.ToString());

                }

                foreach (var e in LargerCoordinateStruct)
                {
                    Console.WriteLine(
                        "({0}, {1})", 
                        e.X.ToString(), e.Y.ToString());

                }

                // replace larger to original 
                God.MainWindow.MyPrintf("replaceing");

                Coordinate = LargerCoordinateStruct;
                /*
                foreach (var e in Coordinate)
                {
                    Console.WriteLine("({0}, {1})", 
                        e.X.ToString(), e.Y.ToString());

                }
                */
                return 0;
            }
            else
                return -1;
        }
    }

    /*
     * Class JoinedAgent 
     * 
     * this agent is combined with two agent (ex A attach B)
     * it will perform various ways according to type A and B.
     */
    public class JoinedAgent
    {
        //c# windows applicaiton UI
        God God;
        public Dictionary<string, string> Properties;

        public NaturalElementAgentTypes AgentTypeA;
        public AttachableObjectAgentTypes AgentTypeB;

        public bool IsDead;

        public bool IsActivated;

        //public CoordinateSystem Coordinate;

        public PointLatLng LatLng;
        //public CoordinateStruct[] Coordinate;

        public Environment MyEnvironment;

        // constructor
        public JoinedAgent(
            God God,
            NaturalElementAgentTypes TypeA, 
            AttachableObjectAgentTypes TypeB, 
            Dictionary<string, string> PropertiesA,
            Dictionary<string, string> PropertiesB,
            PointLatLng LatLng, 
            Environment AgentEnvironment
            )
        {
            this.God = God;

            God.MainWindow.MyPrintf("-constructing JoinedAgent");

            this.AgentTypeA = TypeA;
            this.AgentTypeB = TypeB;
            this.LatLng = LatLng;

            this.MyEnvironment = AgentEnvironment;
            this.IsDead = false;
            this.IsActivated = true;

            switch(AgentTypeA)
            {
                case NaturalElementAgentTypes.FIRE:
                    switch(AgentTypeB)
                    {
                        case AttachableObjectAgentTypes.BUILDING:
                            // assign detail properties
                            Properties = SimulateBuildingJoinedFire
                                (PropertiesA, PropertiesB);
                            Properties.Add("Name", 
                                string.Format("Fire X Building 0{0}"
                                , God.JoinedAgentNumber+1));
                            break;
                        case AttachableObjectAgentTypes.TREE:
                            // assign detail properties
                            Properties = SimulateTreeJoinedFire
                                (PropertiesA, PropertiesB);
                           Properties.Add("Name", 
                                string.Format("Fire X Tree 0{0}", 
                                God.JoinedAgentNumber+1));
                            break;

                        default:
                            //error
                            break;
                    }

                    break;

                case NaturalElementAgentTypes.SMOKE:
                    switch (AgentTypeB)
                    {
                        case AttachableObjectAgentTypes.BUILDING:
                            // assign detail properties
                            Properties = SimulateBuildingJoinedSmoke
                                (PropertiesA, PropertiesB);
                            Properties.Add("Name", 
                                    string.Format("Smoke X Building 0{0}", 
                                    God.JoinedAgentNumber+1));
                            break;

                        default:
                            //error
                            break;
                    }

                    break;

                default:
                    //error
                    break;
            }

            //add to joined agent list
            God.AddToJoinedAgentList(this);

        }//end of constructor



        // Building X Fire
        public Dictionary<string, string> SimulateBuildingJoinedFire(
            Dictionary<string, string> FireProperties,
            Dictionary<string, string> BuildingProperties)
        {
            God.MainWindow.MyPrintf("--SimulateBuildingJoinedFire");
            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();

            NewProperties.Clear();

            // computation
            switch (FireProperties["FireClass"].ToString())
            {
                case "A":
                    switch (BuildingProperties["Floor"].ToString())
                    {
                        case "20":
                            // computation
                            //NewProperties.Add("Level", "A20");
                            break;
                        case "":
                            break;
                        default:
                            break;
                    }
                    break;
                case "B":
                    break;
            }

            //for testing
            FireProperties.Remove("Name");
            BuildingProperties.Remove("Name");

            NewProperties =
                FireProperties.Concat(BuildingProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }

        //Tree X Fire
        public Dictionary<string, string> SimulateTreeJoinedFire(
            Dictionary<string, string> FireProperties, 
            Dictionary<string, string> TreeProperties)
        {
            God.MainWindow.MyPrintf("--SimulateTreeJoinedFire");
            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();
            NewProperties.Clear();

            // computation
            switch (FireProperties["FireClass"].ToString())
            {
                case "A":
                    switch (TreeProperties["Height"].ToString())
                    {
                        case "20":
                            // computation
                            //NewProperties.Add("Level", "A20");
                            break;
                        case "":
                            break;
                        default:
                            break;
                    }
                    break;
                case "B":
                    break;
            }

            //for testing
            FireProperties.Remove("Name");
            TreeProperties.Remove("Name");

            NewProperties = 
                FireProperties.Concat(TreeProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }

        // Building X smoke
        public Dictionary<string, string> SimulateBuildingJoinedSmoke(
            Dictionary<string, string> SmokeProperties,
            Dictionary<string, string> BuildingProperties)
        {

            God.MainWindow.MyPrintf("--SimulateBuildingJoinedSmoke");

            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();

            NewProperties.Clear();

            // computation
            switch (SmokeProperties["SmokeType"].ToString())
            {
                case "A":
                    switch (BuildingProperties["Floor"].ToString())
                    {
                        case "20":
                            // computation
                            //NewProperties.Add("Level", "A20");
                            break;
                        case "":
                            break;
                        default:
                            break;
                    }
                    break;
                case "B":
                    break;
            }

            //for testing
            SmokeProperties.Remove("Name");
            BuildingProperties.Remove("Name");
            NewProperties =
                SmokeProperties.Concat(BuildingProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }


    }

    #endregion

}
