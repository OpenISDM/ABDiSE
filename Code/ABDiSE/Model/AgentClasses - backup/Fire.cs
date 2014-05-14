using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABDiSE.Controller;
using GMap.NET;
namespace ABDiSE.Model.AgentClasses
{
    public class Fire : Agent
    {
        // definitions
        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;

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
            
            this.IsNaturalElementAgent = true;
            this.IsAttachableObjectAgent = false;

            //set value 
            this.AgentProperties.Add("", "");

        }


        /* 
         * public Dictionary<string, string> 
            ReturnDefaultProperties(string ConfigString) 
         * 
         * Description:
         *      returns properties of default data type of fire
         *      
         * Arguments:     
         *      ConfigString - custom fire type in string format
         * Return Value:
         *      Dictionary<string, string> - fire agent properties
         */
        public Dictionary<string, string> 
            ReturnDefaultProperties(string ConfigString) 
        {
            //initialize
            AgentProperties.Clear();

            string name = string.Format("{0} #0{1}", 
                ConfigString, CoreController.God.AgentNumber + 1);

            AgentProperties.Add("Name", name);
            AgentProperties.Add("FireLife", "100");
            
            switch (ConfigString)
            {
                case "ClassA":
                    AgentProperties.Add("FireClass", "A");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case "ClassB":
                    AgentProperties.Add("FireClass", "B");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case "ClassC":
                    AgentProperties.Add("FireClass", "C");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case "ClassD":
                    AgentProperties.Add("FireClass", "D");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case "ClassE":
                    AgentProperties.Add("FireClass", "E");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                case "ClassF":
                    AgentProperties.Add("FireClass", "F");
                    AgentProperties.Add("FireLevel", "10");
                    break;
                default:
                    AgentProperties.Add("FireClass", "?");
                    AgentProperties.Add("FireLevel", "0");
                    break;
            }
            

            return AgentProperties;
        }
         

        public override MethodReturnResults Attach(Agent B)
        {
            return MethodReturnResults.FAILED;
        }

        public override void SetMarkerFormat() 
        { 
            
        }

    }
}
