/** 
 *  @file Smoke.cs
 *  Smoke is an Agent type in the OpenISDM ABDiSE project.
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
 *  Version:
 *
 *      2.0
 *
 *  Abstract:
 *
 *      Smoke is an Agent type in the OpenISDM ABDiSE project.
 *      It is one of the basic NaturalElementAgentTypes object.
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
     *  Smoke is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic NaturalElementAgentTypes object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Smoke : Agent
    {

        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;

        /**
         *  No parameter consturctor for testing.
         */ 
        public Smoke()
            : base()
        {
            Console.WriteLine("Smoke() called");

        }

        /**
         *  Constructor of TreeJoinedFire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Smoke(
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
            this.AgentType = "Smoke";

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
         *  Set defualt configuration of Building agent as a sample.
         *  
         *  @return ConfigStrings data structure
         */
        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "Smoke";
            ConfigStrings.ClassFullName = typeof(Smoke).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("SmokeType");
            ConfigStrings.Keys.Add("SmokeLife");
            ConfigStrings.Keys.Add("SmokeLevel");
            
            


            // assign types
            SubTypeStrings typeWetSmokeResidues = new SubTypeStrings();
            typeWetSmokeResidues.AgentSubType = "TypeDrySmokeResidues";
            typeWetSmokeResidues.Values.Add("Smoke(WetSmokeResidues)");
            typeWetSmokeResidues.Values.Add("Wet Smoke Residues");
            typeWetSmokeResidues.Values.Add("100");
            typeWetSmokeResidues.Values.Add("1");
            ConfigStrings.SubTypes.Add(typeWetSmokeResidues);

            SubTypeStrings typeDrySmokeResidues = new SubTypeStrings();
            typeDrySmokeResidues.AgentSubType = "TypeDrySmokeResidues";
            typeDrySmokeResidues.Values.Add("Smoke(DrySmokeResidues)");
            typeDrySmokeResidues.Values.Add("Dry Smoke Residues");
            typeDrySmokeResidues.Values.Add("100");
            typeDrySmokeResidues.Values.Add("2");
            ConfigStrings.SubTypes.Add(typeDrySmokeResidues);

            SubTypeStrings typeProteinResidues = new SubTypeStrings();
            typeProteinResidues.AgentSubType = "TypeProteinResidues";
            typeProteinResidues.Values.Add("Smoke(ProteinResidues)");
            typeProteinResidues.Values.Add("Protein Residues");
            typeProteinResidues.Values.Add("100");
            typeProteinResidues.Values.Add("3");
            ConfigStrings.SubTypes.Add(typeProteinResidues);

            SubTypeStrings typeFuelOilSoot = new SubTypeStrings();
            typeFuelOilSoot.AgentSubType = "TypeFuelOilSoot";
            typeFuelOilSoot.Values.Add("Smoke(TypeFuelOilSoot)");
            typeFuelOilSoot.Values.Add("TypeFuelOilSoot");
            typeFuelOilSoot.Values.Add("100");
            typeFuelOilSoot.Values.Add("4");
            ConfigStrings.SubTypes.Add(typeFuelOilSoot);

            SubTypeStrings typeOtherTypesOfResidues = new SubTypeStrings();
            typeOtherTypesOfResidues.AgentSubType = "TypeOtherTypesOfResidues";
            typeOtherTypesOfResidues.Values.Add("Smoke(OtherTypesOfResidues)");
            typeOtherTypesOfResidues.Values.Add("Other Types Of Residues");
            typeOtherTypesOfResidues.Values.Add("100");
            typeOtherTypesOfResidues.Values.Add("5");
            ConfigStrings.SubTypes.Add(typeOtherTypesOfResidues);


            SubTypeStrings defaultSmoke = new SubTypeStrings();
            defaultSmoke.AgentSubType = "default";
            defaultSmoke.Values.Add("Smoke-default");
            defaultSmoke.Values.Add("default");
            defaultSmoke.Values.Add("0");
            defaultSmoke.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultSmoke);

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

            SimulateSmokeLife();
            MoveByWind();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /** 
         *  Decrease life of fire agent.
         *  User can edit this method.
         *  For example, add formula for it.
         */
        public void SimulateSmokeLife()
        {
            // smokelife null - error
            if (!this.AgentProperties.ContainsKey("SmokeLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentValue = int.Parse(this.AgentProperties["SmokeLife"]);


            if (currentValue > SMOKE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= SMOKE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["SmokeLife"] = currentValue.ToString();
            }
            else
            {
                //disappear
                this.IsDead = true;
                this.IsActivated = false;
            }
        }

        /**
         *  Smoke can not attach other agents.
         *  Different situation will be handled by different code.
         *  User can freely edit the code.
         *  
         *  @param B - the attach target agent
         *  @return - succeed or fail
         */ 
        public override MethodReturnResults Attach(Agent B)
        {
            // if you want to create new rule, please see Fire.cs for more information

            return MethodReturnResults.FAIL;
        }

        /**
         *  Customizing marker format for better display in GUI.
         *  Loads multiple .png images
         *  User can adjust x and y of image.
         */
        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.LightGray);
            Marker.OuterPen.Color = Color.DarkGray;
            //OuterPen.Width = 2;

            Marker.ImageX = -5;
            Marker.ImageY = -5;

            this.Marker.RandomSeed = new Random(Guid.NewGuid().GetHashCode());

            Marker.CustomImages = new Image[4];
            for (int ii = 0; ii < 4; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/FireImages/smoke0" + ii + ".png");
            }

        }

    }
}
