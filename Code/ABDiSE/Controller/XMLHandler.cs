/** 
 *  @file XMLHandler.cs
 *  This file contains classes that control XML file save and load functions
 *  
 *  
 *  Copyright (c) 2014  OpenISDM
 *   
 *  Project Name: 
 * 
 *      ABDiSE 
 *          (Agent-Based Disaster Simulation Environment)
 *
 *  Authors:  
 *      Li Jiawei (Livic Lee), lijiaweicpp@gmail.com
 *      Tzu-Liang Hsu, Lightorz@gmail.com
 *
 *  License:
 *
 *      GPL 3.0 This file is subject to the terms and conditions defined 
 *      in file 'COPYING.txt', which is part of this source code package.
 *
 *  Major Revision History:
 *
 *      2013/11/02: alpha version by Livic
 *      2014/10/12: implemented in 2.0 by Lightorz
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ABDiSE.Model;
using ABDiSE.Model.AgentClasses;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace ABDiSE.Controller
{
    /**
     *  This class provides the save/load functions to XML files 
     */
    public class XMLHandler
    {
        private int tempCurrentStep = 0;
        private int tempAgentNum = 0;              
        private int tempAgentCount = 0;            

        private CoreController coreController;
        private God god;
        /**
         * record pointer to core controller
         */
        public XMLHandler(CoreController cc) 
        {
            this.coreController = cc;
            this.god = cc.God;
        }

        /**
         *  save to xml 
         */ 
        public void Save()
        {
            // init
            tempCurrentStep = god.CurrentStep;
            tempAgentNum = 0;              
            tempAgentCount = 0;

            //show dialog, wait for user input
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string directoryPath = dialog.SelectedPath;

            saveAgentList(god.WorldAgentList, directoryPath);
        }

        public void Load()
        {
            
            //show dialog, wait for user input
            
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string path = dialog.SelectedPath;
            
            loadAgentDirectory(god.WorldAgentList, path);

        }
        private void loadAgentDirectory(Agent[] agentList, string path)
        {
            //init agent list (clear)
            god.WorldAgentList = new Agent[god.MaximumAgents];

            string[] filenames = Directory.GetFiles(path);

            for (int i = 0; i < filenames.Length; i++)
            {
                loadSingleAgent(filenames[i]);
            }

            // GUI needs refresh
        }

        /**
         * serialize one agent to xml file
         */ 
        private void saveSingleAgent(Agent agent, string fileName)
        {
            
            var serializer = new DataContractSerializer(agent.GetType());

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true,
                NewLineChars = "\n"
            };

            Stream s = File.Open(fileName, FileMode.Create);
            using (var writer = XmlWriter.Create(Console.Out, settings))
            {
                serializer.WriteObject(s, agent);
            }
            
            s.Close();

        }

        /**
         *  Load one agent from xml file.
         */ 
        private Agent loadSingleAgent(string fileName)
        {   
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader = 
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());


            Type agentType = null;
            Object agent = null;
            DataContractSerializer ser;

            while (reader.Read())
            {
                //
                /// important: if the user adds agent types in dll, this order MUST be updated
                /// because this part can not dynamically get type name
                /// the user needs to check index of AllTypes[].
                //
                string[] types = {
                                 "Building",
                                 "BuildingJoinedFire",
                                 "Flood",
                                 "TreeJoinedFire",
                                 "Smoke",
                                 "Tree",
                                 "Fire",
                                 "BuildingJoinedFlood",
                                 "TreeJoinedFlood",
                                 "Tornado"
                                 
                                 };

                bool isFound = false;
                for(int ii=0; ii<types.Length; ii++){

                    if(reader.Name == types[ii]){
                        //search for index of AllTypes[index]
                        for (int jj = 0; jj < coreController.ConfigStrings.Count; jj++)
                        {
                            //found
                            if (types[ii] == coreController.ConfigStrings[jj].ClassShortName)
                            {
                                isFound = true;
                                agentType = coreController.AllTypes[jj]; // building
                                ser = new DataContractSerializer(agentType);
                                agent = ser.ReadObject(reader);
                                break;
                            }

                        }

                    }

                    if (isFound)
                        break;
                }

                #region old XML load
                /*
                switch (reader.Name)
                {

                    case "Building":
                        agentType = coreController.AllTypes[0]; // building
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "BuildingJoinedFire":
                        agentType = coreController.AllTypes[1]; // building@fire
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;        
                    case "Flood":
                        agentType = coreController.AllTypes[2]; // flood
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "TreeJoinedFire":
                        agentType = coreController.AllTypes[3]; // tree@fire
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "Smoke":
                        agentType = coreController.AllTypes[4]; // smoke
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "Tree":
                        agentType = coreController.AllTypes[5]; // tree
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "Fire":
                        agentType = coreController.AllTypes[6]; // fire
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "BuildingJoinedFlood":
                        agentType = coreController.AllTypes[7]; // building@flood
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;            
                    case "TreeJoinedFlood":
                        agentType = coreController.AllTypes[8]; // tree@flood
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                    case "Tornado":
                        agentType = coreController.AllTypes[9]; // tornado
                        ser = new DataContractSerializer(agentType);
                        agent = ser.ReadObject(reader);
                        break;
                }
                */
                #endregion 
            }
            
            
            fs.Close();
            reader.Close();
            
            Agent loadedAgent = (Agent)agent;

            // recover data of loadd agent
            loadedAgent.RecoverAgent(coreController);

            return loadedAgent;

        }

        /**
         *  save all agents in agent list into one xml file
         */ 
        private void saveAgentList(Agent[] agentList, string path)
        {
            int temp = 0;
            string agentFileName = "";
            for (int i = 0; i < coreController.God.MaximumAgents; i++)
            {
                if (agentList[i] != null)
                {
                    temp++;
                    agentFileName = path + "/agent" + temp + ".xml";
                    saveSingleAgent(agentList[i], agentFileName);
                    tempAgentNum++;
                    tempAgentCount++;
                }
            }
        }

    }

   
}
