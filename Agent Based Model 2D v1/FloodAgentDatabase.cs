/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    FloodAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for flood agent
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ABDiSE.AgentDatabase.Flood
{
    public class FloodAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        //god pointer
        ABDiSE.God God;

        /* 
         * public BuildingAgentDatabase(God God)
         * 
         * Description:
         *      Constructor of BuildingAgentDatabase.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public FloodAgentDatabase(God God)
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
            FloodAgentDataTypes type)        
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case FloodAgentDataTypes.FlashFloods:
                    name = "FlashFlood";
                    AgentProperties.Add("FloodType", "FlashFloods");
                    AgentProperties.Add("Level", "1");
                    AgentProperties.Add("Depth(cm)", "20");
                    break;
                case FloodAgentDataTypes.CoastalFloods:
                    name = "CoastalFloods";
                    AgentProperties.Add("FloodType", "CoastalFloods");
                    AgentProperties.Add("Level", "2");
                    AgentProperties.Add("Depth(cm)", "30");
                    break;
                case FloodAgentDataTypes.Ponding:
                    name = "Ponding";
                    AgentProperties.Add("FloodType", "Ponding");
                    AgentProperties.Add("Level", "3");
                    AgentProperties.Add("Depth(cm)", "40");
                    break;
                case FloodAgentDataTypes.RiverFloods:
                    name = "RiverFloods";
                    AgentProperties.Add("FloodType", "RiverFloods");
                    AgentProperties.Add("Level", "4");
                    AgentProperties.Add("Depth(cm)", "22");
                    break;
                case FloodAgentDataTypes.UrbanFloods:
                    name = "UrbanFloods";
                    AgentProperties.Add("FloodType", "UrbanFloods");
                    AgentProperties.Add("Level", "1");
                    AgentProperties.Add("Depth(cm)", "33");
                    break;
            }
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }
    }
}
