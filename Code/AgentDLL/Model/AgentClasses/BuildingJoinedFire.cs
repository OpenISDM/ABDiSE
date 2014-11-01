/** 
 *  @file BuildingJoinedFire.cs
 *  BuildingJoinedFire is an Joined Agent type in the OpenISDM ABDiSE project.
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
 *  Version:
 *
 *      2.0
 *
 *  Abstract:
 *
 *      BuildingJoinedFire is an Joined Agent type in the OpenISDM ABDiSE project.
 *      It is one of the basic Joined Agent object.
 *      Detail information is in the SetDefaultConfigStrings method.
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
 *      2014/7/3: edit comments for doxygen
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
    public class BuildingJoinedFire : Agent
    {

        public const int CREATE_NEW_FIRE_THRESHOLD = 50;

        /**
         *  No parameter consturctor for testing.
         */ 
        public BuildingJoinedFire()
            : base()
        {
            Console.WriteLine("BuildingJoinedFire() called");

        }

        /**
         *  Constructor of BuildingJoinedFire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public BuildingJoinedFire(
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
            this.AgentType = "BuildingJoinedFire";

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

            ConfigStrings.ClassShortName = "BuildingJoinedFire";
            ConfigStrings.ClassFullName = typeof(BuildingJoinedFire).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");
            ConfigStrings.Keys.Add("BuildingType");
            ConfigStrings.Keys.Add("Floor");
            ConfigStrings.Keys.Add("BuiltYear");
            ConfigStrings.Keys.Add("BuildingLife");

            // assign types

            SubTypeStrings defaultBuildingJoinedFire = new SubTypeStrings();
            defaultBuildingJoinedFire.AgentSubType = "default";
            defaultBuildingJoinedFire.Values.Add("Building@Fire-default");
            defaultBuildingJoinedFire.Values.Add("default");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("default");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultBuildingJoinedFire);

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


            SimulateBuildingJoinedFireLife();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /**
         *  Compute life of BuildingJoinedFire agent.
         *  User can freely edit this method.
         */
        public void SimulateBuildingJoinedFireLife()
        {
            // firelife null - error
            if (!this.AgentProperties.ContainsKey("FireLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                Console.WriteLine("{0} firelife null - error", this.AgentProperties["Name"]);
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            double currentFireLife = double.Parse(this.AgentProperties["FireLife"]);
            double currentBuildingLife = double.Parse(this.AgentProperties["BuildingLife"]);


            if (currentFireLife > CREATE_NEW_FIRE_THRESHOLD)
            {   
                //
                // create new fire agent and
                // share "Fire Life" for it 
                //
                currentFireLife *=0.8;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                double currentFireLevel = double.Parse(this.AgentProperties["FireLevel"]);
                
                currentFireLevel *= 0.8;
                this.AgentProperties["FireLevel"] = currentFireLevel.ToString();

                Dictionary<string, string> fireProperties
                    = new Dictionary<string, string>();
                fireProperties.Add("Name", "B@F Created Fire");
                fireProperties.Add("FireClass", this.AgentProperties["FireClass"]);
                fireProperties.Add("FireLife", currentFireLife.ToString());
                fireProperties.Add("FireLevel", currentFireLevel.ToString());

                Fire newFire = new Fire(
                    CoreController,
                    fireProperties,
                    this.LatLng,
                    this.MyEnvironment
                    );

            }

            if (currentFireLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentFireLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                //
                // create smoke agents
                //
                Dictionary<string, string> smokeProperties 
                    = new Dictionary<string, string>();

                smokeProperties.Add("Name", "B@F Created Smoke");
                smokeProperties.Add("SmokeType", "Wet Smoke Residues");
                smokeProperties.Add("SmokeLife", currentFireLife.ToString());
                smokeProperties.Add("SmokeLevel", this.AgentProperties["FireLevel"]);


                Smoke newAgent = new Smoke(
                    CoreController,
                    smokeProperties, 
                    this.LatLng, 
                    this.MyEnvironment
                    );

            }
            if (currentBuildingLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentBuildingLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["BuildingLife"] = currentBuildingLife.ToString();
            }
            else
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                Marker.InnerBrush = new SolidBrush(Color.Black);
            }
        }


        /**
         * BuildingJoinedFire can attach other agent if user wants.
         *  @todo multi-level attach
         */
        public override MethodReturnResults Attach(Agent B)
        {
            /*
            //not activated
            if (this.IsActivated == false || B.IsActivated == false)
                return MethodReturnResults.FAILED;
            //already dead
            if (this.IsDead == true || B.IsDead == true)
                return MethodReturnResults.FAILED;



            //closeby level
            if (B.AgentDistance(this) != MethodReturnResults.FAILED)
            {

                Console.WriteLine("!!\n([{0}] attaches [{1}])",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                //create new joinedAgent
                //AgentB.AttachedBy(this);

                //wait to be disposed(free)
                //this.IsActivated = false;
                //this.IsDead = true;

                //succeed method status
                //return MethodReturnResults.SUCCEED;
                return MethodReturnResults.FAILED;
            }
            else
            {
                //nothing happens
                return MethodReturnResults.FAILED;
            }
            */
            return MethodReturnResults.FAIL;
        }

        public override void SetMarkerFormat()
        {
            Marker.IsCircle = false;
            Marker.IsSquare = true;

            Marker.InnerBrush = new SolidBrush(Color.DarkRed);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;


        }

    }
}
