/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    Agent.cs

    SVN $Revision: $

    Abstract:     Agent is an active object, behaves differently according 
                  to its type.
                  This class describes methods and attributes of Agents.
 
    Authors:      T.L. Hsu  
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ABDiSE;
using ABDiSE.AgentDatabase.Fire;
using ABDiSE.AgentDatabase.Smoke;
using ABDiSE.AgentDatabase.Water;
using GMap.NET;
using System.Threading;
using ABDiSE.GUI;

namespace ABDiSE
{
    /*  
    * public class Agent
    * 
    * Description:
    *      Agent is the basic element of ABDiSE, it interacts with environment,
    *      other agents, and joined agents.
    *      
    */
    public class Agent
    {
        //God pointer
        God God;


        //time driven counter
        public int CurrentStep = 0;

        // detail properties of agent
        public Dictionary<string, string> Properties;


        //types 
        public NaturalElementAgentTypes NaturalElementAgentType;
        public AttachableObjectAgentTypes AttachableObjectAgentType;

        public bool IsDead;

        public bool IsActivated;

        //for GUI use
        public GMapMarkerCircle Marker;

        public PointLatLng LatLng;

        public ABDiSE.Environment MyEnvironment;

        /*  
        * public MethodReturnResults AgentDistance(Agent TargetAgent)
        * 
        * Description:
        *      judge/determine collision between Agent A and B
        *      
        * Arguments:     
        *      Agent - target agent it compares to
        * Return Value:
        *      MethodReturnResults - succeed ,failed or etc
        */
        public MethodReturnResults AgentDistance(Agent TargetAgent)
        {
            MethodReturnResults result = MethodReturnResults.FAILED;

            //check if [X Y Level 0]
            if (Math.Abs(TargetAgent.LatLng.Lng - this.LatLng.Lng)
                <= Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_1
                && Math.Abs(TargetAgent.LatLng.Lat - this.LatLng.Lat)
                <= Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_1)
            {
                // return status: close by level 1
                result = MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_1;

                if (Math.Abs(TargetAgent.LatLng.Lng - this.LatLng.Lng)
                < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_2 &&
                    Math.Abs(TargetAgent.LatLng.Lat - this.LatLng.Lat)
                < Definitions.AGENT_CLOSEBY_DISTANCE_LEVEL_2)
                {
                    // return status: close by level 2
                    result = MethodReturnResults.CLOSEBY_DISTANCE_LEVEL_2;
                }


            }

            return result;
        }


        /*  
        * public Agent(
        *    God God,
        *    NaturalElementAgentTypes AgentType,
        *    Dictionary<string, string> Properties,
        *    PointLatLng LatLng,
        *    ABDiSE.Environment AgentEnvironment
        *    )
        * 
        * Description:
        *      Constructor of Agent class
        *      
        * Arguments:     
        *      God - pointer to God
        *      AgentType - types like fire, smoke, etc
        *      Properties - properties in dictionary
        *      LatLng - Point of lat and lng.
        *      AgentEnvironment - environment this agent locate
        *      
        */
        public Agent(
            God God,
            NaturalElementAgentTypes AgentType,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Environment AgentEnvironment
            )
        {
            // init
            this.God = God;
            
            //God.MainWindow.MyPrintf(
            //        "-constructing NaturalElementAgentTypes V3 Agent");

            this.NaturalElementAgentType = AgentType;
            this.AttachableObjectAgentType =
                AttachableObjectAgentTypes.NULL;

            IsDead = false;
            IsActivated = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.Properties = Properties;

            // init marker
            this.Marker = new GMapMarkerCircle(LatLng);

            //assign marker type and color
            switch (AgentType)
            {
                //specify every type of agents
                case NaturalElementAgentTypes.FIRE:
                    Marker.IsFireMarker();
                    break;
                case NaturalElementAgentTypes.SMOKE:
                    Marker.IsSmokeMarker();
                    break;
                case NaturalElementAgentTypes.CINDER:
                    //TODO: this is for demo usage
                    if(this.Properties["MarkerType"]=="Building")
                        Marker.IsCinderMarker(1);
                    else if (this.Properties["MarkerType"] == "Tree")
                        Marker.IsCinderMarker(2);
                    break;

                case NaturalElementAgentTypes.FLOOD:
                    Marker.IsFloodMarker();
                    break;

                case NaturalElementAgentTypes.WATER:
                    Marker.IsWaterMarker();
                    break;
            }
            // add to agent list
            God.AddToAgentList(this);

        }

        /*  
        * public Agent(
        *    God God,
        *    NaturalElementAgentTypes AgentType,
        *    Dictionary<string, string> Properties,
        *    PointLatLng LatLng,
        *    ABDiSE.Environment AgentEnvironment
        *    )
        * 
        * Description:
        *      Constructor of Agent class
        *      
        * Arguments:     
        *      God - pointer to God
        *      AgentType - types like building, tree, etc
        *      Properties - properties in dictionary
        *      LatLng - Point of lat and lng.
        *      AgentEnvironment - environment this agent locate
        *      
        */
        public Agent(
            God God,
            AttachableObjectAgentTypes AgentType,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Environment AgentEnvironment
            )
        {
            //init
            this.God = God;
            //God.MainWindow.MyPrintf(
            //    "-constructing AttachableObjectAgentTypes V3 Agent");

            this.AttachableObjectAgentType = AgentType;
            this.NaturalElementAgentType =
                NaturalElementAgentTypes.NULL;

            IsDead = false;
            IsActivated = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.Properties = Properties;

            this.Marker = new GMapMarkerCircle(LatLng);

            switch (AgentType)
            {
                //specify every type of agents
                case AttachableObjectAgentTypes.BUILDING:
                    Marker.IsBuildingMarker();
                    break;
                case AttachableObjectAgentTypes.TREE:
                    Marker.IsTreeMarker();
                    break;

            }
            // add to agent list
            God.AddToAgentList(this);

        }

        /*
         * ThreadPoolCallback test
         */
        //private ManualResetEvent _doneEvent;

        /*public void SetDoneEvent(ManualResetEvent doneEvent)
        {
            //thread pool testing
            _doneEvent = doneEvent;
        }
        */

        //lock of each agent
        private Object agentLock = new Object();

        /*  
        * public void ThreadPoolCallback(Object threadContext)
        * 
        * Description:
        *      callback for agent
        *      this method is parallelized     
        *      
        *      
        * Arguments:     
        *      threadContext - the index of for loop
        * Return Value:
        *      void
        */
        public void ThreadPoolCallback(Object threadContext)
        {
            //int threadIndex = int.Parse(threadContext.ToString());
            /*Console.WriteLine(" Step{2}: thread {0} computing {1} ", 
                threadIndex, this.Properties["Name"].ToString(), God.CurrentStep);
            */
            
            lock(agentLock){
                Update();
            }

            Console.WriteLine(" Step{2}: {0} computed {1} ",
                Thread.CurrentThread.Name, this.Properties["Name"].ToString(), God.CurrentStep);
            //_doneEvent.Set();
        }

        /* 
         * public void Update()
         * 
         * Description:
         *      update agent itself, do actions according to agent type
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        public void Update()
        {
            FireAgentDatabase fireDB = new FireAgentDatabase(God);
            SmokeAgentDatabase smokeDB = new SmokeAgentDatabase(God);
            WaterAgentDatabase waterDB = new WaterAgentDatabase(God);

            switch (this.NaturalElementAgentType)
            {
                case NaturalElementAgentTypes.FIRE:

                    // simulate when to disappear
                    fireDB.SimulateFireLife(this);

                    // environment movement
                    MoveByWind();
                    break;
                case NaturalElementAgentTypes.SMOKE:

                    // simulate when to disappear
                    smokeDB.SimulateSmokeLife(this);

                    // environment movement
                    MoveByWind();
                    break;
                
                case NaturalElementAgentTypes.WATER:
                    //TODO: call DLL
                    waterDB.SimulateWaterDepth(this);
                    waterDB.SimulateWaterFlood(this);

                    break;

                case NaturalElementAgentTypes.FLOOD:
                    FloodRandomMove();
                    break;
                // not Natural element types
                case NaturalElementAgentTypes.NULL:

                    switch(this.AttachableObjectAgentType){
                        case AttachableObjectAgentTypes.BUILDING:
                            break;
                        case AttachableObjectAgentTypes.TREE:
                            break;
                    }
                    break;

            }

            this.CurrentStep = God.CurrentStep;

        }

        /* 
         * public MethodReturnResults MoveByEnvironment()
         * 
         * Description:
         *      testing movement of agent
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      MethodReturnResults - succeed , failed, etc
         */
        public MethodReturnResults MoveByWind()
        {
            //Console.WriteLine(this.Properties["Name"].ToString() + ".MoveByEnvironment before(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            double unit = Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * 
                this.MyEnvironment.WindSpeed;

            //random variable
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = Definitions.AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE;

            this.LatLng.Lat -= 
                unit * Math.Sin(this.MyEnvironment.WindDirection)
                * (1 + 0.001 * range * seed.Next(-10, 10));
            this.LatLng.Lng -= 
                unit * Math.Cos(this.MyEnvironment.WindDirection)
                * (1 + 0.001 * range * seed.Next(-10, 10));

            // update marker point
            this.Marker.Position = this.LatLng;

            //Console.WriteLine(this.Properties["Name"].ToString() + ".MoveByEnvironment after(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            return MethodReturnResults.SUCCEED;
        }

        /* 
         * public MethodReturnResults FloodRandomMove()
         * 
         * Description:
         *      testing movement of agent
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      MethodReturnResults - succeed , failed, etc
         */
        public MethodReturnResults FloodRandomMove()
        {
            
            double unit = Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT*10;

            //random variable
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = Definitions.AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE;

            this.LatLng.Lat +=
                unit * Math.Sin(seed.Next(0, 360))
                * (1 + 0.001 * range * seed.Next(-10, 10));
            this.LatLng.Lng -=
                unit * Math.Cos(seed.Next(0, 360))
                * (1 + 0.001 * range * seed.Next(-10, 10));

            // update marker point
            this.Marker.Position = this.LatLng;

            //Console.WriteLine(this.Properties["Name"].ToString() + ".MoveByEnvironment after(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            return MethodReturnResults.SUCCEED;
        }

        /* 
         * public MethodReturnResults Attach(Agent AgentB)
         * 
         * Description:
         *      simulate agent attachment
         *      A attach B => A.Attach(B)
         *      B attached by A => B.AttachedBy(A)
         *      attaches to a target if conditions are true
         *      Agent A,B itself disappears,
         *      create new joined agent
         *      ex: becomes agent: target (joined with) fire
         * Arguments:     
         *      AgentB - attach target (attachable agent like building)
         * Return Value:
         *      MethodReturnResults - succeed , failed, etc
         */
        public MethodReturnResults Attach(Agent AgentB)
        {

            //not activated
            if (this.IsActivated == false || AgentB.IsActivated == false)
                return MethodReturnResults.FAILED;

            //two types condition
            if ((this.NaturalElementAgentType
                == NaturalElementAgentTypes.NULL)
                || (AgentB.AttachableObjectAgentType
                == AttachableObjectAgentTypes.NULL))
                return MethodReturnResults.FAILED;

            //closeby level
            if (AgentB.AgentDistance(this) != MethodReturnResults.FAILED)
            {
                /*
                God.MainWindow.MyPrintf(
                    string.Format("Attach close by ({0} attaches{1})",
                    this.Properties["Name"].ToString(),
                    AgentB.Properties["Name"].ToString())
                    );

                God.MainWindow.MyPrintf(
                    string.Format("Agent {0} is dead",
                    this.Properties["Name"].ToString()));
                */
                Console.WriteLine("Attach close by ({0} attaches{1})",
                    this.Properties["Name"].ToString(),
                    AgentB.Properties["Name"].ToString());

                //call attachedby and create new joinedAgent
                AgentB.AttachedBy(this);

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

        /* 
         * public MethodReturnResults AttachedBy(Agent AgentA)
         * 
         * Description:
         *      simulate agent attachment
         *      A attach B => A.Attach(B)
         *      B attached by A => B.AttachedBy(A)
         *      attached by a target if conditions are true
         *      Agent A,B itself disappears,
         *      create new joined agent
         *      ex: becomes agent: building (joined with) target
         * Arguments:     
         *      AgentA - attach by this target (element agent like fire)
         * Return Value:
         *      MethodReturnResults - succeed , failed, etc
         */
        public MethodReturnResults AttachedBy(Agent AgentA)
        {
            /*
            God.MainWindow.MyPrintf(
                string.Format("{1} Attached By {0}",
                AgentA.Properties["Name"].ToString(),
                this.Properties["Name"].ToString())
                );
            */
            Console.WriteLine(string.Format("{1} Attached By {0}",
                AgentA.Properties["Name"].ToString(),
                this.Properties["Name"].ToString())
                );
            switch (AgentA.NaturalElementAgentType)
            {
                case NaturalElementAgentTypes.FIRE:
                    switch (this.AttachableObjectAgentType)
                    {
                        //possible cases
                        case AttachableObjectAgentTypes.TREE:
                        case AttachableObjectAgentTypes.BUILDING:

                            //create new joined agent
                            JoinedAgent NewJoinedAgent = new JoinedAgent(
                                God,
                                AgentA.NaturalElementAgentType,
                                this.AttachableObjectAgentType,
                                AgentA.Properties, this.Properties,
                                LatLng, MyEnvironment
                                );

                            this.IsActivated = false;
                            this.IsDead = true;

                            return MethodReturnResults.SUCCEED;
                            //break;

                        // impossible cases
                        case AttachableObjectAgentTypes.CAR:
                            //do nothing
                            break;
                    }
                    break;

                case NaturalElementAgentTypes.SMOKE:
                    switch (this.AttachableObjectAgentType)
                    {

                        case AttachableObjectAgentTypes.BUILDING:
                            /*
                            //create new joined agent
                            JoinedAgent NewJoinedAgent = new JoinedAgent(
                                God,
                                AgentA.NaturalElementAgentType,
                                this.AttachableObjectAgentType,
                                AgentA.Properties, this.Properties,
                                LatLng, MyEnvironment
                                );

                            //agent A and B disappers
                            this.IsActivated = false;
                            this.IsDead = true;
                            */
                            break;

                        //impossible cases
                        case AttachableObjectAgentTypes.TREE:
                        case AttachableObjectAgentTypes.CAR:
                            break;

                    }
                    break;

                default:
                    break;
            }

            return MethodReturnResults.FAILED;
        }//end of attachedby

    }
}
