using NguyenVietHau_2280600893.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NguyenVietHau_2280600893
{
    public partial class frmSinhvien : Form
    {
        public frmSinhvien()
        {
            InitializeComponent();
        }
        private void LoadStudentData()
        {
            using (var context = new Model1())
            {
                var listStu = context.Sinhviens
                    .Select(s => new
                    {
                        s.MaSv,
                        s.HotenSV,
                        s.Ngaysinh,
                        s.Malop,
                        TenLop = s.Lop.TenLop
                    })
                    .ToList();

                dgvSinhvien.DataSource = listStu;

                dgvSinhvien.Columns["Ngaysinh"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss tt";

                dgvSinhvien.Columns["MaSv"].HeaderText = "Mã SV";
                dgvSinhvien.Columns["HotenSV"].HeaderText = "Họ và tên";
                dgvSinhvien.Columns["Ngaysinh"].HeaderText = "Ngày sinh";
                dgvSinhvien.Columns["Malop"].Visible = false;
                dgvSinhvien.Columns["TenLop"].HeaderText = "Lớp";
            }
        }

        private void EditStudent()
        {
            using (var context = new Model1())
            {
                var MaSv = txtMaSV.Text.Trim();
                var student = context.Sinhviens.FirstOrDefault(s => s.MaSv == MaSv);

                if (student != null)
                {
                    student.HotenSV = txtHotenSV.Text.Trim();
                    student.Ngaysinh = dtNgaysinh.Value;
                    student.Malop = cboLop.SelectedValue.ToString();

                    context.SaveChanges();
                    MessageBox.Show("Sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            LoadStudentData();
        }

        private void DeleteStudent()
        {
            using (var context = new Model1())
            {
                var MaSv = txtMaSV.Text.Trim();
                var student = context.Sinhviens.FirstOrDefault(s => s.MaSv == MaSv);

                if (student != null)
                {
                    DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xoá không?",
                    "Xác nhận xoá",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        context.Sinhviens.Remove(student);
                        context.SaveChanges();
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            LoadStudentData();
        }

        private void dgvSinhvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhvien.Rows[e.RowIndex];
                txtMaSV.Text = row.Cells["MaSv"].Value.ToString();
                txtHotenSV.Text = row.Cells["HotenSV"].Value.ToString();
                dtNgaysinh.Text = row.Cells["Ngaysinh"].Value.ToString();

                string facultyId = row.Cells["MaLop"].Value.ToString();
                cboLop.SelectedValue = facultyId;
            }
        }

        private void LoadFacultyData()
        {
            using (var context = new Model1())
            {
                var faculties = context.Lops.ToList();

                cboLop.DataSource = faculties;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            EditStudent();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn thoát không?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            var listStu1 = new BindingList<Sinhvien>();
            LoadStudentData();
            LoadFacultyData();
        }

        private void btThem_Click(object sender, EventArgs e)
        {

            using (var context = new Model1())
            {
                var newStudent = new Sinhvien
                {
                    MaSv = txtMaSV.Text.Trim(),
                    HotenSV = txtHotenSV.Text.Trim(),
                    Ngaysinh = dtNgaysinh.Value,
                    Malop = cboLop.SelectedValue.ToString()
                };

                if (!context.Sinhviens.Any(s => s.MaSv == newStudent.MaSv))
                {
                    context.Sinhviens.Add(newStudent);
                    context.SaveChanges();
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Student ID đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            btLuu.Enabled = true;
            btKhong.Enabled = true;
            LoadStudentData();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã lưu sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btLuu.Enabled = false;
            btKhong.Enabled = false;
            txtHotenSV.Clear();
            txtMaSV.Clear();
        }

        private void btKhong_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                var MaSv = txtMaSV.Text.Trim();
                var student = context.Sinhviens.FirstOrDefault(s => s.MaSv == MaSv);
                    context.Sinhviens.Remove(student);
                    context.SaveChanges();
                    MessageBox.Show("Đã xoá dữ liệu sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadStudentData();

            btLuu.Enabled = false;
            btKhong.Enabled = false;
            txtHotenSV.Clear();
            txtMaSV.Clear();
        }



        private void btTim_Click(object sender, EventArgs e)
        {
            string searchText = txtTim.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {

                foreach (DataGridViewRow row in dgvSinhvien.Rows)
                {
                   
                    if (row.Cells["HotenSV"].Value != null && row.Cells["HotenSV"].Value.ToString().ToLower().Contains(searchText.ToLower()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
            else
                foreach (DataGridViewRow row in dgvSinhvien.Rows)
                {
                    row.Visible = true;
                }
            }
        }
    }
