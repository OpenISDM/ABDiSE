/** 
 *  @file TreeJoinedFire.cs
 *  TreeJoinedFire is an Joined Agent type in the OpenISDM ABDiSE project.
 *  It is one of the basic Joined Agent object.
 *  Detail information is in the SetDefaultConfigStrings method.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
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
     *  TreeJoinedFire is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic Joined Agent object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class TreeJoinedFire : Agent
    {

        public const int CREATE_NEW_FIRE_THRESHOLD = 50;

        /**
         *  No parameter consturctor for testing.
         */ 
        public TreeJoinedFire()
            : base()
        {
            Console.WriteLine("TreeJoinedFire() called");

        }

        /**
         *  Constructor of TreeJoinedFire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public TreeJoinedFire(
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
            this.AgentType = "TreeJoinedFire";

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

            ConfigStrings.ClassShortName = "TreeJoinedFire";
            ConfigStrings.ClassFullName = typeof(TreeJoinedFire).ToString();

            //
            // assign keys in dictionary properties
            // orders are important!
            //
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");
            ConfigStrings.Keys.Add("TreeType");
            ConfigStrings.Keys.Add("Height");
            ConfigStrings.Keys.Add("Age");
            ConfigStrings.Keys.Add("TreeLife");

            //
            // assign types
            //
            SubTypeStrings defaultTreeJoinedFire = new SubTypeStrings();
            defaultTreeJoinedFire.AgentSubType = "default";
            defaultTreeJoinedFire.Values.Add("Tree@Fire-default");
            defaultTreeJoinedFire.Values.Add("default");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("default");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultTreeJoinedFire);

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


            SimulateTreeJoinedFireLife();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /**
         *  Compute TreeJoinedFire agent's Life. 
         *  User can freely edit this method.
         */
        public void SimulateTreeJoinedFireLife()
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
            double currentTreeLife = double.Parse(this.AgentProperties["TreeLife"]);


            if (currentFireLife > CREATE_NEW_FIRE_THRESHOLD)
            {
                //
                // create new fire agent and
                // share "Fire Life" for it 
                //
                currentFireLife *= 0.8;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                double currentFireLevel = int.Parse(this.AgentProperties["FireLevel"]);

                currentFireLevel *=0.8;
                this.AgentProperties["FireLevel"] = currentFireLevel.ToString();

                Dictionary<string, string> fireProperties
                    = new Dictionary<string, string>();
                fireProperties.Add("Name", "T@F Created Fire");
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

                smokeProperties.Add("Name", "T@F Created Smoke");
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
            if (currentTreeLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentTreeLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["TreeLife"] = currentTreeLife.ToString();
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

            return MethodReturnResults.FAIL;
        }

        /**
         *  Assign detailed value for GMap.NET custom markers.
         *  User can freely edit this method.
         */
        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.DarkRed);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;

        }

    }
}
