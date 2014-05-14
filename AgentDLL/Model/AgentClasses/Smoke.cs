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
    public class Smoke : Agent
    {
        // definitions
        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;

        public Smoke()
            : base()
        {
            Console.WriteLine("Smoke() called");

        }

        public Smoke(
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
            this.AgentType = "Smoke";

            this.IsNaturalElementAgent = true;
            this.IsAttachableObjectAgent = false;

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

            ConfigStrings.ClassShortName = "Smoke";
            ConfigStrings.ClassFullName = typeof(Smoke).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("SmokeType");
            ConfigStrings.Keys.Add("SmokeLife");
            ConfigStrings.Keys.Add("SmokeLevel");
            
            


            // assign types
            SubTypeStrings typeWetSmokeResidues = new SubTypeStrings();
            typeWetSmokeResidues.AgentSubType = "TypeDrySmokeResidues";
            typeWetSmokeResidues.Values.Add("Smoke(WetSmokeResidues)");
            typeWetSmokeResidues.Values.Add("Wet Smoke Residues");
            typeWetSmokeResidues.Values.Add("100");
            typeWetSmokeResidues.Values.Add("1");
            ConfigStrings.SubTypes.Add(typeWetSmokeResidues);

            SubTypeStrings typeDrySmokeResidues = new SubTypeStrings();
            typeDrySmokeResidues.AgentSubType = "TypeDrySmokeResidues";
            typeDrySmokeResidues.Values.Add("Smoke(DrySmokeResidues)");
            typeDrySmokeResidues.Values.Add("Dry Smoke Residues");
            typeDrySmokeResidues.Values.Add("100");
            typeDrySmokeResidues.Values.Add("2");
            ConfigStrings.SubTypes.Add(typeDrySmokeResidues);

            SubTypeStrings typeProteinResidues = new SubTypeStrings();
            typeProteinResidues.AgentSubType = "TypeProteinResidues";
            typeProteinResidues.Values.Add("Smoke(ProteinResidues)");
            typeProteinResidues.Values.Add("Protein Residues");
            typeProteinResidues.Values.Add("100");
            typeProteinResidues.Values.Add("3");
            ConfigStrings.SubTypes.Add(typeProteinResidues);

            SubTypeStrings typeFuelOilSoot = new SubTypeStrings();
            typeFuelOilSoot.AgentSubType = "TypeFuelOilSoot";
            typeFuelOilSoot.Values.Add("Smoke(TypeFuelOilSoot)");
            typeFuelOilSoot.Values.Add("TypeFuelOilSoot");
            typeFuelOilSoot.Values.Add("100");
            typeFuelOilSoot.Values.Add("4");
            ConfigStrings.SubTypes.Add(typeFuelOilSoot);

            SubTypeStrings typeOtherTypesOfResidues = new SubTypeStrings();
            typeOtherTypesOfResidues.AgentSubType = "TypeOtherTypesOfResidues";
            typeOtherTypesOfResidues.Values.Add("Smoke(OtherTypesOfResidues)");
            typeOtherTypesOfResidues.Values.Add("Other Types Of Residues");
            typeOtherTypesOfResidues.Values.Add("100");
            typeOtherTypesOfResidues.Values.Add("5");
            ConfigStrings.SubTypes.Add(typeOtherTypesOfResidues);


            SubTypeStrings defaultSmoke = new SubTypeStrings();
            defaultSmoke.AgentSubType = "default";
            defaultSmoke.Values.Add("Smoke-default");
            defaultSmoke.Values.Add("default");
            defaultSmoke.Values.Add("0");
            defaultSmoke.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultSmoke);

            //ConfigStrings.Print();

            return ConfigStrings;
        }

        public override void Update()
        {
            SimulateSmokeLife();
            MoveByWind();

        }

        /* 
         * public void SimulateSmokeLife()
         * 
         * Description:
         *      decrease/compute life of fire agent
         *      
         * Arguments:     
         * Return Value:
         *      void
         */
        public void SimulateSmokeLife()
        {
            // smokelife null - error
            if (!this.AgentProperties.ContainsKey("SmokeLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                return;
            }

            //simple simulate: firelevel -=10 or -= 10%
            int currentValue = int.Parse(this.AgentProperties["SmokeLife"]);


            if (currentValue > SMOKE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= SMOKE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["SmokeLife"] = currentValue.ToString();
            }
            else
            {
                //disappear
                this.IsDead = true;
                this.IsActivated = false;
            }
        }

        public override MethodReturnResults Attach(Agent B)
        {
            //not activated
            if (this.IsActivated == false || B.IsActivated == false)
                return MethodReturnResults.FAILED;
            //already dead
            if (this.IsDead == true || B.IsDead == true)
                return MethodReturnResults.FAILED;



            //closeby level
            if (B.AgentDistance(this) != MethodReturnResults.FAILED)
            {

                Console.WriteLine("Attach close by ({0} attaches{1})",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                //create new joinedAgent
                //AgentB.AttachedBy(this);

                //wait to be disposed(free)
                this.IsActivated = false;
                this.IsDead = true;

                //succeed method status
                return MethodReturnResults.SUCCEED;
            }
            else
            {
                //nothing happens
                return MethodReturnResults.FAILED;
            }

        }

        public override void SetMarkerFormat()
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.LightGray);
            Marker.OuterPen.Color = Color.DarkGray;
            //OuterPen.Width = 2;

            Marker.ImageX = -5;
            Marker.ImageY = -5;

            this.Marker.RandomSeed = new Random(Guid.NewGuid().GetHashCode());

            Marker.CustomImages = new Image[4];
            for (int ii = 0; ii < 4; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/FireImages/smoke0" + ii + ".png");
            }

        }

    }
}
