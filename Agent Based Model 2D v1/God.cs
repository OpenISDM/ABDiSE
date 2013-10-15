/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    God.cs

    SVN $Revision: $

    Abstract:     Each Simulation experiment is set up through this entity. 
                  include Main
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using ABDiSE;
using GMap.NET;
using System.Threading;
using ABDiSE.ThreadPool;
using ABDiSE.GUI;

namespace ABDiSE
{
    /*  
    * public class God
    * 
    * Description:
    *      God class is the core of ABDiSE, there is only 1 God instance.
    *      Maintenances world agent list and environment list      
    *      do creation and can change agent directly.
    *      
    */
    public class God
    {
        //c# windows applicaiton
        MainWindow MainWindow;

        public SimpleThreadPool SimpleTP;

        public int CurrentStep = 0;

        public ABDiSE.Environment[] WorldEnvironmentList;
        // reference of agents, recorded by God
        // this list is used for recording ALL agents in the simulation world
        public Agent[] WorldAgentList;
        public JoinedAgent[] WorldJoinedAgentList;

        // for naming new Agent (Maximum array index)
        public int AgentNumber = 0;
        public int JoinedAgentNumber = 0;

        //real agent count
        public int AgentCount = 0;
        public int JoinedAgentCount = 0;

        public Dictionary<string, string> properties =
            new Dictionary<string, string>();

        public int MaximumEnvironments = 100;
        public int MaximumAgents = 1000;

        /* 
         * private God()
         * 
         * Description:
         *      constructor of God class
         *      
         * Arguments:     
         *      void
         */
        private God()
        {
            // create the only GUI, and assign pointers of itself to it.
            MainWindow = new MainWindow(this);

            this.WorldEnvironmentList = new ABDiSE.
                Environment[MaximumEnvironments];
            this.WorldAgentList = new Agent[MaximumAgents];
            this.WorldJoinedAgentList = new JoinedAgent[MaximumAgents];



        }

        /* 
         *  public MethodReturnResults AddToEnvironmentList
         *  (ABDiSE.Environment En)
         * 
         * Description:
         *      add target environment to god's list
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        public MethodReturnResults AddToEnvironmentList(ABDiSE.Environment En)
        {
            // search for space
            for (int ii = 0; ii < MaximumEnvironments; ii++)
            {
                if (WorldEnvironmentList[ii] == null)
                {
                    WorldEnvironmentList[ii] = En;
                    return MethodReturnResults.SUCCEED;
                    //break;
                }
            }

            return MethodReturnResults.FAILED;

        }

        /* 
         *  public void ClearDeadAgent()
         * 
         * Description:
         *      clear agent and joined agent which "IsDead" == ture
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        public void ClearDeadAgent()
        {

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                //clear dead agent
                if (WorldAgentList[ii] != null &&
                    WorldAgentList[ii].IsDead == true)
                {
                    WorldAgentList[ii] = null;
                    /*
                    MainWindow.MyPrintf(
                        string.Format("Clear Dead Agent #{0}", ii));*/
                    Console.WriteLine("Clear Dead Agent #{0}", ii);
                    // agent count -1
                    AgentCount--;
                }
            }

            for (int ii = 0; ii < JoinedAgentNumber; ii++)
            {
                //clear dead agent
                if (WorldJoinedAgentList[ii] != null &&
                    WorldJoinedAgentList[ii].IsDead == true)
                {
                    WorldJoinedAgentList[ii] = null;
                    /*
                    MainWindow.MyPrintf(
                        string.Format("Clear Dead JoinedAgent #{0}", ii));
                    */
                    Console.WriteLine("Clear Dead JoinedAgent #{0}", ii);
                    // agent count -1
                    JoinedAgentCount--;
                }
            }

        }


        /* 
         *  public MethodReturnResults AddToAgentList(Agent TargetAgent)
         * 
         * Description:
         *      add target agent to god's list
         *      
         * Arguments:     
         *      TargetAgent - target agent to be added
         * Return Value:
         *      void
         */
        public MethodReturnResults AddToAgentList(Agent TargetAgent)
        {
            // agent number +1
            AgentNumber++;
            AgentCount++;

            // name does NOT exist, do nothing
            if (!TargetAgent.Properties.ContainsKey("Name"))
                return MethodReturnResults.FAILED;
            /*
            MainWindow.MyPrintf(string.Format(
                    "-AddToAgentList({0})", TargetAgent.Properties["Name"]));*/
            Console.WriteLine
                ("-AddToAgentList({0})", TargetAgent.Properties["Name"]);
            // way 1: very simple and stupid way: 
            //        search first null slot and replace it

            for (int ii = 0; ii < AgentNumber; ii++)
            {

                if (WorldAgentList[ii] == null)
                {
                    WorldAgentList[ii] = TargetAgent;

                    //refresh GUI
                    //01/23 disable
                    //MainWindow.RefreshAgentList();
                    //MainWindow.RefreshGMapMarkers();

                    return MethodReturnResults.SUCCEED;
                    //break;
                }
            }

            return MethodReturnResults.FAILED;           

        }


        /* 
         *  public MethodReturnResults AddToJoinedAgentList
         *  (Agent TargetAgent)
         * 
         * Description:
         *      add target joined agent to god's list
         *      
         * Arguments:     
         *      TargetAgent - target joined agent to be added
         * Return Value:
         *      void
         */
        public MethodReturnResults AddToJoinedAgentList
            (JoinedAgent TargetAgent)
        {
            JoinedAgentNumber++;
            JoinedAgentCount++;
            /*
            MainWindow.MyPrintf(string.Format(
                    "-AddToJoinedAgentList({0})",
                    TargetAgent.Properties["Name"]));
            */
            Console.WriteLine("-AddToJoinedAgentList({0})",
                    TargetAgent.Properties["Name"]);
            // way 1: very simple and stupid way: 
            //        search first null slot and replace it

            for (int ii = 0; ii < JoinedAgentNumber; ii++)
            {

                if (WorldJoinedAgentList[ii] == null)
                {
                    WorldJoinedAgentList[ii] = TargetAgent;

                    //windows form
                    //MainWindow.RefreshAgentList();
                    //MainWindow.RefreshJoinedAgentList();

                    return MethodReturnResults.SUCCEED;
                    //break;
                }
            }

            return MethodReturnResults.FAILED;
        }

        /* 
         *  public void CheckAgentAttachment(Agent TargetAgent)
         * 
         * Description:
         *      let the agent checks if there is suitable agent to attach
         *      TODO : only find same environment, search smartly
         *      
         * Arguments:     
         *      TargetAgent - target agent to be checked
         * Return Value:
         *      void
         */
        public void CheckAgentAttachment(Agent TargetAgent)
        {
            MethodReturnResults Result = MethodReturnResults.FAILED;
            for (int ii = 0; ii < AgentNumber; ii++)
            {
                if (WorldAgentList[ii] != null)
                {
                    Result = TargetAgent.Attach(WorldAgentList[ii]);
                    if (Result != MethodReturnResults.FAILED)
                        break;
                }
                else
                    continue;
            }

            switch (Result)
            {
                case MethodReturnResults.FAILED:
                    break;
                case MethodReturnResults.SUCCEED:
                    //Attach succeeds

                    //ClearDeadAgent();
                    //MainWindow.RefreshAgentList();
                    //MainWindow.RefreshJoinedAgentList();
                    //MainWindow.RefreshGMapMarkers();
                    break;

            }

        }


        /* 
        *  static void Main(string[] args)
        * 
        * Description:
        *      main method of all project, do initialzation
        *      
        * Arguments:     
        *      TargetAgent - target agent to be checked
        * Return Value:
        *      void
        */
        [STAThread]
        static void Main(string[] args)
        {
            
            // initialize UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // initialize God
            God god = new God();

            //Application.Run(MainWindow);
            /*god.MainWindow.MyPrintf(
                "Welcome to Agent-Based Model Simulation World!");*/
            Console.WriteLine
                ("Welcome to Agent-Based Model Simulation World!");

            //initialize default Environment
            ABDiSE.Environment HsinchuCity =
                new ABDiSE.Environment
                    (100, 20.0, 10.0, 9.2, 45.1, 31.3, 422554);

            //add to global environments
            god.AddToEnvironmentList(HsinchuCity);

            //debug
            /*
            god.MainWindow.MyPrintf(string.Format
                ("environment[0] = {0}", god.WorldEnvironmentList[0].
                ToString()));
            */
            //set main window for UI
            HsinchuCity.SetMainWindow(god.MainWindow);

            //start main window UI
            //god.MainWindow.MyPrintf("\n::::::::: Start ABDiSE GUI :::::::::");
            Console.WriteLine("\n::::::::: Start ABDiSE GUI :::::::::");
            Application.Run(god.MainWindow);

        }

        /* 
        *  internal MethodReturnResults create(
            NaturalElementAgentTypes agentType,
            Dictionary<string, string> properties,
            PointLatLng latLng,
            ABDiSE.Environment targetEnvironment
            )
        * 
        * Description:
        *      create new agent
        *      
        *      ( internal	 存取能力限制於同一組件內)
        *      組件，Assembly，為編譯後產物，副檔名為dll或exe。
        * Arguments:     
        *      agentType - natural elements like fire, smoke
        *      properties - properties in dictionary form
        *      latLng - point of lat and lng
        *      targetEnvironment - environment this agent locate
        * Return Value:
        *      MethodReturnResults - succeed , failed, etc
        */
        internal MethodReturnResults create(
            NaturalElementAgentTypes agentType,
            Dictionary<string, string> properties,
            PointLatLng latLng,
            ABDiSE.Environment targetEnvironment
            )
        {
            switch (agentType)
            {
                case NaturalElementAgentTypes.FIRE:
                    // create and initialize new fire
                    // assign correct properties

                    Agent newFire =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newFire);

                    CheckAgentAttachment(newFire);
                    break;

                case NaturalElementAgentTypes.SMOKE:
                    Agent newSmoke =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newSmoke);

                    CheckAgentAttachment(newSmoke);
                    break;

                case NaturalElementAgentTypes.CINDER:
                    Agent newCinder =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newCinder);

                    CheckAgentAttachment(newCinder);
                    break;

                case NaturalElementAgentTypes.FLOOD:
                    // create and initialize new flood
                    // assign correct properties

                    Agent newFlood =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newFlood);

                    CheckAgentAttachment(newFlood);
                    break;

                case NaturalElementAgentTypes.WATER:
                    // create and initialize new water
                    // assign correct properties

                    Agent newWater =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newWater);

                    CheckAgentAttachment(newWater);
                    break;

                default:
                    //unknown agent type: error
                    return MethodReturnResults.FAILED;

            }
            //succeed 
            return MethodReturnResults.SUCCEED;
        }


        /* 
        *  internal MethodReturnResults create(
            NaturalElementAgentTypes agentType,
            Dictionary<string, string> properties,
            PointLatLng latLng,
            ABDiSE.Environment targetEnvironment
            )
        * 
        * Description:
        *      create new agent
        *      
        *      ( internal	 存取能力限制於同一組件內)
        *      組件，Assembly，為編譯後產物，副檔名為dll或exe。
        * Arguments:     
        *      agentType - attachable types like building, tree
        *      properties - properties in dictionary form
        *      latLng - point of lat and lng
        *      targetEnvironment - environment this agent locate
        * Return Value:
        *      MethodReturnResults - succeed , failed, etc
        */
        internal MethodReturnResults create(
            AttachableObjectAgentTypes agentType,
            Dictionary<string, string> properties,
            PointLatLng latLng,
            ABDiSE.Environment targetEnvironment
            )
        {

            switch (agentType)
            {
                case AttachableObjectAgentTypes.BUILDING:

                    Agent newBuilding =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newBuilding);
                    break;

                case AttachableObjectAgentTypes.TREE:
                    Agent newTree =
                        new Agent(this, agentType, properties,
                            latLng, targetEnvironment);

                    //force activated
                    activate(newTree);
                    break;

                default:
                    //unknown agent type: error
                    return MethodReturnResults.FAILED;
            }

            return MethodReturnResults.SUCCEED;
        }

        /* 
         * private MethodReturnResults activate(Agent target)
         * 
         * Description:
         *      Activate: make specified objects active.
         *      
         * Arguments:     
         *      target - target agent to be activated
         * Return Value:
         *      MethodReturnResults - succeed, failed, etc
         */
        private MethodReturnResults activate(Agent target)
        {

            //if(...)
            target.IsActivated = true;
            // and do other things

            //succeed or fail method status
            return MethodReturnResults.SUCCEED;
        }

        /* 
         * private MethodReturnResults affect
            (Agent target, Dictionary<string, string> controls)
         * 
         * Description:
         *      Affect(or Assign): change environment parameters 
         *      and attributes of objects in non-causal ways.
         *      
         * Arguments:     
         *      target - target agent to be activated
         *      controls - control commands in dictionary
         * Return Value:
         *      MethodReturnResults - succeed, failed, etc
         */
        private MethodReturnResults affect
            (Agent target, Dictionary<string, string> controls)
        {
            // TODO
            //if(controls.ooo)
            //  target.OOO = XXX;

            //succeed or fail method status
            return MethodReturnResults.SUCCEED;

        }

        /* 
         * private MethodReturnResults control
            (Agent target, Dictionary<string, string> controls)
         * 
         * Description:
         *      Control: cause an object to change behavior/state in ways not 
         *      defined by object’s own behavior-change methods. 
         *      
         * Arguments:     
         *      target - target agent to be activated
         *      controls - control commands in dictionary
         * Return Value:
         *      MethodReturnResults - succeed, failed, etc
         */
        private MethodReturnResults control
            (Agent target, Dictionary<string, string> controls)
        {
            //if(controls.ooo)
            //  target.OOO = XXX;
            //succeed or fail method status
            return MethodReturnResults.SUCCEED;
        }
    }
}
