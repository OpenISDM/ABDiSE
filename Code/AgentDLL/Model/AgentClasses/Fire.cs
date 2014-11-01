/** 
 *  @file Fire.cs
 *  Fire is an Agent type in the OpenISDM ABDiSE project.
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
 *  Abstract:
 *
 *      Fire is an Agent type in the OpenISDM ABDiSE project.
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
     *  Fire is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic NaturalElementAgentTypes object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Fire : Agent
    {

        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;

        /**
         *  No parameter consturctor for testing.
         */ 
        public Fire() : base() 
        {
            Console.WriteLine("fire() called");
        }

        /**
         *  Constructor of Fire. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Fire(
            CoreController CoreController,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Model.Environment AgentEnvironment
            ) : base(
               CoreController,
               Properties,
               LatLng,
               AgentEnvironment
               ) 
        {
            this.AgentType = "Fire";

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

            ConfigStrings.ClassShortName = "Fire";
            ConfigStrings.ClassFullName = typeof(Fire).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");


            /*
             Class A
                These are fires that involve some solid material like, clothers, paper, junk-heap, wood etc.
             Class B
                These are fires that involve liquid materials like: petrol, gasoline, diesel, oil etc.
             Class C
                These are fires that involve electrical elements
             Class D
                These are fires are those involve metals
             */

            // assign types
            SubTypeStrings classA = new SubTypeStrings();
            classA.AgentSubType = "ClassA";
            classA.Values.Add("Fire-ClassA");
            classA.Values.Add("ClassA");
            classA.Values.Add("100");
            classA.Values.Add("2");
            ConfigStrings.SubTypes.Add(classA);

            SubTypeStrings classB = new SubTypeStrings();
            classB.AgentSubType = "ClassB";
            classB.Values.Add("Fire-ClassB");
            classB.Values.Add("ClassB");
            classB.Values.Add("100");
            classB.Values.Add("3");
            ConfigStrings.SubTypes.Add(classB);

            SubTypeStrings classC = new SubTypeStrings();
            classC.AgentSubType = "ClassC";
            classC.Values.Add("Fire-ClassC");
            classC.Values.Add("ClassC");
            classC.Values.Add("100");
            classC.Values.Add("1");
            ConfigStrings.SubTypes.Add(classC);

            SubTypeStrings classD = new SubTypeStrings();
            classD.AgentSubType = "ClassD";
            classD.Values.Add("Fire-ClassD");
            classD.Values.Add("ClassD");
            classD.Values.Add("100");
            classD.Values.Add("2");
            ConfigStrings.SubTypes.Add(classD);

            SubTypeStrings defaultFire = new SubTypeStrings();
            defaultFire.AgentSubType = "default";
            defaultFire.Values.Add("Fire-default");
            defaultFire.Values.Add("default");
            defaultFire.Values.Add("0");
            defaultFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultFire);

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

            this.SimulateFireLife();
            this.MoveByWind();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /** 
         *  Decrease life of fire agent.
         *  User can edit this method.
         *  For example, add formula for it.
         */
        public void SimulateFireLife()
        {
            //
            // firelife null - error
            //
            if (!this.AgentProperties.ContainsKey("FireLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                return;
            }

            
            int currentValue = int.Parse(this.AgentProperties["FireLife"]);


            if (currentValue > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["FireLife"] = currentValue.ToString();
            }
            else
            {
                this.IsDead = true;
                this.IsActivated = false;
            }
        }

        
        /**
         *  Fire can attach Building and Tree.
         *  Different situation will be handled by different code.
         *  User can freely edit the code.
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
                
                Console.WriteLine("!!\n[{0}] attaches [{1}]",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                //create new joinedAgent
                //AgentB.AttachedBy(this);

                Console.WriteLine(B.AgentType);

                Dictionary<string, string> properties
                            = new Dictionary<string, string>();

                switch (B.AgentType) 
                { 
                        
                    case "Building":
                        //
                        // create Building Joined with Fire
                        // dictionary orders are important!
                        // 
                        properties.Add("Name", B.AgentProperties["Name"]+"(@Fire)");
                        properties.Add("FireClass", this.AgentProperties["FireClass"]);
                        properties.Add("FireLife", this.AgentProperties["FireLife"]);
                        properties.Add("FireLevel", this.AgentProperties["FireLevel"]);
                        properties.Add("BuildingType", B.AgentProperties["BuildingType"]);
                        properties.Add("Floor", B.AgentProperties["Floor"]);
                        properties.Add("BuiltYear", B.AgentProperties["BuiltYear"]);
                        properties.Add("BuildingLife", "100");

                        BuildingJoinedFire newBuildingFireAgent = 
                            new BuildingJoinedFire(
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

                    case "Tree":
                        //
                        // create Building Joined with Fire
                        // dictionary orders are important!
                        //

                        properties.Add("Name", B.AgentProperties["Name"]+"(@Fire)");
                        properties.Add("FireClass", this.AgentProperties["FireClass"]);
                        properties.Add("FireLife", this.AgentProperties["FireLife"]);
                        properties.Add("FireLevel", this.AgentProperties["FireLevel"]);
                        properties.Add("TreeType", B.AgentProperties["TreeType"]);
                        properties.Add("Height", B.AgentProperties["Height"]);
                        properties.Add("Age", B.AgentProperties["Age"]);
                        properties.Add("TreeLife", "100");

                        TreeJoinedFire newTreeFireAgent = 
                            new TreeJoinedFire(
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
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;

            Marker.ImageX = -4;
            Marker.ImageY = -5;

            this.Marker.RandomSeed = new Random(Guid.NewGuid().GetHashCode());

            Marker.CustomImages = new Image[6];
            for (int ii = 0; ii < 6; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/FireImages/fire0" + ii + ".png");
            }

        }

    }
}
