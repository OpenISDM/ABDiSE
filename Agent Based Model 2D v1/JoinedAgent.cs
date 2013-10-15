/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    JoinedAgent.cs

    SVN $Revision: $

    Abstract:     JoinedAgent is combined with 2 agents.
                  Their new attributes need simulations.
                  This class describes methods and attributes of JoinedAgents.
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ABDiSE;
using ABDiSE.AgentDatabase;
using ABDiSE.AgentDatabase.Fire;
using ABDiSE.AgentDatabase.Smoke;
using GMap.NET;
using System.Threading;
using ABDiSE.GUI;

namespace ABDiSE
{
    public class JoinedAgent
    {
        //god pointer
        God God;

        //time driven counter
        public int CurrentStep = 0;

        public Dictionary<string, string> Properties, PropertiesA, 
            PropertiesB;

        public NaturalElementAgentTypes AgentTypeA;
        public AttachableObjectAgentTypes AgentTypeB;

        public bool IsDead;

        public bool IsActivated;

        //for GUI use
        public GMapMarkerCircle Marker;

        public PointLatLng LatLng;

        public ABDiSE.Environment MyEnvironment;

        // constructor delegate : non-switch, use table instead.
        public delegate void JoinedAgentTypeDelegate();

        // table [N*M], N = number of element types, M = attachable types
        JoinedAgentTypeDelegate[ , ] JoinedAgentTable =
            new JoinedAgentTypeDelegate[
                (int)NaturalElementAgentTypes.MAXIMUM_AGENT, 
                (int)AttachableObjectAgentTypes.MAXIMUM_AGENT];


        /*  
        * public JoinedAgent(
        *    God God,
        *    NaturalElementAgentTypes TypeA,
        *    AttachableObjectAgentTypes TypeB,
        *    Dictionary<string, string> PropertiesA,
        *    Dictionary<string, string> PropertiesB,
        *    PointLatLng LatLng,
        *    ABDiSE.Environment AgentEnvironment
        *    )
        * 
        * Description:
        *      Constructor of JoinedAgent class
        *      
        * Arguments:     
        *      God - pointer to God
        *      TypeA - types like fire, smoke, etc
        *      TypeB - types like building, tree, etc
        *      PropertiesA, PropertiesB - properties in dictionary
        *      LatLng - Point of lat and lng.
        *      AgentEnvironment - environment this agent locate
        *      
        */
        public JoinedAgent(
            God God,
            NaturalElementAgentTypes TypeA,
            AttachableObjectAgentTypes TypeB,
            Dictionary<string, string> PropertiesA,
            Dictionary<string, string> PropertiesB,
            PointLatLng LatLng,
            ABDiSE.Environment AgentEnvironment
            )
        {
            
            //init
            this.God = God;

            //God.MainWindow.MyPrintf("-constructing JoinedAgent");

            this.AgentTypeA = TypeA;
            this.AgentTypeB = TypeB;
            this.PropertiesA = PropertiesA;
            this.PropertiesB = PropertiesB;

            this.LatLng = LatLng;

            this.MyEnvironment = AgentEnvironment;
            this.IsDead = false;
            this.IsActivated = true;

            this.Marker = new GMapMarkerCircle(LatLng);


            // load needed databases (optional) 
            FireAgentDatabase FireDB = new FireAgentDatabase(God);
            SmokeAgentDatabase SmokeDB = new SmokeAgentDatabase(God);

            // fill table
            JoinedAgentTable[(int)NaturalElementAgentTypes.FIRE, 
                (int)AttachableObjectAgentTypes.BUILDING] = 
                IsBuildingJoinedFire;

            JoinedAgentTable[(int)NaturalElementAgentTypes.FIRE,
                (int)AttachableObjectAgentTypes.TREE] =
                IsTreeJoinedFire;

            JoinedAgentTable[(int)NaturalElementAgentTypes.SMOKE,
                (int)AttachableObjectAgentTypes.BUILDING] =
                IsBuildingJoinedSmoke;

            JoinedAgentTable[(int)AgentTypeA, (int)AgentTypeB]();

        }//end of constructor

        /* 
         * private void IsBuildingJoinedSmoke()
         * 
         * Description:
         *      setting properties to building joined smoke
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        private void IsBuildingJoinedSmoke()
        {
            // assign detail properties
            Properties = new
                SmokeAgentDatabase(God).
                SimulateBuildingJoinedSmoke
                (PropertiesA, PropertiesB);
            Properties.Add("Name",
                    string.Format("Building@Smoke 0{0}",
                    God.JoinedAgentNumber + 1));
            //assign marker type
            Marker.IsBuildingJoinedSmokeMarker();

            //add to joined agent list
            God.AddToJoinedAgentList(this);
        }

        /* 
         * private void IsBuildingJoinedFire()
         * 
         * Description:
         *      setting properties to building joined fire
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        private void IsBuildingJoinedFire()
        {
            // assign detail properties
            Properties = new
                FireAgentDatabase(God).
                SimulateBuildingJoinedFire
                (PropertiesA, PropertiesB);

            Properties.Add("Name",
                string.Format("Building@Fire 0{0}"
                , God.JoinedAgentNumber + 1));

            //assign marker type
            Marker.IsBuildingJoinedFireMarker();

            //add to joined agent list
            God.AddToJoinedAgentList(this);
        }

        /* 
         * private void IsTreeJoinedFire()
         * 
         * Description:
         *      setting properties to tree joined fire
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
         */
        private void IsTreeJoinedFire()
        {
            // assign detail properties
            Properties = new
                FireAgentDatabase(God).
                SimulateTreeJoinedFire
                (PropertiesA, PropertiesB);
            Properties.Add("Name",
                 string.Format("Tree@Fire 0{0}",
                 God.JoinedAgentNumber + 1));

            //assign marker type
            Marker.IsTreeJoinedFireMarker();

            //add to joined agent list
            God.AddToJoinedAgentList(this);
        }


        //lock of each agent
        private Object joinedagentLock = new Object();

        public void ThreadPoolCallback(Object threadContext)
        {

            lock (joinedagentLock)
            {
                Update();
            }
            Console.WriteLine(" JoinedAgent in pool: Step{2}: {0} computed {1} ",
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
            switch (AgentTypeA)
            {
                case NaturalElementAgentTypes.FIRE:
                    switch (AgentTypeB)
                    {
                        case AttachableObjectAgentTypes.BUILDING:
                            
                            //simulate life
                            new FireAgentDatabase(God).
                                SimulateBuildingFireLife(this);

                            // create new fire agents
                            // TODO: details properties computation
                            Dictionary<string, string> buildingFireProperties 
                                =  new FireAgentDatabase(God).
                                ReturnProperties(FireAgentDataTypes.ClassA);

                            God.create(NaturalElementAgentTypes.FIRE,
                                buildingFireProperties,
                                LatLng,
                                MyEnvironment
                                );

                            //create new smoke agents
                            // TODO: details properties computation
                            Dictionary<string, string> buildingSmokeProperties
                                = new SmokeAgentDatabase(God).
                                ReturnProperties(SmokeAgentDataTypes.
                                TypeDrySmokeResidues);

                            God.create(NaturalElementAgentTypes.SMOKE,
                                buildingSmokeProperties,
                                LatLng,
                                MyEnvironment
                                );

                            break;
                        case AttachableObjectAgentTypes.TREE:

                            //simulate life
                            //TODO: rewrite Tree x Fire
                            new FireAgentDatabase(God).
                                SimulateBuildingFireLife(this);

                            // create new fire agents
                            // TODO: details properties computation
                            Dictionary<string, string> treeFireProperties
                                = new FireAgentDatabase(God).
                                ReturnProperties(FireAgentDataTypes.ClassA);

                            God.create(NaturalElementAgentTypes.FIRE,
                                treeFireProperties,
                                LatLng,
                                MyEnvironment
                                );

                            //create new smoke agents
                            // TODO: details properties computation
                            Dictionary<string, string> treeSmokeProperties
                                = new SmokeAgentDatabase(God).
                                ReturnProperties(SmokeAgentDataTypes.
                                TypeDrySmokeResidues);

                            God.create(NaturalElementAgentTypes.SMOKE,
                                treeSmokeProperties,
                                LatLng,
                                MyEnvironment
                                );
                            break;


                        default:
                            //error
                            break;
                    }

                    break;

                case NaturalElementAgentTypes.SMOKE:
                    switch (AgentTypeB)
                    {
                        case AttachableObjectAgentTypes.BUILDING:
                            

                            break;

                        default:
                            
                            break;
                    }

                    break;

                default:
                    //error
                    break;
            }

        }

    }

}
