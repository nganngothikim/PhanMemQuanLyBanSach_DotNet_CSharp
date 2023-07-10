using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCongNgheNet
{
    public partial class MyReport_ChiTietHD : Form
    {
        public MyReport_ChiTietHD()
        {
            InitializeComponent();
        }

        string _str = "";
        public MyReport_ChiTietHD(string str) : this()
        {
            _str = str;
        }

        private void MyReport_ChiTietHD_Load(object sender, EventArgs e)
        {
            ReportHoaDon rpt = new ReportHoaDon();
            crystalReportViewer1.ReportSource = rpt;

            rpt.SetDatabaseLogon("sa", "123");

            rpt.SetParameterValue("LocHoaDon", _str);

            crystalReportViewer1.Refresh();
        }
    }
}
