/** 
 *  @file Building.cs
 *  Building is an Agent type in the OpenISDM ABDiSE project.
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
 *
 *  Abstract:
 *
 *      Building is an Agent type in the OpenISDM ABDiSE project.
 *      It is one of the basic attachable object.
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
     *  Building is an Agent type in the OpenISDM ABDiSE project.
     *  It is one of the basic attachable object.
     *  Detail information is in the SetDefaultConfigStrings method.
     */
    public class Building : Agent
    {
        /**
         *  No parameter consturctor for testing.
         */ 
        public Building() : base() 
        {
            Console.WriteLine("Building() called");     
        }

        /**
         *  Constructor of Building. Assign parameters to its data structure.
         *  
         *  @param CoreController - Pointer to CoreController
         *  @param Properties - Dictionary form data structure
         *  @param LatLng - Coordinates of Building Agent
         *  @param AgentEnvironment - Environment of Building
         */
        public Building(
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
            this.AgentType = "Building";

            this.IsNaturalElementAgent = false;
            this.IsAttachableObjectAgent = true;
            
            //set properties 
            if (Properties.ContainsKey("Name"))
            {
                this.AgentProperties = Properties;
                //add id to agent's name
                this.AgentProperties["Name"] = Properties["Name"] +"#"+ CoreController.God.AgentNumber;
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

            ConfigStrings.ClassShortName = "Building";
            ConfigStrings.ClassFullName = typeof(Building).ToString();

            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("BuildingType");
            ConfigStrings.Keys.Add("Floor");
            ConfigStrings.Keys.Add("BuiltYear");

            // assign types
            SubTypeStrings ssh = new SubTypeStrings();
            ssh.AgentSubType = "TypeSingleStoryHouse";
            ssh.Values.Add("Single-Story House");
            ssh.Values.Add("Single-Story House");
            ssh.Values.Add("1");
            ssh.Values.Add("5");
            ConfigStrings.SubTypes.Add(ssh);

            SubTypeStrings villa = new SubTypeStrings();
            villa.AgentSubType = "TypeVilla";
            villa.Values.Add("Villa");
            villa.Values.Add("Villa");
            villa.Values.Add("3");
            villa.Values.Add("11");
            ConfigStrings.SubTypes.Add(villa);

            SubTypeStrings edifice = new SubTypeStrings();
            edifice.AgentSubType = "TypeEdifice";
            edifice.Values.Add("Edifice");
            edifice.Values.Add("Edifice");
            edifice.Values.Add("10");
            edifice.Values.Add("2");
            ConfigStrings.SubTypes.Add(edifice);

            SubTypeStrings defaultBuilding = new SubTypeStrings();
            defaultBuilding.AgentSubType = "default";
            defaultBuilding.Values.Add("Building-default");
            defaultBuilding.Values.Add("default");
            defaultBuilding.Values.Add("0");
            defaultBuilding.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultBuilding);

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
            Marker.IsCircle = false;
            Marker.IsSquare = true;

            Marker.InnerBrush = new SolidBrush(Color.Gray);
            Marker.OuterPen.Color = Color.Black;
            
        }

    }
}
