/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    SmokeAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for smoke agent
                  TODO: support XML file I/O
                  data from http://www.servpro.com/fire_smoke
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.AgentDatabase.Smoke
{
    /*  
    * public class SmokeDefinitions
    * 
    * Description:
    *      this class includes definitions of smoke agent
    *      
    */
    public class SmokeDefinitions
    {
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;
    }

    /*  
    * public class SmokeAgentDatabase
    * 
    * Description:
    *      this class is the database of smoke data properties
    *      
    */
    public class SmokeAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();

        //god pointer
        God God;

        /* 
         * public SmokeAgentDatabase(God God)
         * 
         * Description:
         *      Constructor of SmokeAgentDatabase.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public SmokeAgentDatabase(God God)
        {
            this.God = God;
        }

        /* 
         * public Dictionary<string, string> ReturnProperties(
            SmokeAgentDataTypes type)
         * 
         * Description:
         *      returns properties of default data type of smoke
         *      
         * Arguments:     
         *      type - default data properties of smoke
         * Return Value:
         *      Dictionary<string, string> - properties
         */
        public Dictionary<string, string> ReturnProperties(
            SmokeAgentDataTypes type
            )
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case SmokeAgentDataTypes.TypeWetSmokeResidues:
                    name = "Smoke(WetSmokeResidues)";
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Wet Smoke Residues");

                    break;
                case SmokeAgentDataTypes.TypeDrySmokeResidues:
                    name = "Smoke(DrySmokeResidues)";
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Dry Smoke Residues");

                    break;
                case SmokeAgentDataTypes.TypeProteinResidues:
                    name = "Smoke(ProteinResidues)";
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Protein Residues");

                    break;
                case SmokeAgentDataTypes.TypeFuelOilSoot:
                    name = "Smoke(TypeFuelOilSoot)";
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Fuel Oil Soot");

                    break;
                case SmokeAgentDataTypes.TypeOtherTypesOfResidues:
                    name = "Smoke(OtherTypesOfResidues)";
                    AgentProperties.Add("SmokeLevel", "0");
                    AgentProperties.Add("SmokeType", "Other Types Of Residues");
                    break;
                default:
                    name = "Smoke(default)";
                    AgentProperties.Add("SmokeLevel", "00");
                    AgentProperties.Add("SmokeType", "default");
                    break;
            }
            AgentProperties.Add("SmokeLife", "100");

            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }

        /* 
        * public Dictionary<string, string> SimulateBuildingJoinedSmoke(
           Dictionary<string, string> SmokeProperties,
           Dictionary<string, string> BuildingProperties)
        * 
        * Description:
        *      returns properties of Building X Smoke
        *      
        * Arguments:     
        *      SmokeProperties, BuildingProperties
        *      - properties of two agent
        * Return Value:
        *      Dictionary<string, string> - new properties
        */
        public Dictionary<string, string> SimulateBuildingJoinedSmoke(
            Dictionary<string, string> SmokeProperties,
            Dictionary<string, string> BuildingProperties)
        {

            //God.MainWindow.MyPrintf("--SimulateBuildingJoinedSmoke");
            Console.WriteLine("--SimulateBuildingJoinedSmoke");

            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();

            NewProperties.Clear();

            // computation
            switch (SmokeProperties["SmokeType"].ToString())
            {
                case "A":
                    switch (BuildingProperties["Floor"].ToString())
                    {
                        case "20":
                            // computation
                            //NewProperties.Add("Level", "A20");
                            break;
                        case "":
                            break;
                        default:
                            break;
                    }
                    break;
                case "B":
                    break;
            }

            //for testing
            SmokeProperties.Remove("Name");
            BuildingProperties.Remove("Name");
            NewProperties =
                SmokeProperties.Concat(BuildingProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }

        /* 
         * public void SimulateSmokeLife(Agent smokeAgent)
         * 
         * Description:
         *      decrease/compute life of smoke agent
         *      
         * Arguments:     
         *      smokeAgent - target smoke agent
         * Return Value:
         *      void
         */
        public void SimulateSmokeLife(Agent smokeAgent)
        {
            // firelife null - error
            if (!smokeAgent.Properties.ContainsKey("SmokeLife"))
            {
                smokeAgent.IsDead = true;
                smokeAgent.IsActivated = false;
                return;
            }

            //simple simulate: smoke level -=10 or -= 10%
            int currentValue = int.Parse(smokeAgent.Properties["SmokeLife"]);

            if (currentValue > 100)
            {
                currentValue = (int)(0.9 * currentValue);
                smokeAgent.Properties["SmokeLife"] = currentValue.ToString();
            }
            else if (currentValue > SmokeDefinitions.SMOKE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= SmokeDefinitions.SMOKE_LEVEL_DECREASE_UNIT;
                smokeAgent.Properties["SmokeLife"] = currentValue.ToString();
            }
            else
            {
                smokeAgent.IsDead = true;
                smokeAgent.IsActivated = false;
            }
        }

    }
}
