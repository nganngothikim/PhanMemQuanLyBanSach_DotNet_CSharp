using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace DoAnCongNgheNet
{
    public partial class MyReport_ChiTietPN : MetroFramework.Forms.MetroForm
    {
        public MyReport_ChiTietPN()
        {
            InitializeComponent();
        }
        string _str = "";
        public MyReport_ChiTietPN(string str) : this()
        {
            _str = str;
        }

        private void MyReport_ChiTietPN_Load(object sender, EventArgs e)
        {
            ReportPhieuNhap rpt = new ReportPhieuNhap();
            crystalReportViewer1.ReportSource = rpt;

            rpt.SetDatabaseLogon("sa", "123");

            rpt.SetParameterValue("LocPhieuNhap", _str);

            crystalReportViewer1.Refresh();
        }


    }
}
