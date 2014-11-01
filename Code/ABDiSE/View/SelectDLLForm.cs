/** 
 *  @file SelectDLLForm.cs
 *  SelectDLLForm is a sub-windows of ABDiSE GUI, for selecting DLL functions (agent types)
 *  
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
 *      SelectDLLForm.cs
 *
 *  Abstract:
 *
 *      SelectDLLForm is a sub-windows of ABDiSE GUI, for selecting DLL functions (agent types)
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
 *      2014/8/28: edit comments
 *
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABDiSE.Controller;

namespace ABDiSE.View
{
    /**
     *  SelectDLLForm is a sub-windows of ABDiSE GUI, for selecting DLL functions (agent types)
     */
    public partial class SelectDLLForm : Form
    {
        //
        /// pointer to CoreController
        //
        CoreController CoreController;        


        /**
         *  Constructor of this Form
         *  
         *  @param controller reference of CoreController
         */ 
        public SelectDLLForm(CoreController controller)
        {
            InitializeComponent();

            //
            // set pointer to core controller
            //
            this.CoreController = controller;

        }

        /**
         *  Load agent classes (types) into data structure of GUI
         *  
         *  @param classes agent classes in ArrayList form
         */ 
        public void LoadDLLClasses(ArrayList classes)
        {
            
            checkedListBox_AgentDLL.CheckOnClick = true;

            //dynamically add agent types in .dll
            for (int ii = 0; ii < classes.Count; ii++)
            {
                string agentType = classes[ii].ToString();

                checkedListBox_AgentDLL.Items.Add(agentType);

            }
        }

        /**
         *  Click event for "select all" function
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the DoWorkEventArgs of the object, contains the event data. 
         */
        private void button_SelectAll_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < checkedListBox_AgentDLL.Items.Count; ii++)
            {
                checkedListBox_AgentDLL.SetItemChecked(ii, true);
            }
        }

        /**
         *  Click event for "deselect all" function
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the DoWorkEventArgs of the object, contains the event data. 
         */
        private void button_DeselectAll_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < checkedListBox_AgentDLL.Items.Count; ii++ )
            {
                checkedListBox_AgentDLL.SetItemChecked(ii, false);
            }
        }

        /**
         *  Click event for "save" function,  save selected agent classes (types)
         *  
         *  @param sender - refers to the object that invoked the event
         *  @param e - the DoWorkEventArgs of the object, contains the event data. 
         */
        private void button_Save_Click(object sender, EventArgs e)
        {
            CoreController.SelectedClasses = new ArrayList();

            for (int ii = 0; ii < checkedListBox_AgentDLL.Items.Count; ii++ )
            {
                if(checkedListBox_AgentDLL.GetItemChecked(ii)){
                    CoreController.SelectedClasses.Add(checkedListBox_AgentDLL.Items[ii].ToString());
                    
                }
            }

            //
            // debug printf
            //
            for (int jj = 0; jj < CoreController.SelectedClasses.Count; jj++)
            {
                Console.WriteLine("new selected DLL : " + CoreController.SelectedClasses[jj].ToString());
            }


            this.Close();
        }

    }
}
