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
using System.Data.SqlClient;

namespace DoAnCongNgheNet
{
    public partial class frm_DangNhap : MetroFramework.Forms.MetroForm
    {
        DBConnect conn = new DBConnect();
        public frm_DangNhap()
        {
            InitializeComponent();
        }

        private string getID(string username, string pass)
        {
            string id = "";
            try
            {
                conn.openConnection();
                string str = "SELECT * FROM DangNhap WHERE MaNhanVien ='" + username + "' and MatKhau='" + pass + "'";
                SqlCommand cmd = new SqlCommand(str, conn.Connect);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        id = dr["MaNhanVien"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                conn.closeConnection();
            }
            return id;
        }

        
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "" && txtMaNV.Text == "")
            {
                MessageBox.Show("Mã Nhân Viên Và Mật Khẩu Không Được Để Trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
            }
            else if (txtMaNV.Text == "")
            {
                MessageBox.Show("Mã Nhân Viên Không Được Để Trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Mật Khẩu Không Được Để Trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
            }
            else
            {
                conn.ID_USER = getID(txtMaNV.Text, txtMatKhau.Text);
                if (conn.ID_USER != "")
                {
                    frm_Main fmain = new frm_Main();
                    fmain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản và mật khẩu không đúng !");
                    txtMaNV.Focus();
                }
            }

        }

        private void frm_DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát from không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
