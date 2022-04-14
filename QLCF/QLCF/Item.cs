using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCF
{
    public class Item
    {
        public string TenMon;
        public int DonGia;
        public string loai;
        public Item(string a, string b, int c)
        {
            TenMon = b;
            DonGia = c;
            loai = a;
        }
    }
}
