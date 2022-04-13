using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCF
{
    public partial class Form1 : Form
    {
        DataTable data = new DataTable();
        List<string> Doan = new List<string>(new string[] { "element1", "element2", "element3" });
        List<string> Douong = new List<string>(new string[] { "element3", "element4", "element5" });
        string tenban;
        public Form1()
        {
            InitializeComponent();
        }
        private void showban()
        {
            data.Columns.AddRange(new DataColumn[] {
                new DataColumn{ColumnName = "ID bàn" , DataType = typeof(int)},
                new DataColumn{ColumnName ="Tên món",DataType =typeof(string)},
                new DataColumn{ColumnName ="Số lượng",DataType=typeof(NumericUpDown)},
                new DataColumn{ColumnName ="Giá",DataType=typeof (int)},
            });

        }
        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tenban = button.Text;

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbtenmon.Items.Clear();
            if (cbbLoai.SelectedItem.ToString() == "Thức ăn")
            {
                cbbtenmon.Enabled = true;
                foreach (string i in Doan)
                {
                    cbbtenmon.Items.Add(i);
                }

            }
            else
            {
                cbbtenmon.Enabled = true;
                foreach (string i in Douong)
                {
                    cbbtenmon.Items.Add(i);
                }

            }
        }

        private void btthemmon_Click(object sender, EventArgs e)
        {
            string tenban = this.tenban;
            if (cbbLoai.SelectedIndex >= 0 && cbbtenmon.SelectedIndex >=0)
            {
                
            }
        }
    }
    
}
