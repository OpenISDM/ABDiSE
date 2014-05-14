/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    Environment.cs

    SVN $Revision: $

    Abstract:     this class can create “target environment”
                  according to custom class Map, or other GIS data format
                  A local environment of a region in the world is a set of parameters that affects the objects in the region
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMap.NET;
using ABDiSE.Model.AgentClasses;

namespace ABDiSE.Model
{
    /*  
    * public class Environment
    * 
    * Description:
    *   Agents interact with environment. An environment is defined 
    *   by a set of parameters. In general, the values of the parameters 
    *   are functions of time and space.
    *      
    */
    public class Environment
    {

        // reference of agents, recorded by environment
        // this list is used for searching
        // agents in this list are not created by environment
        public Agent[] AgentPointers;
        
        // detail properties of agent
        /* include:
            // average altitude of this local environment
            double AvgAltitude;

            // rain fall data
            double RainFall;

            // wind speed data
            double WindSpeed;

            // wind direction data, maybe 0 ~ 360 degree
            double WindDirection;

            // local air temperature
            double Temperature;

            // local citizen's and animal's population 
            int Population;
          
            String Weather;
         */
        public Dictionary<string, string> EnvProperties;


        /* TODO: edit new GUI
        public void SetMainWindow(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }
        */

        public Environment(
            int maxNumberOfAgents, 
            double avgAltitude,
            double rainFall, 
            double windSpeed,
            double windDirection, 
            double temperature, 
            int population, 
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
            EnvProperties.Add("Weather", weather.ToString());

        }

        // update during simulation
        /*
        public void updateData(...)
        {
            this.RainFall = ...;
        }
        */

        // get information string of the environment
        public string CreateEnvironmentPropertiesString()
        {

            string DisplayString = string.Format(
                "Weather:{0}  WindSpeed: {1}  WindDirection:{2}  RainFall:{3} ", 
                EnvProperties["Weather"], EnvProperties["WindSpeed"], 
                EnvProperties["WindDirection"], EnvProperties["RainFall"] );
            
            return DisplayString;
            
        }

    } // end of Class Environment
}
