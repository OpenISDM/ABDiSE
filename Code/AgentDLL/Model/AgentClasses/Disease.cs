/** 
 *  @file Disease.cs
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

    public class Disease : Agent
    {

        public const int MOVEMENT_UNIT = 5;

        /**
         *  No parameter consturctor for testing.
         */
        public Disease()
            : base()
        {
            Console.WriteLine("Disease() called");
        }

        /**
         *  Constructor of Fire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Disease(
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
            this.AgentType = "Disease";

            this.IsNaturalElementAgent = true;
            this.IsAttachableObjectAgent = false;

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

            ConfigStrings.ClassShortName = "Disease";
            ConfigStrings.ClassFullName = typeof(Disease).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("Intensity");

            // assign types
            SubTypeStrings classA = new SubTypeStrings();
            classA.AgentSubType = "Ebola";
            classA.Values.Add("Ebola");
            classA.Values.Add("1");
            ConfigStrings.SubTypes.Add(classA);


            SubTypeStrings defaultDisease = new SubTypeStrings();
            defaultDisease.AgentSubType = "unknown";
            defaultDisease.Values.Add("unknown disease");
            defaultDisease.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultDisease);

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
            Simulate();
            this.MoveByWind();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        public void Simulate()
        {
            double intensity = double.Parse(this.AgentProperties["Intensity"]);
            intensity -= 0.3;

            if (intensity <= 0)
            {
                this.IsActivated = false;
                this.IsDead = true;
                return;
            }

            this.AgentProperties["Intensity"] = intensity.ToString();

        }


        /**
         *  Disease can attach human.
         *  
         *  @param B - the attach target agent
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

                Console.WriteLine("\n[{0}] attaches [{1}]",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                Dictionary<string, string> properties
                            = new Dictionary<string, string>();

                switch (B.AgentType)
                {

                    case "Human":
                        //
                        // create Building Joined with Fire
                        // dictionary orders are important!
                        // 
                        properties.Add("Name", B.AgentProperties["Name"] + "@" + this.AgentProperties["Name"]);
                        properties.Add("Intensity", this.AgentProperties["Intensity"]);
                        properties.Add("VirusState", this.AgentProperties["Name"]);
                        properties.Add("MoveSpeed", "0");
                        properties.Add("Age", B.AgentProperties["Age"]);
                        properties.Add("Gender", B.AgentProperties["Gender"]);

                        HumanJoinedDisease newJoinedAgent =
                            new HumanJoinedDisease(
                                CoreController,
                                properties,
                                B.LatLng,
                                B.MyEnvironment
                                );

                        //
                        // wait to be disposed(free)
                        //
                        this.IsActivated = false;
                        this.IsDead = true;
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
            Marker.OuterPen.Color = Color.LightGreen;
            //OuterPen.Width = 2;

        }

    }
}
