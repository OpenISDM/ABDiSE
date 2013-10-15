/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    TreeAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for tree agent
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.AgentDatabase.Tree
{
    /*  
    * public class TreeAgentDatabase
    * 
    * Description:
    *      this class is the database of tree data properties
    *      
    */
    public class TreeAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        //god pointer
        God God;

        /* 
         * public TreeAgentDatabase(God God)
         * 
         * Description:
         *      Constructor of TreeAgentDatabase.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public TreeAgentDatabase(God God)
        {
            this.God = God;
        }

        /* 
         * public Dictionary<string, string> ReturnProperties(
            TreeAgentDataTypes type)
         * 
         * Description:
         *      returns properties of default data type of tree
         *      
         * Arguments:     
         *      type - default data properties of tree
         * Return Value:
         *      Dictionary<string, string> - properties
         */
        public Dictionary<string, string> ReturnProperties(
            TreeAgentDataTypes type
            )
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case TreeAgentDataTypes.TypePineTree:
                    name = "Pine Tree";
                    AgentProperties.Add("TreeType", "Pine Tree");
                    AgentProperties.Add("Height", "10.0");
                    AgentProperties.Add("Age", "20");
                    break;
                case TreeAgentDataTypes.TypeBanyan:
                    name = "Banyan";
                    AgentProperties.Add("TreeType", "Banyan");
                    AgentProperties.Add("Height", "10.0");
                    AgentProperties.Add("Age", "20");
                    break;
                case TreeAgentDataTypes.TypeMaple:
                    name = "Maple";
                    AgentProperties.Add("TreeType", "Maple");
                    AgentProperties.Add("Height", "10.0");
                    AgentProperties.Add("Age", "20");
                    break;
                case TreeAgentDataTypes.TypeCupressaceae:
                    name = "Cupressaceae";
                    AgentProperties.Add("TreeType", "Cupressaceae");
                    AgentProperties.Add("Height", "10.0");
                    AgentProperties.Add("Age", "20");
                    break;

                case TreeAgentDataTypes.TypeOtherTypesOfTree:
                    name = "Tree(Other Type)";
                    AgentProperties.Add("TreeType", "OtherTypes");
                    AgentProperties.Add("Height", "20.0");
                    AgentProperties.Add("Age", "20");

                    break;

                default:
                    name = "Tree(default)";
                    AgentProperties.Add("TreeType", "default");
                    AgentProperties.Add("Height", "");
                    AgentProperties.Add("Age", "");
                    break;
            }
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }
    }
}
