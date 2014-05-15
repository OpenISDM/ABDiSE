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
    public class BuildingJoinedFire : Agent
    {
        // definitions
        

        public BuildingJoinedFire()
            : base()
        {
            Console.WriteLine("BuildingJoinedFire() called");

        }

        public BuildingJoinedFire(
            CoreController CoreController,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Model.Environment AgentEnvironment
            )
            : base(
             CoreController,
             Properties,
             LatLng,
             AgentEnvironment
             )
        {
            this.AgentType = "BuildingJoinedFire";

            this.IsNaturalElementAgent = false;
            this.IsAttachableObjectAgent = false;
            this.IsJoinedAgent = true;

            //set properties 
            if (Properties.ContainsKey("Name"))
            {
                this.AgentProperties = Properties;

                //add id to agent's name
                this.AgentProperties["Name"] = Properties["Name"] + "#" + CoreController.God.AgentNumber;
            }
            else
            {


            }


        }

        public override ConfigStrings SetDefaultConfigStrings()
        {
            this.ConfigStrings = new ConfigStrings();

            ConfigStrings.ClassShortName = "BuildingJoinedFire";
            ConfigStrings.ClassFullName = typeof(BuildingJoinedFire).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");
            ConfigStrings.Keys.Add("BuildingType");
            ConfigStrings.Keys.Add("Floor");
            ConfigStrings.Keys.Add("BuiltYear");
            ConfigStrings.Keys.Add("BuildingLife");

            // assign types

            SubTypeStrings defaultBuildingJoinedFire = new SubTypeStrings();
            defaultBuildingJoinedFire.AgentSubType = "default";
            defaultBuildingJoinedFire.Values.Add("Building@Fire-default");
            defaultBuildingJoinedFire.Values.Add("default");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("default");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            defaultBuildingJoinedFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultBuildingJoinedFire);

            //ConfigStrings.Print();

            return ConfigStrings;
        }

        public override void Update()
        {
            //
            // terminate, if this agent has been updated
            //
            if (this.CurrentStep >= CoreController.God.CurrentStep)
                return;


            SimulateBuildingJoinedFireLife();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /* 
         * public void SimulateBuildingJoinedFireLife()
         * 
         * Description:
         *      decrease/compute life of building joined with fire agent
         *      
         * Arguments:     
         * Return Value:
         *      void
         */
        public void SimulateBuildingJoinedFireLife()
        {
            // firelife null - error
            if (!this.AgentProperties.ContainsKey("FireLife"))
            {
                this.IsDead = true;
                this.IsActivated = false;
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentFireLife = int.Parse(this.AgentProperties["FireLife"]);
            int currentBuildingLife = int.Parse(this.AgentProperties["BuildingLife"]);

            if (currentFireLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentFireLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                //
                // create smoke agents
                //
                
                Dictionary<string, string> smokeProperties 
                    = new Dictionary<string, string>();

                smokeProperties.Add("Name", "B@F Created Smoke");
                smokeProperties.Add("SmokeType", "Wet Smoke Residues");
                smokeProperties.Add("SmokeLife", currentFireLife.ToString());
                smokeProperties.Add("SmokeLevel", this.AgentProperties["FireLevel"]);


                Smoke newAgent = new Smoke(
                    CoreController,
                    smokeProperties, 
                    this.LatLng, 
                    this.MyEnvironment
                    );

            }
            if (currentBuildingLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentBuildingLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["BuildingLife"] = currentBuildingLife.ToString();
            }
            else
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                Marker.InnerBrush = new SolidBrush(Color.Black);
            }
        }

        //TODO: multi level attach
        public override MethodReturnResults Attach(Agent B)
        {
            /*
            //not activated
            if (this.IsActivated == false || B.IsActivated == false)
                return MethodReturnResults.FAILED;
            //already dead
            if (this.IsDead == true || B.IsDead == true)
                return MethodReturnResults.FAILED;



            //closeby level
            if (B.AgentDistance(this) != MethodReturnResults.FAILED)
            {

                Console.WriteLine("!!\n([{0}] attaches [{1}])",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                //create new joinedAgent
                //AgentB.AttachedBy(this);

                //wait to be disposed(free)
                //this.IsActivated = false;
                //this.IsDead = true;

                //succeed method status
                //return MethodReturnResults.SUCCEED;
                return MethodReturnResults.FAILED;
            }
            else
            {
                //nothing happens
                return MethodReturnResults.FAILED;
            }
            */
            return MethodReturnResults.FAILED;
        }

        public override void SetMarkerFormat()
        {
            Marker.IsCircle = false;
            Marker.IsSquare = true;

            Marker.InnerBrush = new SolidBrush(Color.Gray);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;


        }

    }
}
