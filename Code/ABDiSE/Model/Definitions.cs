/** 
 *  @file Definitions.cs
 *  Definitions.cs includes necessary data constants of variables 
 *  in the OpenISDM ABDiSE project.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *
 *  File Name:
 *
 *      Definitions.cs
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
 *      2014/7/01: edit comments for doxygen
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.Model
{
    /**
     * This class includes constants and movement unit, etc. 
     */
    public class Definitions
    {
        //
        /// agent attach constants
        //
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_1 = 0.000200;
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_2 = 0.000100;
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_3 = 0.000050;
        public const double AGENT_CLOSEBY_DISTANCE_LEVEL_4 = 0.000025;

        //
        /// agent movement (environment)
        //
        public const double AGENT_MOVEMENT_ENVIRONMENT_BASIC_UNIT = 0.000006;
        
        //
        /// 1 means +-1%
        //
        public const double AGENT_MOVEMENT_RANDOM_RANGE_PERCENTAGE = 30.0;
    }


    /**
     * This enum includes results which a ABDiSE method may return.
     */
    public enum MethodReturnResults
    {
        SUCCEED,
        FAIL,

        CLOSEBY_DISTANCE_LEVEL_1,
        CLOSEBY_DISTANCE_LEVEL_2,
        CLOSEBY_DISTANCE_LEVEL_3,
        CLOSEBY_DISTANCE_LEVEL_4,

        MAXIMUM_RESULT

    }


    /**
     * This enum includes diameter information which GMap.NET markers need.
     */ 
    public enum CircleDiameterTypes
    {
        IntActiveDiameter = 12,
        IntPassiveDiameter = 12,
        MaximumDiameter
    }
}
