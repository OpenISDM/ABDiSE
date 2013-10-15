/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    WaterAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for Water agent(lake, river)
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABDiSE.AgentDatabase.Flood;

namespace ABDiSE.AgentDatabase.Water
{
    public class WaterAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        //god pointer
        ABDiSE.God God;

        /* 
         * public WaterAgentDatabase(God God)
         * 
         * Description:
         *      Constructor of WaterAgentDatabase.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public WaterAgentDatabase(God God)
        {
            this.God = God;
        }

        /* 
         * public Dictionary<string, string> ReturnProperties(
            BuildingAgentDataTypes type)
         * 
         * Description:
         *      returns properties of default data type of building
         *      
         * Arguments:     
         *      type - default data properties of building
         * Return Value:
         *      Dictionary<string, string> - properties
         */
        public Dictionary<string, string> ReturnProperties(
            WaterAgentDataTypes type)
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case WaterAgentDataTypes.Lake:
                    name = "Lake";
                    AgentProperties.Add("LakeType", "Default Lake");
                    AgentProperties.Add("Level", "2");
                    AgentProperties.Add("Depth(cm)", "100");
                    AgentProperties.Add("MaxDepth(cm)", "150");
                    break;
                case WaterAgentDataTypes.River:
                    name = "River";
                    AgentProperties.Add("RiverType", "Default River");
                    AgentProperties.Add("Level", "2");
                    AgentProperties.Add("Depth(cm)", "300");
                    AgentProperties.Add("MaxDepth(cm)", "400");
                    break;

            }
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }
        /* 
         * public void SimulateWaterDepth(Agent waterAgent)
         * 
         * Description:
         *      decrease/compute depth of target agent
         *      
         * Arguments:     
         *      waterAgent - target water agent
         * Return Value:
         *      void
         */
        public void SimulateWaterDepth(Agent waterAgent)
        {
            // property null - error
            if (!waterAgent.Properties.ContainsKey("Depth(cm)"))
            {
                waterAgent.IsDead = true;
                waterAgent.IsActivated = false;
                return;
            }

            
            double currentDepth = 
                double.Parse(waterAgent.Properties["Depth(cm)"]);

            if(waterAgent.MyEnvironment.Properties["Weather"].Equals("Rainy"))
            {
                // simple depth increase: +=
                currentDepth += waterAgent.MyEnvironment.RainFall * 0.1;

                //update environment property
                waterAgent.Properties.Remove("Depth(cm)");
                waterAgent.Properties.Add(
                    "Depth(cm)", currentDepth.ToString());
            }
        }
        /* 
         * public void SimulateWaterFlood(Agent waterAgent)
         * 
         * Description:
         *      create flood agent if conditions are ture
         *      
         * Arguments:     
         *      waterAgent - target water agent
         * Return Value:
         *      void
         */
        public void SimulateWaterFlood(Agent waterAgent)
        {
            // firelife null - error
            if (!waterAgent.Properties.ContainsKey("Depth(cm)") 
                || !waterAgent.Properties.ContainsKey("MaxDepth(cm)"))
            {
                Console.WriteLine("DEBUG: water agent key error");
                waterAgent.IsDead = true;
                waterAgent.IsActivated = false;
                return;
            }

       
            double currentDepth = 
                double.Parse(waterAgent.Properties["Depth(cm)"]);
            double maxDepth = 
                double.Parse(waterAgent.Properties["MaxDepth(cm)"]);

            if (currentDepth >= maxDepth)
            {
                //create new flood agents
                // TODO: details properties computation
                Dictionary<string, string> newFloodProperties
                    = new FloodAgentDatabase(God).
                    ReturnProperties(FloodAgentDataTypes.RiverFloods);

                God.create(NaturalElementAgentTypes.FLOOD,
                    newFloodProperties,
                    waterAgent.LatLng,  //TODO: edit coordinate
                    waterAgent.MyEnvironment
                    );

                //TODO: decrease this agent's depth
                // simple decrease
                currentDepth *= 0.95;

                //update environment property
                waterAgent.Properties.Remove("Depth(cm)");
                waterAgent.Properties.Add(
                    "Depth(cm)", currentDepth.ToString());
            }
            else 
            { 
                //do nothing
            }

        }//end of SimulateWaterFlood
    }
}
