/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    FireAgentDatabase.cs

    SVN $Revision: $

    Abstract:     This class provide default database for fire agent
                  and do simulations of joined agent
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABDiSE.AgentDatabase.Cinder;

namespace ABDiSE.AgentDatabase.Fire
{
    /*  
    * public class FireDefinitions
    * 
    * Description:
    *      this class includes definitions of fire agent
    *      
    */
    public class FireDefinitions
    {
        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;
    }

    /*  
    * public class FireAgentDatabase
    * 
    * Description:
    *      this class is the database of fire data properties
    *      
    */
    public class FireAgentDatabase
    {
        public Dictionary<string, string> AgentProperties =
            new Dictionary<string, string>();
        
        //god pointer
        God God;

        /* 
         * public FireAgentDatabase(God God)
         * 
         * Description:
         *      Constructor of FireAgentDatabase.
         *      
         * Arguments:     
         *      god - the pointer to ABDiSE core, god, to call some method
         *      of it (for example, create, access agent list, etc)
         */
        public FireAgentDatabase(God God)
        {
            this.God = God;
        }


        /* 
         * public Dictionary<string, string> ReturnProperties(
            FireAgentDataTypes type)
         * 
         * Description:
         *      returns properties of default data type of fire
         *      
         * Arguments:     
         *      type - default data properties of fire
         * Return Value:
         *      Dictionary<string, string> - properties
         */
        public Dictionary<string, string> ReturnProperties(
            FireAgentDataTypes type
            )
        {
            //initialize
            AgentProperties.Clear();
            string name = "";

            switch (type)
            {
                case FireAgentDataTypes.ClassA:
                    name = "Fire(ClassA)";
                    AgentProperties.Add("FireClass", "A");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case FireAgentDataTypes.ClassB:
                    name = "Fire(ClassB)";
                    AgentProperties.Add("FireClass", "B");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case FireAgentDataTypes.ClassC:
                    name = "Fire(ClassC)";
                    AgentProperties.Add("FireClass", "C");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case FireAgentDataTypes.ClassD:
                    name = "Fire(ClassD)";
                    AgentProperties.Add("FireClass", "D");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case FireAgentDataTypes.ClassE:
                    name = "Fire(ClassE)";
                    AgentProperties.Add("FireClass", "E");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case FireAgentDataTypes.ClassF:
                    name = "Fire(ClassF)";
                    AgentProperties.Add("FireClass", "F");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                default:
                    name = "Fire(default)";
                    AgentProperties.Add("FireClass", "?");
                    AgentProperties.Add("FireLevel", "0");
                    break;
            }
            AgentProperties.Add("FireLife", "100");
            AgentProperties.Add("Name", string.Format
                ("{0} #0{1}", name, God.AgentNumber + 1));

            return AgentProperties;
        }

        /* 
         * public Dictionary<string, string> SimulateBuildingJoinedFire(
            Dictionary<string, string> FireProperties,
            Dictionary<string, string> BuildingProperties)
         * 
         * Description:
         *      returns properties of Building X Fire
         *      
         * Arguments:     
         *      FireProperties, BuildingProperties
         *      - properties of two agent
         * Return Value:
         *      Dictionary<string, string> - new properties
         */
        public Dictionary<string, string> SimulateBuildingJoinedFire(
            Dictionary<string, string> FireProperties,
            Dictionary<string, string> BuildingProperties)
        {
            //God.MainWindow.MyPrintf("--SimulateBuildingJoinedFire");
            Console.WriteLine("--SimulateBuildingJoinedFire");
            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();

            NewProperties.Clear();

            // computation
            switch (FireProperties["FireClass"].ToString())
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
            FireProperties.Remove("Name");
            BuildingProperties.Remove("Name");

            NewProperties =
                FireProperties.Concat(BuildingProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }

        /* 
         * public Dictionary<string, string> SimulateTreeJoinedFire(
            Dictionary<string, string> FireProperties,
            Dictionary<string, string> TreeProperties)
         * 
         * Description:
         *      returns properties of Tree X Fire
         *      
         * Arguments:     
         *      FireProperties, TreeProperties
         *      - properties of two agent
         * Return Value:
         *      Dictionary<string, string> - new properties
         */
        public Dictionary<string, string> SimulateTreeJoinedFire(
            Dictionary<string, string> FireProperties,
            Dictionary<string, string> TreeProperties)
        {
            //God.MainWindow.MyPrintf("--SimulateTreeJoinedFire");
            Console.WriteLine("--SimulateTreeJoinedFire");

            Dictionary<string, string> NewProperties
                = new Dictionary<string, string>();
            NewProperties.Clear();

            // computation
            switch (FireProperties["FireClass"].ToString())
            {
                case "A":
                    switch (TreeProperties["Height"].ToString())
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
            FireProperties.Remove("Name");
            TreeProperties.Remove("Name");

            NewProperties =
                FireProperties.Concat(TreeProperties).
                ToDictionary(k => k.Key, v => v.Value);

            return NewProperties;
        }

        /* 
         * public void SimulateFireLife(Agent fireAgent)
         * 
         * Description:
         *      decrease/compute life of fire agent
         *      
         * Arguments:     
         *      fireAgent - target fire agent
         * Return Value:
         *      void
         */
        public void SimulateFireLife(Agent fireAgent)
        {
            // firelife null - error
            if (!fireAgent.Properties.ContainsKey("FireLife"))
            {
                fireAgent.IsDead = true;
                fireAgent.IsActivated = false;
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentValue = int.Parse(fireAgent.Properties["FireLife"]);

            if (currentValue > 100)
            {
                currentValue = (int)(0.9 * currentValue);
                fireAgent.Properties["FireLife"] = currentValue.ToString();
            }
            else if (currentValue > FireDefinitions.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= FireDefinitions.FIRE_LEVEL_DECREASE_UNIT;
                fireAgent.Properties["FireLife"] = currentValue.ToString();
            }
            else
            {
                fireAgent.IsDead = true;
                fireAgent.IsActivated = false;
            }
        }

        /* 
         * public void SimulateBuildingFireLife(JoinedAgent building)
         * 
         * Description:
         *      decrease/compute life of building joined fire joinedagent
         *      
         * Arguments:     
         *      building - target building joined agent
         * Return Value:
         *      void
         */
        public void SimulateBuildingFireLife(JoinedAgent building)
        {
            // firelife null - error
            if (!building.Properties.ContainsKey("FireLife"))
            {
                building.IsDead = true;
                building.IsActivated = false;
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentValue = int.Parse(building.Properties["FireLife"]);

            if (currentValue > 100)
            {
                currentValue = (int)(0.9 * currentValue);
                building.Properties["FireLife"] = currentValue.ToString();
            }
            else if (currentValue > FireDefinitions.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= FireDefinitions.FIRE_LEVEL_DECREASE_UNIT;
                building.Properties["FireLife"] = currentValue.ToString();
            }
            else
            {
                building.IsDead = true;
                building.IsActivated = false;

                //create cinder
                Dictionary<string, string> newProperties
                    = new CinderAgentDatabase(God).ReturnProperties
                    (CinderAgentDataTypes.TypeCinder);
                
                //(temp) use for tree and building
                //change marker type
                switch(building.AgentTypeB)
                {
                    case AttachableObjectAgentTypes.BUILDING:
                        newProperties.Add("MarkerType", "Building");

                    break;
                    case AttachableObjectAgentTypes.TREE:
                        newProperties.Add("MarkerType", "Tree");
                    break;
                }
                God.create(
                        NaturalElementAgentTypes.CINDER,
                        newProperties,
                        building.LatLng,
                        God.WorldEnvironmentList[0]
                        );
            }

        }

    }
}
