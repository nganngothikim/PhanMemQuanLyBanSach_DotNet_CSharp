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
    public partial class frm_Main : MetroFramework.Forms.MetroForm
    {
        //--------------------------------
        DBConnect conn;
        SqlDataAdapter adap_Sach;
        DataTable dtSach = new DataTable();
        DataView dtvSach;
        DataSet dsSach = new DataSet();
        //----------------------------
        SqlDataAdapter adap_TheLoai;
        DataTable dtTheLoai = new DataTable();
        DataView dtvTheLoai;
        DataSet dsTheLoai = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_TacGia;
        DataTable dtTacGia = new DataTable();
        DataView dtvTacGia;
        DataSet dsTacGia = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_NXB;
        DataTable dtNXB = new DataTable();
        DataView dtvNXB;
        DataSet dsNXB = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_NCC;
        DataTable dtNCC = new DataTable();
        DataView dtvNCC;
        DataSet dsNCC = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_KhachHang;
        DataTable dtKhachHang = new DataTable();
        DataView dtvKhachHang;
        DataSet dsKH = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_NhanVien;
        DataTable dtNhanVien = new DataTable();
        DataView dtvNhanVien;
        DataSet dsNV = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_LapPhieunhap;
        DataView dtvLapPhieunhap;
        DataSet dsLapPhieunhap = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_Phieunhap;
        DataView dtvPhieuNhap;
        DataSet dsPhieuNhap = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_LapHoaDon;
        DataView dtvLapHoaDon;
        DataSet dsLapHoaDon = new DataSet();
        //----------------------------------
        SqlDataAdapter adap_HoaDon;
        DataView dtvHoaDon;
        DataSet dsHoaDon = new DataSet();
        //----------------------------------
        public frm_Main()
        {
            conn = new DBConnect();
            adap_Sach = new SqlDataAdapter();
            adap_TheLoai = new SqlDataAdapter();
            adap_TacGia = new SqlDataAdapter();
            adap_NXB = new SqlDataAdapter();
            adap_NCC = new SqlDataAdapter();
            adap_KhachHang = new SqlDataAdapter();
            adap_NhanVien = new SqlDataAdapter();
            adap_LapPhieunhap = new SqlDataAdapter();
            adap_Phieunhap = new SqlDataAdapter();
            adap_LapHoaDon = new SqlDataAdapter();
            adap_HoaDon = new SqlDataAdapter();
            InitializeComponent();
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát from không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.openConnection();
                if (conn.Connect.State.ToString() == "Open")
                {
                    TabControlForm.SelectedIndex = 0;
                    if (TabControlForm.SelectedIndex == 1)
                    {
                        TabControl_DM.SelectedIndex = 0;
                    }
                    else
                    {
                        if (TabControlForm.SelectedIndex == 2)
                        {
                            metroTabControl3.SelectedIndex = 0;
                        }
                    }
                    //lấy kích thước của màn hình
                    int widthScreen = Screen.PrimaryScreen.WorkingArea.Width;
                    int heightScreen = Screen.PrimaryScreen.WorkingArea.Height;

                    //cho form hiển thị theo kích thước của màn hình
                    this.Width = widthScreen;
                    this.Height = heightScreen;
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();

                    //---------------------
                    enableButton();
                    //---------------------
                    enableSach(false);
                    Load_dgvSach();
                    LoadCbo_TheLoai();
                    LoadCbo_TacGia();
                    LoadCbo_NXB();
                    //---------------------
                    enableTheLoai(false);
                    Load_dgvTheLoai();
                    //---------------------
                    enableTacGia(false);
                    Load_dgvTacGia();
                    //---------------------
                    enableNXB(false);
                    Load_dgvNXB();
                    //---------------------
                    enableNCC(false);
                    Load_dgvNCC();
                    //---------------------
                    enableKhachHang(false);
                    Load_dgvKhachHang();
                    //---------------------
                    enableNhanVien(false);
                    Load_dgvNhanVien();
                    //-------------
                    Load_dgvSach_LapPhieuNhap();
                    Load_grvChiTietPhieuNhap();
                    Load_grvPhieuNhap();
                    //---------
                    Load_dgvSach_LapHoaDon();
                    Load_grvChiTietHoaDon();
                    Load_grvHoaDon();


                    Load_ThongKe();
                    //---------
                    TabControl_DM.SelectedIndex = 0;
                    txtNamXB_Sach.MaxLength = 4;
                    txtSdtNXB_NXB.MaxLength = 10;
                    txtSdtNcc_NCC.MaxLength = 10;
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối !");
                return;
            }

        }

        //bỏ chọn trên grv
        private void TabControl_DM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvSach.SelectedRows.Count > 0)
            {
                grvSach.CurrentRow.Selected = false;
            }
            if (grvTheLoai.SelectedRows.Count > 0)
            {
                grvTheLoai.CurrentRow.Selected = false;
            }
            if (grvNCC.SelectedRows.Count > 0)
            {
                grvNCC.CurrentRow.Selected = false;
            }
            if (grvTacGia.SelectedRows.Count > 0)
            {
                grvTacGia.CurrentRow.Selected = false;
            }
            if (grvKhachHang.SelectedRows.Count > 0)
            {
                grvKhachHang.CurrentRow.Selected = false;
            }
            if (grvNV.SelectedRows.Count > 0)
            {
                grvNV.CurrentRow.Selected = false;
            }
            if (grvNCC.SelectedRows.Count > 0)
            {
                grvNCC.CurrentRow.Selected = false;
            }
            if (grvNXB.SelectedRows.Count > 0)
            {
                grvNXB.CurrentRow.Selected = false;
            }
            if (grvChiTietPN_LapPN.SelectedRows.Count > 0)
            {
                grvChiTietPN_LapPN.CurrentRow.Selected = false;
            }
            if (grvDSSach_LapPN.SelectedRows.Count > 0)
            {
                grvDSSach_LapPN.CurrentRow.Selected = false;
            }
            if (grvXemPN_XemPN.SelectedRows.Count > 0)
            {
                grvXemPN_XemPN.CurrentRow.Selected = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (metroTile1.BackColor == Color.DarkSlateBlue)
            {
                metroTile1.Text = "Xin chào!";
                metroTile1.BackColor = Color.SteelBlue;
                metroTile1.UseTileImage = false;
            }
            else
            {
                metroTile1.Text = " ";
                metroTile1.BackColor = Color.DarkSlateBlue;
                metroTile1.UseTileImage = true;

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (metroTile2.BackColor == Color.SteelBlue)
            {
                metroTile2.BackColor = Color.DarkSlateBlue;
                metroTile2.UseTileImage = true;
            }
            else if (metroTile2.BackColor == Color.DarkSlateBlue)
            {

                metroTile2.BackColor = Color.MidnightBlue;
                metroTile2.UseTileImage = true;
            }
            else
            {

                metroTile2.BackColor = Color.SteelBlue;
                metroTile2.UseTileImage = true;
            }
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {


        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (metroTile3.BackColor == Color.MidnightBlue)
            {
                metroTile3.BackColor = Color.DarkSlateBlue;

                metroTile3.UseTileImage = true;
                metroTile3.Refresh();
            }
            else
            {
                metroTile3.BackColor = Color.MidnightBlue;
                metroTile3.UseTileImage = true;
            }
        }

        void enableButton()
        {
            btnSach_TrangThaiLoad();
            btnTheLoai_TrangThaiLoad();
            btnTacGia_TrangThaiLoad();
            btnNCC_TrangThaiLoad();
            btnNXB_TrangThaiLoad();
            btnKH_TrangThaiLoad();
            txtKH_TrangThaiLoad();
            foreach (Control s in this.tbl_btn_KhachHang.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Sửa" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
            //---------------------------------------------------------------
            foreach (Control s in this.tbl_btn_Sach.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroComboBox))
                {
                    s.Enabled = false;
                }
            }
        }

        //===========================================
        //Xử Lý Sách
        //===========================================
        void Load_dgvSach()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from Sach";
            //dtSach = conn.getDataTable(strsel, "Sach");

            adap_Sach = new SqlDataAdapter(strsel, conn.Connect);
            adap_Sach.Fill(dsSach, "Sach");
            grvSach.DataSource = dsSach.Tables["Sach"];
            primaryKey[0] = dsSach.Tables["Sach"].Columns["MaSach"];
            dsSach.Tables["Sach"].PrimaryKey = primaryKey;
            dtvSach = new DataView(dsSach.Tables["Sach"]);
            grvSach.ReadOnly = true;
            grvSach.AllowUserToAddRows = false;

            grvSach.Columns["MaSach"].HeaderText = "Mã Sách";
            grvSach.Columns["TenSach"].HeaderText = "Tên Sách";
            grvSach.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            grvSach.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            grvSach.Columns["MaNhaXuatBan"].HeaderText = "Mã Nhà Xuất Bản";
            grvSach.Columns["NoiDungTomTat"].HeaderText = "Nội Dung Tóm Tắt";
            grvSach.Columns["NamXuatBan"].HeaderText = "Năm Xuất Bản";
            grvSach.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            grvSach.Columns["GiaBan"].HeaderText = "Giá Bán";
            grvSach.Columns["SoLuongCon"].HeaderText = "Số Lượng Còn";
        }

        public void btnSach_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_Sach.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtSach_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_Sach1.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        public void enableSach(bool e)
        {
            //xử lý enable Sách
            foreach (Control s in this.tbl_txt_Sach1.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
            foreach (Control s in this.tbl_txt_Sach2.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void grvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenSach_Sach.Text = grvSach.CurrentRow.Cells[1].Value.ToString();
            txtNamXB_Sach.Text = grvSach.CurrentRow.Cells[6].Value.ToString();
            txtGiaNhap_Sach.Text = grvSach.CurrentRow.Cells[7].Value.ToString();
            txtGiaBan_Sach.Text = grvSach.CurrentRow.Cells[8].Value.ToString();
            txtSoLuong_Sach.Text = grvSach.CurrentRow.Cells[9].Value.ToString();
            txtNoiDung_Sach.Text = grvSach.CurrentRow.Cells[5].Value.ToString();
            cboTheLoai_Sach.SelectedValue = grvSach.CurrentRow.Cells[2].Value.ToString();
            cboTacGia_Sach.SelectedValue = grvSach.CurrentRow.Cells[3].Value.ToString();
            cboNXB_Sach.SelectedValue = grvSach.CurrentRow.Cells[4].Value.ToString();
            btnXoa_Sach.Enabled = true;
            btnLuu_Sach.Enabled = false;
        }

        void LoadCbo_TheLoai()
        {
            string str_select = "select * from TheLoai";
            DataTable dt = conn.getDataTable(str_select, "TheLoai");

            cboTheLoai_Sach.DataSource = dt;
            cboTheLoai_Sach.DisplayMember = "TenTheLoai";
            cboTheLoai_Sach.ValueMember = "MaTheLoai";

        }

        void LoadCbo_TacGia()
        {
            string str_select = "select * from TacGia";
            DataTable dt = conn.getDataTable(str_select, "TacGia");

            cboTacGia_Sach.DataSource = dt;
            cboTacGia_Sach.DisplayMember = "TenTacGia";
            cboTacGia_Sach.ValueMember = "MaTacGia";
        }

        void LoadCbo_NXB()
        {
            string str_select = "select * from NhaXuatBan";
            DataTable dt = conn.getDataTable(str_select, "NhaXuatBan");

            cboNXB_Sach.DataSource = dt;
            cboNXB_Sach.DisplayMember = "TenXuatBan";
            cboNXB_Sach.ValueMember = "MaNhaXuatBan";
        }

        private void btnThem_Sach_Click(object sender, EventArgs e)
        {
            grvSach.CurrentRow.Selected = false;//khi muốn thêm 1 sách mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_Sach1.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                }
            }
            txtNoiDung_Sach.Clear();
            enableSach(true);//mở tất cả textbox

            txtSoLuong_Sach.Enabled = false;//đóng txt số lượng
            txtGiaNhap_Sach.Enabled = false;
            txtTenSach_Sach.Focus();
            foreach (Control s in this.tbl_btn_Sach.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu" || s.Text == "Sửa")
                    {
                        s.Enabled = true; //mở
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }

        }

        private void btnLuu_Sach_Click(object sender, EventArgs e)
        {
            if (txtTenSach_Sach.Text.Trim().Length > 0 && txtNamXB_Sach.Text.Trim().Length > 0 && txtGiaBan_Sach.Text.Trim().Length > 0 && txtNoiDung_Sach.Text.Trim().Length > 0)
            {
                try
                {
                    string masach = "SH00" + (dsSach.Tables["Sach"].Rows.Count + 1).ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Insert_Sach"; //Dùng để chỉ định proc ở dưới sql
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn.Connect;

                    cmd.Parameters.Add("@mash", SqlDbType.VarChar).Value = masach;
                    cmd.Parameters.Add("@tensh", SqlDbType.NVarChar).Value = txtTenSach_Sach.Text;
                    cmd.Parameters.Add("@matl", SqlDbType.NVarChar).Value = cboTheLoai_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@matg", SqlDbType.NVarChar).Value = cboTacGia_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@manxb", SqlDbType.NVarChar).Value = cboNXB_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@noidung", SqlDbType.NVarChar).Value = txtNoiDung_Sach.Text;
                    cmd.Parameters.Add("@namxb", SqlDbType.Char).Value = txtNamXB_Sach.Text;
                    cmd.Parameters.Add("@giaban", SqlDbType.Int).Value = txtGiaBan_Sach.Text;

                    conn.openConnection();
                    cmd.ExecuteNonQuery();
                    conn.closeConnection();

                    //dsSach.Tables["Sach"].Rows.Add(cmd);//Thêm dòng mới vào dataset
                    //dtvSach = new DataView(dsSach.Tables["Sach"]);
                    //grvSach.DataSource = dtvSach;

                    Load_dgvSach();
                    MessageBox.Show("Thêm thành công Sách !");

                    txtSach_TrangThaiLoad();
                    btnSach_TrangThaiLoad();

                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    Load_dgvSach();
                    txtSach_TrangThaiLoad();
                    btnSach_TrangThaiLoad();
                }
            }
            else
            {
                Load_dgvSach();
                txtSach_TrangThaiLoad();
                btnSach_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Sách !");

            }

        }

        private void btnSua_Sach_Click(object sender, EventArgs e)
        {
            btnLuu_Sach.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvSach.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsSach.Tables["Sach"].Rows.Find(grvSach.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update_Sach";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn.Connect;

                    cmd.Parameters.Add("@mash", SqlDbType.VarChar).Value = dr["MaSach"];
                    cmd.Parameters.Add("@tensh", SqlDbType.NVarChar).Value = txtTenSach_Sach.Text;
                    cmd.Parameters.Add("@matl", SqlDbType.NVarChar).Value = cboTheLoai_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@matg", SqlDbType.NVarChar).Value = cboTacGia_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@manxb", SqlDbType.NVarChar).Value = cboNXB_Sach.SelectedValue.ToString();
                    cmd.Parameters.Add("@noidung", SqlDbType.NVarChar).Value = txtNoiDung_Sach.Text;
                    cmd.Parameters.Add("@namxb", SqlDbType.Char).Value = txtNamXB_Sach.Text;
                    cmd.Parameters.Add("@giaban", SqlDbType.Int).Value = txtGiaBan_Sach.Text;

                    conn.openConnection();
                    cmd.ExecuteNonQuery();
                    conn.closeConnection();

                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_Sach);
                adap_Sach.Update(dsSach, "Sach");
                MessageBox.Show("Sửa Sách " + grvSach.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                Load_dgvSach();
                txtSach_TrangThaiLoad();
                btnSach_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                Load_dgvSach();
                txtSach_TrangThaiLoad();
                btnSach_TrangThaiLoad();

            }
        }

        private void btnXoa_Sach_Click(object sender, EventArgs e)
        {
            if (grvSach.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvSach.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsSach.Tables["Sach"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "DELETE_SACH";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = conn.Connect;

                            cmd.Parameters.Add("@mash", SqlDbType.VarChar).Value = dr["MaSach"];

                            conn.openConnection();
                            cmd.ExecuteNonQuery();
                            conn.closeConnection();

                            grvSach.Rows.Remove(grvSach.CurrentRow);
                            Load_dgvSach();
                            //dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_Sach);
                            adap_Sach.Update(dsSach, "Sach");
                            MessageBox.Show("Xóa thành công sách " + strMa);

                            txtSach_TrangThaiLoad();
                            btnSach_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại sách để sách " + strMa);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        Load_dgvSach();
                        txtSach_TrangThaiLoad();
                        btnSach_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
            }
        }

        private void btnHuy_Sach_Click(object sender, EventArgs e)
        {
            txtSach_TrangThaiLoad();
            btnSach_TrangThaiLoad();
            grvSach.CurrentRow.Selected = false;//khi hủy grvSach ko tự động chọn dòng trên bảng
        }

        private void rdoMaSach_CheckedChanged(object sender, EventArgs e)
        {
            txtTimKiem_Sach.Text = "";
            Load_dgvSach();

        }

        private void rdoTenSach_CheckedChanged(object sender, EventArgs e)
        {
            txtTimKiem_Sach.Text = "";
            Load_dgvSach();

        }

        private void btnTimKiem_Sach_Click(object sender, EventArgs e)
        {
            if (txtTimKiem_Sach.Text.Length == 0)
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txtTimKiem_Sach.Text.Length > 0 && rdoMaSach.Checked == false && rdoTenSach.Checked == false)
                {
                    MessageBox.Show("Vui Lòng Chọn Đúng Kiểu Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTimKiem_Sach.Text = "";
                    Load_dgvSach();

                }
                else if (rdoMaSach.Checked)
                {
                    SqlCommand cmd = new SqlCommand("select * from dbo.TimKiemSach (@mash, @tensh)", conn.Connect);
                    cmd.Parameters.AddWithValue("@mash", txtTimKiem_Sach.Text);
                    cmd.Parameters.AddWithValue("@tensh", "");
                    dtSach = new DataTable();
                    adap_Sach = new SqlDataAdapter(cmd);
                    adap_Sach.Fill(dtSach);

                    grvSach.DataSource = dtSach;
                }

                else if (rdoTenSach.Checked)
                {
                    SqlCommand cmd = new SqlCommand("select * from dbo.TimKiemSach (@mash, @tensh)", conn.Connect);
                    cmd.Parameters.AddWithValue("@mash", "");
                    cmd.Parameters.AddWithValue("@tensh", txtTimKiem_Sach.Text);
                    dtSach = new DataTable();
                    adap_Sach = new SqlDataAdapter(cmd);
                    adap_Sach.Fill(dtSach);

                    grvSach.DataSource = dtSach;
                }
            }
        }

        private void txtGiaNhap_Sach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnHuyTim_Sach_Click(object sender, EventArgs e)
        {
            txtTimKiem_Sach.Clear();
            Load_dgvSach();
            grvSach.CurrentRow.Selected = false;
        }
        //===========================================
        //Xử Lý Thể Loại
        //===========================================
        public void btnTheLoai_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_TheLoai.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtTheLoai_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_TheLoai.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvTheLoai()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from TheLoai";
            //dtSach = conn.getDataTable(strsel, "Sach");

            adap_TheLoai = new SqlDataAdapter(strsel, conn.Connect);
            adap_TheLoai.Fill(dsTheLoai, "TheLoai");
            dtvTheLoai = new DataView(dsTheLoai.Tables["TheLoai"]);

            grvTheLoai.DataSource = dtvTheLoai;
            primaryKey[0] = dsTheLoai.Tables["TheLoai"].Columns["MaSach"];
            dsTheLoai.Tables["TheLoai"].PrimaryKey = primaryKey;
            grvTheLoai.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            grvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên Thể Loại";
            grvTheLoai.ReadOnly = true;
            grvTheLoai.AllowUserToAddRows = false;
        }

        private void grvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtTenTheLoai_TheLoai.Text = grvTheLoai.CurrentRow.Cells[1].Value.ToString();
            btnXoa_TheLoai.Enabled = true;
        }

        public void enableTheLoai(bool e)
        {
            //xử lý enable Thể Loại
            foreach (Control s in this.tbl_txt_TheLoai.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void btnThem_TheLoai_Click(object sender, EventArgs e)
        {

            grvTheLoai.CurrentRow.Selected = false;//khi muốn thêm 1 thể loại mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_TheLoai.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Text = string.Empty;
                }
            }

            txtTenTheLoai_TheLoai.Enabled = true;
            txtTenTheLoai_TheLoai.Focus();
            foreach (Control s in this.tbl_btn_TheLoai.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Lưu" || s.Text == "Sửa" || s.Text == "Hủy")
                    {
                        s.Enabled = true; //mở
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }

        }

        private void btnLuu_TheLoai_Click(object sender, EventArgs e)
        {

            if (txtTenTheLoai_TheLoai.Text.Trim().Length > 0)
            {
                try
                {
                    DataRow newRow = dsTheLoai.Tables["TheLoai"].NewRow(); //Tạo dòng mới
                    newRow["MaTheLoai"] = "TL00" + (dsTheLoai.Tables["TheLoai"].Rows.Count + 1).ToString();
                    newRow["TenTheLoai"] = txtTenTheLoai_TheLoai.Text;
                    dsTheLoai.Tables["TheLoai"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TheLoai);
                    adap_TheLoai.Update(dsTheLoai, "TheLoai");
                    MessageBox.Show("Thêm thành công Thể Loại !");
                    btnTheLoai_TrangThaiLoad();
                    txtTheLoai_TrangThaiLoad();
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    btnTheLoai_TrangThaiLoad();
                    txtTheLoai_TrangThaiLoad();
                }
            }
            else
            {
                btnTheLoai_TrangThaiLoad();
                txtTheLoai_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Thể Loại !");

            }
        }

        private void btnSua_TheLoai_Click(object sender, EventArgs e)
        {
            btnLuu_TheLoai.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvTheLoai.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsTheLoai.Tables["TheLoai"].Rows.Find(grvTheLoai.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    dr["TenTheLoai"] = txtTenTheLoai_TheLoai.Text;

                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TheLoai);
                adap_TheLoai.Update(dsTheLoai, "TheLoai");
                MessageBox.Show("Sửa Thể Loại " + grvTheLoai.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                btnTheLoai_TrangThaiLoad();
                txtTheLoai_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                btnTheLoai_TrangThaiLoad();
                txtTheLoai_TrangThaiLoad();

            }
        }

        private void btnXoa_TheLoai_Click(object sender, EventArgs e)
        {
            if (grvTheLoai.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvTheLoai.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsTheLoai.Tables["TheLoai"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TheLoai);
                            adap_TheLoai.Update(dsTheLoai, "TheLoai");
                            MessageBox.Show("Xóa thành công Thể Loại " + strMa);
                            btnTheLoai_TrangThaiLoad();
                            txtTheLoai_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại Thể Loại để xóa " + strMa);

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        btnTheLoai_TrangThaiLoad();
                        txtTheLoai_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
            }

        }

        private void btnHuy_TheLoai_Click(object sender, EventArgs e)
        {
            btnTheLoai_TrangThaiLoad();
            txtTheLoai_TrangThaiLoad();
            grvTheLoai.CurrentRow.Selected = false;//khi hủy grvSach ko tự động chọn dòng trên bảng
        }
        //===========================================
        //Xử Lý Tác Giả
        //===========================================
        public void btnTacGia_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_TacGia.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtTacGia_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_TacGia.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvTacGia()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];

            string strsel = "select * from TacGia";
            //dtTacGia = conn.getDataTable(strsel, "TacGia");
            adap_TacGia = new SqlDataAdapter(strsel, conn.Connect);
            adap_TacGia.Fill(dsTacGia, "TacGia");

            primaryKey[0] = dsTacGia.Tables["TacGia"].Columns[0];
            dsTacGia.Tables["TacGia"].PrimaryKey = primaryKey;

            dtvTacGia = new DataView(dsTacGia.Tables["TacGia"]);
            grvTacGia.DataSource = dtvTacGia;
            grvTacGia.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            grvTacGia.Columns["TenTacGia"].HeaderText = "Tên Tác Giả";
            grvTacGia.Columns["GhiChu"].HeaderText = "Ghi Chú";

            grvTacGia.ReadOnly = true;
            grvTacGia.AllowUserToAddRows = false;
        }

        private void grvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtTenTG_TacGia.Text = grvTacGia.CurrentRow.Cells[1].Value.ToString();
            txtGhiChu_TacGia.Text = grvTacGia.CurrentRow.Cells[2].Value.ToString();
        }

        public void enableTacGia(bool e)
        {
            //xử lý enable Tác Giả
            foreach (Control s in this.tbl_txt_TacGia.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void btnThem_TacGia_Click(object sender, EventArgs e)
        {

            grvTacGia.CurrentRow.Selected = false;//khi muốn thêm 1 sách mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_TacGia.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Text = string.Empty;
                }
            }
            enableTacGia(true);//mở tất cả textbox
            txtTenTG_TacGia.Focus();
            foreach (Control s in this.tbl_btn_TacGia.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu" || s.Text == "Sửa")
                    {
                        s.Enabled = true; //mở
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnLuu_TacGia_Click(object sender, EventArgs e)
        {
            if (txtTenTG_TacGia.Text.Trim().Length > 0)
            {
                try
                {
                    DataRow newRow = dsTacGia.Tables["TacGia"].NewRow(); //Tạo dòng mới
                    newRow["MaTacGia"] = "TG00" + (dsTacGia.Tables["TacGia"].Rows.Count + 1).ToString();
                    newRow["TenTacGia"] = txtTenTG_TacGia.Text;
                    dsTacGia.Tables["TacGia"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TacGia);
                    adap_TacGia.Update(dsTacGia, "TacGia");
                    MessageBox.Show("Thêm thành công Tác Giả !");
                    txtTacGia_TrangThaiLoad();
                    btnTacGia_TrangThaiLoad();
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    txtTacGia_TrangThaiLoad();
                    btnTacGia_TrangThaiLoad();
                }
            }
            else
            {
                txtTacGia_TrangThaiLoad();
                btnTacGia_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Thể Loại !");

            }
        }

        private void btnSua_TacGia_Click(object sender, EventArgs e)
        {
            btnLuu_TacGia.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvTacGia.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsTacGia.Tables["TacGia"].Rows.Find(grvTacGia.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    dr["TenTacGia"] = txtTenTG_TacGia.Text;
                    dr["GhiChu"] = txtGhiChu_TacGia.Text;
                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TacGia);
                adap_TacGia.Update(dsTacGia, "TacGia");
                MessageBox.Show("Sửa Tác Giả " + grvTacGia.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                txtTacGia_TrangThaiLoad();
                btnTacGia_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                txtTacGia_TrangThaiLoad();
                btnTacGia_TrangThaiLoad();

            }
        }

        private void btnXoa_TacGia_Click(object sender, EventArgs e)
        {
            if (grvTacGia.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvTacGia.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsTacGia.Tables["TacGia"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_TacGia);
                            adap_TacGia.Update(dsTacGia, "TacGia");
                            MessageBox.Show("Xóa thành công sách " + strMa);
                            txtTacGia_TrangThaiLoad();
                            btnTacGia_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại Tác Giả để xóa");
                            txtTacGia_TrangThaiLoad();
                            btnTacGia_TrangThaiLoad();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        txtTacGia_TrangThaiLoad();
                        btnTacGia_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
                txtTacGia_TrangThaiLoad();
                btnTacGia_TrangThaiLoad();
            }
        }

        private void btnHuy_TacGia_Click(object sender, EventArgs e)
        {
            txtTacGia_TrangThaiLoad();
            btnTacGia_TrangThaiLoad();
            grvTacGia.CurrentRow.Selected = false;
        }

        //===========================================
        //Xử Lý Nhà Xuất Bản
        //===========================================
        public void btnNXB_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_NXB.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtNXB_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_NXB.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvNXB()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];

            string strsel = "select * from NhaXuatBan";
            //dtNXB = conn.getDataTable(strsel, "NhaXuatBan");
            adap_NXB = new SqlDataAdapter(strsel, conn.Connect);
            adap_NXB.Fill(dsNXB, "NhaXuatBan");

            primaryKey[0] = dsNXB.Tables["NhaXuatBan"].Columns[0];
            dsNXB.Tables["NhaXuatBan"].PrimaryKey = primaryKey;

            dtvNXB = new DataView(dsNXB.Tables["NhaXuatBan"]);
            grvNXB.DataSource = dtvNXB;
            grvNXB.Columns["MaNhaXuatBan"].HeaderText = "Mã Nhà Xuất Bản";
            grvNXB.Columns["TenXuatBan"].HeaderText = "Tên Nhà Xuất Bản";
            grvNXB.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grvNXB.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";

            grvNXB.ReadOnly = true;
            grvNXB.AllowUserToAddRows = false;
        }

        private void grvNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtTenNXB_NXB.Text = grvNXB.CurrentRow.Cells[1].Value.ToString();
            txtDChiNXB_NXB.Text = grvNXB.CurrentRow.Cells[2].Value.ToString();
            txtSdtNXB_NXB.Text = grvNXB.CurrentRow.Cells[3].Value.ToString();
            btnXoa_NXB.Enabled = true;
        }

        public void enableNXB(bool e)
        {
            //xử lý enable Nhà Xuất Bản
            foreach (Control s in this.tbl_txt_NXB.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }
        private void btnThem_NXB_Click(object sender, EventArgs e)
        {
            grvNXB.CurrentRow.Selected = false;//khi muốn thêm 1 sách mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_NXB.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Text = string.Empty;
                }
            }
            enableNXB(true);//mở tất cả textbox
            txtTenNXB_NXB.Focus();
            foreach (Control s in this.tbl_btn_NXB.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu" || s.Text == "Sửa")
                    {
                        s.Enabled = true; //mở
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnLuu_NXB_Click(object sender, EventArgs e)
        {
            if (txtTenNXB_NXB.Text.Trim().Length > 0 && txtSdtNXB_NXB.Text.Trim().Length > 0 && txtDChiNXB_NXB.Text.Trim().Length > 0)
            {
                try
                {
                    DataRow newRow = dsNXB.Tables["NhaXuatBan"].NewRow(); //Tạo dòng mới
                    newRow["MaNhaXuatBan"] = "NXB00" + (dsNXB.Tables["NhaXuatBan"].Rows.Count + 1).ToString();
                    newRow["TenXuatBan"] = txtTenNXB_NXB.Text;
                    newRow["DiaChi"] = txtDChiNXB_NXB.Text;
                    newRow["SoDienThoai"] = txtSdtNXB_NXB.Text;

                    dsNXB.Tables["NhaXuatBan"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NXB);
                    adap_NXB.Update(dsNXB, "NhaXuatBan");
                    MessageBox.Show("Thêm thành công Nhà Xuất Bản !");
                    txtNXB_TrangThaiLoad();
                    btnNXB_TrangThaiLoad();
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    txtNXB_TrangThaiLoad();
                    btnNXB_TrangThaiLoad();
                }
            }
            else
            {
                txtNXB_TrangThaiLoad();
                btnNXB_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Nhà Xuất Bản !");
            }
        }
        private void btnSua_NXB_Click(object sender, EventArgs e)
        {
            btnLuu_NXB.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvNXB.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsNXB.Tables["NhaXuatBan"].Rows.Find(grvNXB.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    dr["TenXuatBan"] = txtTenNXB_NXB.Text;
                    dr["DiaChi"] = txtDChiNXB_NXB.Text;
                    dr["SoDienThoai"] = txtSdtNXB_NXB.Text;
                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NXB);
                adap_NXB.Update(dsNXB, "NhaXuatBan");
                MessageBox.Show("Sửa Nhà Xuất Bản " + grvNXB.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                txtNXB_TrangThaiLoad();
                btnNXB_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                txtNXB_TrangThaiLoad();
                btnNXB_TrangThaiLoad();

            }
        }

        private void btnXoa_NXB_Click(object sender, EventArgs e)
        {
            if (grvNXB.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvNXB.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsNXB.Tables["NhaXuatBan"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NXB);
                            adap_NXB.Update(dsNXB, "NhaXuatBan");
                            MessageBox.Show("Xóa thành công Nhà Xuất Bản " + strMa);
                            txtNXB_TrangThaiLoad();
                            btnNXB_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại Nhà Xuất Bản để xóa");
                            txtNXB_TrangThaiLoad();
                            btnNXB_TrangThaiLoad();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        txtNXB_TrangThaiLoad();
                        btnNXB_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
                txtNXB_TrangThaiLoad();
                btnNXB_TrangThaiLoad();
            }
        }

        private void btnHuy_NXB_Click(object sender, EventArgs e)
        {
            txtNXB_TrangThaiLoad();
            btnNXB_TrangThaiLoad();
            grvNXB.CurrentRow.Selected = false;
        }

        //===========================================
        //Xử Lý Nhà Cung Cấp
        //===========================================

        public void btnNCC_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_NCC.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtNCC_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_NCC.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvNCC()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];

            string strsel = "select * from NhaCungCap";
            //dtNCC = conn.getDataTable(strsel, "NhaCungCap");
            adap_NCC = new SqlDataAdapter(strsel, conn.Connect);
            adap_NCC.Fill(dsNCC, "NhaCungCap");
            primaryKey[0] = dsNCC.Tables["NhaCungCap"].Columns[0];
            dsNCC.Tables["NhaCungCap"].PrimaryKey = primaryKey;

            dtvNCC = new DataView(dsNCC.Tables["NhaCungCap"]);
            grvNCC.DataSource = dtvNCC;
            grvNCC.Columns["MaNhaCungCap"].HeaderText = "Mã Nhà Cung Cấp";
            grvNCC.Columns["TenNhaCungCap"].HeaderText = "Tên Nhà Cung Cấp";
            grvNCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grvNCC.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            grvNCC.Columns["EMail"].HeaderText = "Email";

            grvNCC.ReadOnly = true;
            grvNCC.AllowUserToAddRows = false;
        }

        private void grvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtTenNcc_NCC.Text = grvNCC.CurrentRow.Cells[1].Value.ToString();
            txtDChiNcc_NCC.Text = grvNCC.CurrentRow.Cells[2].Value.ToString();
            txtSdtNcc_NCC.Text = grvNCC.CurrentRow.Cells[3].Value.ToString();
            txtEmailNcc_NCC.Text = grvNCC.CurrentRow.Cells[4].Value.ToString();
        }

        public void enableNCC(bool e)
        {
            //xử lý enable Nhà Cung Cấp
            foreach (Control s in this.tbl_txt_NCC.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void btnThem_NCC_Click(object sender, EventArgs e)
        {
            grvNCC.CurrentRow.Selected = false;//khi muốn thêm 1 sách mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_NCC.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Text = string.Empty;
                }
            }
            enableNCC(true);//mở tất cả textbox
            txtTenNcc_NCC.Focus();
            foreach (Control s in this.tbl_btn_NCC.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu" || s.Text == "Sửa")
                    {
                        s.Enabled = true; //mở
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnLuu_NCC_Click(object sender, EventArgs e)
        {
            if (txtTenNcc_NCC.Text.Trim().Length > 0 && txtDChiNcc_NCC.Text.Trim().Length > 0 && txtSdtNcc_NCC.Text.Trim().Length > 0 && txtEmailNcc_NCC.Text.Trim().Length > 0)
            {
                try
                {
                    DataRow newRow = dsNCC.Tables["NhaCungCap"].NewRow(); //Tạo dòng mới
                    newRow["MaNhaCungCap"] = "NCC00" + (dsNCC.Tables["NhaCungCap"].Rows.Count + 1).ToString();
                    newRow["TenNhaCungCap"] = txtTenNcc_NCC.Text;
                    newRow["DiaChi"] = txtDChiNcc_NCC.Text;
                    newRow["SoDienThoai"] = txtSdtNcc_NCC.Text;
                    newRow["EMail"] = txtEmailNcc_NCC.Text;
                    dsNCC.Tables["NhaCungCap"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NCC);
                    adap_NCC.Update(dsNCC, "NhaCungCap");
                    MessageBox.Show("Thêm thành công Nhà Cung Cấp !");
                    txtNCC_TrangThaiLoad();
                    btnNCC_TrangThaiLoad();
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    txtNCC_TrangThaiLoad();
                    btnNCC_TrangThaiLoad();
                }
            }
            else
            {
                txtNCC_TrangThaiLoad();
                btnNCC_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Nhà Cung Cấp !");

            }
        }

        private void btnSua_NCC_Click(object sender, EventArgs e)
        {
            btnLuu_NCC.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvNCC.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsNCC.Tables["NhaCungCap"].Rows.Find(grvNCC.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    dr["TenNhaCungCap"] = txtTenNcc_NCC.Text;
                    dr["DiaChi"] = txtDChiNcc_NCC.Text;
                    dr["SoDienThoai"] = txtSdtNcc_NCC.Text;
                    dr["EMail"] = txtEmailNcc_NCC.Text;
                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NCC);
                adap_NCC.Update(dsNCC, "NhaCungCap");
                MessageBox.Show("Sửa Nhà Cung Cấp " + grvNCC.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                txtNCC_TrangThaiLoad();
                btnNCC_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                txtNCC_TrangThaiLoad();
                btnNCC_TrangThaiLoad();
            }
        }

        private void btnXoa_NCC_Click(object sender, EventArgs e)
        {
            if (grvNCC.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvNCC.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsNCC.Tables["NhaCungCap"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_NCC);
                            adap_NCC.Update(dsNCC, "NhaCungCap");
                            MessageBox.Show("Xóa thành công Nhà Cung Cấp " + strMa);
                            txtNCC_TrangThaiLoad();
                            btnNCC_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại Nhà Cung Cấp để xóa");
                            txtNCC_TrangThaiLoad();
                            btnNCC_TrangThaiLoad();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        txtNCC_TrangThaiLoad();
                        btnNCC_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
                txtNCC_TrangThaiLoad();
                btnNCC_TrangThaiLoad();
            }
        }

        private void btnHuy_NCC_Click(object sender, EventArgs e)
        {
            txtNCC_TrangThaiLoad();
            btnNCC_TrangThaiLoad();
            grvNCC.CurrentRow.Selected = false;
        }

        //===========================================
        //Xử Lý Khách Hàng
        //===========================================
        public void btnKH_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_KhachHang.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtKH_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_KH.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvKhachHang()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];

            string strsel = "select * from KhachHang";
            //dtKhachHang = conn.getDataTable(strsel, "KhachHang");
            adap_KhachHang = new SqlDataAdapter(strsel, conn.Connect);
            adap_KhachHang.Fill(dsKH, "KhachHang");
            primaryKey[0] = dsKH.Tables["KhachHang"].Columns[0];
            dsKH.Tables["KhachHang"].PrimaryKey = primaryKey;

            dtvKhachHang = new DataView(dsKH.Tables["KhachHang"]);
            grvKhachHang.DataSource = dtvKhachHang;
            grvKhachHang.Columns["MaKhachHang"].HeaderText = "Mã Khách Hàng";
            grvKhachHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            grvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            grvKhachHang.Columns["EMail"].HeaderText = "Email";

            grvKhachHang.ReadOnly = true;
            grvKhachHang.AllowUserToAddRows = false;
        }

        private void grvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenKH_KH.Text = grvKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtDChiKH_KH.Text = grvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtSdtKH_KH.Text = grvKhachHang.CurrentRow.Cells[3].Value.ToString();
            txtEmailKH_KH.Text = grvKhachHang.CurrentRow.Cells[4].Value.ToString();
        }

        public void enableKhachHang(bool e)
        {
            //xử lý enable Khách Hàng
            foreach (Control s in this.tbl_txt_KH.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void btnThem_KH_Click(object sender, EventArgs e)
        {
            grvKhachHang.CurrentRow.Selected = false;//khi muốn thêm 1 sách mới thì tự động bỏ chọn trên gridview
            foreach (Control s in this.tbl_txt_KH.Controls)//clear tất cả txt
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Text = string.Empty;
                }
            }
            foreach (Control s in this.tbl_btn_KhachHang.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu" || s.Text == "Sửa")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }

            enableKhachHang(true);
            txtTenKH_KH.Focus();
        }

        private void btnLuu_KH_Click(object sender, EventArgs e)
        {
            if (txtTenKH_KH.Text.Trim().Length > 0 && txtSdtKH_KH.Text.Trim().Length > 0 && txtDChiKH_KH.Text.Trim().Length > 0)
            {
                try
                {
                    DataRow newRow = dsKH.Tables["KhachHang"].NewRow(); //Tạo dòng mới
                    newRow["MaKhachHang"] = "KH00" + (dsKH.Tables["KhachHang"].Rows.Count + 1).ToString();
                    newRow["TenKhachHang"] = txtTenKH_KH.Text;
                    newRow["DiaChi"] = txtDChiKH_KH.Text;
                    newRow["SoDienThoai"] = txtSdtKH_KH.Text;

                    dsKH.Tables["KhachHang"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_KhachHang);
                    adap_KhachHang.Update(dsKH, "KhachHang");
                    MessageBox.Show("Thêm thành công Khách Hàng !");
                    txtKH_TrangThaiLoad();
                    btnKH_TrangThaiLoad();
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    txtKH_TrangThaiLoad();
                    btnKH_TrangThaiLoad();
                }
            }
            else
            {
                txtKH_TrangThaiLoad();
                btnKH_TrangThaiLoad();
                MessageBox.Show("Phải thêm đầy đủ thông tin của Nhà Xuất Bản !");

            }
        }

        private void btnHuyTim_KH_Click(object sender, EventArgs e)
        {
            txtTimKiem_KH.Clear();
            Load_dgvKhachHang();
            grvKhachHang.CurrentRow.Selected = false;
        }

        private void btnSua_KH_Click(object sender, EventArgs e)
        {
            btnLuu_KH.Enabled = false; //khi sửa không làm cùng hành động lưu
            //mở tất cả txt; ẩn button thêm, in, xóa
            if (grvKhachHang.SelectedRows.Count > 0)//kiểm tra xem có dòng nào đang đc chọn hay không
            {
                DataRow dr = dsKH.Tables["KhachHang"].Rows.Find(grvKhachHang.CurrentRow.Cells[0].Value.ToString());
                if (dr != null)//có tồn tại dòng cần sửa
                {
                    dr["TenKhachHang"] = txtTenKH_KH.Text;
                    dr["DiaChi"] = txtDChiKH_KH.Text;
                    dr["SoDienThoai"] = txtSdtKH_KH.Text;
                    dr["EMail"] = txtEmailKH_KH.Text;
                }
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_KhachHang);
                adap_KhachHang.Update(dsKH, "KhachHang");
                MessageBox.Show("Sửa Khách Hang " + grvKhachHang.CurrentRow.Cells[0].Value.ToString() + " thành công ! ");
                txtKH_TrangThaiLoad();
                btnKH_TrangThaiLoad();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn 1 dòng trên bản để sửa !");
                txtKH_TrangThaiLoad();
                btnKH_TrangThaiLoad();
            }
        }

        private void btnXoa_KH_Click(object sender, EventArgs e)
        {
            if (grvKhachHang.SelectedRows.Count > 0)//nếu có dòng đang chọn
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        string strMa = grvKhachHang.CurrentRow.Cells[0].Value.ToString();
                        DataRow dr = dsKH.Tables["KhachHang"].Rows.Find(strMa);
                        if (dr != null)//Có dòng cần xóa hay không
                        {
                            dr.Delete();
                            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adap_KhachHang);
                            adap_KhachHang.Update(dsKH, "KhachHang");
                            MessageBox.Show("Xóa thành công Khách Hàng " + strMa);
                            txtKH_TrangThaiLoad();
                            btnKH_TrangThaiLoad();
                        }
                        else
                        {
                            MessageBox.Show("Không tồn tại Khách Hàng để xóa");
                            txtKH_TrangThaiLoad();
                            btnKH_TrangThaiLoad();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        txtKH_TrangThaiLoad();
                        btnKH_TrangThaiLoad();
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dòng bạn muốn xóa !");
                txtKH_TrangThaiLoad();
                btnKH_TrangThaiLoad();
            }
        }

        private void btnTimKiem_KH_Click(object sender, EventArgs e)
        {
            if (txtTimKiem_KH.Text.Length == 0)
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txtTimKiem_KH.Text.Length > 0 && rdoMaKH.Checked == false && rdoTenKH.Checked == false)
                {
                    MessageBox.Show("Vui Lòng Chọn Đúng Kiểu Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTimKiem_KH.Text = "";
                    Load_dgvKhachHang();
                }
                else if (rdoMaKH.Checked)
                {
                    txtTimKiem_KH.Enabled = true;
                    dtvKhachHang.RowFilter = "MaKhachHang='" + txtTimKiem_KH.Text + "'";
                    grvKhachHang.DataSource = dtvKhachHang;
                }

                else if (rdoTenKH.Checked)
                {
                    txtTimKiem_KH.Enabled = true;
                    dtvKhachHang.RowFilter = "TenKhachHang='" + txtTimKiem_KH.Text + "'";
                    grvKhachHang.DataSource = dtvKhachHang;
                }
            }
        }
        //===========================================
        //Xử Lý Nhân Viên
        //===========================================
        public void btnNV_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_btn_NhanVien.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Xem In")
                    {
                        s.Enabled = false;
                    }
                    else
                    {
                        s.Enabled = false;
                    }
                }
            }
        }

        public void txtNV_TrangThaiLoad()
        {
            foreach (Control s in this.tbl_txt_NhanVien.Controls)//clear tất cả txt đã chọn khi xóa
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Text = string.Empty;
                    s.Enabled = false;
                }
            }
        }

        void Load_dgvNhanVien()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];

            string strsel = "select * from NhanVien";
            //dtNhanVien = conn.getDataTable(strsel, "NhanVien");
            adap_NhanVien = new SqlDataAdapter(strsel, conn.Connect);
            adap_NhanVien.Fill(dsNV, "NhanVien");
            primaryKey[0] = dsNV.Tables["NhanVien"].Columns["MaNhanVien"];
            dsNV.Tables["NhanVien"].PrimaryKey = primaryKey;


            dtvNhanVien = new DataView(dsNV.Tables["NhanVien"]);
            grvNV.DataSource = dtvNhanVien;
            grvNV.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
            grvNV.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
            grvNV.Columns["QueQuan"].HeaderText = "Quê Quán";
            grvNV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            grvNV.Columns["CMND"].HeaderText = "CCCD";

            grvNV.ReadOnly = true;
            grvNV.AllowUserToAddRows = false;
        }

        private void grvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenNV_NV.Text = grvNV.CurrentRow.Cells[1].Value.ToString();
            txtQueQuanNV_NV.Text = grvNV.CurrentRow.Cells[2].Value.ToString();
            txtNgaySinhNV_NV.Text = grvNV.CurrentRow.Cells[3].Value.ToString();
            txtCccdNV_NV.Text = grvNV.CurrentRow.Cells[4].Value.ToString();
        }

        public void enableNhanVien(bool e)
        {
            //xử lý enable Nhân Viên
            foreach (Control s in this.tbl_txt_NhanVien.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox) || s.GetType() == typeof(MaskedTextBox) || s.GetType() == typeof(RichTextBox))
                {
                    s.Enabled = e;
                }
            }
        }

        private void btnThem_NV_Click(object sender, EventArgs e)
        {
            foreach (Control s in this.tbl_btn_NhanVien.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Hủy" || s.Text == "Lưu")
                    {
                        s.Enabled = true;
                    }
                    else
                    {
                        if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Sửa")
                        {
                            s.Enabled = false;
                        }
                    }
                }
            }
            enableNhanVien(true);
            txtTenNV_NV.Focus();
        }

        private void btnLuu_NV_Click(object sender, EventArgs e)
        {
            foreach (Control s in this.tbl_btn_NhanVien.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    if (s.Text == "Thêm" || s.Text == "Xoá" || s.Text == "Sửa")
                    {
                        s.Enabled = true;
                    }

                }
            }
        }

        //============================================
        //Xử Lý Lập Phiếu Nhập
        //============================================

        void Load_dgvSach_LapPhieuNhap()
        {
            grvDSSach_LapPN.DataSource = dtvSach;
            grvDSSach_LapPN.Columns["MaSach"].HeaderText = "Mã Sách";
            grvDSSach_LapPN.Columns["TenSach"].HeaderText = "Tên Sách";
            grvDSSach_LapPN.Columns["MaTheLoai"].Visible = false;
            grvDSSach_LapPN.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            grvDSSach_LapPN.Columns["MaNhaXuatBan"].HeaderText = "Mã Nhà Xuất Bản";
            grvDSSach_LapPN.Columns["NoiDungTomTat"].Visible = false;
            grvDSSach_LapPN.Columns["NamXuatBan"].Visible = false;
            grvDSSach_LapPN.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            grvDSSach_LapPN.Columns["GiaBan"].HeaderText = "Giá Bán";
            grvDSSach_LapPN.Columns["SoLuongCon"].HeaderText = "Số Lượng Còn";
            btnLapPhieuNhap_TrangThaiLoad(false);
            txtLapPhieuNhap_TrangThaiLoad(false);
            LoadCboNCC_LapPN();
            btnGhiPN_LapPN.Enabled = false;
        }

        void Load_grvChiTietPhieuNhap()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from ChiTietPhieuNhap";
            adap_LapPhieunhap = new SqlDataAdapter(strsel, conn.Connect);
            adap_LapPhieunhap.Fill(dsLapPhieunhap, "ChiTietPhieuNhap");
            grvChiTietPN_LapPN.ReadOnly = true;
            grvChiTietPN_LapPN.AllowUserToAddRows = false;

            grvDSSach_LapPN.ReadOnly = true;
            grvDSSach_LapPN.AllowUserToAddRows = false;

            txtTongTien_LapPN.Enabled = false;
        }

        void Load_grvPhieuNhap()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from PhieuNhap";
            adap_Phieunhap = new SqlDataAdapter(strsel, conn.Connect);
            adap_Phieunhap.Fill(dsPhieuNhap, "PhieuNhap");
            grvXemPN_XemPN.DataSource = dsPhieuNhap.Tables["PhieuNhap"];
            primaryKey[0] = dsPhieuNhap.Tables["PhieuNhap"].Columns["MaPhieuNhap"];
            dsPhieuNhap.Tables["PhieuNhap"].PrimaryKey = primaryKey;
            dtvPhieuNhap = new DataView(dsPhieuNhap.Tables["PhieuNhap"]);
            grvXemPN_XemPN.ReadOnly = true;
            grvXemPN_XemPN.AllowUserToAddRows = false;

            grvXemPN_XemPN.Columns["MaPhieuNhap"].HeaderText = "Mã Phiếu Nhập";
            grvXemPN_XemPN.Columns["MaNhaCungCap"].HeaderText = "Mã Nhà Cung Cấp";
            grvXemPN_XemPN.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            grvXemPN_XemPN.Columns["TongTien"].HeaderText = "Tổng Tiền";

        }

        void LoadCboNCC_LapPN()
        {
            cboNCC_LapPN.DataSource = dsNCC.Tables["NhaCungCap"];
            cboNCC_LapPN.DisplayMember = "TenNhaCungCap";
            cboNCC_LapPN.ValueMember = "MaNhaCungCap";
        }

        private void btnTimSach_LapPN_Click(object sender, EventArgs e)
        {
            DataView dtvSach_PhieuNhap = new DataView(dsSach.Tables["Sach"]);
            if (txtTenSach_LapPN.Text.Length > 0)
            {
                dtvSach_PhieuNhap.RowFilter = "TenSach='" + txtTenSach_LapPN.Text + "'";
                grvDSSach_LapPN.DataSource = dtvSach_PhieuNhap;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuy_LapPN_Click(object sender, EventArgs e)
        {
            txtTenSach_LapPN.Clear();
            Load_dgvSach_LapPhieuNhap();
        }

        private void btnGhiPN_LapPN_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtGiaNhap_LapPhieuNhap.Text) < int.Parse(grvDSSach_LapPN.CurrentRow.Cells[8].Value.ToString()))
            {
                if (txtSoLuong_LapPN.Text.Trim().Length > 0)
                {
                    try
                    {
                        var index = grvChiTietPN_LapPN.Rows.Add();//tạo dòng mới trên grid
                        grvChiTietPN_LapPN.Rows[index].Cells["MaPhieuNhap"].Value = txtMaPhieu_LapPN.Text;
                        grvChiTietPN_LapPN.Rows[index].Cells["MaSach"].Value = grvDSSach_LapPN.CurrentRow.Cells[0].Value.ToString();
                        grvChiTietPN_LapPN.Rows[index].Cells["SoLuongNhap"].Value = txtSoLuong_LapPN.Text;
                        grvChiTietPN_LapPN.Rows[index].Cells["DonGia"].Value = txtGiaNhap_LapPhieuNhap.Text;
                        int thanhTien = int.Parse(txtSoLuong_LapPN.Text) * int.Parse(txtGiaNhap_LapPhieuNhap.Text);
                        grvChiTietPN_LapPN.Rows[index].Cells["ThanhTien"].Value = thanhTien.ToString();
                        int tongTien = 0;
                        foreach (DataGridViewRow item in grvChiTietPN_LapPN.Rows)
                        {
                            tongTien += int.Parse(item.Cells[4].Value.ToString());
                        }
                        txtTongTien_LapPN.Text = tongTien.ToString();
                        if (grvChiTietPN_LapPN.Rows.Count > 0)
                        {
                            btnLapPhieuNhap_TrangThaiLoad(true);
                        }
                        btnGhiPN_LapPN.Enabled = false;
                        btnLapPhieuNhap_TrangThaiLoad(true);
                        txtLapPhieuNhap_TrangThaiLoad(true);
                        MessageBox.Show("Thêm thành công chi tiết phiếu nhập !");

                    }
                    catch
                    {
                        MessageBox.Show("Thêm không thành công");
                        btnLapPhieuNhap_TrangThaiLoad(false);
                        txtLapPhieuNhap_TrangThaiLoad(false);
                    }
                }
                else
                {
                    btnLapPhieuNhap_TrangThaiLoad(false);
                    txtLapPhieuNhap_TrangThaiLoad(false);
                    MessageBox.Show("Phải thêm đầy đủ số lượng để Lập Phiếu Nhập!");

                }
            }
            else
            {
                MessageBox.Show("Giá nhập phải nhỏ hơn giá bán !");
                txtGiaNhap_LapPhieuNhap.Clear();
                txtGiaNhap_LapPhieuNhap.Focus();
            }
        }

        private void btnThemCTPN_LapPN_Click(object sender, EventArgs e)
        {
            txtLapPhieuNhap_TrangThaiLoad(true);
            btnGhiPN_LapPN.Enabled = true;
            txtSoLuong_LapPN.Focus();
        }

        public void txtLapPhieuNhap_TrangThaiLoad(bool e)
        {
            foreach (Control s in this.tbl_txtLapPhieuNhap.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Enabled = e;
                }
                txtMaTenSach_LapPN.Enabled = false;
                txtMaPhieu_LapPN.Enabled = false;
            }
        }

        public void btnLapPhieuNhap_TrangThaiLoad(bool e)
        {
            //xử lý enable Sách
            foreach (Control s in this.tbl_btnLapPhieuNhap.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    s.Enabled = e;
                }
            }
        }

        private void grvDSSach_LapPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhieu_LapPN.Text = "PN00" + (grvXemPN_XemPN.Rows.Count + 1).ToString();
            txtMaTenSach_LapPN.Text = grvDSSach_LapPN.CurrentRow.Cells[0].Value.ToString() + "-" + grvDSSach_LapPN.CurrentRow.Cells[1].Value.ToString();

        }

        private void btnLapPhieu_LapPN_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = dsPhieuNhap.Tables["PhieuNhap"].NewRow(); //Tạo dòng mới
                newRow["MaPhieuNhap"] = txtMaPhieu_LapPN.Text;
                newRow["MaNhanVien"] = null;
                newRow["MaNhaCungCap"] = cboNCC_LapPN.SelectedValue.ToString();
                newRow["NgayNhap"] = Convert.ToString(DateTime.Now);
                newRow["TongTien"] = txtTongTien_LapPN.Text; ;
                dsPhieuNhap.Tables["PhieuNhap"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                SqlCommandBuilder cmdBuilder1 = new SqlCommandBuilder(adap_Phieunhap);
                adap_Phieunhap.Update(dsPhieuNhap, "PhieuNhap");

                //Thêm các cùng lúc các chi tiết phiếu nhập xuống sql
                foreach (DataGridViewRow dt in grvChiTietPN_LapPN.Rows)
                {
                    DataRow newRow1 = dsLapPhieunhap.Tables["ChiTietPhieuNhap"].NewRow(); //Tạo dòng mới
                    newRow1["MaPhieuNhap"] = txtMaPhieu_LapPN.Text;
                    newRow1["MaSach"] = dt.Cells[1].Value.ToString();
                    newRow1["SoLuongNhap"] = dt.Cells[2].Value.ToString();
                    newRow1["DonGia"] = dt.Cells[3].Value.ToString();
                    newRow1["ThanhTien"] = dt.Cells[4].Value.ToString();
                    dsLapPhieunhap.Tables["ChiTietPhieuNhap"].Rows.Add(newRow1);
                    SqlCommandBuilder cmdBuilder2 = new SqlCommandBuilder(adap_LapPhieunhap);
                    adap_LapPhieunhap.Update(dsLapPhieunhap, "ChiTietPhieuNhap");
                }
                MessageBox.Show("Thêm thành công Chi Tiết Phiếu Nhập !");

                grvChiTietPN_LapPN.Rows.Clear();
                Load_dgvSach();
                Load_dgvSach_LapPhieuNhap();
                txtLapPhieuNhap_TrangThaiLoad(false);
            }
            catch
            {
                MessageBox.Show("Lập không thành công Phiếu Nhập");
            }


        }

        private void btnXoaCTPN_LapPN_Click(object sender, EventArgs e)
        {
            if (grvChiTietPN_LapPN.CurrentRow != null)
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        grvChiTietPN_LapPN.Rows.Remove(grvChiTietPN_LapPN.CurrentRow);
                        MessageBox.Show("Xóa thành công chi tiết phiếu nhập !");
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        btnLapPhieuNhap_TrangThaiLoad(false);
                        txtLapPhieuNhap_TrangThaiLoad(false);
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
        }

        private void btnHuyLap_LapPN_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn chắc chắn muốn hủy không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)//Nếu đồng ý xóa
            {
                try
                {
                    grvChiTietPN_LapPN.Rows.Clear();
                    MessageBox.Show("Hủy thành công Phiếu Nhập !");
                    btnLapPhieuNhap_TrangThaiLoad(false);
                    txtLapPhieuNhap_TrangThaiLoad(false);
                }
                catch
                {
                    MessageBox.Show("Hủy không thành công");
                    btnLapPhieuNhap_TrangThaiLoad(false);
                    txtLapPhieuNhap_TrangThaiLoad(false);
                }

            }
            else //không đồng ý thì thoát
            {
                return;
            }
        }

        private void btnTimKiem_XemPN_Click(object sender, EventArgs e)
        {
            if (txtTimKiem_XemPN.Text.Length > 0)
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.TimKiemPhieuNhap (@maph)", conn.Connect);
                cmd.Parameters.AddWithValue("@maph", txtTimKiem_XemPN.Text);
                DataTable dtPN = new DataTable();
                adap_Phieunhap = new SqlDataAdapter(cmd);
                adap_Phieunhap.Fill(dtPN);

                grvXemPN_XemPN.DataSource = dtPN;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXemIn_Sach_Click(object sender, EventArgs e)
        {
            MyReport_Sach myrpt_sach = new MyReport_Sach();
            myrpt_sach.ShowDialog();
        }
        public string str1 = "";

        private void btnIn_XemPN_Click(object sender, EventArgs e)
        {
            str1 = grvXemPN_XemPN.CurrentRow.Cells[0].Value.ToString();
            MyReport_ChiTietPN r = new MyReport_ChiTietPN(str1);
            r.ShowDialog();
        }

        private void btnHuyTim_XemPN_Click(object sender, EventArgs e)
        {
            txtTimKiem_XemPN.Clear();
            Load_grvPhieuNhap();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Đăng Xuất?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frm_DangNhap frm = new frm_DangNhap();
                this.Hide();
                frm.ShowDialog();
            }
        }

        private void btnThoatChuongTrinh_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGiaNhap_LapPhieuNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        //============================================
        //Xử Lý Lập Hóa Đơn
        //============================================
        void Load_dgvSach_LapHoaDon()
        {
            grvDSSach_HoaDon.DataSource = dtvSach;
            grvDSSach_HoaDon.Columns["MaSach"].HeaderText = "Mã Sách";
            grvDSSach_HoaDon.Columns["TenSach"].HeaderText = "Tên Sách";
            grvDSSach_HoaDon.Columns["MaTheLoai"].Visible = false;
            grvDSSach_HoaDon.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            grvDSSach_HoaDon.Columns["MaNhaXuatBan"].HeaderText = "Mã Nhà Xuất Bản";
            grvDSSach_HoaDon.Columns["NoiDungTomTat"].Visible = false;
            grvDSSach_HoaDon.Columns["NamXuatBan"].Visible = false;
            grvDSSach_HoaDon.Columns["GiaNhap"].Visible = false;
            grvDSSach_HoaDon.Columns["GiaBan"].HeaderText = "Giá Bán";
            grvDSSach_HoaDon.Columns["SoLuongCon"].HeaderText = "Số Lượng Còn";
            btnLapHoaDon_TrangThaiLoad(false);
            txtLapHoaDon_TrangThaiLoad(false);
            LoadCboKH_LapHD();
            btnGhiCTHD.Enabled = false;
        }

        void Load_grvChiTietHoaDon()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from ChiTietHoaDon";
            adap_LapHoaDon = new SqlDataAdapter(strsel, conn.Connect);
            adap_LapHoaDon.Fill(dsLapHoaDon, "ChiTietHoaDon");
            grvChiTietHoaDon.ReadOnly = true;
            grvChiTietHoaDon.AllowUserToAddRows = false;

            grvDSSach_HoaDon.ReadOnly = true;
            grvDSSach_HoaDon.AllowUserToAddRows = false;

            txtTongTienHoaDon.Enabled = false;
        }

        void Load_grvHoaDon()
        {
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            string strsel = "select * from HoaDon";
            adap_HoaDon = new SqlDataAdapter(strsel, conn.Connect);
            adap_HoaDon.Fill(dsHoaDon, "HoaDon");
            grvXemHD.DataSource = dsHoaDon.Tables["HoaDon"];
            primaryKey[0] = dsHoaDon.Tables["HoaDon"].Columns[0];
            dsHoaDon.Tables["HoaDon"].PrimaryKey = primaryKey;
            dtvHoaDon = new DataView(dsHoaDon.Tables["HoaDon"]);
            grvXemHD.ReadOnly = true;
            grvXemHD.AllowUserToAddRows = false;

            grvXemHD.Columns["SoHoaDon"].HeaderText = "Mã Hóa Đơn";
            grvXemHD.Columns["MaKhachHang"].HeaderText = "Mã Khách Hang";
            grvXemHD.Columns["NgayLap"].HeaderText = "Ngày Lập";
            grvXemHD.Columns["TongTien"].HeaderText = "Tổng Tiền";

        }

        void LoadCboKH_LapHD()
        {
            cboKhachHang_HoaDon.DataSource = dsKH.Tables["KhachHang"];
            cboKhachHang_HoaDon.DisplayMember = "TenKhachHang";
            cboKhachHang_HoaDon.ValueMember = "MaKhachHang";
        }

        public void txtLapHoaDon_TrangThaiLoad(bool e)
        {
            foreach (Control s in this.tbl_txtLapPhieuNhap.Controls)//Trạng thái btn khi load
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroTextBox))
                {
                    s.Enabled = e;
                }
                txtMaTenSach_HoaDon.Enabled = false;
                txtSoHD_HoaDon.Enabled = false;
            }
        }

        public void btnLapHoaDon_TrangThaiLoad(bool e)
        {
            //xử lý enable Sách
            foreach (Control s in this.tbl_btnLapPhieuNhap.Controls)
            {
                if (s.GetType() == typeof(MetroFramework.Controls.MetroButton))
                {
                    s.Enabled = e;
                }
            }
        }
        private void btnTimKiem_HoaDon_Click(object sender, EventArgs e)
        {
            DataView dtvSach_HoaDon = new DataView(dsSach.Tables["Sach"]);
            if (txtTenSach_HoaDon.Text.Length > 0)
            {
                dtvSach_HoaDon.RowFilter = "TenSach='" + txtTenSach_HoaDon.Text + "'";
                grvDSSach_HoaDon.DataSource = dtvSach_HoaDon;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuyTim_HoaDon_Click(object sender, EventArgs e)
        {
            txtTenSach_HoaDon.Clear();
            Load_dgvSach_LapHoaDon();
        }
        private void btnGhiCTHD_Click(object sender, EventArgs e)
        {
            if (txtSoLuong_HoaDon.Text.Trim().Length > 0)
            {
                try
                {
                    var index = grvChiTietHoaDon.Rows.Add();//tạo dòng mới trên grid
                    grvChiTietHoaDon.Rows[index].Cells["SoHD"].Value = txtSoHD_HoaDon.Text;
                    grvChiTietHoaDon.Rows[index].Cells["MaSH"].Value = grvDSSach_HoaDon.CurrentRow.Cells[0].Value.ToString();
                    grvChiTietHoaDon.Rows[index].Cells["SoLuongBan"].Value = txtSoLuong_HoaDon.Text;
                    grvChiTietHoaDon.Rows[index].Cells["DonGia_HD"].Value = grvDSSach_HoaDon.CurrentRow.Cells["GiaBan"].Value.ToString();
                    float thanhTien = float.Parse(txtSoLuong_HoaDon.Text) * float.Parse(grvDSSach_HoaDon.CurrentRow.Cells["GiaBan"].Value.ToString());
                    grvChiTietHoaDon.Rows[index].Cells["ThanhTien_HD"].Value = thanhTien.ToString();
                    float tongTien = 0;
                    foreach (DataGridViewRow item in grvChiTietHoaDon.Rows)
                    {
                        tongTien += float.Parse(item.Cells[4].Value.ToString());
                    }
                    txtTongTienHoaDon.Text = tongTien.ToString();
                    if (grvChiTietHoaDon.Rows.Count > 0)
                    {
                        btnLapHoaDon_TrangThaiLoad(true);
                    }
                    btnGhiPN_LapPN.Enabled = false;
                    btnLapHoaDon_TrangThaiLoad(true);
                    txtLapHoaDon_TrangThaiLoad(true);
                    MessageBox.Show("Thêm thành công chi tiết hóa đơn!");

                }
                catch
                {
                    MessageBox.Show("Thêm không thành công");
                    btnLapHoaDon_TrangThaiLoad(true);
                    txtLapHoaDon_TrangThaiLoad(true);
                }
            }
            else
            {
                btnLapHoaDon_TrangThaiLoad(true);
                txtLapHoaDon_TrangThaiLoad(true);
                MessageBox.Show("Phải thêm đầy đủ số lượng để Lập Hóa Đơn!");

            }
        }

        private void grvDSSach_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHD_HoaDon.Text = "HD00" + (grvXemHD.Rows.Count + 1).ToString();
            txtMaTenSach_HoaDon.Text = grvDSSach_HoaDon.CurrentRow.Cells[0].Value.ToString() + "-" + grvDSSach_HoaDon.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            txtLapHoaDon_TrangThaiLoad(true);
            btnGhiCTHD.Enabled = true;
            txtSoLuong_HoaDon.Focus();
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = dsHoaDon.Tables["HoaDon"].NewRow(); //Tạo dòng mới
                newRow["SoHoaDon"] = txtSoHD_HoaDon.Text;
                newRow["MaNhanVien"] = null;
                newRow["MaKhachHang"] = cboKhachHang_HoaDon.SelectedValue.ToString();
                newRow["NgayLap"] = Convert.ToString(DateTime.Now);
                newRow["TongTien"] = txtTongTienHoaDon.Text; ;
                dsHoaDon.Tables["HoaDon"].Rows.Add(newRow);//Thêm dòng mới vào dataset
                SqlCommandBuilder cmdBuilder1 = new SqlCommandBuilder(adap_HoaDon);
                adap_HoaDon.Update(dsHoaDon, "HoaDon");

                //Thêm các cùng lúc các chi tiết phiếu nhập xuống sql
                foreach (DataGridViewRow dt in grvChiTietHoaDon.Rows)
                {
                    DataRow newRow1 = dsLapHoaDon.Tables["ChiTietHoaDon"].NewRow(); //Tạo dòng mới
                    newRow1["SoHoaDon"] = txtSoHD_HoaDon.Text;
                    newRow1["MaSach"] = dt.Cells[1].Value.ToString();
                    newRow1["SoLuongBan"] = dt.Cells[2].Value.ToString();
                    newRow1["GiaBan"] = dt.Cells[3].Value.ToString();
                    newRow1["ThanhTien"] = dt.Cells[4].Value.ToString();
                    dsLapHoaDon.Tables["ChiTietHoaDon"].Rows.Add(newRow1);
                    SqlCommandBuilder cmdBuilder2 = new SqlCommandBuilder(adap_LapHoaDon);
                    adap_LapHoaDon.Update(dsLapHoaDon, "ChiTietHoaDon");
                }
                MessageBox.Show("Thêm thành công Chi Tiết Hóa Đơn !");

                grvChiTietHoaDon.Rows.Clear();
                Load_dgvSach();
                Load_dgvSach_LapHoaDon();
                txtLapHoaDon_TrangThaiLoad(false);
            }
            catch
            {
                MessageBox.Show("Lập không thành công Hóa Đơn");
            }
        }

        private void btnXoaCTHD_Click(object sender, EventArgs e)
        {
            if (grvChiTietHoaDon.CurrentRow != null)
            {
                DialogResult r;
                r = MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)//Nếu đồng ý xóa
                {
                    try
                    {
                        grvChiTietHoaDon.Rows.Remove(grvChiTietHoaDon.CurrentRow);
                        MessageBox.Show("Xóa thành công chi tiết hóa đơn !");
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công");
                        btnLapHoaDon_TrangThaiLoad(false);
                        txtLapHoaDon_TrangThaiLoad(false);
                    }
                }
                else //không đồng ý thì thoát
                {
                    return;
                }
            }
        }

        private void btnHuyLap_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn chắc chắn muốn hủy không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)//Nếu đồng ý xóa
            {
                try
                {
                    grvChiTietHoaDon.Rows.Clear();
                    MessageBox.Show("Hủy thành công Hóa Đơn !");
                    btnLapHoaDon_TrangThaiLoad(false);
                    txtLapHoaDon_TrangThaiLoad(false);
                }
                catch
                {
                    MessageBox.Show("Hủy không thành công");
                    btnLapHoaDon_TrangThaiLoad(false);
                    txtLapHoaDon_TrangThaiLoad(false);
                }

            }
            else //không đồng ý thì thoát
            {
                return;
            }
        }

        private void btnHuy_XemHD_Click(object sender, EventArgs e)
        {
            txtSoHD_XemHoaDon.Clear();
            Load_grvHoaDon();
        }

        private void btnXemIn_HD_Click(object sender, EventArgs e)
        {
            string str2 = grvXemHD.CurrentRow.Cells[0].Value.ToString();
            MyReport_ChiTietHD r = new MyReport_ChiTietHD(str2);
            r.ShowDialog();
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            if (rdoNgay_ThongKeDoanhThu.Checked != true && rdoThongKeTheLoai.Checked != true && rdoThongKeNXB.Checked != true)
            {
                MessageBox.Show("Vui lòng chọn một mục để xem thống kê!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (rdoNgay_ThongKeDoanhThu.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("select * from dbo.DoanhThu_TinhTheoNgay (@TuNgay, @DenNgay)", conn.Connect);
                    cmd.Parameters.AddWithValue("@TuNgay", txtTuNgay_ThongKeDoanhThu.Text);
                    cmd.Parameters.AddWithValue("@DenNgay", txtDenNgay_ThongKeDoanhThu.Text);

                    txtDoanhThu.Text = cmd.ExecuteScalar().ToString();

                }
                else if (rdoThongKeTheLoai.Checked == true)
                {
                    SqlCommand cmd1 = new SqlCommand("select * from dbo.ThongKeDoanhThuSach_TheoTheLoai (@matl)", conn.Connect);
                    cmd1.Parameters.AddWithValue("@matl", cboThongKeTheLoai.SelectedValue.ToString());
                    txtDoanhThu.Text = cmd1.ExecuteScalar().ToString();

                }
                else if (rdoThongKeNXB.Checked == true)
                {
                    SqlCommand cmd2 = new SqlCommand("select * from dbo.ThongKeDoanhThuSach_TheoNXB (@manxb)", conn.Connect);
                    cmd2.Parameters.AddWithValue("@manxb", cboThongKeNXB.SelectedValue.ToString());
                    txtDoanhThu.Text = cmd2.ExecuteScalar().ToString();
                }
            }
        }

        void Load_ThongKe()
        {
            txtDoanhThu.Enabled = false;

            string str_select = "select * from TheLoai";
            DataTable dt = conn.getDataTable(str_select, "TheLoai");

            cboThongKeTheLoai.DataSource = dt;
            cboThongKeTheLoai.DisplayMember = "TenTheLoai";
            cboThongKeTheLoai.ValueMember = "MaTheLoai";


            string str_select1 = "select * from NhaXuatBan";
            DataTable dt1 = conn.getDataTable(str_select1, "NhaXuatBan");

            cboThongKeNXB.DataSource = dt1;
            cboThongKeNXB.DisplayMember = "TenXuatBan";
            cboThongKeNXB.ValueMember = "MaNhaXuatBan";

            string str_select2 = "select * from KhachHang";
            DataTable dt2 = conn.getDataTable(str_select2, "KhachHang");

            cboKhachHang_ThongKe.DataSource = dt2;
            cboKhachHang_ThongKe.DisplayMember = "TenKhachHang";
            cboKhachHang_ThongKe.ValueMember = "MaKhachHang";

        }

        private void btnThongKeSoLuong_Click(object sender, EventArgs e)
        {
            if (rdoThongKeSachKhachHang.Checked != true && rdoSachGanHet.Checked != true && rdoSachHet.Checked != true)
            {
                MessageBox.Show("Vui lòng chọn một mục để xem thống kê!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (rdoThongKeSachKhachHang.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("select * from dbo.SL_SachKHMua (@makh)", conn.Connect);
                    cmd.Parameters.AddWithValue("@makh", cboKhachHang_ThongKe.SelectedValue.ToString());
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    grvThongKeSach.DataSource = dt;
                }
                else if (rdoSachGanHet.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("exec Proc_ThongKeSachGanHet select * from ChiTietHoaDon", conn.Connect);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    grvThongKeSach.DataSource = dt;

                }
                else if (rdoSachHet.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("exec Proc_ThongKeSachHet select * from ChiTietHoaDon", conn.Connect);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    grvThongKeSach.DataSource = dt;
                }
            }


        }

        private void btnTimKiem_XemHD_Click(object sender, EventArgs e)
        {
            if (txtSoHD_XemHoaDon.Text.Length > 0)
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.TimKiemHoaDon (@mahd)", conn.Connect);
                cmd.Parameters.AddWithValue("@mahd", txtSoHD_XemHoaDon.Text);
                DataTable dtHD = new DataTable();
                adap_HoaDon = new SqlDataAdapter(cmd);
                adap_HoaDon.Fill(dtHD);

                grvXemHD.DataSource = dtHD;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Vào Ô Để Tìm Kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




















    }
}
