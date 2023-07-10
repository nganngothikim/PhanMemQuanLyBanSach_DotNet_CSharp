using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCongNgheNet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           //Application.Run(new frm_DangNhap());
            Application.Run(new frm_Main());
            //Application.Run(new MyReport_Sach());
            //Application.Run(new MyReport_ChiTietPN());

        }
    }
}
