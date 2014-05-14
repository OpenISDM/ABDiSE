/*
    Project Name: ABDiSE ThreadpoolGUI
                  (Agent-Based Disaster Simulation Environment)
 
    Version:      pre-alpha
    
    File Name:    SimpleThreadPool.cs

    SVN $Revision: $

    Abstract:     ABDiSE thread pool
                  controls threads and workitems
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
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
    /*  
    * public class SimpleThreadPool : IDisposable
    * 
    * Description:
    *      simple custom thread pool
    *      queue workitem and execute
    *      
    */
    public class SimpleThreadPool : IDisposable
    {
        //pointer
        //CoreController CoreController;

        //c# windows applicaiton

        public bool DynamicMode = true;

        //time of execute (random)
        public static int ExecuteTime;

        DateTime timerStart, timerEnd;

        //public int computedWorkItem;

        /*  
        * public static void ShowMessage(Object stateInfo)
        * 
        * Description:
        *      show message of workitem
        *      
        * Arguments:     
        *      stateInfo - thread object
        * Return Value:
        *      void
        */
        public static void ShowMessage(Object stateInfo)
        {
            //Thread.Sleep(new Random().Next(ExecuteTime));
            //Thread.Sleep(ExecuteTime);

            //fake computation
            int computation = 0;
            int ExecuteCount = new Random(Guid.NewGuid().GetHashCode()).Next(ExecuteTime);

            Console.WriteLine(
                "[" + (String)stateInfo + "] : by  " + Thread.CurrentThread.Name
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
                " [" + (String)stateInfo  + "] ends : by\t " +
                Thread.CurrentThread.Name
                );

        }

        // list of threads
        private List<Thread> workerThreads = new List<Thread>();
   
        // flags: stop and cancel 
        private bool stopFlag = false;
        private bool cancelFlag = false;
   
        // time out setting
        private TimeSpan maxWorkerThreadTimeout;

        // max thread number in dynamic mode
        private int maxWorkerThreadCount = 0;

        // priority
        private ThreadPriority workerThreadPriority = 
            ThreadPriority.Normal;
        
        // queue of workitems
        private Queue<WorkItem> workitems = new Queue<WorkItem>();
        
        // count of workitem queue
        public int WorkitemNumber = 0;

        // manual reset event
        private ManualResetEvent enqueueNotify = 
            new ManualResetEvent(false);

        /*  
        * public SimpleThreadPool(
            int threads, 
            ThreadPriority priority, 
            int idleTimeout,
            int executeTime
            )
        * 
        * Description:
        *      constructor of thread pool
        *      do the initializtion and create worker threads
        *      
        * Arguments:     
        *      threads - thread number
        *      priority - thread priority
        *      idleTimeout - thread idle time 
        *      executeTime - random range of execute time
        * Return Value:
        *      void
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

        /*  
        * private void CreateWorkerThread()
        * 
        * Description:
        *      create worker thread
        *      assign thread priority
        * Arguments:     
        *      void
        * Return Value:
        *      void
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

        private void CreateWorkerThread()
        {
            this.CreateWorkerThread(-1);
        
        }

        /*  
        * public bool QueueUserWorkItem(WaitCallback callback)
        * 
        * Description:
        *      queue workitem
        *      
        * Arguments:     
        *      callback - WaitCallback
        * Return Value:
        *      boolean
        */
        public bool QueueUserWorkItem(WaitCallback callback)
        {
            return this.QueueUserWorkItem(callback, null);
        }

        /*  
        * public bool QueueUserWorkItem(WaitCallback callback, object state)
        * 
        * Description:
        *      queue workitem
        *      
        * Arguments:     
        *      callback - Wait Callback
        *      state - object state      
        * Return Value:
        *      boolean
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

        /*  
        * public void EndPool()
        * 
        * Description:
        *      call EndPool (false)
        *      
        * Arguments:     
        *      void      
        * Return Value:
        *      void
        */
        public void EndPool()
        {
            this.EndPool(false);
        }

        /*  
        * public void CancelPool()
        * 
        * Description:
        *      call EndPool (true)
        *      
        * Arguments:     
        *      void      
        * Return Value:
        *      void
        */
        public void CancelPool()
        {
            this.EndPool(true);
        }

        /*  
        * public void EndPool(bool cancelQueueItem)
        * 
        * Description:
        *      End thread pool
        *      join threads
        *      handle cancel or stop situations
        *      
        * Arguments:     
        *      cancelQueueItem - cancel or end flag      
        * Return Value:
        *      void
        */
        public void EndPool(bool cancelQueueItem)
        {
            Console.WriteLine("[STP]...EndPool:" + cancelQueueItem);

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

        /*  
        * private void DoWorkerThread()
        * 
        * Description:
        *      execute workitems
        *      
        * Arguments:     
        *      void      
        * Return Value:
        *      void
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

        /*  
        * private class WorkItem
        * 
        * Description:
        *      define workitem and callback
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

        /*  
        * public void Dispose()
        * 
        * Description:
        *      dispose and call Endpool
        *      
        * Arguments:     
        *      void      
        * Return Value:
        *      void
        */
        public void Dispose()
        {
            Console.WriteLine("[STP] public void Dispose()");
            this.EndPool(false);
        }
    }
}
