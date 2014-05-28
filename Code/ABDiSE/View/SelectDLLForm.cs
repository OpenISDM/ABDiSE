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
    public partial class SelectDLLForm : Form
    {

        CoreController CoreController;        

        public SelectDLLForm(CoreController controller)
        {
            InitializeComponent();

            //
            // set pointer to core controller
            //
            this.CoreController = controller;

        }


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

        private void button_SelectAll_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < checkedListBox_AgentDLL.Items.Count; ii++)
            {
                checkedListBox_AgentDLL.SetItemChecked(ii, true);
            }
        }

        private void button_DeselectAll_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < checkedListBox_AgentDLL.Items.Count; ii++ )
            {
                checkedListBox_AgentDLL.SetItemChecked(ii, false);
            }
        }

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
