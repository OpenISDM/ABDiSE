/** 
 *  @file Tree.cs
 *  Tree is an Agent type in the OpenISDM ABDiSE project.
 *  It is one of the basic AttachableObjectAgentTypes object.
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
 *      2014/9/1: edit comments for doxygen
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
     *  Tree is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic AttachableObjectAgentTypes object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Tree : Agent
    {
        /**
         *  No parameter consturctor for testing.
         */ 
        public Tree()
            : base()
        {
            Console.WriteLine("Tree() called");

        }

        /**
         *  Constructor of Tree. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Tree(
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
            this.AgentType = "Tree";

            this.IsNaturalElementAgent = false;
            this.IsAttachableObjectAgent = true;

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

            ConfigStrings.ClassShortName = "Tree";
            ConfigStrings.ClassFullName = typeof(Tree).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("TreeType");
            ConfigStrings.Keys.Add("Height");
            ConfigStrings.Keys.Add("Age");


            // assign types
            SubTypeStrings typePineTree = new SubTypeStrings();
            typePineTree.AgentSubType = "TypePineTree";
            typePineTree.Values.Add("Pine Tree");
            typePineTree.Values.Add("TypePineTree");
            typePineTree.Values.Add("10.0");
            typePineTree.Values.Add("20");
            ConfigStrings.SubTypes.Add(typePineTree);

            SubTypeStrings typeBanyan = new SubTypeStrings();
            typeBanyan.AgentSubType = "TypeBanyan";
            typeBanyan.Values.Add("Banyan");
            typeBanyan.Values.Add("TypeBanyan");
            typeBanyan.Values.Add("12.1");
            typeBanyan.Values.Add("11");
            ConfigStrings.SubTypes.Add(typeBanyan);

            SubTypeStrings typeMaple = new SubTypeStrings();
            typeMaple.AgentSubType = "TypeMaple";
            typeMaple.Values.Add("Maple");
            typeMaple.Values.Add("TypeMaple");
            typeMaple.Values.Add("4");
            typeMaple.Values.Add("5");
            ConfigStrings.SubTypes.Add(typeMaple);

            SubTypeStrings typeCupressaceae = new SubTypeStrings();
            typeCupressaceae.AgentSubType = "TypeCupressaceae";
            typeCupressaceae.Values.Add("Cupressaceae");
            typeCupressaceae.Values.Add("TypeCupressaceae");
            typeCupressaceae.Values.Add("5");
            typeCupressaceae.Values.Add("6");
            ConfigStrings.SubTypes.Add(typeCupressaceae);

            SubTypeStrings defaultTree = new SubTypeStrings();
            defaultTree.AgentSubType = "default";
            defaultTree.Values.Add("Tree-default");
            defaultTree.Values.Add("default");
            defaultTree.Values.Add("0");
            defaultTree.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultTree);

            //ConfigStrings.Print();

            return ConfigStrings;
        }

        /**
         *  Update() will be executed in every simulation steps. (time-driven mode)
         *  User can customize this method to model different agent behaviors.
         */ 
        public override void Update()
        {
            //
            // terminate, if this agent has been updated
            //
            if (this.CurrentStep >= CoreController.God.CurrentStep)
                return;


            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /**
         *  Building can not attach other agents.
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
         */ 
        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.Green);
            Marker.OuterPen.Color = Color.Brown;

        }

    }
}
