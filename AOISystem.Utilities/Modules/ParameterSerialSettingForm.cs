using AOISystem.Utilities.Forms;
using AOISystem.Utilities.Modules.Ports;
using AOISystem.Utilities.Resources;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules
{
    public partial class ParameterSerialSettingForm : Form
    {
        private SerialPort comport = new SerialPort();
        private ParameterSerial serialPara;
        private bool isInitFinished;

        private ParameterSerialSettingForm()
        {
            InitializeComponent();
        }

        internal ParameterSerialSettingForm(ParameterSerial serialPara, string deviceName)
        {
            InitializeComponent();
            this.serialPara = serialPara;
            this.Text = deviceName;
        }

        private void ParameterSerialSettingForm_Load(object sender , EventArgs e)
        {
            InitializeControlValues();
            isInitFinished = true;
        }

        private void InitializeControlValues()
        {
            refreshComPortList();
            cmbParity.Items.Clear();
            cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear();
            cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cmbParity.Text = serialPara.Parity.ToString();
            cmbStopBits.Text = serialPara.StopBits.ToString();
            cmbDataBits.Text = serialPara.DataBits.ToString();
            cmbBaudRate.Text = serialPara.BaudRate.ToString();
            cmbPortName.Text = serialPara.PortNumber.ToString();
            ntxbReadTimeout.Text = serialPara.ReadTimeout.ToString();
            ntxbWriteTimeout.Text = serialPara.WriteTimeout.ToString();
            ntxbReadDelayTime.Text = serialPara.ReadDelayTime.ToString();
            ntxbWriteIntervalTime.Text = serialPara.WriteIntervalTime.ToString();
            //set tag
            cmbPortName.Tag = "PortNumber";
            cmbBaudRate.Tag = "BaudRate";
            cmbParity.Tag = "Parity";
            cmbDataBits.Tag = "DataBits";
            cmbStopBits.Tag = "StopBits";
            ntxbReadTimeout.Tag = "ReadTimeout";
            ntxbWriteTimeout.Tag = "WriteTimeout";
            ntxbReadDelayTime.Tag = "ReadDelayTime";
            ntxbWriteIntervalTime.Tag = "WriteIntervalTime";
        }

        private void refreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = refreshComPortList(cmbPortName.Items.Cast<string>() , cmbPortName.SelectedItem as string , comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(orderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private string refreshComPortList(IEnumerable<string> PreviousPortNames , string CurrentSelection , bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = orderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection))
                        selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest))
                        selected = newest;
                    else
                        selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest))
                        selected = newest;
                    else if (ports.Contains(CurrentSelection))
                        selected = CurrentSelection;
                    else
                        selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        private string[] orderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3) , out num) ? num : 0).ToArray();
        }

        private void FrmParameterSerialSetting_FormClosing(object sender , FormClosingEventArgs e)
        {
            if (MessageBox.Show(ResourceHelper.Language.GetString("ApplicationFinish"), ResourceHelper.Language.GetString("SystemHint"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Console.WriteLine("FrmPropertyGrid says : 888888888888");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cmbPortName_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                if (((ComboBox)sender).Name == "cmbDataBits")
                {
                    serialPara.DataBits = Convert.ToInt32(((ComboBox)sender).Text);
                }

                if (((ComboBox)sender).Name == "cmbBaudRate")
                {
                    serialPara.BaudRate = Convert.ToInt32(((ComboBox)sender).Text);
                }
                else
                {
                    PropertyInfo[] pi = serialPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (PropertyInfo p in pi)
                    {
                        if (((ComboBox)sender).Tag.ToString() == p.Name)
                        {
                            UInt16 val = Convert.ToUInt16(((ComboBox)sender).SelectedIndex);
                            if (p.PropertyType.BaseType.Name == "Enum")
                                p.SetValue(serialPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                            if (p.PropertyType.BaseType.Name == "ValueType")
                                p.SetValue(serialPara , Convert.ChangeType(val , p.PropertyType) , null);
                            if (p.PropertyType.BaseType.Name == "Object")
                                p.SetValue(serialPara , ((ComboBox)sender).Text , null);
                        }
                    }
                }
            }
        }

        private void ntxbTimeout_TextChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = serialPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((NumTextBox)sender).Tag.ToString() == p.Name)
                    {
                        string val;
                        if (((NumTextBox)sender).Text.Length > 0 && ((NumTextBox)sender).Text != "-")
                        {
                            val = ((NumTextBox)sender).Text;
                        }
                        else
                        {
                            val = "0";
                        }
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(serialPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }
    }
}