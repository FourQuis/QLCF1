using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCF
{
    public partial class Form1 : Form
    {
        DataTable data = new DataTable();
        List<Item> items;
        string tenban;
        Button buttonc;
        DataTable ThongKe;
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
            ThongKe = new DataTable();
            ThongKe.Columns.AddRange(new DataColumn[] {
                new DataColumn {ColumnName = "Thời gian", DataType = typeof(DateTime)},
                new DataColumn {ColumnName =  "Bàn", DataType = typeof(string)},
                new DataColumn {ColumnName = "Tông tiền", DataType=typeof(double)},
            });

            LoadData();
            foreach (DataRow row in data.Rows)
            {

                foreach (Control item in this.flowLayoutPanel3.Controls)
                {
                    if (row[0].ToString().Equals(item.Text))
                    {
                        item.BackColor = Color.Red;
                    }
                }
            }
        }

        // Hàm xử lí
        public void LoadData()
        {
            string path = "data.txt";
            StreamReader sr = new StreamReader(path);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                string[] m = s.Split(',');

                data.Rows.Add(m[0], m[1], Convert.ToInt64(m[2]), Convert.ToInt64(m[3]));
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
                list.Add(new Item(m[0], m[1], Convert.ToInt32(m[2])));
            }
            sr.Close();
            return list;
        }

        public List<string> LayDanhSachTenMonTheoLoai(string Loai)
        {
            List<string> list = new List<string>();
            if(Loai == "")
            {
                foreach (Item i in LayDanhSachMonTuFile())
                {
                    list.Add(i.TenMon);
                }
            }    
            else
            foreach (Item i in LayDanhSachMonTuFile())
            {
                if (i.loai.Equals(Loai))
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
            foreach (Item i in items)
            {
                if (i.TenMon == tenmon)
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
                if (i["Tên bàn"].ToString() == tenban)
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
        private int tongtien()
        {

            int tongtien = 0;
            string tenban = this.tenban;
            foreach (DataRow i in data.Rows)
            {
                if (i["Tên bàn"].Equals(tenban))
                {
                    tongtien += Convert.ToInt16(i["Số lượng"]) * LayGiaTheoTenMon(i["Tên món"].ToString());
                }
            }
            return tongtien;
        }

        private DataTable DuLieuThongKe(string txt, DateTime from, DateTime to)
        {
            DataTable dt = new DataTable();
            int tong = 0;
            if(txt == "Hóa đơn")
            {
                dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Thời gian", typeof(DateTime)),
                    new DataColumn("Tên bàn", typeof(string)),
                    new DataColumn("Tổng tiền", typeof(int))
                });

                string path = "datathongke.txt";
                StreamReader sr = new StreamReader(path);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] m = s.Split(',');
                    DateTime time = Convert.ToDateTime(m[0]);
                    int tien = Convert.ToInt32(m[2]);
                    if (time >= from && time <= to)
                    {
                        dt.Rows.Add(time, m[1], tien);
                        tong += tien;
                    }
                }
                sr.Close();
                dt.Rows.Add(null, "Tổng", tong);
            }    
            else if(txt == "Tất cả món ăn")
            {
                dt.Columns.AddRange(new DataColumn[]
               {
                    new DataColumn("Thời gian", typeof(DateTime)),
                    new DataColumn("Tên bàn", typeof(string)),
                    new DataColumn("Tên món", typeof(string)),
                    new DataColumn("Số lượng", typeof(int)),
                    new DataColumn("Đơn giá", typeof(int)),

               });

                string path = "datahoadon.txt";
                StreamReader sr = new StreamReader(path);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] m = s.Split(',');
                    DateTime time = Convert.ToDateTime(m[0]);
                    int sl = Convert.ToInt16(m[3]);
                    int dongia = Convert.ToInt32(m[4]);
                    if (time >= from && time <= to)
                    {
                        Console.WriteLine(m[4]);
                        dt.Rows.Add(time, m[1], m[2],sl , dongia);
                        tong += sl * dongia;
                    }
                }
                dt.Rows.Add(null, "", "Tổng tiền", null, tong);
                sr.Close();
            }
            else
            {
                dt.Columns.AddRange(new DataColumn[]
              {
                    new DataColumn("Thời gian", typeof(DateTime)),
                    new DataColumn("Tên bàn", typeof(string)),
                    new DataColumn("Tên món", typeof(string)),
                    new DataColumn("Số lượng", typeof(int)),
                    new DataColumn("Đơn giá", typeof(int)),

              });

                string path = "datahoadon.txt";
                StreamReader sr = new StreamReader(path);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] m = s.Split(',');
                    DateTime time = Convert.ToDateTime(m[0]);
                    int sl = Convert.ToInt16(m[3]);
                    int dongia = Convert.ToInt32(m[4]);
                    if (time >= from && time <= to && txt == m[2])
                    {
                        Console.WriteLine(m[4]);
                        dt.Rows.Add(time, m[1], m[2], sl, dongia);
                        tong += sl * dongia;
                    }
                }
                dt.Rows.Add(null, "", "Tổng tiền", null, tong);
                sr.Close();
            }    
            return dt;
        }
        // Tab 1
        private void button_click(object sender, EventArgs e)
        {
            txttongtien.Text = tongtien().ToString();
            
            Button button = (Button)sender;
            buttonc = button;
            tenban = button.Text;
            lbTenBan.Text = button.Text.ToString();
            txttongtien.Text = tongtien().ToString();
            show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   cbbtenmon.Items.Clear();
            cbbtenmon.SelectedItem = null;
            cbbtenmon.Text = "";
            cbbtenmon.Enabled = true;
            foreach (string i in LayDanhSachTenMonTheoLoai(cbbLoai.SelectedItem.ToString()).Distinct())
            {
                cbbtenmon.Items.Add(i);
            }
        }
        private void btthemmon_Click(object sender, EventArgs e)
        {  
            
            string tenban = this.tenban;
            bool check = true;
            int sl = Convert.ToInt32(numericUpDown1.Value);

            if (cbbLoai.SelectedIndex >= 0 && cbbtenmon.SelectedIndex >= 0 && tenban != null)
            {
                foreach (DataRow i in data.Rows)
                {
                    if (i["Tên bàn"].Equals(tenban) && cbbtenmon.SelectedItem.Equals(i["Tên món"]))
                    {
                        check = false;
                        i["Số lượng"] = (Convert.ToInt16(i["Số lượng"]) + sl);
                        if (Convert.ToInt16(i["Số lượng"]) <= 0)
                            data.Rows.Remove(i);
           
                        break;
                    }
                }
                if (check)
                {
                    if (sl > 0)
                        data.Rows.Add(tenban, cbbtenmon.SelectedItem.ToString(), sl, LayGiaTheoTenMon(cbbtenmon.SelectedItem.ToString()));
                }
                buttonc.BackColor = Color.Red;
                show();
                SaveData();
                txttongtien.Text = tongtien().ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin trước khi thêm món");
            }


        }
       
        private void btthanhtoan_Click(object sender, EventArgs e)
        {
            //XHD f = new XHD(data, tenban, tongtien());
            //f.Show();
            if (txttongtien.Text == "0")
                return;
            string Path = "datahoadon.txt";
            using (StreamWriter F = File.AppendText(Path))
            {
            foreach (DataRow i in data.Rows)
                if ((i["Tên bàn"].Equals(tenban)))
                {
                    F.WriteLine(DateTime.Now.ToString() +","+ i[0] + "," + i[1] + "," + i[2] + "," + i[3]);
                }
            }
            Path = "datathongke.txt";
            using (StreamWriter F = File.AppendText(Path))
            {
                F.WriteLine(DateTime.Now.ToString() +","+tenban+"," + txttongtien.Text);
            }
            buttonc.BackColor = Color.Bisque;
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow row in data.Rows)
            {
                if (row["Tên Bàn"].Equals(tenban))
                    
                {
                    rowsToDelete.Add(row);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                row.Delete();
            }
            data.AcceptChanges();
          
            SaveData();
            dataGridView1.DataSource = null;
            txttongtien.Text = "0";
        }

       
        // tab 2
        private void btShow_Click(object sender, EventArgs e)
        {
            
            foreach (Item item in LayDanhSachMonTuFile() )
            {
                dataGridView2.Rows.Add(item.loai,item.TenMon,item.DonGia);
            }
            
        }

        private void btsua_Click(object sender, EventArgs e)
        {   
            
            if (dataGridView2.SelectedRows.Count == 1)
            {
                dataGridView2.SelectedRows[0].Cells[0].Value = txtloaib.Text ;
                dataGridView2.SelectedRows[0].Cells[1].Value = txttenmonb.Text;
                 dataGridView2.SelectedRows[0].Cells[2].Value =txtgiab.Text ;
            }

            txtgiab.Text = "";
            txtloaib.Text = "";
            txttenmonb.Text = "";
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                txtloaib.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txttenmonb.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                txtgiab.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            }
        }
        
        private void btthem_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (int.TryParse(this.txtgiab.Text, out n))
            {
            
                dataGridView2.Rows.Add(txtloaib.Text , txttenmonb.Text,txtgiab.Text);
             }
            else
            {
                MessageBox.Show("Gía phải là số");
            }
            string Path = "Danhsachmon.txt";
            StreamWriter F = new StreamWriter(Path);
            foreach (DataGridViewRow i in dataGridView2.Rows)
            {
                if (i.Cells[0].Value != null && i.Cells[0].Value!=Tenmon)
                    F.WriteLine(i.Cells[0].Value + "," + i.Cells[1].Value + "," + i.Cells[2].Value);
            }
            F.Close();
           
            //cbbtenmon.Items.Clear();
            cbbLoai.Items.Clear();
            foreach (string i in LayDaySachLoai().Distinct())
            {
                cbbLoai.Items.Add(i);
            }

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow i in dataGridView2.SelectedRows)
                {
                    dataGridView2.Rows.Remove(i);
                }
            }
            else
            {
                MessageBox.Show("Chọn hàng để xóa");
            }
        }

        private void tabControl1_SelectChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            foreach (Item item in LayDanhSachMonTuFile())
            {
                dataGridView2.Rows.Add(item.loai, item.TenMon, item.DonGia);
            }
            string Path = "Danhsachmon.txt";
            StreamWriter F = new StreamWriter(Path);
            foreach (DataGridViewRow i in dataGridView2.Rows)
            {
                if (i.Cells[0].Value != null)
                    F.WriteLine(i.Cells[0].Value + "," + i.Cells[1].Value + "," + i.Cells[2].Value);
            }
            F.Close();
            cbbtenmonan.Items.Clear();
            cbbtenmonan.Items.Add("Tất cả món ăn");
            cbbtenmonan.Items.Add("Hóa đơn");
     
            foreach(string s in LayDanhSachTenMonTheoLoai(""))
            {
                cbbtenmonan.Items.Add(s);
            }    

        }

        private void btnapply_Click(object sender, EventArgs e)
        {
            if(cbbtenmonan.SelectedIndex > -1)
            dataGridView3.DataSource = DuLieuThongKe(cbbtenmonan.SelectedItem.ToString(), dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void cbbtenmonan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
