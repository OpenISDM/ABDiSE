/** 
 *  @file ConfigStrings.cs
 *  ConfigStrings is a custom data structure for agent subtypes and properties. 
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *  Version:
 *
 *      2.0
 *
 *  File Name:
 *
 *      ConfigStrings.cs
 *
 *  Abstract:
 *
 *      ConfigStrings is a custom data structure for agent subtypes and properties. 
 *      It contains agent class name and subtypes.
 *      Detailed agent properties are in the dictionary data format.
 *      ConfigStrings.Keys is for the dictionary in agent instances.
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
 *      2014/5/28: version 2.0 alpha
 *      2014/6/20: edit comments for doxygen
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABDiSE.Model.AgentClasses
{
    /**
     * 
     *  ConfigStrings is a custom data structure for agent subtypes and properties. 
     *  
     *  It contains agent class name and subtypes.
     *  The subtypes is in another class SubTypeStrings.
     *  Detailed agent properties are in the dictionary data format.
     *  ConfigStrings.Keys is for the dictionary in agent instances.
     *        
     */
    public class ConfigStrings
    {
        public string ClassShortName;
        public string ClassFullName;

        //
        /// subtypes of agent type
        //
        public List<SubTypeStrings> SubTypes;

        //
        /// keys for agent dictionary
        //
        public List<string> Keys;

        /**
         *  Constructor of ConfigStrings.
         *  
         *  use for create new data structure.
         */
        public ConfigStrings() 
        {
            ClassShortName = "unknown";
            ClassFullName = "ABDiSE.Model.AgentClasses.Unknown";

            SubTypes = new List<SubTypeStrings>();

            Keys = new List<string>();
            
        
        }

        /**
         *  Display all the information of ConfigStrings .
         * 
         *  display data in this structure, include class name and subtypes. 
         */
        public void Print()
        {
            Console.WriteLine("Class:" + this.ClassFullName);
            for (int ii = 0; ii < SubTypes.Count; ii++ )
            {
                Console.WriteLine(SubTypes[ii].AgentSubType + ":");

                for (int jj = 0; jj < SubTypes[ii].Values.Count; jj++ )
                {
                    Console.WriteLine("\t" + Keys[jj] + ", " + SubTypes[ii].Values[jj] );
                }
            }
        }
    }


    /**
     *  SubTypeStrings is a minor data sturcute for saving agent subtypes. 
     *  
     *  It contains subtype name and default values. 
     */
    public class SubTypeStrings
    {
        public string AgentSubType = "";
        public List<string> Values;

        /**
         *  Constructor of SubTypeStrings.
         * 
         *  Simply create new instance of this class.
         */
        public SubTypeStrings() 
        {
            Values = new List<string>();
        }
    }
}
