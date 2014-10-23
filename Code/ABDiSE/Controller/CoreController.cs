/** 
 *  @file CoreController.cs
 *  CoreController.cs is the main controller class.
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
 *      Corecontroller is the main controller class in the OpenISDM ABDiSE project.
 *      It interacts with Model and View classes.
 *          
 *      Model–view–controller (MVC)  is a software architectural pattern. 
 *      It divides a given software application into three interconnected parts.
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
using System.Threading.Tasks;

using ABDiSE.Model;
using ABDiSE.Model.AgentClasses;
using ABDiSE.Controller.ThreadPool;
using GMap.NET;
using System.Reflection;
using System.Collections;

namespace ABDiSE.Controller
{
    /**
     * CoreController is the main controller class, it interacts with Model and View classes 
     * (in MVC architectural pattern).
     */
    public class CoreController
    {
        //
        /// Pointer to Model.God.
        //
        public God God;

        //
        /// Pointer to custom simple thread pool.
        //
        public SimpleThreadPool STP;

        //
        /// Configuration strings from agent types in DLL.
        //
        public List<ConfigStrings> ConfigStrings;

        Assembly Assemblies = null;

        //
        /// All loaded DLL classes
        //
        public ArrayList Classes;

        //
        /// types of agent classes: for xml save/load
        //
        public Type[] AllTypes;

        //
        /// Loaded and selected classes by user
        //
        public ArrayList SelectedClasses;

        //
        /// Providing xml save/load functions
        //
        public XMLHandler XMLController;

        /**
         * Initialize environment and load DLLs dynamically
         */ 
        public CoreController()
        {
            this.God = new God(this);

            this.XMLController = new XMLHandler(this);

            Console.WriteLine
                ("Welcome to ABDiSE 2.0 - An Agent-Based Disaster Simulation Environment");

            //create default Environment
            ABDiSE.Model.Environment HsinchuCity = 
                new ABDiSE.Model.Environment(
                    400, 
                    20.0, 
                    0.0, 
                    9.2, 
                    45.1, 
                    23.3, 
                    431029,
                    4138.44,
                    2014,
                    "Sunny");

            //add to global environments
            God.AddToEnvironmentList(HsinchuCity);

            //debug
            
            Console.WriteLine(string.Format
                ("environment[0] = {0}", God.WorldEnvironmentList[0].
                ToString()));

            //debug
            string DLLName = "AgentDLL";
            

            Classes = GetAllTypesFromDLLstring(DLLName);
            
            SelectedClasses = Classes;

            //remove the last useless item
            //Classes.RemoveAt( Classes.Count - 1);

            int classesIndex=0;
            foreach(var ii in Classes)
            {
                Console.WriteLine("----Classes: " + ii.ToString());
                

                //Assembly assemblies = Assembly.LoadFrom(DLLName);

                //don't need to load the last class 
                /*if (classesIndex == Classes.Count - 1) 
                {
                    break;
                }*/

                //object obj = assemblies.CreateInstance(Classes[classesIndex].ToString(), true);

                ArrayList Methods = GetAllTypesFromClass(DLLName, ii.ToString());

                
                
                //print methods
                foreach (var jj in Methods)
                {
                    Console.WriteLine("----Methods: " + jj.ToString());

                }


                classesIndex++;
            }


            //debug
            //load ConfigStrings
            ConfigStrings = new List<ConfigStrings>();

            for (int ii = 0; ii < Classes.Count; ii++)
            {
                ConfigStrings configStr = (ConfigStrings)RunClass(
                    DLLName, Classes[ii].ToString(), "SetDefaultConfigStrings");

                ConfigStrings.Add(configStr);

                
            }

            for (int ii = 0; ii < ConfigStrings.Count; ii++)
            {
                Console.WriteLine("Index[" + ii + "] : " + ConfigStrings[ii].ClassFullName);
            }
            

        }

        #region Dynamic Loading DLL Controller


        /**
         * Return all types loaded from desired DLL
         * 
         * @param   dllName The DLL in which to parse and get the types from
         * @return  A filled ArrayList object containing all types
         */
        public ArrayList GetAllTypesFromDLLstring(string dllName)
        {
            
            try
            {
                Assemblies = Assembly.Load(dllName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nError - couldn't obtain assemblies from " + dllName);
                Console.WriteLine("EXCEPTION OUTPUT\n" + ex.Message + "\n" + ex.InnerException);
                ArrayList _Quit = new ArrayList(1);
                _Quit.Add("QUIT");
                return _Quit;
            }

            AllTypes = Assemblies.GetTypes();

            ArrayList _Temp = new ArrayList();

            foreach (Type t in AllTypes)
            {
                _Temp.Add(t.ToString());
            }

            return _Temp;
        }


        /**
         * Returns all method names from desired DLL/Class
         * 
         * @param dllName   The DLL in which to parse for desired class
         * @param className The class in which to parse for all methods
         * 
         * @return  An ArrayList of each method from desired class
         */
        public ArrayList GetAllTypesFromClass(string dllName, string className)
        {
            //Assembly _Assemblies = Assembly.Load(dllName);

            Type _Type = Assemblies.GetType(className);

            ArrayList _Temp = new ArrayList();

            try
            {
                MethodInfo[] _Methods = _Type.GetMethods();

                foreach (MethodInfo meth in _Methods)
                {
                    _Temp.Add(meth.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nError - couldn't obtain methods from " + dllName);
                Console.WriteLine("EXCEPTION OUTPUT\n" + ex.Message + "\n" + ex.InnerException);
                _Temp.Clear();
                _Temp.Capacity = 1;
                _Temp.Add("QUIT");
            }

            return _Temp;
        }

        /**
         * Runs target Method from target Class from target DLL.
         * 
         * @param dllName The DLL to load and use parse for methods
         * @param className The class to load from specific DLL
         * @param methodName The method to call from class
         * 
         * @return object reference
         */
        public object RunClass(string dllName, string className, string methodName)
        {
            object ReturnObj;
            // Create the assemblies from our current DLL.
            //Assembly _Assemblies = Assembly.Load(dllName);

            // Get the type that we want from the assemblies.
            //  IE: This would be the fully qualified class name (including namespace)
            //  Example: "Reflectionism.Examples.Example1" or "Reflectionism.Examples.Example2"
            Type _Type = null;
            try
            {
                _Type = Assemblies.GetType(className);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nError - couldn't obtain classrd from " + className);
                Console.WriteLine("EXCEPTION OUTPUT\n" + ex.Message + "\n" + ex.InnerException);
                return null;
            }

            // Get the desired method we want from the target type.
            MethodInfo _MethodInfo = null;
            try
            {
                _MethodInfo = _Type.GetMethod(methodName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nError - couldn't obtain method " +
                    methodName + " from " + className);
                Console.WriteLine("EXCEPTION OUTPUT\n" + ex.Message + "\n" + ex.InnerException);
                return null;
            }

            // The first parameter to pass into the Invoke Method coming up.
            Object _InvokeParam1 = Activator.CreateInstance(_Type);

            // This calls the target method ("DisplayMyself").
            //  NOTE: I'm not passing any arguments down to the method being invoked.
            //  Therefore, I'm passing null as my argument, otherwise Invoke takes an
            //  array of Objects.
            ReturnObj = _MethodInfo.Invoke(_InvokeParam1, null);

            return ReturnObj;
        }


        /**
         * Create target Agent from target Class from target DLL.
         * Call the constructor of target Agent class.
         * 
         * @param className The class to load from specific DLL
         * @param args The arguments to create instance
         * 
         * @return created object reference
         */
        public object CreateDLLInstance(string className, params object[] args)
        {
           
            // Create the assemblies from our current DLL.
            //Assembly _Assemblies = Assembly.Load(dllName);

            // Get the type that we want from the assemblies.
            //  IE: This would be the fully qualified class name (including namespace)
            //  Example: "Reflectionism.Examples.Example1" or "Reflectionism.Examples.Example2"
            Type _Type = null;
            try
            {
                _Type = Assemblies.GetType(className);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nError - couldn't obtain classrd from " + className);
                Console.WriteLine("EXCEPTION OUTPUT\n" + ex.Message + "\n" + ex.InnerException);
                return null;
            }


            //debug: show constructor parameters
            var ctors = _Type.GetConstructors();
            // Assuming class ObjectType has only one constructor:
            var ctor = ctors[1];
            Console.WriteLine("debug: PrintParameters:");
            foreach (var param in ctor.GetParameters())
            {
                Console.WriteLine(string.Format("Parameter {0} is named {1} and is of type {2}", param.Position, param.Name, param.ParameterType));
            }


            // The first parameter to pass into the Invoke Method coming up.
            // args means arguments array
            Object _ObjectRef = (Object)Activator.CreateInstance(_Type, args);

            // This calls the target method ("DisplayMyself").
            //  NOTE: I'm not passing any arguments down to the method being invoked.
            //  Therefore, I'm passing null as my argument, otherwise Invoke takes an
            //  array of Objects.
            //ReturnObj = _MethodInfo.Invoke(_InvokeParam1, null);

            return _ObjectRef;
        }


        #endregion


        #region GMap Markers Controll


        /**
         * Enable all the marker's animation
           Turn on "IsAnimated = true"    
         */
        public void EnableMarkerAnimation()
        {
            for (int ii = 0; ii < God.AgentNumber; ii++)
            {
                if (God.WorldAgentList[ii] == null)
                    continue;

                God.WorldAgentList[ii].Marker.IsAnimated = true;

            }


            return;
        }


        /**
         * disable all the marker's animation
         * turn off "IsAnimated"  
         */
        public void DisableMarkerAnimation()
        {
            for (int ii = 0; ii < God.AgentNumber; ii++)
            {
                if (God.WorldAgentList[ii] == null)
                    continue;

                God.WorldAgentList[ii].Marker.IsAnimated = false;

            }


            return;
        }


        /**
         * This method cancels all markers' selection. 
         * Use for refreshing gMapExplorer's agents and joined agents
         */
        public void DeselectMarkers()
        {
            // for all agents
            for (int ii = 0; ii <= God.AgentNumber; ii++)
            {
                Agent currentAgent = God.WorldAgentList[ii];

                if (currentAgent == null)
                    continue;

                // cancel selection
                currentAgent.Marker.IsSelected = false;

            }

        }
        #endregion 

        #region STP


        /**
         * create SimpleThreadPool with parameters in GUI
         * 
         * @param ThreadsNum   number of threads
         * @param IdleTimeout  idle time for timeout
         * @param ExecuteTime  execution time for testing workitems
         */
        public void StartThreadPool(int ThreadsNum, int IdleTimeout, int ExecuteTime)
        {


            STP = new SimpleThreadPool(
                this,
                ThreadsNum,
                System.Threading.ThreadPriority.Normal,
                IdleTimeout,
                ExecuteTime
                );

        }

        #endregion

    }
}
