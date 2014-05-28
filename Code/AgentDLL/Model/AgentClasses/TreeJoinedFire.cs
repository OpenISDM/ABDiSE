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
    public class TreeJoinedFire : Agent
    {
        // definitions
        public const int CREATE_NEW_FIRE_THRESHOLD = 50;

        public TreeJoinedFire()
            : base()
        {
            Console.WriteLine("TreeJoinedFire() called");

        }

        public TreeJoinedFire(
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
            this.AgentType = "TreeJoinedFire";

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

            ConfigStrings.ClassShortName = "TreeJoinedFire";
            ConfigStrings.ClassFullName = typeof(TreeJoinedFire).ToString();

            //
            // assign keys in dictionary properties
            // orders are important!
            //
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");
            ConfigStrings.Keys.Add("TreeType");
            ConfigStrings.Keys.Add("Height");
            ConfigStrings.Keys.Add("Age");
            ConfigStrings.Keys.Add("TreeLife");

            //
            // assign types
            //
            SubTypeStrings defaultTreeJoinedFire = new SubTypeStrings();
            defaultTreeJoinedFire.AgentSubType = "default";
            defaultTreeJoinedFire.Values.Add("Tree@Fire-default");
            defaultTreeJoinedFire.Values.Add("default");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("default");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            defaultTreeJoinedFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultTreeJoinedFire);

            //ConfigStrings.Print();

            return ConfigStrings;
        }

        public override void Update()
        {
            Console.WriteLine("debug!!!!");
            //
            // terminate, if this agent has been updated
            //
            if (this.CurrentStep >= CoreController.God.CurrentStep)
                return;


            SimulateTreeJoinedFireLife();

            //
            // update step according to God
            //
            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /* 
         * public void SimulateTreeJoinedFireLife()
         * 
         * Description:
         *      decrease/compute life of building joined with tree agent
         *      
         * Arguments:     
         * Return Value:
         *      void
         */
        public void SimulateTreeJoinedFireLife()
        {
            // firelife null - error
            if (!this.AgentProperties.ContainsKey("FireLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                Console.WriteLine("{0} firelife null - error", this.AgentProperties["Name"]);
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentFireLife = int.Parse(this.AgentProperties["FireLife"]);
            int currentTreeLife = int.Parse(this.AgentProperties["TreeLife"]);


            if (currentFireLife > CREATE_NEW_FIRE_THRESHOLD)
            {
                //
                // create new fire agent and
                // share "Fire Life" for it 
                //
                currentFireLife /= 2;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                int currentFireLevel = int.Parse(this.AgentProperties["FireLevel"]);

                currentFireLevel /= 2;
                this.AgentProperties["FireLevel"] = currentFireLevel.ToString();

                Dictionary<string, string> fireProperties
                    = new Dictionary<string, string>();
                fireProperties.Add("Name", "T@F Created Fire");
                fireProperties.Add("FireClass", this.AgentProperties["FireClass"]);
                fireProperties.Add("FireLife", currentFireLife.ToString());
                fireProperties.Add("FireLevel", currentFireLevel.ToString());

                Fire newFire = new Fire(
                    CoreController,
                    fireProperties,
                    this.LatLng,
                    this.MyEnvironment
                    );

            }

            if (currentFireLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentFireLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["FireLife"] = currentFireLife.ToString();

                //
                // create smoke agents
                //
                Dictionary<string, string> smokeProperties
                    = new Dictionary<string, string>();

                smokeProperties.Add("Name", "T@F Created Smoke");
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
            if (currentTreeLife > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentTreeLife -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["TreeLife"] = currentTreeLife.ToString();
            }
            else
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                Marker.InnerBrush = new SolidBrush(Color.Black);
            }
        }

        //
        // TODO: multi level attach
        //
        public override MethodReturnResults Attach(Agent B)
        {

            return MethodReturnResults.FAILED;
        }

        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.DarkRed);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;


        }

    }
}
