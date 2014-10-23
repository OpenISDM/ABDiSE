/** 
 *  @file Tornado.cs
 *  Tornado is an Agent type in the OpenISDM ABDiSE project.
 *  It is one of the NaturalElementAgentTypes object.
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
 *      2014/10/14 new 2.0 tornado
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
     *  tornado is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the NaturalElementAgentTypes object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Tornado : Agent
    {

        //
        /// tornado path will randomly change according to this variable
        /// 0 = will not randomly change
        /// 100 = plus or minus 100% 
        //
        public const double TORNADO_RANDOM_PERCENTAGE = 10;
        public const double TORNADO_MOVEMENT_UNIT = 
            Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * 9.2;

        /**
         *  No parameter consturctor for testing.
         */
        public Tornado()
            : base()
        {
            Console.WriteLine("Tornado() called");
        }

        /**
         *  Constructor of Tornado. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Tornado(
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
            this.AgentType = "Tornado";

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

            ConfigStrings.ClassShortName = "Tornado";
            ConfigStrings.ClassFullName = typeof(Tornado).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("TornadoType");
            ConfigStrings.Keys.Add("Intensity");


            // assign types
            SubTypeStrings MultipleVortex = new SubTypeStrings();
            MultipleVortex.AgentSubType = "MultipleVortex";
            MultipleVortex.Values.Add("MultipleVortex");
            MultipleVortex.Values.Add("MultipleVortex");
            MultipleVortex.Values.Add("2");
            ConfigStrings.SubTypes.Add(MultipleVortex);

            SubTypeStrings Waterspout = new SubTypeStrings();
            Waterspout.AgentSubType = "Waterspout";
            Waterspout.Values.Add("Waterspout");
            Waterspout.Values.Add("Waterspout");
            Waterspout.Values.Add("2");
            ConfigStrings.SubTypes.Add(Waterspout);

            SubTypeStrings Landspout = new SubTypeStrings();
            Landspout.AgentSubType = "Landspout";
            Landspout.Values.Add("Landspout");
            Landspout.Values.Add("Landspout");
            Landspout.Values.Add("2");
            ConfigStrings.SubTypes.Add(Landspout);

            SubTypeStrings DustDevil = new SubTypeStrings();
            DustDevil.AgentSubType = "DustDevil";
            DustDevil.Values.Add("DustDevil");
            DustDevil.Values.Add("DustDevil");
            DustDevil.Values.Add("2");
            ConfigStrings.SubTypes.Add(DustDevil);

            SubTypeStrings defaultTornado = new SubTypeStrings();
            defaultTornado.AgentSubType = "defaultTornado";
            defaultTornado.Values.Add("defaultTornado");
            defaultTornado.Values.Add("defaultTornado");
            defaultTornado.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultTornado);

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

            this.SimulateTornado();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /** 
         *  Simulate tornado behaviors
         *  
         *  User can edit this method.
         *  For example, add formula for it.
         */
        public void SimulateTornado()
        {
            // data missing
            if (!AgentProperties.ContainsKey("TornadoType") || !AgentProperties.ContainsKey("Intensity"))
            {
                this.IsActivated = false;
                this.IsDead = true;
            }

            string TornadoType = this.AgentProperties["TornadoType"];
            double Intensity = double.Parse(this.AgentProperties["Intensity"]);

            //it doesn't need simulate
            if (Intensity <= 0)
            {
                this.IsActivated = false;
                this.IsDead = true;
                return;
            }
            else if (Intensity >= 5)
            {

                //
                // super tornado: create new tornado (level 2)
                //
                Dictionary<string, string> newAgentProperties
                    = new Dictionary<string, string>();

                newAgentProperties.Add("Name", this.AgentProperties["Name"] + "created ");
                newAgentProperties.Add("TornadoType", this.AgentProperties["TornadoType"]);
                newAgentProperties.Add("Intensity", "2");


                Tornado newAgent = new Tornado(
                    CoreController,
                    newAgentProperties,
                    this.LatLng,
                    this.MyEnvironment
                    );

            }


            // simulate movement
            MoveByWind();

            string weather = this.MyEnvironment.EnvProperties["Weather"];
            double rainfall = double.Parse(this.MyEnvironment.EnvProperties["RainFall"]);

            switch(weather)
            {
                case "Sunny":
                    Intensity -= 0.01;
                    //level -= 1;
                    break;
                case "Cloudy":

                    break;
                case "Rainy":
                    Intensity -= 0.02; 
                    break;

            }

            //write back
            this.AgentProperties["Intensity"] = Intensity.ToString();
            
        }


        /**
         *  Tornado can attach Building and Tree. TODO: attach fire
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
                        // destroy the building
                        //
                        B.Marker.OuterPen.Color = Color.BlanchedAlmond;
                        B.Marker.InnerBrush = new SolidBrush(Color.Black);

                        //weak
                        double intensity = double.Parse(this.AgentProperties["Intensity"]);
                        intensity -= 0.5;
                        this.AgentProperties["Intensity"] = intensity.ToString();
                        
                        B.IsActivated = false;
                        //B.IsDead = true;

                        return MethodReturnResults.SUCCEED;

                    case "Tree":
                        //
                        // destroy the tree
                        //
                        B.Marker.OuterPen.Color = Color.BlanchedAlmond;
                        B.Marker.InnerBrush = new SolidBrush(Color.Black);

                        //weak
                        double intensity2 = double.Parse(this.AgentProperties["Intensity"]);
                        intensity2 -= 0.3;
                        this.AgentProperties["Intensity"] = intensity2.ToString();

                        B.IsActivated = false;
                        //B.IsDead = true;

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

            Marker.CustomImages = new Image[1];
            for (int ii = 0; ii < 1; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/TornadoImages/0" + ii + ".png");
            }

        }

    }
}
