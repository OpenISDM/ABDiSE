/** 
 *  @file SimpleThreadPool.cs
 *  Custom thread pool in ABDiSE project, controls threads and workitems
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
 *  Abstract:
 *
 *      Custom thread pool in ABDiSE project, controls threads and workitems
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
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

using ABDiSE.Controller;

namespace ABDiSE.Controller.ThreadPool
{
    /**  
     *  Simple custom thread pool.
     *  queue workitems and execute them.
     *      
     */
    public class SimpleThreadPool : IDisposable
    {

        public bool DynamicMode = true;

        //
        ///time of execute (random)
        //
        public static int ExecuteTime;

        DateTime timerStart, timerEnd;


        /**
         * Show message of workitems,  does some fake computations
         * 
         * @param stateInfo - thread object
         * 
         */
        public static void ShowTestMessage(Object stateInfo)
        {
            //Thread.Sleep(new Random().Next(ExecuteTime));
            //Thread.Sleep(ExecuteTime);

            //fake computation
            int computation = 0;
            int ExecuteCount = new Random(Guid.NewGuid().GetHashCode()).Next(ExecuteTime);

            Console.WriteLine(
                "TestMessage:[" + (String)stateInfo + "start] by " + Thread.CurrentThread.Name
                 +
                "\tExecuteCount: " + ExecuteCount
                );

            //int FireSimulatorValue = FireSimulator.MethodB(ExecuteCount);

            //Console.WriteLine("call DLL LZFireSimulator.MethodB(ExecuteCount): " + FireSimulatorValue);

            for (int i = 0; i < ExecuteCount; i++)
            {
                computation += new Random().Next(ExecuteTime);
            }

            Console.WriteLine(
                "ShowTestMessage:[" + (String)stateInfo + "end]"
                );

        }

        //
        ///list of threads
        //
        private List<Thread> workerThreads = new List<Thread>();
   
        private bool stopFlag = false;
        private bool cancelFlag = false;
   
        //
        /// time out setting
        //
        private TimeSpan maxWorkerThreadTimeout;

        //
        /// max thread number in dynamic mode
        //
        private int maxWorkerThreadCount = 0;

        private ThreadPriority workerThreadPriority = 
            ThreadPriority.Normal;
        
        //
        /// queue of workitems
        //
        private Queue<WorkItem> workitems = new Queue<WorkItem>(WorkitemQueueLength);

        public const int WorkitemQueueLength = 1000;

        //
        /// count of workitem queue
        //
        public int WorkitemNumber = 0;

        //
        // manual reset event
        //
        private ManualResetEvent enqueueNotify = 
            new ManualResetEvent(false);


        /**
         * Constructor of thread pool
         * do the initializtion and create worker threads
         * 
         * @param CoreController - reference to the core controller
         * @param threads - thread number
         * @param priority - thread priority
         * @param idleTimeout - thread idle time 
         * @param executeTime - random range of execute time
         * 
         */
        public SimpleThreadPool(
            CoreController CoreController,
            int threads, 
            ThreadPriority priority, 
            int idleTimeout,
            int executeTime
            )
        {
            Console.WriteLine("[STP] ...SimpleThreadPool Strating... ");

            //computedWorkItem = 0;
            //this.CoreController = CoreController;

            timerStart = DateTime.Now;

            
            /*
            //queue workitems
            //continuously queue workitems
            for (int count = 0; count < Workitems; count++)
            {
                ///threadPool.QueueUserWorkItem(
                //    new WaitCallback(SimpleThreadPool.ShowMessage),
                //    string.Format("WorkItem[{0}]", count));
                QueueUserWorkItem(
                    new WaitCallback(SimpleThreadPool.ShowMessage),
                    count.ToString());

                //delayed queue time
                //Thread.Sleep(ExecuteTime);
            }
            Debug.Print("all workitems already in queue");
            Console.WriteLine("SimpleThreadPool Started and all workitems are already in queue");
            */

            maxWorkerThreadTimeout =
            TimeSpan.FromMilliseconds(idleTimeout);

            this.maxWorkerThreadCount = threads;

            this.workerThreadPriority = priority;

            ExecuteTime = executeTime;

            

            //myMainForm.bw.ReportProgress(0);

            // create specific number of worker thread
            // static mode: create all workers in the beginning
            for (int i = 0; i < threads; i++)
                this.CreateWorkerThread(i);

        }


        /**
         * create worker thread and assign thread priority
         * 
         * @param myid - ID of the thread 
         */
        private void CreateWorkerThread(int myid)
        {
            //Console.WriteLine("CreateWorkerThread");
            Thread worker = new Thread
                (new ThreadStart(this.DoWorkerThread));

            worker.Name = "STPWorkerThread-" + myid.ToString();
            worker.Priority = this.workerThreadPriority;
            this.workerThreads.Add(worker);
            worker.Start();
            //Console.WriteLine(worker.Name + "called worker.start()");
        }

        /**
         * create worker thread
         * 
         */
        private void CreateWorkerThread()
        {
            this.CreateWorkerThread(-1);
        
        }


        /**
         * Queue user workitem in thread pool.
         * 
         * @param callback - WaitCallback
         * @return bool result
         * 
         */
        public bool QueueUserWorkItem(WaitCallback callback)
        {
            return this.QueueUserWorkItem(callback, null);
        }


        /**
         * Queue user workitem in thread pool.
         * 
         * @param callback - WaitCallback
         * @param state - object state  
         * @return bool result
         * 
         */
        public bool QueueUserWorkItem(WaitCallback callback, object state)
        {
            if (this.stopFlag == true) 
                return false;
   
            WorkItem wi = new WorkItem();
            wi.callback = callback;
            wi.state = state;
            wi.id = int.Parse(state.ToString());

            //dynamic create worker thread (less than maxWorkerThreadCount)
            /*
            if (DynamicMode)
            {
                if (this.workitems.Count > 0 &&
                    this.workerThreads.Count < this.maxWorkerThreadCount)
                    this.CreateWorkerThread();
            }*/

            this.workitems.Enqueue(wi);
            this.enqueueNotify.Set();

            //update
            WorkitemNumber = this.workitems.Count;

            return true;
        }


        /**
         * End the threadpool.
         * Calls EndPool(false)
         * 
         */
        public void EndPool()
        {
            this.EndPool(false);
        }


        /**
         *  Cancel the threadpool.
         *  Calls EndPool(true)
         */
        public void CancelPool()
        {
            this.EndPool(true);
        }

        /**
         * End the threadpool with parameter.
         * This method joins threads and handles cancel or stop situations
         * 
         * @param cancelQueueItem - cancel or end flag   
         */
        public void EndPool(bool cancelQueueItem)
        {
            Console.WriteLine("[STP]...EndPool cancelQueueItem: " + cancelQueueItem);

            if (this.workerThreads.Count == 0) 
                return;
   
            this.stopFlag = true;
            this.cancelFlag = cancelQueueItem;
            this.enqueueNotify.Set();
   
            do
            {
                //TODO: exception
                Thread worker = this.workerThreads[0];
                //Console.WriteLine("remove worker = ", worker.Name);
                if (worker == null)
                    continue;

                worker.Join();
                this.workerThreads.Remove(worker);

            } while (this.workerThreads.Count > 0);
            
            timerEnd = DateTime.Now;
            string time = 
                ((TimeSpan)(timerEnd - timerStart)).TotalMilliseconds.ToString();
            Console.WriteLine("[STP] EndPool finished. Time: " + time);
        }

        /**
         * Execute workitems
         */
        private void DoWorkerThread()
        {
            while (true)
            {
                //Debug.Print(Thread.CurrentThread.Name + 
                //    " enters DoWorkerThread() while");

                while (this.workitems.Count > 0)
                {
                    /*
                    //cancel testing
                    if ((myMainForm.bw.CancellationPending == true))
                    {
                        EndPool(true);
                        Debug.Print("CancellationPending start EndPool(true)");

                    }
                    */
                    WorkItem item = null;
                    lock (this.workitems)
                    {
                        //update
                        WorkitemNumber = this.workitems.Count;

                        if (this.workitems.Count > 0)
                        {
                            item = this.workitems.Dequeue();

                            

                            //Debug.Print("dequeue " + 
                            //    Thread.CurrentThread.Name + item.id);

                        }
                    }

                    if (item == null) 
                        continue;
   
                    try
                    {

                        

                        item.Execute();

                        //computedWorkItem++;


                        /*this.myMainForm.bw.ReportProgress
                            (100 * computedWorkItem / Workitems);*/
                    }
                    catch (Exception)
                    {
                        //
                        //  exception handler
                        //
                    }
   
                    if (this.cancelFlag == true) 
                        break;
                    
                    //context switch
                    //Thread.Sleep(1);

                }


                if (this.stopFlag == true || this.cancelFlag == true) 
                    break;
                
                //dynamic close the idle timeout thread
                if (DynamicMode)
                {
                    if (this.enqueueNotify.WaitOne
                        (this.maxWorkerThreadTimeout, true) == true)
                        continue;
                    else  
                        break;
                }
               
            }

            //exception
            //Console.WriteLine("remove thread" + Thread.CurrentThread.Name);
            //this.workerThreads.Remove(Thread.CurrentThread);
        }


        /**
         * WorkItem is a structure in threadpool.
         * 
         * This class defines workitem and its callback.
         * 
         */
        private class WorkItem
        {
            public WaitCallback callback;
            public object state;
            public int id;

            public void Execute()
            {
                this.callback(this.state);
            }
        }


        /**
         * Dispose the threadpool.
         * 
         * Call Endpool(false).
         */
        public void Dispose()
        {
            Console.WriteLine("[STP] public void Dispose()");
            this.EndPool(false);
        }
    }
}
