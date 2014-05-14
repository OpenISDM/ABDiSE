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
using ABDiSE.Model;
using ABDiSE.Model.AgentClasses;

using GMap.NET;
using System.Threading;


namespace ABDiSE.Model
{
    /*  
    * public class God
    * 
    * Description:
    *      God class is the core of ABDiSE, there is only one God instance.
    *      Maintenances world agent list and environment list      
    *      do creation and can change agent directly.
    *      
    */
    public class God
    {
        //c# windows applicaiton
        //MainWindow MainWindow;

        //public SimpleThreadPool SimpleTP;

        public int CurrentStep = 0;

        public Environment[] WorldEnvironmentList;
        // reference of agents, recorded by God
        // this list is used for recording ALL agents in the simulation world
        public Agent[] WorldAgentList;
        //public JoinedAgent[] WorldJoinedAgentList;

        // for naming new Agent (Maximum array index)
        public int AgentNumber = 0;
        //public int JoinedAgentNumber = 0;

        //real agent count
        public int AgentCount = 0;
        //public int JoinedAgentCount = 0;

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
        public God()
        {
            // create the only GUI, and assign pointers of itself to it.

            this.WorldEnvironmentList = new Environment[MaximumEnvironments];
            this.WorldAgentList = new Agent[MaximumAgents];
            //this.WorldJoinedAgentList = new JoinedAgent[MaximumAgents];

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
        public MethodReturnResults AddToEnvironmentList(Environment En)
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

                    Console.WriteLine("Clear Dead Agent #{0}", ii);
                    // agent count -1
                    AgentCount--;
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
        public MethodReturnResults AddToAgentList(Agent targetAgent)
        {
            // agent number +1
            AgentNumber++;
            AgentCount++;

            // name does NOT exist, do nothing
            if (!targetAgent.AgentProperties.ContainsKey("Name"))
                return MethodReturnResults.FAILED;

            Console.WriteLine
                ("-AddToAgentList({0})", targetAgent.AgentProperties["Name"]);
            // way 1: very simple and stupid way: 
            //        search first null slot and replace it

            for (int ii = 0; ii < AgentNumber; ii++)
            {

                if (WorldAgentList[ii] == null)
                {
                    WorldAgentList[ii] = targetAgent;

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
        public void CheckAgentAttachment(Agent targetAgent)
        {
            MethodReturnResults result = MethodReturnResults.FAILED;

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                if (WorldAgentList[ii] != null)
                {
                    result = targetAgent.Attach(WorldAgentList[ii]);
                    if (result != MethodReturnResults.FAILED)
                    {
                        Console.WriteLine(result.ToString());
                        break;
                    }
                }
                else
                    continue;
            }

            switch (result)
            {
                case MethodReturnResults.FAILED:
                    break;
                case MethodReturnResults.SUCCEED:
                    //Attach succeeds

                    //Console.WriteLine("//Attach succeeds");
                    //ClearDeadAgent();
                    //MainWindow.RefreshAgentList();
                    //MainWindow.RefreshJoinedAgentList();
                    //MainWindow.RefreshGMapMarkers();
                    break;

            }

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
