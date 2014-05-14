using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABDiSE.Controller;
using GMap.NET;
using ABDiSE.View;

namespace ABDiSE.Model.AgentClasses
{
    public abstract class JoinedAgent : Agent
    {
        public JoinedAgent(
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
            CoreController.God.AddToJoinedAgentList(this);
        
        }
        /*
        public MethodReturnResults Attach(Agent B)
        {
            return MethodReturnResults.SUCCEED;
        }*/
    }
}
