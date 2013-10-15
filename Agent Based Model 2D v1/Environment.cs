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

using ABDiSE;
using GMap.NET;
using ABDiSE.GUI;

namespace ABDiSE
{

    public class Environment
    {

        //c# windows applicaiton
        public MainWindow MainWindow;


        // reference of agents, recorded by environment
        // this list is used for searching
        // agents in this list are NOT created by environment, just references
        public Agent[] RefAgentList;
        public int[] RefAgentID;


        //TODO: make a dictionary 
        
        // detail properties of agent
        public Dictionary<string, string> Properties;


        // average altitude of this local environment
        public double AvgAltitude;

        // rain fall data
        public double RainFall;

        // wind speed data
        public double WindSpeed;

        // wind direction data, maybe 0 ~ 360 degree
        public double WindDirection;

        // local air temperature
        public double Temperature;

        // local citizen's and animal's population 
        public int[] Population;

        // historical data dictionary
        public Dictionary<string, string> HistoricalData =
            new Dictionary<string, string>();


        public void SetMainWindow(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public Environment(
            int MaxNumberOfAgents, double AvgAltitude,
            double RainFall, double WindSpeed,
            double WindDirection, double Temperature, int Population
            )
        {
            this.RefAgentList = new Agent[MaxNumberOfAgents];
            this.RefAgentID = new int[MaxNumberOfAgents];
            this.AvgAltitude = AvgAltitude;
            this.RainFall = RainFall;
            this.WindSpeed = WindSpeed;
            this.WindDirection = WindDirection;
            this.Temperature = Temperature;
            this.Population = new int[10];
            this.Population[0] = Population;

            this.Properties = new Dictionary<string, string>();
            Properties.Add("Weather", "Sunny");
        }

        // update during time period

        /*
        public void updateData(...)
        {
            this.RainFall = ...;
        }
        */

        // get all data of local environment
        public string CreateEnvironmentProperties()
        {

            string s = string.Format(
                "Weather:{3}  WindSpeed: {0}  WindDirection:{1}  RainFall:{2} ", 
                WindSpeed, WindDirection, RainFall, Properties["Weather"]);
            
            return s;
            
            // return all data
            //return EnvironmentDataStruct;
        }

    }
}
