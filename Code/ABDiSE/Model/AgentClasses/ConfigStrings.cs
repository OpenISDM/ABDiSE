using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABDiSE.Model.AgentClasses
{
    public class ConfigStrings
    {
        public string ClassShortName;
        public string ClassFullName;
        public List<SubTypeStrings> SubTypes;
        public List<string> Keys;

        public ConfigStrings() 
        {
            ClassShortName = "unknown";
            ClassFullName = "ABDiSE.Model.AgentClasses.Unknown";

            SubTypes = new List<SubTypeStrings>();

            Keys = new List<string>();
            
        
        }

        public void Print()
        {
            Console.WriteLine("Class:" + this.ClassFullName);
            for (int ii = 0; ii < SubTypes.Count; ii++ )
            {
                Console.WriteLine(SubTypes[ii].AgentSubType + ":");

                for (int jj = 0; jj < SubTypes[ii].Values.Count; jj++ )
                {
                    Console.WriteLine("\t" + Keys[jj] + ", " + SubTypes[ii].Values[jj] );
                }
            }
        }
    }

    public class SubTypeStrings
    {
        public string AgentSubType = "";
        public List<string> Values;

        public SubTypeStrings() 
        {
            Values = new List<string>();
        }
    }
}
