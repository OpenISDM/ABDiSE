/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    BuildingAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for building agent
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.AgentDatabase.Building
{
    /*  
    * public class BuildingAgentDatabase
    * 
    * Description:
    *      this class is the database of building data properties
    *      
    */
    public class BuildingAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        //god pointer
        God God;

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
        public BuildingAgentDatabase(God God)
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
            BuildingAgentDataTypes type)        
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case BuildingAgentDataTypes.TypeSingleStoryHouse:
                    name = "Single-Story House";
                    AgentProperties.Add("BuildingType", "Single-Story House");
                    AgentProperties.Add("Floor", "1");
                    AgentProperties.Add("BuiltYear", "5");
                    break;
                case BuildingAgentDataTypes.TypeVilla:
                    name = "Villa";
                    AgentProperties.Add("BuildingType", "Villa");
                    AgentProperties.Add("Floor", "3");
                    AgentProperties.Add("BuiltYear", "5");
                    break;
                case BuildingAgentDataTypes.TypeEdifice:
                    name = "Edifice";
                    AgentProperties.Add("BuildingType", "Edifice");
                    AgentProperties.Add("Floor", "30");
                    AgentProperties.Add("BuiltYear", "5");
                    break;
                default:
                    name = "Building(default)";
                    AgentProperties.Add("BuildingType", "default");
                    AgentProperties.Add("Floor", "");
                    AgentProperties.Add("BuiltYear", "");
                    break;
            }
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }
    }
}
