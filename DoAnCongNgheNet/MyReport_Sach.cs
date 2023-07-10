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
    public partial class MyReport_Sach : MetroFramework.Forms.MetroForm
    {
        public MyReport_Sach()
        {
            InitializeComponent();
        }

        private void MyReport_Sach_Load(object sender, EventArgs e)
        {
            ReportSach rpt = new ReportSach();
            crystalReportViewer1.ReportSource = rpt;

            rpt.SetDatabaseLogon("sa", "123");

            
            crystalReportViewer1.Refresh();
        }
    }
}
