﻿/** 
 *  @file Agent.cs
 *  Agent.cs is a abstract class for "Agent" in the OpenISDM ABDiSE project.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *  Version:
 *
 *      2.0
 *
 *  File Name:
 *
 *      Agent.cs
 *
 *  Abstract:
 *
 *      Agent.cs is a abstract class for "Agent" in the OpenISDM ABDiSE project.
 *       
 *      Agent is like an active object, it can interact with other agent and environment.
 *      ABDiSE divides all agents into two major types: 
 *      NaturalElementAgentTypes and AttachableObjectAgentTypes.
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
 *      2014/5/28: version 2.0 alpha
 *      2014/6/20: edit comments for doxygen
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ABDiSE.View;
using GMap.NET;
using ABDiSE.Controller;
using System.Threading;
using System.Runtime.Serialization;

namespace ABDiSE.Model.AgentClasses
{
    /**
     *  Agent is a abstract class for "Agent" in the OpenISDM ABDiSE project.
     *       
     *  Agent is like an active object, it can interact with other agnet and environment.
     *  ABDiSE divides all agents into two major types: 
     *  NaturalElementAgentTypes and AttachableObjectAgentTypes.
     */
    [DataContract] // for XML
    public abstract class Agent
    {

        //
        /// for example: fire, smoke, building
        //
        [DataMember]
        public string AgentType;

        //
        /// Controller pointer
        //
        public CoreController CoreController;

        //
        /// time driven counter - simulation step
        //
        [DataMember]
        public int CurrentStep = -1;

        //
        /// detail properties of agent
        // 
        [DataMember]
        public Dictionary<string, string> AgentProperties;

        //
        /// configuration strings , will be assigned by AgentDLL
        //
        public ConfigStrings ConfigStrings;

        //
        /// agent type - select one from NE or AO
        //
        [DataMember]
        public bool IsNaturalElementAgent = false;
        [DataMember]
        public bool IsAttachableObjectAgent = false;

        //
        /// joined agent flag
        //
        [DataMember]
        public bool IsJoinedAgent = false;

        [DataMember]
        public bool IsDead = true;

        [DataMember]
        public bool IsActivated = false;

        public Environment MyEnvironment;
        
        // 
        /// GMap.NET custom marker - for display in GUI map
        //
        public GMapMarkerCircle Marker;

        [DataMember]
        public PointLatLng LatLng;

        /**
         * Constructor of Agent.
         * basically you will not call it.
         */
        public Agent() 
        {
            //debug:
            //Console.WriteLine("Agent() called");
        }


        #region XML file save/load

        /**
         *  fill the missing data of agent
         */ 
        public void RecoverAgent(CoreController coreController)
        {
            // fill other attributes
            this.CoreController = coreController;
            this.MyEnvironment = coreController.God.WorldEnvironmentList[0];
            // reset marker
            this.Marker = new View.GMapMarkerCircle(LatLng);
            this.SetMarkerFormat();

            coreController.God.AddToAgentList(this);
        }

        #endregion

        /**
         * assign values of parameters to agent data structure.
         * 
         * this constructor will be called by other types of Agent in AgentDLL
         * (for example, fire, building...)
         * 
         *  @param CoreController - pointer to controller
         *  @param Properties - data structure agent needs
         *  @param LatLng - the point of coordinates
         *  @param AgentEnvironment - environment the agent locates
         */
        public Agent(
            CoreController CoreController,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Model.Environment AgentEnvironment
            )
        {
            //
            // assign pointer
            //
            this.CoreController = CoreController;

            this.CurrentStep = CoreController.God.CurrentStep;

            IsDead = false;
            IsActivated = true;
            IsJoinedAgent = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.AgentProperties = Properties;

            //
            // init marker
            //
            this.Marker = new GMapMarkerCircle(LatLng);

            this.SetMarkerFormat();

            //
            // add to agent list (obj ref)
            //
            CoreController.God.AddToAgentList(this);

        }

        //
        ///lock of each agent
        //
        private Object agentLock = new Object();

        /**
         * Callback method of agent.
         * 
         * This method is parallelized.
         * This block is like a workitem in threadpool.
         * 
         * @param threadContext - the index of thread
         * 
         */
        public void ThreadPoolCallback(Object threadContext)
        {
            //int threadIndex = int.Parse(threadContext.ToString());
            /*Console.WriteLine(" Step{2}: thread {0} computing {1} ", 
                threadIndex, this.Properties["Name"].ToString(), God.CurrentStep);
            */

            lock (agentLock)
            {
                Update();
            }

            Console.WriteLine(
                "Step[{2}]: {0} computed {1} (TPCallback)",
                Thread.CurrentThread.Name, 
                this.AgentProperties["Name"].ToString(), 
                CoreController.God.CurrentStep
                );
        }


        /**
         * Simulates agent attachment
         * 
         * This is an abstract method, will be implemented by children class
         * simulates agent attachment
         * A attach B => A.Attach(B)
         *
         * attaches to a target if conditions are true
         * Agent A,B itself disappears,
         * create new joined agent
         * ex: becomes agent: target (joined with) fire    
         * 
         * @param B - attach target (attachable agent like building)
         * 
         * @return MethodReturnResults - succeed or fail
         * 
         */
        public abstract MethodReturnResults Attach(Agent B);


        /**
         * Set format of marker for agents.
         * 
         * This is an abstract method, will be implemented by children class                  
         * 
         */
        public abstract void SetMarkerFormat();


        /**
         * Return configuration strings in AgentDLL database.
         * 
         * This is an abstract method, will be implemented by children classes.
         * 
         * @return ConfigStrings - configuration strings
         */
        public abstract ConfigStrings SetDefaultConfigStrings();


        /**
         * Agents will excute this method once in every simulation step.
         * 
         * This is an abstract method, will be implemented by children class.
         */
        public abstract void Update();


        /**
         * This method simulates wind movement of agents
         * 
         * @return MethodReturnResults - succeed , fail, etc
         */
        public MethodReturnResults MoveByWind()
        {
            //Console.WriteLine(this.Properties["Name"].ToString() + 
            //".MoveByEnvironment before(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            double windSpeed =
                double.Parse(this.MyEnvironment.EnvProperties["WindSpeed"]);
            double windDir =
                double.Parse(this.MyEnvironment.EnvProperties["WindDirection"]);

            double unit =
                Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * windSpeed;

            //
            //random variable
            //
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = Definitions.AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE;

            this.LatLng.Lat -=
                unit * Math.Sin(windDir)
                * (1 + 0.001 * range * seed.Next(-10, 10));
            this.LatLng.Lng -=
                unit * Math.Cos(windDir)
                * (1 + 0.001 * range * seed.Next(-10, 10));

            //
            // update marker point
            //
            this.Marker.Position = this.LatLng;

            /*
            Console.WriteLine(
                this.AgentProperties["Name"].ToString() + 
                ".MoveByEnvironment after(" + 
                this.LatLng.Lat + "," + this.LatLng.Lng + ")"
            );*/

            return MethodReturnResults.SUCCEED;
        }


        /**
         * Computes distance between Agent A and B .
         * 
         * @param target - target agent which will be computed
         * @return MethodReturnResults - AGENT_CLOSEBY_DISTANCE_LEVEL or fail
         */
        public MethodReturnResults AgentDistance(Agent target)
        {
            
            //
            // compute distance (c^2 = a^2 + b^2)
            // a = lat difference
            // b = lng difference
            //
            double a2 = Math.Pow(target.LatLng.Lat - this.LatLng.Lat, 2);
            double b2 = Math.Pow(target.LatLng.Lng - this.LatLng.Lng, 2);
            double c2 = a2 + b2;
            double distance = Math.Pow(c2, 0.5);

            //
            // debug - print distance
            //
            /*Console.WriteLine("({0} , {1}) = {2}", 
                this.AgentProperties["Name"], target.AgentProperties["Name"], distance);
            */
            if (distance < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_4)
                return MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_4;
            else if (distance < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_3)
                return MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_3;
            else if (distance < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_2)
                return MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_2;
            else if (distance < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_1)
                return MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_1;
            else
                return MethodReturnResults.FAIL;

            //
            // check if [X Y Level 0]
            //
            /* old code
            if (Math.Abs(target.LatLng.Lng - this.LatLng.Lng)
                <= Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_1
                && Math.Abs(target.LatLng.Lat - this.LatLng.Lat)
                <= Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_1)
            {
                // return status: close by level 1
                result = MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_1;

                if (Math.Abs(target.LatLng.Lng - this.LatLng.Lng)
                < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_2 &&
                    Math.Abs(target.LatLng.Lat - this.LatLng.Lat)
                < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_2)
                {
                    // return status: close by level 2
                    result = MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_2;
                }


            }
            */
        }
        

    }
}
