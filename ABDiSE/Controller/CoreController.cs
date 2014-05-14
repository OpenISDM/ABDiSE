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
    public class CoreController
    {
        public God God;

        public SimpleThreadPool STP;

        public List<ConfigStrings> ConfigStrings;

        Assembly Assemblies = null;

        public ArrayList Classes;

        //constructor
        public CoreController()
        {
            this.God = new God();

            Console.WriteLine
                ("Welcome to ABDiSE 2.0 - Agent-Based Model Simulation World !");

            //create default Environment
            ABDiSE.Model.Environment HsinchuCity = 
                new ABDiSE.Model.Environment(
                    400, 
                    20.0, 
                    0.0, 
                    9.2, 
                    45.1, 
                    23.3, 
                    422554,
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
                    Console.WriteLine(jj + "----Methods: " + jj.ToString());

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
            

            //debug
            /*
            if(ConfigStrings.Count >0)
            {
                ConfigStrings[0].Print();
                ConfigStrings[1].Print();
            }*/

            // debug
            // create agent with args
            /*
            for (int ii = 0; ii < Classes.Count; ii++) 
            {
                Agent agent;
                Object[] args = new Object[] {};

                

                agent = CreateDLLInstance(Classes[ii].ToString(), );

                God.AddToAgentList(agent); 
            
            }*/

        }

        #region Dynamic Loading DLL Controller


        /// <summary>
        /// Return all types loaded from desired DLL
        /// </summary>
        /// <param name="dllName">The DLL in which to parse and get the types from</param>
        /// <returns>A filled ArrayList object containing all types </returns>
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

            Type[] _AllTypes = Assemblies.GetTypes();

            ArrayList _Temp = new ArrayList();

            foreach (Type t in _AllTypes)
            {
                _Temp.Add(t.ToString());
            }

            return _Temp;
        }

        /// <summary>
        /// Returns all method names from desired DLL/Class
        /// </summary>
        /// <param name="dllName">The DLL in which to parse for desired class</param>
        /// <param name="className">The class in which to parse for all methods</param>
        /// <returns>An ArrayList of each method from desired class</returns>
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

        /// <summary>
        /// Runs target Method from target Class from target DLL.
        /// </summary>
        /// <param name="dllName">The DLL to load and use parse for methods</param>
        /// <param name="className">The class to load from specific DLL</param>
        /// <param name="methodName">The method to call from class</param>
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

        /// <summary>
        /// Create target Agent from target Class from target DLL.
        /// Call the constructor of target Agent class
        /// </summary>
        /// <param name="className">The class to load from specific DLL</param>
        /// <param name="args">The arguments to create instance</param>
        public object CreateDLLInstance(
            string className, 
            CoreController coreContorller, 
            Dictionary<string, string> properties, 
            PointLatLng latLng, 
            ABDiSE.Model.Environment env
            )
        {


            // Get the type that we want from the assemblies.
            //  IE: This would be the fully qualified class name (including namespace)
            //  Example: 
            //  "Reflectionism.Examples.Example1" or "Reflectionism.Examples.Example2"
            Type _Type = null;
            try
            {
                _Type = Assemblies.GetType(className);
            }
            catch (Exception ex)
            {
                Console.WriteLine
                    ("\n\nError - couldn't obtain classrd from " + className);
                Console.WriteLine
                    ("EXCEPTION OUTPUT\n" + ex.Message + "\n" + 
                    ex.InnerException);

                return null;
            }


            Object args = new Object[] { coreContorller, properties, latLng, env };
            Object args2 = new Object[] { coreContorller, latLng };

            // The first parameter to pass into the Invoke Method coming up.
            // args means arguments array
            Object _ObjectRef = new Object();
            try
            {
                _ObjectRef = (Object)Activator.CreateInstance
                    (_Type, coreContorller, properties, latLng, env);
            }
            catch(MissingMemberException ex)
            {
                throw ex;

            }
            // This calls the target method ("DisplayMyself").
            //  NOTE: I'm not passing any arguments down to the method being invoked.
            //  Therefore, I'm passing null as my argument, otherwise Invoke takes an
            //  array of Objects.
            //ReturnObj = _MethodInfo.Invoke(_InvokeParam1, null);

            return _ObjectRef;
        }

        /// <summary>
        /// Create target Agent from target Class from target DLL.
        /// Call the constructor of target Agent class
        /// </summary>
        /// <param name="className">The class to load from specific DLL</param>
        /// <param name="args">The arguments to create instance</param>
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


        public MethodReturnResults Create(
            Dictionary<string, string> properties,
            PointLatLng latLng,
            ABDiSE.Model.Environment targetEnvironment
            ) 
        {

            return MethodReturnResults.FAILED;
        }

        #endregion

        #region GMap Markers Controll
        /* 
            * public void EnableMarkerAnimation()
            * 
            * Description:
            *      Enable all the marker's animation
            *      turn on "IsAnimated = true"      
            *      
            * Arguments:     
            *      void
            * Return Value:
            *      void
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

        /* 
        * public void DisableMarkerAnimation()
        * 
        * Description:
        *      Enable all the marker's animation
        *      turn on "IsAnimated = true"      
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
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

        /* 
         * public void DeselectMarkers()
         * 
         * Description:
         *      This method cancels selection of all markers. 
         *      use for refreshing gMapExplorer's agents and joined agents
         *      
         * Arguments:     
         *      void
         * Return Value:
         *      void
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

        /*  
        * public void StartThreadPool()
        *   
        * 
        * Description:
        *      create SimpleThreadPool     
        *      
        *      
        * Arguments:     
        *      void
        * Return Value:
        *      void
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
