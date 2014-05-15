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
    public class Fire : Agent
    {
        // definitions
        public const int FIRE_LEVEL_DECREASE_UNIT = 5;
        public const int SMOKE_LEVEL_DECREASE_UNIT = 5;

        public Fire() : base() 
        {
            Console.WriteLine("fire() called");
        
        }

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
            this.AgentType = "Fire";

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

            ConfigStrings.ClassShortName = "Fire";
            ConfigStrings.ClassFullName = typeof(Fire).ToString();


            // assign keys in dictionary properties
            // orders are important!
            ConfigStrings.Keys.Add("Name");
            ConfigStrings.Keys.Add("FireClass");
            ConfigStrings.Keys.Add("FireLife");
            ConfigStrings.Keys.Add("FireLevel");


            // assign types
            SubTypeStrings classA = new SubTypeStrings();
            classA.AgentSubType = "ClassA";
            classA.Values.Add("Fire-ClassA");
            classA.Values.Add("ClassA");
            classA.Values.Add("100");
            classA.Values.Add("2");
            ConfigStrings.SubTypes.Add(classA);

            SubTypeStrings classB = new SubTypeStrings();
            classB.AgentSubType = "ClassB";
            classB.Values.Add("Fire-ClassB");
            classB.Values.Add("ClassB");
            classB.Values.Add("100");
            classB.Values.Add("3");
            ConfigStrings.SubTypes.Add(classB);

            SubTypeStrings classC = new SubTypeStrings();
            classC.AgentSubType = "ClassC";
            classC.Values.Add("Fire-ClassC");
            classC.Values.Add("ClassC");
            classC.Values.Add("100");
            classC.Values.Add("1");
            ConfigStrings.SubTypes.Add(classC);

            SubTypeStrings classD = new SubTypeStrings();
            classD.AgentSubType = "ClassD";
            classD.Values.Add("Fire-ClassD");
            classD.Values.Add("ClassD");
            classD.Values.Add("100");
            classD.Values.Add("2");
            ConfigStrings.SubTypes.Add(classD);

            SubTypeStrings classE = new SubTypeStrings();
            classE.AgentSubType = "ClassE";
            classE.Values.Add("Fire-ClassE");
            classE.Values.Add("ClassE");
            classE.Values.Add("100");
            classE.Values.Add("4");
            ConfigStrings.SubTypes.Add(classE);

            SubTypeStrings classF = new SubTypeStrings();
            classF.AgentSubType = "ClassF";
            classF.Values.Add("Fire-ClassF");
            classF.Values.Add("ClassF");
            classF.Values.Add("100");
            classF.Values.Add("2");
            ConfigStrings.SubTypes.Add(classF);

            SubTypeStrings defaultFire = new SubTypeStrings();
            defaultFire.AgentSubType = "default";
            defaultFire.Values.Add("Fire-default");
            defaultFire.Values.Add("default");
            defaultFire.Values.Add("0");
            defaultFire.Values.Add("0");
            ConfigStrings.SubTypes.Add(defaultFire);

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


            this.CoreController.God.CheckAgentAttachment(this);

            this.SimulateFireLife();
            this.MoveByWind();

            this.CurrentStep = CoreController.God.CurrentStep;
        }

        /* 
         * public void SimulateFireLife()
         * 
         * Description:
         *      decrease/compute life of fire agent
         *      
         * Arguments:     
         * Return Value:
         *      void
         */
        public void SimulateFireLife()
        {
            //
            // firelife null - error
            //
            if (!this.AgentProperties.ContainsKey("FireLife"))
            {
                //this.IsDead = true;
                //this.IsActivated = false;
                return;
            }

            
            int currentValue = int.Parse(this.AgentProperties["FireLife"]);


            if (currentValue > Fire.FIRE_LEVEL_DECREASE_UNIT)
            {
                currentValue -= Fire.FIRE_LEVEL_DECREASE_UNIT;
                this.AgentProperties["FireLife"] = currentValue.ToString();
            }
            else
            {
                this.IsDead = true;
                this.IsActivated = false;
            }
        }

        

        public override MethodReturnResults Attach(Agent B)
        {
            //not the same agent
            if (B == this)
                return MethodReturnResults.FAILED;

            //not activated
            if (this.IsActivated == false || B.IsActivated == false)
                return MethodReturnResults.FAILED;
            //already dead
            if (this.IsDead == true || B.IsDead == true)
                return MethodReturnResults.FAILED;


            
            //closeby level
            if (B.AgentDistance(this) != MethodReturnResults.FAILED)
            {
                
                Console.WriteLine("!!\n[{0}] attaches [{1}]",
                    this.AgentProperties["Name"].ToString(),
                    B.AgentProperties["Name"].ToString());

                //create new joinedAgent
                //AgentB.AttachedBy(this);

                Console.WriteLine(B.AgentType);

                Dictionary<string, string> properties
                            = new Dictionary<string, string>();

                switch (B.AgentType) 
                { 
                        
                    case "Building":
                        //
                        // create Building Joined with Fire
                        // dictionary orders are important!
                        // 
                        properties.Add("Name", B.AgentProperties["Name"].ToString()+"(@Fire)");
                        properties.Add("FireClass", this.AgentProperties["FireClass"]);
                        properties.Add("FireLife", this.AgentProperties["FireLife"]);
                        properties.Add("FireLevel", this.AgentProperties["FireLevel"]);
                        properties.Add("BuildingType", B.AgentProperties["BuildingType"]);
                        properties.Add("Floor", B.AgentProperties["Floor"]);
                        properties.Add("BuiltYear", B.AgentProperties["BuiltYear"]);
                        properties.Add("BuildingLife", "100");

                        BuildingJoinedFire newBuildingFireAgent = 
                            new BuildingJoinedFire(
                                CoreController, 
                                properties, 
                                B.LatLng, 
                                B.MyEnvironment
                                );

                        //
                        // wait to be disposed(free)
                        //
                        this.IsActivated = false;
                        this.IsDead = true;
                        B.IsActivated = false;
                        B.IsDead = true;

                        
                        return MethodReturnResults.SUCCEED;

                    case "Tree":
                        //
                        //create Tree Joined with Fire
                        //

                        // orders are important!
                        /*
                        properties.Add("Name", B.AgentProperties["Name"].ToString()+"(@Fire)");
                        properties.Add("FireClass", this.AgentProperties["FireClass"]);
                        properties.Add("FireLife", this.AgentProperties["FireLife"]);
                        properties.Add("FireLevel", this.AgentProperties["FireLevel"]);
                        properties.Add("BuildingType", B.AgentProperties["BuildingType"]);
                        properties.Add("Floor", B.AgentProperties["Floor"]);
                        properties.Add("BuiltYear", B.AgentProperties["BuiltYear"]);
                        properties.Add("BuildingLife", "100");

                        BuildingJoinedFire newTreeFireAgent = 
                            new BuildingJoinedFire(
                                CoreController, 
                                properties, 
                                B.LatLng, 
                                B.MyEnvironment
                                );

                        CoreController.God.AddToAgentList(newTreeFireAgent);*/
                        break;

                    default:
                        break;
                        
                }


                
            }

            //
            // nothing happened
            //
            return MethodReturnResults.FAILED;

        }

        

        public override void SetMarkerFormat() 
        {
            Marker.IsCircle = true;
            Marker.IsSquare = false;

            Marker.InnerBrush = new SolidBrush(Color.Yellow);
            Marker.OuterPen.Color = Color.Red;
            //OuterPen.Width = 2;

            Marker.ImageX = -4;
            Marker.ImageY = -5;

            this.Marker.RandomSeed = new Random(Guid.NewGuid().GetHashCode());

            Marker.CustomImages = new Image[6];
            for (int ii = 0; ii < 6; ii++)
            {
                Marker.CustomImages[ii] = Image.FromFile("MarkerImage/FireImages/fire0" + ii + ".png");
            }

        }

    }
}
