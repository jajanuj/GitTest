using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

namespace AOISystem.Utilities.Threading
{
    public partial class frmTaskMonitor : Form
    {
        private volatile static frmTaskMonitor _instance = null;
        private static readonly object lockHelper = new object();

        private SynchronizationContext _context;

        private BindingList<Task> _taskBindingList;

        private bool _isFormClose = false;
        private bool _isFormOpened = false;

        public frmTaskMonitor()
        {
            InitializeComponent();

            _context = SynchronizationContext.Current;
            _taskBindingList = new BindingList<Task>(new List<Task>());
            this.dgvTaskMonitor.DataSource = _taskBindingList;
        }

        public static frmTaskMonitor CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new frmTaskMonitor();
                }
            }
            return _instance;
        }

        private void frmTaskMonitor_Activated(object sender, EventArgs e)
        {
            _isFormOpened = true;
        }

        private void frmTaskMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isFormOpened = false;
            if (!_isFormClose)
            {
                e.Cancel = true;
                //MessageBox.Show("It can't support this operation.");
                this.Hide();
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            _isFormOpened = false;
            this.Hide();
        }

        public void CloseForm()
        {
            _isFormClose = true;
            this.Close();
        }

        public void AddTask(Task task)
        {
            _context.Send(new SendOrPostCallback((state) => 
            {
                lock (lockHelper)
                {
                    _taskBindingList.Add(task);
                }
            }), null);
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new Action(() => 
            //    {
            //        lock (lockHelper)
            //        {
            //            _taskBindingList.Add(task); 
            //        }
            //    }));
            //}
        }

        public void ClearIsCompleted()
        {
            lock (lockHelper)
            {
                var taskList = from task in _taskBindingList
                            where (!task.IsCompleted)
                            select task;
                _taskBindingList = new BindingList<Task>(taskList.ToList());
                this.dgvTaskMonitor.DataSource = _taskBindingList;
            }
        }

        public void ClearIsCanceled()
        {
            lock (lockHelper)
            {
                var taskList = from task in _taskBindingList
                               where (!task.IsCanceled)
                               select task;
                _taskBindingList = new BindingList<Task>(taskList.ToList());
                this.dgvTaskMonitor.DataSource = _taskBindingList;
            }
        }

        public void ClearIsFaulted()
        {
            lock (lockHelper)
            {
                var taskList = from task in _taskBindingList
                               where (!task.IsFaulted)
                               select task;
                _taskBindingList = new BindingList<Task>(taskList.ToList());
                this.dgvTaskMonitor.DataSource = _taskBindingList;
            }
        }

        public void RefreshData()
        {
            this.dgvTaskMonitor.Invalidate();
        }

        private void btnClearIsCompleted_Click(object sender, EventArgs e)
        {
            ClearIsCompleted();
        }

        private void btnClearIsCanceled_Click(object sender, EventArgs e)
        {
            ClearIsCanceled();
        }

        private void btnClearIsFaulted_Click(object sender, EventArgs e)
        {
            ClearIsFaulted();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
