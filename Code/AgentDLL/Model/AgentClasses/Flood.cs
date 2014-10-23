/** 
 *  @file Flood.cs
 *  Flood is an Agent type in the OpenISDM ABDiSE project.
 *  It is one of the basic NaturalElementAgentTypes object.
 *  Detail information is in the SetDefaultConfigStrings method.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
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
 *      2014/10/14 remake 2.0 flood
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABDiSE.Controller;
using GMap.NET;
using System.Drawing;

namespace ABDiSE.Model.AgentClasses
{
    /**
     *  Flood is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic NaturalElementAgentTypes object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Flood : Agent
    {

        //
        /// flood water amount will randomly change according to this variable
        /// 0 = will not randomly change
        /// 100 = plus or minus 100% 
        //
        public const double FLOOD_RANDOM_PERCENTAGE = 10;
        public const double FLOOD_MOVEMENT_UNIT = 
            Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * 9.2;

        /**
         *  No parameter consturctor for testing.
         */
        public Flood()
            : base()
        {
            Console.WriteLine("flood() called");
        }

        /**
         *  Constructor of Flood. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of the Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Flood(
            CoreController CoreController,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Model.Environment AgentEnvironment
            )
            : base(
             CoreController,
             Properties,
             LatLng,
             AgentEnvironment
             )
        {
            this.AgentType = "Flood";

            this.IsNaturalElementAgent = true;
            this.IsAttachableObjectAgent = false;

            //set properties 
            if (Properties.ContainsKey("Name"))
            {
                this.AgentProperties = Properties;

                //add id to agent's name
                this.AgentProperties["Name"] = Properties["Name"] + "#" + CoreController.God.AgentNumber;
            }
            else
            {


            }


        }


        /**
         *  Set defualt configuration of Flood agent as a sample.
         *  
         *  @return ConfigStrings data structure
         */
        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "Flood";
            ConfigStrings.ClassFullName = typeof(Flood).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FloodType");
            ConfigStrings.Keys.Add("FloodLevel");
            ConfigStrings.Keys.Add("FloodDepth");


            // assign types
            SubTypeStrings flashFloods = new SubTypeStrings();
            flashFloods.AgentSubType = "FlashFloods";
            flashFloods.Values.Add("FlashFloods");
            flashFloods.Values.Add("FlashFloods");
            flashFloods.Values.Add("2");
            flashFloods.Values.Add("10");
            ConfigStrings.SubTypes.Add(flashFloods);

            SubTypeStrings coastalFloods = new SubTypeStrings();
            coastalFloods.AgentSubType = "CoastalFloods";
            coastalFloods.Values.Add("CoastalFloods");
            coastalFloods.Values.Add("CoastalFloods");
            coastalFloods.Values.Add("2");
            coastalFloods.Values.Add("20");
            ConfigStrings.SubTypes.Add(coastalFloods);

            SubTypeStrings ponding = new SubTypeStrings();
            ponding.AgentSubType = "Ponding";
            ponding.Values.Add("Ponding");
            ponding.Values.Add("Ponding");
            ponding.Values.Add("2");
            ponding.Values.Add("30");
            ConfigStrings.SubTypes.Add(ponding);

            SubTypeStrings riverFloods = new SubTypeStrings();
            riverFloods.AgentSubType = "RiverFloods";
            riverFloods.Values.Add("RiverFloods");
            riverFloods.Values.Add("RiverFloods");
            riverFloods.Values.Add("2");
            riverFloods.Values.Add("10");
            ConfigStrings.SubTypes.Add(riverFloods);

            SubTypeStrings urbanFloods = new SubTypeStrings();
            urbanFloods.AgentSubType = "UrbanFloods";
            urbanFloods.Values.Add("UrbanFloods");
            urbanFloods.Values.Add("UrbanFloods");
            urbanFloods.Values.Add("2");
            urbanFloods.Values.Add("15");
            ConfigStrings.SubTypes.Add(urbanFloods);

            SubTypeStrings defaultFloods = new SubTypeStrings();
            defaultFloods.AgentSubType = "defaultFloods";
            defaultFloods.Values.Add("defaultFloods");
            defaultFloods.Values.Add("defaultFloods");
            defaultFloods.Values.Add("0");
            defaultFloods.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultFloods);

            //ConfigStrings.Print();

            return ConfigStrings;
        }


        /**
         *  Update() will be executed in every simulation steps.
         *  User can customize this method to model different agent behaviors.
         */
        public override void Update()
        {
            //
            // terminate, if this agent has been updated
            //
            if (this.CurrentStep >= CoreController.God.CurrentStep)
                return;


            this.CoreController.God.CheckAgentAttachment(this);

            this.SimulateFloods();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /** 
         *  Simulate floods movement
         *  Specify behavior rules of floods
         *  
         *  User can edit this method.
         *  For example, add formula for it.
         */
        public void SimulateFloods()
        {
            // data missing
            if (!AgentProperties.ContainsKey("FloodLevel") || !AgentProperties.ContainsKey("FloodDepth"))
            {
                this.IsActivated = false;
                this.IsDead = true;
            }

            double level = double.Parse(this.AgentProperties["FloodLevel"]);
            double depth = double.Parse(this.AgentProperties["FloodDepth"]);

            //it doesn't need simulate
            if(level <= 0 || depth <= 0){
                this.IsActivated = false;
                this.IsDead = true;
                return;
            }
            else if (depth >= 100)
            {

                level /= 2;
                depth /= 2;

                //
                // create new flood agent
                //
                Dictionary<string, string> newAgentProperties
                    = new Dictionary<string, string>();

                newAgentProperties.Add("Name", this.AgentProperties["Name"] + "created ");
                newAgentProperties.Add("FloodType", this.AgentProperties["FloodType"]);
                newAgentProperties.Add("FloodLevel", level.ToString());
                newAgentProperties.Add("FloodDepth", depth.ToString());

                

                Flood newAgent = new Flood(
                    CoreController,
                    newAgentProperties,
                    this.LatLng,
                    this.MyEnvironment
                    );

            }


            // simulate movement
            FloodMove();

            string weather = this.MyEnvironment.EnvProperties["Weather"];
            double rainfall = double.Parse(this.MyEnvironment.EnvProperties["RainFall"]);

            switch(weather)
            {
                case "Sunny":
                    depth -= rainfall/2;
                    //level -= 1;
                    break;
                case "Cloudy":

                    break;
                case "Rainy":
                    depth += rainfall; 
                    break;

            }

            //write back
            this.AgentProperties["FloodLevel"] = level.ToString();
            this.AgentProperties["FloodDepth"] = depth.ToString();
            
        }

        /**
         * flood agent moves according to this method.
         * TODO: formula
         * @return MethodReturnResults - succeed , fail, etc
         */
        public MethodReturnResults FloodMove()
        {

            double unit = FLOOD_MOVEMENT_UNIT;

            //
            //random variable
            //
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = FLOOD_RANDOM_PERCENTAGE;

            //random move
            this.LatLng.Lat -=
                unit * Math.Sin(seed.Next(0,360))
                * (1 + 0.001 * range * seed.Next(-10, 10));
            this.LatLng.Lng -=
                unit * Math.Cos(seed.Next(0, 360))
                * (1 + 0.001 * range * seed.Next(-10, 10));

            //
            // update marker point
            //
            this.Marker.Position = this.LatLng;

            return MethodReturnResults.SUCCEED;
        }

        /**
         *  Flood can attach Building and Tree. TODO: attach fire
         *  Different situation will be handled by different code.
         *  User can freely edit the code.
         *  
         *  @param B - the target agent to attach
         *  @return - succeed or fail
         */
        public override MethodReturnResults Attach(Agent B)
        {
            //not the same agent
            if (B == this)
                return MethodReturnResults.FAIL;

            //not activated
            if (this.IsActivated == false || B.IsActivated == false)
                return MethodReturnResults.FAIL;
            //already dead
            if (this.IsDead == true || B.IsDead == true)
                return MethodReturnResults.FAIL;



            //closeby level
            if (B.AgentDistance(this) != MethodReturnResults.FAIL)
            {

                Console.WriteLine("FLOOD: A[{0}] attaches B[{1}]",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                Dictionary<string, string> properties
                            = new Dictionary<string, string>();

                switch (B.AgentType)
                {
                    case "Building":
                        //
                        // create Building Joined with Flood
                        // 
                        properties.Add("Name", B.AgentProperties["Name"] + "(@Floods)");
                        properties.Add("FloodType", this.AgentProperties["FloodType"]);
                        properties.Add("FloodLevel", this.AgentProperties["FloodLevel"]);
                        properties.Add("FloodDepth", this.AgentProperties["FloodDepth"]);
                        properties.Add("BuildingType", B.AgentProperties["BuildingType"]);
                        properties.Add("Floor", B.AgentProperties["Floor"]);
                        properties.Add("BuiltYear", B.AgentProperties["BuiltYear"]);
                        properties.Add("BuildingLife", "100");

                        BuildingJoinedFlood newBuildingFloodAgent =
                            new BuildingJoinedFlood(
                                CoreController,
                                properties,
                                B.LatLng,
                                B.MyEnvironment
                                );

                        //
                        // wait to be disposed(free)
                        //
                        //this.IsActivated = false;
                        //this.IsDead = true;
                        B.IsActivated = false;
                        B.IsDead = true;

                        return MethodReturnResults.SUCCEED;

                    case "Tree":
                        //
                        // create Building Joined with Flood
                        //
                        properties.Add("Name", B.AgentProperties["Name"] + "(@Flood)");
                        properties.Add("FloodType", this.AgentProperties["FloodType"]);
                        properties.Add("FloodLevel", this.AgentProperties["FloodLevel"]);
                        properties.Add("FloodDepth", this.AgentProperties["FloodDepth"]);
                        properties.Add("TreeType", B.AgentProperties["TreeType"]);
                        properties.Add("Height", B.AgentProperties["Height"]);
                        properties.Add("Age", B.AgentProperties["Age"]);
                        properties.Add("TreeLife", "100");

                        TreeJoinedFlood newTreeFloodAgent =
                            new TreeJoinedFlood(
                                CoreController,
                                properties,
                                B.LatLng,
                                B.MyEnvironment
                                );

                        //
                        // wait to be disposed(free)
                        //
                        //this.IsActivated = false;
                        //this.IsDead = true;
                        B.IsActivated = false;
                        B.IsDead = true;

                        return MethodReturnResults.SUCCEED;


                    default:
                        break;

                }



            }

            //
            // nothing happened
            //
            return MethodReturnResults.FAIL;

        }

        /**
         *  Loads multiple .png images for better display in GUI.
         *  User can adjust x and y of image.
         */
        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.Yellow);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;

            Marker.ImageX = -20;
            Marker.ImageY = -20;

            this.Marker.RandomSeed = new Random(Guid.NewGuid().GetHashCode());

            Marker.CustomImages = new Image[3];
            for (int ii = 0; ii < 3; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/FloodImages/flood0" + ii + ".png");
            }

        }

    }
}
