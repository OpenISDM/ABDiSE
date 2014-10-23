/** 
 *  @file Program.cs
 *  Entrance of ABDiSE program.
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
 *      Entrance of ABDiSE program.
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
 *      2014/7/2: edit comments for doxygen
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABDiSE.View;
using ABDiSE.Controller;

namespace ABDiSE
{
    static class Program
    {

        /**
         * Entrance of ABDiSE program.
         */
        [STAThread]
        static void Main()
        {

            CoreController CoreController = new CoreController();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(CoreController));
        }
    }
}
