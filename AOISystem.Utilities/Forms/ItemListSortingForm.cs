using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class ItemListSortingForm : Form
    {
        public ItemListSortingForm(string[] itemList)
        {
            InitializeComponent();

            this.SortedItemList = new string[itemList.Length];
            for (int i = 0; i < itemList.Length; i++)
            {
                this.dataGridView.Rows.Add(itemList[i]);
                SortedItemList[i] = itemList[i];
            }
        }

        public string[] SortedItemList { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> itemList = new List<string>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                object value = this.dataGridView.Rows[i].Cells[0].Value;
                if (value != null)
                {
                    itemList.Add(value.ToString());   
                }
            }
            this.SortedItemList = itemList.ToArray();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow item = this.dataGridView.SelectedRows[0];
                object value = item.Cells[0].Value;
                int itemIndex = this.dataGridView.Rows.IndexOf(item);
                if (value != null && itemIndex > 0)
                {
                    this.dataGridView.Rows.RemoveAt(itemIndex);
                    this.dataGridView.Rows.Insert(itemIndex - 1, item);
                    this.dataGridView.CurrentCell = this.dataGridView.Rows[itemIndex - 1].Cells[0];
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow item = this.dataGridView.SelectedRows[0];
                object value = item.Cells[0].Value;
                int itemIndex = this.dataGridView.Rows.IndexOf(item);
                if (value != null && itemIndex < this.dataGridView.Rows.Count - 2)
                {
                    this.dataGridView.Rows.RemoveAt(itemIndex);
                    this.dataGridView.Rows.Insert(itemIndex + 1, item);
                    this.dataGridView.CurrentCell = this.dataGridView.Rows[itemIndex + 1].Cells[0];
                }
            }
        }
    }
}
