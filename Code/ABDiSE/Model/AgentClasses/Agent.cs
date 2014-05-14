using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ABDiSE.View;
using GMap.NET;
using ABDiSE.Controller;
using System.Threading;

namespace ABDiSE.Model.AgentClasses
{
    public abstract class Agent
    {
        // for example: fire, smoke, building
        public string AgentType;

        // God pointer
        public CoreController CoreController;

        // time driven counter - simulation step
        public int CurrentStep = -1;

        // detail properties of agent
        // include: 
        // 
        //
        public Dictionary<string, string> AgentProperties;

        public ConfigStrings ConfigStrings;

        // agent type - select one 
        public bool IsNaturalElementAgent = false;
        public bool IsAttachableObjectAgent = false;
        public bool IsJoinedAgent = false;

        public bool IsDead = true;
        public bool IsActivated = false;

        public Environment MyEnvironment;
        

        // for GUI use TODO: redesign IsFireMarker.
        
        public GMapMarkerCircle Marker;

        public PointLatLng LatLng;

        public Agent() 
        {
            //debug:
            //Console.WriteLine("Agent() called");
        }

        /*  
        * public Agent(
        *    CoreController CoreController, 
        *    Dictionary<string, string> Properties,
        *    PointLatLng LatLng,
        *    ABDiSE.Environment AgentEnvironment
        *    )
        * 
        * Description:
        *      Constructor of Agent class
        *      
        * Arguments:     
        *      CoreController - pointer to CoreController
        *      Properties - properties in dictionary
        *      LatLng - Point of lat and lng.
        *      AgentEnvironment - environment which this agent locates
        *      
        */
        public Agent(
            CoreController CoreController,
            Dictionary<string, string> Properties,
            PointLatLng LatLng,
            ABDiSE.Model.Environment AgentEnvironment
            )
        {
            //assign pointer
            this.CoreController = CoreController;

            this.CurrentStep = 0;

            IsDead = false;
            IsActivated = true;
            IsJoinedAgent = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.AgentProperties = Properties;

            // init marker
            this.Marker = new GMapMarkerCircle(LatLng);

            this.SetMarkerFormat();

            // add to agent list (obj ref)
            CoreController.God.AddToAgentList(this);

        }

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

            lock (agentLock)
            {
                Update();
            }

            Console.WriteLine("Step[{2}]: {0} computed {1} (ThreadPoolCallback)",
                Thread.CurrentThread.Name, this.AgentProperties["Name"].ToString(), CoreController.God.CurrentStep);
            //_doneEvent.Set();
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
        public abstract MethodReturnResults Attach(Agent B);

        public abstract void SetMarkerFormat();

        public abstract ConfigStrings SetDefaultConfigStrings();

        public abstract void Update();

        /* 
         * public MethodReturnResults MoveByWind()
         * 
         * Description:
         *      wind movement of agent
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      MethodReturnResults - succeed , failed, etc
         */
        public MethodReturnResults MoveByWind()
        {
            //Console.WriteLine(this.Properties["Name"].ToString() + ".MoveByEnvironment before(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            double windSpeed =
                double.Parse(this.MyEnvironment.EnvProperties["WindSpeed"]);
            double windDir =
                double.Parse(this.MyEnvironment.EnvProperties["WindDirection"]);

            double unit =
                Definitions.AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT * windSpeed;


            //random variable
            Random seed = new Random(Guid.NewGuid().GetHashCode());

            double range = Definitions.AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE;

            this.LatLng.Lat -=
                unit * Math.Sin(windDir)
                * (1 + 0.001 * range * seed.Next(-10, 10));
            this.LatLng.Lng -=
                unit * Math.Cos(windDir)
                * (1 + 0.001 * range * seed.Next(-10, 10));

            // update marker point
            this.Marker.Position = this.LatLng;

            //Console.WriteLine(this.Properties["Name"].ToString() + ".MoveByEnvironment after(" + this.LatLng.Lat + "," + this.LatLng.Lng + ")");

            return MethodReturnResults.SUCCEED;
        }

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
        // end of AgentDistance
        

    }
}
