/** 
 *  @file Environment.cs
 *  Environment class can create “target environment”in ABDiSE.
 *  A local environment of a region in the world is a set of parameters that affects the objects in the region.
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *  Abstract:
 *
 *      Environment class can create “target environment”in ABDiSE.
 *      A local environment of a region in the world is a set of parameters that affects the objects in the region.
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
 *      2014/7/02: edit comments for doxygen
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMap.NET;
using ABDiSE.Model.AgentClasses;

namespace ABDiSE.Model
{
    /** 
     *   Environment class can create “target environment”in ABDiSE.
     *   
     *   An Environment is defined by a set of parameters. Agents can interact with environment.
     *   In general, the values of the parameters are functions of time and space.    
     *   A local environment of a region in the world is a set of parameters that affects the objects in the region.
     */
    public class Environment
    {

        /**
         *  Reference of agents, recorded by environment.
         *  This list is used for searching agents in this list are not created by environment
         */
        public Agent[] AgentPointers;

        /**
         *  Detail properties of agent.
         */
        public Dictionary<string, string> EnvProperties;


        /**
         *  constrouter of Environment.
         *  
         *  @param maxNumberOfAgents    Maximum number of agents
         *  @param avgAltitude          average alititude of this local environment
         *  @param rainFall             rainfall data
         *  @param windSpeed            wind speed data 
         *  @param windDirection        wind direction data, 0~360 degree
         *  @param temperature          local air temperature 
         *  @param population           local citizen's and animal's population
         *  @param populationDensity    local population density
         *  @param mobility             mobility
         *  @param weather              weather status
         */
        public Environment(
            int maxNumberOfAgents, 
            double avgAltitude,
            double rainFall, 
            double windSpeed,
            double windDirection, 
            double temperature, 
            int population,
            double populationDensity,
            double mobility,
            string weather
            )
        {
            if( maxNumberOfAgents > 0)
                this.AgentPointers = new Agent[maxNumberOfAgents];

            EnvProperties = new Dictionary<string,string>();

            //TODO: check data correctness
            EnvProperties.Add("AvgAltitude", avgAltitude.ToString());
            EnvProperties.Add("RainFall", rainFall.ToString());
            EnvProperties.Add("WindSpeed", windSpeed.ToString());
            EnvProperties.Add("WindDirection", windDirection.ToString());
            EnvProperties.Add("Temperature", temperature.ToString());
            EnvProperties.Add("Population", population.ToString());
            EnvProperties.Add("PopulationDensity", populationDensity.ToString());
            EnvProperties.Add("Mobility", mobility.ToString());
            EnvProperties.Add("Weather", weather.ToString());

        }

        /**
         * Get information string of the environment
         * 
         * @return DisplayString information string of Environment
         */
        public string CreateEnvironmentPropertiesString()
        {

            string DisplayString = string.Format(
                "Weather:{0}  WindSpeed: {1}  \nWindDirection:{2}  RainFall:{3} ", 
                EnvProperties["Weather"], EnvProperties["WindSpeed"], 
                EnvProperties["WindDirection"], EnvProperties["RainFall"] );
            
            return DisplayString;
            
        }

    } // end of Class Environment
}
