/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    CinderAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for Cinder agent
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.AgentDatabase.Cinder
{
    /*  
    * public class CinderAgentDatabase
    * 
    * Description:
    *      this class is the database of tree data properties
    *      
    */
    public class CinderAgentDatabase
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
        public CinderAgentDatabase(God God)
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
            CinderAgentDataTypes type
            )
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case CinderAgentDataTypes.TypeCinder:
                    name = "Cinder";
                    AgentProperties.Add("CinderType", "Cinder");
                    AgentProperties.Add("DamageLevel", "10.0");
                    break;
                

                default:
                    name = "Cinder(default)";
                    AgentProperties.Add("CinderType", "default");
                    AgentProperties.Add("DamageLevel", "");
                    break;
            }
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }
    }
}
