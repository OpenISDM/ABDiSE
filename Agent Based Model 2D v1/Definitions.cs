/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    Definitions.cs

    SVN $Revision: $

    Abstract:     Definitions of ABDiSE
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE
{
    /*  
    * public class Definitions
    * 
    * Description:
    *      this class includes constants and movement unit, etc. 
    *      
    */
    public class Definitions
    {
        // agent attach const
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_1 = 0.00020;
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_2 = 0.000015;
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_3 = 0.0000015;

        // agent movement (environment)
        public const double AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT = 0.000006;
        // 1 means +-1%
        public const double AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE = 30.0;
    }

    /*  
    * public enum MethodReturnResults
    * 
    * Description:
    *      this enum includes results method may return 
    *      
    */
    public enum MethodReturnResults
    {
        SUCCEED,
        FAILED,

        CLOSEBY_DISTANCE_LEVEL_1,
        CLOSEBY_DISTANCE_LEVEL_2,

        MAXIMUM_RESULT

    }

    // Agent Types version 3
    /*  
    * public enum NaturalElementAgentTypes
    * 
    * Description:
    *      this enum includes natural element types of agent
    *      
    */
    public enum NaturalElementAgentTypes
    {
        //nature agents
        FIRE, SMOKE, FUILD, CINDER, FLOOD, WATER,

        //it belongs to the AttachableObjectAgentTypes
        NULL,

        //end of types
        MAXIMUM_AGENT

    }

    /*  
    * public enum AttachableObjectAgentTypes
    * 
    * Description:
    *      this enum includes attachable types of agent
    *      
    */
    public enum AttachableObjectAgentTypes
    {

        //others
        TREE, BUILDING, HUMAN, ANIMAL, CAR, SHIP,

        //it belongs to the NaturalElementAgentTypes
        NULL,

        //end of types
        MAXIMUM_AGENT

    }

    /*  
    * public enum AttachableObjectAgentTypes
    * 
    * Description:
    *      this enum includes active and passive diameter
    *      for GUI marker use
    *      
    */
    public enum CircleDiameterTypes
    {
        IntActiveDiameter = 12,
        IntPassiveDiameter = 12,
        MaximumDiameter
    }
}
