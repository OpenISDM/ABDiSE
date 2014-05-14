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
    public class Building : Agent
    {
        // definitions

        public Building() : base() 
        {

            Console.WriteLine("Building() called");
        
        }

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

        public override void Update()
        {
            //do nothing
        }

        public override MethodReturnResults Attach(Agent B)
        {
            return MethodReturnResults.FAILED;
        }

        public override void SetMarkerFormat() 
        {
            Marker.IsCircle = false;
            Marker.IsSquare = true;

            Marker.InnerBrush = new SolidBrush(Color.Gray);
            Marker.OuterPen.Color = Color.Black;
            
        }

    }
}
