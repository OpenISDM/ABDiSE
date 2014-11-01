/** 
 *  @file HumanJoinedDisease.cs
 *  HumanJoinedDisease is an Joined Agent type in the OpenISDM ABDiSE project.
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
 *      2014/10/24 alpha version
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
     *  HumanJoinedDisease is a joined agent in the OpenISDM ABDiSE project.
     */
    public class HumanJoinedDisease : Agent
    {


        /**
         *  No parameter consturctor for testing.
         */ 
        public HumanJoinedDisease()
            : base()
        {
            Console.WriteLine("HumanJoinedDisease() called");

        }

        /**
         *  Constructor of BuildingJoinedFire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public HumanJoinedDisease(
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
            this.AgentType = "HumanJoinedDisease";

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

        }

        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "HumanJoinedDisease";
            ConfigStrings.ClassFullName = typeof(HumanJoinedDisease).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("Intensity");
            ConfigStrings.Keys.Add("VirusState");
            ConfigStrings.Keys.Add("MoveSpeed");
            ConfigStrings.Keys.Add("Age");
            ConfigStrings.Keys.Add("Gender");


            // assign types

            SubTypeStrings defaultHumanJoinedDisease = new SubTypeStrings();
            defaultHumanJoinedDisease.AgentSubType = "default";
            defaultHumanJoinedDisease.Values.Add("Human@Disease");
            defaultHumanJoinedDisease.Values.Add("0");
            defaultHumanJoinedDisease.Values.Add("Ebola");
            defaultHumanJoinedDisease.Values.Add("0");
            defaultHumanJoinedDisease.Values.Add("33");
            defaultHumanJoinedDisease.Values.Add("male");
            ConfigStrings.SubTypes.Add(defaultHumanJoinedDisease);

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


            Simulate();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /**
         *  Compute life of BuildingJoinedFire agent.
         *  User can freely edit this method.
         */
        public void Simulate()
        {
            if (!IsActivated)
            {
                return;
            }

            Dictionary<string, string> properties
                            = new Dictionary<string, string>();

            properties.Add("Name", "Ebola");
            properties.Add("Intensity", "1");


            Disease newAgent = new Disease(
                CoreController,
                properties,
                this.LatLng,
                this.MyEnvironment
                );

            this.IsActivated = false;
          
            Marker.InnerBrush = new SolidBrush(Color.Black);
            Marker.OuterPen.Color = Color.YellowGreen;
        }


        /**
         *  can attach other agent if user wants.
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

            Marker.InnerBrush = new SolidBrush(Color.YellowGreen);
            Marker.OuterPen.Color = Color.Black;

            
            //OuterPen.Width = 2;


        }

    }
}
