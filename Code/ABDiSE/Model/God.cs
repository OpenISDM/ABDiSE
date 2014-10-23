/** 
 *  @file God.cs
 *  Class God is the core Model of ABDiSE model elements. 
 *  There is only one God instance.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *  Abstract:
 *
 *      Class God is the core Model of ABDiSE model elements. 
 *      There is only one God instance.
 *      This class maintains world agent list and environment list.    
 *      It has the power to change agent properties directly.
 *
 *  Authors:  
 *
 *      Tzu-Liang Hsu, Lightorz@gmail.com
 *
 *  License:
 *
 *      GPL 3.0 This file is subject to the terms and conditions defined 
 *      in file 'COPYING.txt', which is part of this source code package.
 *
 *  Major Revision History:
 *
 *      2014/6/11: version 2.0 alpha
 *      2014/7/02: edit comments for doxygen
 *
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
using ABDiSE.Controller;


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
    /**
     *  Class God is the core Model of ABDiSE model elements. 
     *  There is only one God instance.
     *  This class maintains world agent list and environment list.    
     *  It has the power to change agent properties directly.
     */
    public class God
    {
        CoreController CoreController;
        //
        /// current simulation step
        //
        public int CurrentStep = 0;

        public Environment[] WorldEnvironmentList;

        /**
         * Reference of agents, recorded by God
         * This list is used for recording ALL agents in the simulation world
         */
        public Agent[] WorldAgentList;

        //
        /// for naming new Agent (Maximum array index)
        //
        public int AgentNumber = 0;

        //
        /// Agent counter
        //
        public int AgentCount = 0;

        
        public Dictionary<string, string> properties =
            new Dictionary<string, string>();

        public int MaximumEnvironments = 10;
        public int MaximumAgents = 5000;


        /**
         * create instance of arrays for God
         */
        public God(CoreController CoreController)
        {
            this.CoreController = CoreController;
            this.WorldEnvironmentList = new Environment[MaximumEnvironments];
            this.WorldAgentList = new Agent[MaximumAgents];

        }


        /**
         * This function adds target environment to God's list
         * 
         * @param en ABDiSE environment (pointer) which user wants to add to list. 
         * @return succeed or fail
         */
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

            return MethodReturnResults.FAIL;

        }

        /**
         * This function clears agent and joined agent which "IsDead" == ture
         */
        public void ClearDeadAgent()
        {

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                //
                /// clear dead agent
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


        /**
         * Add target agent to god's list.
         * 
         * @param targetAgent target agent to be added
         * @return succeed of fail
         */
        public MethodReturnResults AddToAgentList(Agent targetAgent)
        {
            // agent number +1
            AgentNumber++;
            AgentCount++;

            // name does NOT exist, do nothing
            if (!targetAgent.AgentProperties.ContainsKey("Name"))
                return MethodReturnResults.FAIL;

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

            return MethodReturnResults.FAIL;

        }


        /**
         * Let the agent checks if there is suitable agent to attach.
         * 
         * @todo only need to find same environment. Search smartly
         * 
         * @param targetAgent target agent to be checked
         * @return succeed of fail
         */
        public MethodReturnResults CheckAgentAttachment(Agent targetAgent)
        {
            MethodReturnResults result = MethodReturnResults.FAIL;

            for (int ii = 0; ii < AgentNumber; ii++)
            {
                if (WorldAgentList[ii] != null)
                {
                    result = targetAgent.Attach(WorldAgentList[ii]);
                    if (result != MethodReturnResults.FAIL)
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
                case MethodReturnResults.FAIL:
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

        public MethodReturnResults Create(
            string agentClass, 
            CoreController coreController, 
            Dictionary<string,string> properties, 
            PointLatLng latLng, 
            Environment env
            )
        {
            CoreController.CreateDLLInstance(
                agentClass,
                coreController,
                properties,
                latLng,
                env
                );

            return MethodReturnResults.SUCCEED;

        }

        /**
         * Activate: make specified objects active.
         * 
         * @param target target agent to be activated
         * @return succeed or fail
         */
        private MethodReturnResults activate(Agent target)
        {

            //if(...)
            target.IsActivated = true;
            //
            // and do other things
            //

            return MethodReturnResults.SUCCEED;
        }

        /**
         *  Affect(or Assign): change environment parameters and attributes of objects in non-causal ways.
         *  
         * @param target    target agent
         * @param controls  control commands in dictionary structure
         * 
         * @return succeed or fail
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

        /**
         *  Control: cause an object to change behavior/state in ways not defined by object’s own behavior-change methods. 
         *  
         *  @param target   target agent
         *  @param controls control commands in dictionary structure
         *  
         *  @return succeed or fail
         */
        private MethodReturnResults control
            (Agent target, Dictionary<string, string> controls)
        {

            //if(controls.ooo)
            //  target.OOO = XXX;

            return MethodReturnResults.SUCCEED;
        }

    }
}
