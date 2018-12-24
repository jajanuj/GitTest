using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Component;

namespace AOISystem.Utilities.Flow
{
    public partial class FlowMonitorForm : Form
    {
        private FlowBase inputObject;
        private int userPermissions;
        private List<string> hiddenProperties;

        public FlowMonitorForm(FlowBase obj , string name)
        {
            InitializeComponent();
            inputObject = obj;
            hiddenProperties = new List<string>();
            this.Name = name;
        }

        public FlowMonitorForm(FlowBase obj, string name, bool hideControlButton)
            : this(obj, name)
        {
            if (hideControlButton)
            {
                btnStart.Visible = false;
                btnPause.Visible = false;
                btnStop.Visible = false;
            }
            else
            {
                btnStart.Visible = true;
                btnPause.Visible = true;
                btnStop.Visible = true;
            }
        }

        public FlowMonitorForm(FlowBase obj, string name, int userPermissions, bool hideControlButton)
            : this(obj, name, hideControlButton)
        {
            this.userPermissions = userPermissions;
        }

        public FlowMonitorForm(FlowBase obj, string name, Size dialogSize, bool hideControlButton)
            : this(obj, name, hideControlButton)
        {
            this.Size = dialogSize;
        }

        public FlowMonitorForm(FlowBase obj, string name, int userPermissions, Size dialogSize, bool hideControlButton)
            : this(obj, name, hideControlButton)
        {
            this.userPermissions = userPermissions;
            this.Size = dialogSize;
        }

        public FlowMonitorForm(FlowBase obj, string name, int userPermissions)
            : this(obj, name)
        {
            this.userPermissions = userPermissions;
        }

        public FlowMonitorForm(FlowBase obj, string name, Size dialogSize)
            : this(obj, name)
        {
            this.Size = dialogSize;
        }

        public FlowMonitorForm(FlowBase obj, string name, int userPermissions, Size dialogSize)
            : this(obj, name)
        {
            this.userPermissions = userPermissions;
            this.Size = dialogSize;
        }

        private void appendHiddenProperties()
        {
            if (inputObject != null)
            {
                PropertyInfo[] piObj = inputObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var eachItem in piObj)
                {
                    PermissionsAttribute attribute =
                    (PermissionsAttribute)Attribute.GetCustomAttribute(eachItem , typeof(PermissionsAttribute));
                    if (attribute != null)
                    {
                        if (userPermissions < attribute.Value)
                        {
                            hiddenProperties.Add(eachItem.Name);
                        }
                    }
                }

                this.filteredPropertyGrid.SelectedObject = inputObject;
                this.filteredPropertyGrid.HiddenProperties = hiddenProperties.ToArray();
                this.filteredPropertyGrid.Refresh();
            }
            else
            {
                ExceptionHelper.CommonMessageShow("ErrorOccurInstanceMissing", "SystemHint");
            }
        }

        private void FlowMonitorForm_Load(object sender , EventArgs e)
        {
            appendHiddenProperties();
            timer.Enabled = true;
        }

        private void timer_Tick(object sender , EventArgs e)
        {
            filteredPropertyGrid.Refresh();
            if (inputObject != null)
            {
                if (inputObject.IsRunning)
                {
                    this.Text = "Flow Monitor [Running] - " + this.Name;
                }
                else
                {
                    this.Text = "Flow Monitor [Stopped] - " + this.Name;
                }
            }
        }

        private void btnStart_Click(object sender , EventArgs e)
        {
            inputObject.Start("Start by flow monitor");
        }

        private void btnPause_Click(object sender , EventArgs e)
        {
            inputObject.Pause("Pause by flow monitor");
        }

        private void btnStop_Click(object sender , EventArgs e)
        {
            inputObject.Stop("Stop by flow monitor");
        }

        private void btnClose_Click(object sender , EventArgs e)
        {
            this.Close();
        }
    }
}