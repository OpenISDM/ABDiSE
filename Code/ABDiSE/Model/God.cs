/*++
    Copyright (c) 2014  OpenISDM

    Project Name: 

        ABDiSE
             (Agent-Based Disaster Simulation Environment)

    Version:

        2.0

    File Name:

        God.cs

    Abstract:

        God class is the core of ABDiSE model elements. 
        There is only one God instance.
        This class maintains world agent list and environment list.    
        It can change agent properties directly.
        
    Authors:  

        Tzu-Liang Hsu, Lightorz@gmail.com

    License:

        GPL 3.0 This file is subject to the terms and conditions defined 
        in file 'COPYING.txt', which is part of this source code package.

    Major Revision History:

        2014/6/11: version 2.0 alpha
--*/
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
    /*++
    Class:

        God

    Summary:

        God class is the core of ABDiSE. 
        There is only one God instance.
        This class maintains world agent list and environment list      
        It can change agent properties directly.

    Methods:
        God
            Constructor
     
        AddToEnvironmentList
            This method adds target environment to God's list
        
        ClearDeadAgent
            This method clears dead agents in the world
        
        AddToAgentList
            This method adds target agent to God's list  
        
        CheckAgentAttachment
            This method automatically checks and attaches 
        
        activate
            This method activate target agent
     
        affect
            This method change environment parameters 
            and attributes of objects in non-causal ways
      
        control
            this method causes an object to change behavior/state in ways not 
            defined by object's own behavior-change methods 
      
    --*/
    public class God
    {
        //
        // current simulation step
        //
        public int CurrentStep = 0;

        public Environment[] WorldEnvironmentList;

        //
        // reference of agents, recorded by God
        // this list is used for recording ALL agents in the simulation world
        //
        public Agent[] WorldAgentList;

        //
        // for naming new Agent (Maximum array index)
        //
        public int AgentNumber = 0;

        //
        // real agent count
        //
        public int AgentCount = 0;

        
        public Dictionary<string, string> properties =
            new Dictionary<string, string>();

        public int MaximumEnvironments = 100;
        public int MaximumAgents = 5000;

        /*++
            Constructor:

                public God()

            Function Description:

                This function search from the requested URI of searched term

            Parameters:

                void

            Possible Error Code or Exception:

                Service is not available 
        --*/
        public God()
        {

            this.WorldEnvironmentList = new Environment[MaximumEnvironments];
            this.WorldAgentList = new Agent[MaximumAgents];

        }

        /*++
            Function Name:

                public MethodReturnResults AddToEnvironmentList(Environment En)

            Function Description:

                This function adds target environment to God's list

            Parameters:

                Environment en - ABDiSE environment (pointer) which user wants to add to list.

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
        public MethodReturnResults AddToEnvironmentList(Environment en)
        {
            //
            // search for space
            //
            for (int ii = 0; ii < MaximumEnvironments; ii++)
            {
                if (WorldEnvironmentList[ii] == null)
                {
                    WorldEnvironmentList[ii] = en;
                    return MethodReturnResults.SUCCEED;

                }
            }

            return MethodReturnResults.FAILED;

        }

        /*++
            Function Name:

                public void ClearDeadAgent()

            Function Description:

                This function clears agent and joined agent which "IsDead" == ture

            Parameters:

                void

            Returned Value:

                void

            Possible Error Code or Exception:

                Service is not available 
        --*/
        public void ClearDeadAgent()
        {

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                //
                // clear dead agent
                //
                if (WorldAgentList[ii] != null &&
                    WorldAgentList[ii].IsDead == true)
                {
                    Console.WriteLine("Clear Dead Agent #{0}", WorldAgentList[ii].AgentProperties["Name"]);
                    WorldAgentList[ii] = null;

                    
                    // agent count -1
                    AgentCount--;
                }
            }


        }

        /*++
            Function Name:

                public MethodReturnResults AddToAgentList(Agent targetAgent)

            Function Description:

                add target agent to god's list

            Parameters:

                Agent targetAgent - target agent to be added

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
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

            for (int ii = 0; ii <= AgentNumber; ii++)
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

        /*++
            Function Name:

                public void CheckAgentAttachment(Agent TargetAgent)

            Function Description:

                let the agent checks if there is suitable agent to attach
                TODO : only find same environment, search smartly

            Parameters:

                Agent targetAgent - target agent to be checked

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
        public MethodReturnResults CheckAgentAttachment(Agent targetAgent)
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

            return result;
        }

        /*++
            Function Name:

                private MethodReturnResults activate(Agent target)

            Function Description:

                Activate: make specified objects active.

            Parameters:

                Agent targetAgent - target agent to be activated

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
        private MethodReturnResults activate(Agent target)
        {

            //if(...)
            target.IsActivated = true;
            //
            // and do other things
            //

            return MethodReturnResults.SUCCEED;
        }

        /*++
            Function Name:

                private MethodReturnResults affect
                    (Agent target, Dictionary<string, string> controls)

            Function Description:

                Affect(or Assign): change environment parameters 
                and attributes of objects in non-causal ways.

            Parameters:

                Agent targetAgent - target agent
                controls - control commands in dictionary

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
        private MethodReturnResults affect
            (Agent target, Dictionary<string, string> controls)
        {
            // TODO
            //if(controls.ooo)
            //  target.OOO = XXX;

            //succeed or fail method status
            return MethodReturnResults.SUCCEED;

        }

        /*++
            Function Name:

                private MethodReturnResults control
                    (Agent target, Dictionary<string, string> controls)

            Function Description:

                Control: cause an object to change behavior/state in ways not 
                defined by object’s own behavior-change methods. 

            Parameters:

                Agent targetAgent - target agent
                controls - control commands in dictionary

            Returned Value:

                MethodReturnResults - succeed or failed

            Possible Error Code or Exception:

                Service is not available 
        --*/
        private MethodReturnResults control
            (Agent target, Dictionary<string, string> controls)
        {

            //if(controls.ooo)
            //  target.OOO = XXX;

            return MethodReturnResults.SUCCEED;
        }

    }
}
