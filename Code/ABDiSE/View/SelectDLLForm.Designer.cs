namespace ABDiSE.View
{
    partial class SelectDLLForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox_AgentDLL = new System.Windows.Forms.CheckedListBox();
            this.button_SelectAll = new System.Windows.Forms.Button();
            this.button_DeselectAll = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox_AgentDLL
            // 
            this.checkedListBox_AgentDLL.FormattingEnabled = true;
            this.checkedListBox_AgentDLL.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox_AgentDLL.Name = "checkedListBox_AgentDLL";
            this.checkedListBox_AgentDLL.Size = new System.Drawing.Size(259, 140);
            this.checkedListBox_AgentDLL.TabIndex = 0;
            // 
            // button_SelectAll
            // 
            this.button_SelectAll.Location = new System.Drawing.Point(13, 158);
            this.button_SelectAll.Name = "button_SelectAll";
            this.button_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.button_SelectAll.TabIndex = 1;
            this.button_SelectAll.Text = "Select All";
            this.button_SelectAll.UseVisualStyleBackColor = true;
            this.button_SelectAll.Click += new System.EventHandler(this.button_SelectAll_Click);
            // 
            // button_DeselectAll
            // 
            this.button_DeselectAll.Location = new System.Drawing.Point(94, 158);
            this.button_DeselectAll.Name = "button_DeselectAll";
            this.button_DeselectAll.Size = new System.Drawing.Size(75, 23);
            this.button_DeselectAll.TabIndex = 2;
            this.button_DeselectAll.Text = "Deselect All";
            this.button_DeselectAll.UseVisualStyleBackColor = true;
            this.button_DeselectAll.Click += new System.EventHandler(this.button_DeselectAll_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(175, 158);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(96, 50);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save and Exit";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // SelectDLLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 220);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_DeselectAll);
            this.Controls.Add(this.button_SelectAll);
            this.Controls.Add(this.checkedListBox_AgentDLL);
            this.Name = "SelectDLLForm";
            this.Text = "Select DLL";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_AgentDLL;
        private System.Windows.Forms.Button button_SelectAll;
        private System.Windows.Forms.Button button_DeselectAll;
        private System.Windows.Forms.Button button_Save;
    }
}