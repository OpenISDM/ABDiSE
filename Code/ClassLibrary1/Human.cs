/** 
 *  @file Human.cs
 *  Human is an agent type of people.
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

    public class Human : Agent
    {

        public const double MOVEMENT_UNIT = Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * 9.2;
        public const int RANDOM_PERCENTAGE = 10;
        /**
         *  No parameter consturctor for testing.
         */
        public Human()
            : base()
        {
            Console.WriteLine("Human() called");
        }

        /**
         *  Constructor of Fire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Human(
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
            this.AgentType = "Human";

            this.IsNaturalElementAgent = false;
            this.IsAttachableObjectAgent = true;

            //set properties 
            if (Properties.ContainsKey("Name"))
            {
                this.AgentProperties = Properties;

                //add id to agent's name
                this.AgentProperties["Name"] = Properties["Name"] + "#" + CoreController.God.AgentNumber;
            }

        }


        /**
         *  Set defualt configuration of Building agent as a sample.
         *  
         *  @return ConfigStrings data structure
         */
        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "Human";
            ConfigStrings.ClassFullName = typeof(Human).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("VirusState");
            ConfigStrings.Keys.Add("MoveSpeed");
            ConfigStrings.Keys.Add("Age");
            ConfigStrings.Keys.Add("Gender");

            // assign types
            SubTypeStrings classA = new SubTypeStrings();
            classA.AgentSubType = "Man";
            classA.Values.Add("Man");
            classA.Values.Add("");
            classA.Values.Add("4");
            classA.Values.Add("25");
            classA.Values.Add("male");
            ConfigStrings.SubTypes.Add(classA);

            SubTypeStrings classB = new SubTypeStrings();
            classB.AgentSubType = "Woman";
            classB.Values.Add("Woman");
            classB.Values.Add("");
            classB.Values.Add("4");
            classB.Values.Add("23");
            classB.Values.Add("female");
            ConfigStrings.SubTypes.Add(classB);

            SubTypeStrings classC = new SubTypeStrings();
            classC.AgentSubType = "Boy";
            classC.Values.Add("boy");
            classC.Values.Add("");
            classC.Values.Add("2");
            classC.Values.Add("7");
            classC.Values.Add("male");
            ConfigStrings.SubTypes.Add(classC);

            SubTypeStrings classD = new SubTypeStrings();
            classD.AgentSubType = "Ebola Carrier";
            classD.Values.Add("Ebola Carrier");
            classD.Values.Add("Ebola");
            classD.Values.Add("2");
            classD.Values.Add("30");
            classD.Values.Add("male");
            ConfigStrings.SubTypes.Add(classD);

            SubTypeStrings defaultHuman = new SubTypeStrings();
            defaultHuman.AgentSubType = "default";
            defaultHuman.Values.Add("default human");
            defaultHuman.Values.Add("");
            defaultHuman.Values.Add("0");
            defaultHuman.Values.Add("0");
            defaultHuman.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultHuman);

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

            this.HumanMove();

            this.CurrentStep = CoreController.God.CurrentStep;
        }


        public MethodReturnResults HumanMove()
        {

            double unit = MOVEMENT_UNIT;

            //
            //random variable
            //
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = RANDOM_PERCENTAGE;

            //random move
            this.LatLng.Lat -=
                unit * Math.Sin(seed.Next(0, 360))
                * (1 + 0.001 * range * seed.Next(-RANDOM_PERCENTAGE, RANDOM_PERCENTAGE));
            this.LatLng.Lng -=
                unit * Math.Cos(seed.Next(0, 360))
                * (1 + 0.001 * range * seed.Next(-RANDOM_PERCENTAGE, RANDOM_PERCENTAGE));

            //
            // update marker point
            //
            this.Marker.Position = this.LatLng;

            return MethodReturnResults.SUCCEED;
        }

        /**
         *  Human can not attach 
         *  
         *  @param B - the attach target agent
         *  @return - succeed or fail
         */
        public override MethodReturnResults Attach(Agent B)
        {
            
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

            Marker.InnerBrush = new SolidBrush(Color.LightPink);
            Marker.OuterPen.Color = Color.White;

        }

    }
}
