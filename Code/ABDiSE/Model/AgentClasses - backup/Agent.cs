using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ABDiSE.View;
using GMap.NET;
using ABDiSE.Controller;

namespace ABDiSE.Model.AgentClasses
{
    public abstract class Agent
    {
        // God pointer
        public CoreController CoreController;

        // time driven counter - simulation step
        public int CurrentStep;

        // detail properties of agent
        // include: 
        // 
        //
        public Dictionary<string, string> AgentProperties;

        // agent type - select one 
        public bool IsNaturalElementAgent;
        public bool IsAttachableObjectAgent;
        public bool IsDead;
        public bool IsActivated;

        public Environment MyEnvironment;
        

        // for GUI use TODO: redesign IsFireMarker.
        
        public GMapMarkerCircle Marker;

        public PointLatLng LatLng;

        public Agent() 
        {
 
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
            IsActivated = false;

            this.LatLng = LatLng;
            this.MyEnvironment = AgentEnvironment;
            this.AgentProperties = Properties;

            // init marker
            this.Marker = new GMapMarkerCircle(LatLng);

            // add to agent list
            CoreController.God.AddToAgentList(this);

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
