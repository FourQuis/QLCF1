using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCF
{
    public partial class Form1 : Form
    {
        DataTable data =new DataTable();
        List<Item> items;
        string tenban;
        public Form1()
        {
            items = LayDanhSachMonTuFile();    
            InitializeComponent();
            numericUpDown1.Value = 1;
            foreach (string i in LayDaySachLoai())
            {
                cbbLoai.Items.Add(i);
            }
            data.Columns.AddRange(new DataColumn[] {
                new DataColumn{ColumnName = "Tên bàn" , DataType = typeof(string)},
                new DataColumn{ColumnName ="Tên món",DataType =typeof(string)},
                new DataColumn{ColumnName ="Số lượng",DataType=typeof(int)},
                new DataColumn{ColumnName ="Giá",DataType=typeof (int)},
            });
            LoadData();
        }
        public void LoadData()
        {
            string path = "data.txt";
            StreamReader sr = new StreamReader(path);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                string[] m = s.Split(',');

                data.Rows.Add(m[0], m[1], Convert.ToInt16(m[2]), Convert.ToInt16(m[3]));
            }
            sr.Close();
        }

        public List<Item> LayDanhSachMonTuFile()
        {
            List<Item> list = new List<Item>();
            string Path = "Danhsachmon.txt";
            StreamReader sr = File.OpenText(Path);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                string[] m = s.Split(',');
                list.Add(new Item(m[0], Convert.ToInt32(m[1]), m[2]));
            }
            sr.Close();
            return list;
        }

        public List<string> LayDanhSachTenMonTheoLoai(string Loai)
        {
            List<string> list = new List<string>();
            foreach (Item i in LayDanhSachMonTuFile())
            {
                if (i.loai == Loai)
                    list.Add(i.TenMon);
            }
            return list;
        }
        public List<string> LayDaySachLoai()
        {
            List<string> list = new List<string>();
            foreach (Item i in LayDanhSachMonTuFile())
            {
                if (!list.Contains(i.loai))
                    list.Add(i.loai);
            }
            return list;
        }
        public int LayGiaTheoTenMon(string tenmon)
        {
            foreach(Item i in items)
            {
                if(i.TenMon == tenmon)
                {
                    return i.DonGia;
                }    
            }
            return 0;
        }

        public void show()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(
              new DataColumn[]
              {
                    new DataColumn("Tên món", typeof(string)),
                    new DataColumn("Số lượng", typeof (int)),
                    new DataColumn("Giá", typeof(int))
              }
              );
            foreach (DataRow i in data.Rows)
            {
                if(i["Tên bàn"].ToString() == tenban)
                {
                    dt.Rows.Add(i["Tên món"], i["Số lượng"], LayGiaTheoTenMon(i["Tên món"].ToString()));
                }    
            }
            dataGridView1.DataSource = dt;
        }
        public void SaveData()
        {
            string Path = "data.txt";
            StreamWriter F = new StreamWriter(Path);
            foreach (DataRow i in data.Rows)
            {
                   F.WriteLine(i[0] + "," + i[1] + "," + i[2] + "," + i[3]);
            }
            F.Close();
               
        }











        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tenban = button.Text;
            lbTenBan.Text = button.Text.ToString();
            show();
        }

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                cbbtenmon.Items.Clear();
                cbbtenmon.SelectedItem = null;
                cbbtenmon.Text = "";
                cbbtenmon.Enabled = true;
                foreach (string i in LayDanhSachTenMonTheoLoai(cbbLoai.SelectedItem.ToString()))
                {
                    cbbtenmon.Items.Add(i);
                }
        }

  

        private void btthemmon_Click(object sender, EventArgs e)
        {
            string tenban = this.tenban;
            bool check = true;
            int sl =Convert.ToInt32(numericUpDown1.Value);
          
            if (cbbLoai.SelectedIndex >= 0 && cbbtenmon.SelectedIndex >=0 && tenban != null)
            {
                foreach(DataRow i in data.Rows)
                {
                    if(i["Tên bàn"].ToString() == tenban && cbbtenmon.SelectedItem == i["Tên món"])
                    {
                        check = false;
                        i["Số lượng"] = (Convert.ToInt16(i["Số lượng"]) + sl);
                        if (Convert.ToInt16(i["Số lượng"]) <= 0)
                            data.Rows.Remove(i);
                        break;
                    }    
                }    
                if(check)
                {
                    if(sl > 0)
                        data.Rows.Add(tenban,cbbtenmon.SelectedItem.ToString(), sl, LayGiaTheoTenMon(cbbtenmon.SelectedItem.ToString()));
                }
                show();
                SaveData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin trước khi thêm món");
            }

           
        }
    }
    
}
