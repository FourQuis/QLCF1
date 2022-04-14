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
    public partial class XHD : Form
    {
       
        public XHD(DataTable data , string tenban , int tongtien)
        {
            InitializeComponent();
            foreach (DataRow row in data.Rows)
            {
                if (row[0].Equals(tenban))
                {
                    lbinhoadon.Text += row[1]+"\tx"+row[2]+"\n" ;
                }

            }
            lbinhoadon.Text += tongtien.ToString();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
