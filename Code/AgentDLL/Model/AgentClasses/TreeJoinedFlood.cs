/** 
 *  @file TreeJoinedFlood.cs
 *  TreeJoinedFlood is an Joined Agent type in the OpenISDM ABDiSE project.
 *  It is one of the basic Joined Agent object.
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
 *      2014/10/14 created
 *
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
     *  BuildingJoinedFire is a joined agent in the OpenISDM ABDiSE project.
     *  It is one of the basic Joined Agent object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class TreeJoinedFlood : Agent
    {

        /**
         *  No parameter consturctor for testing.
         */ 
        public TreeJoinedFlood()
            : base()
        {
            Console.WriteLine("TreeJoinedFlood() called");

        }

        /**
         *  Constructor of BuildingJoinedFire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public TreeJoinedFlood(
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
            this.AgentType = "TreeJoinedFlood";

            this.IsNaturalElementAgent = false;
            this.IsAttachableObjectAgent = false;
            this.IsJoinedAgent = true;

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

        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "TreeJoinedFlood";
            ConfigStrings.ClassFullName = typeof(TreeJoinedFlood).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FloodType");
            ConfigStrings.Keys.Add("FloodLevel");
            ConfigStrings.Keys.Add("FloodDepth");
            ConfigStrings.Keys.Add("TreeType");
            ConfigStrings.Keys.Add("Height");
            ConfigStrings.Keys.Add("Age");
            ConfigStrings.Keys.Add("TreeLife");

            // assign types
            // orders are important!
            SubTypeStrings defaultJA = new SubTypeStrings();
            defaultJA.AgentSubType = "default";
            defaultJA.Values.Add("Tree@Flood-default");
            defaultJA.Values.Add("default");
            defaultJA.Values.Add("0");
            defaultJA.Values.Add("0");
            defaultJA.Values.Add("default");
            defaultJA.Values.Add("0");
            defaultJA.Values.Add("0");
            defaultJA.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultJA);

            //ConfigStrings.Print();

            return ConfigStrings;
        }

        public override void Update()
        {
            //
            // terminate, if this agent has been updated
            //
            if (this.CurrentStep >= CoreController.God.CurrentStep)
                return;


            SimulateTreeJoinedFlood();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /**
         *  Compute life of TreeJoinedFlood agent.
         *  User can freely edit this method.
         */
        public void SimulateTreeJoinedFlood()
        {

            double level = double.Parse(this.AgentProperties["FloodLevel"]);
            double depth = double.Parse(this.AgentProperties["FloodDepth"]);
            double life = double.Parse(this.AgentProperties["TreeLife"]);

            //it doesn't need simulate
            if (level <= 0 || depth <= 0)
            {
                this.IsActivated = false;
                this.IsDead = true;
                
                RecoverTree();

                return;
            }
            if (life <= 0)
            {
                Marker.InnerBrush = new SolidBrush(Color.Black);
                Marker.OuterPen.Color = Color.Blue;
                return;
            }

            string weather = this.MyEnvironment.EnvProperties["Weather"];
            double rainfall = double.Parse(this.MyEnvironment.EnvProperties["RainFall"]);

            switch (weather)
            {
                case "Sunny":
                    depth -= 1;
                    level -= 1;
                    break;
                case "Cloudy":

                    break;
                case "Rainy":
                    depth += rainfall;
                    life -= rainfall;
                    break;

            }
            this.AgentProperties["FloodLevel"] = level.ToString();
            this.AgentProperties["FloodDepth"] = depth.ToString();
            this.AgentProperties["TreeLife"] = life.ToString();

        }

        public void RecoverTree()
        {
            this.IsActivated = false;
            this.IsDead = true;

            //
            // create smoke agents
            //
            Dictionary<string, string> treeProperties
                = new Dictionary<string, string>();

            treeProperties.Add("Name", "Recover" + this.AgentProperties["TreeType"]);
            treeProperties.Add("TreeType", this.AgentProperties["TreeType"]);
            treeProperties.Add("Height", this.AgentProperties["Height"]);
            treeProperties.Add("Age", this.AgentProperties["Age"]);


            Tree newAgent = new Tree(
                CoreController,
                treeProperties,
                this.LatLng,
                this.MyEnvironment
                );
        }


        /**
         * BuildingJoinedFire can attach other agent if user wants.
         *  @todo multi-level attach
         */
        public override MethodReturnResults Attach(Agent B)
        {
            return MethodReturnResults.FAIL;
        }

        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.Green);
            Marker.OuterPen.Color = Color.Blue;
            //OuterPen.Width = 2;


        }

    }
}
