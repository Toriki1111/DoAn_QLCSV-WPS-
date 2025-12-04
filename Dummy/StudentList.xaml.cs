using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq; // Để dùng Live Search

namespace DoAn_QLCSV
{
    public partial class StudentList : UserControl
    {
        List<SinhVien> _listSV; // Danh sách gốc

        public StudentList()
        {
            InitializeComponent();
            LoadData();
        }

        // 1. DATA MODEL (Cập nhật full trường theo DB)
        public class SinhVien
        {
            public string MaSV { get; set; }        // student_id
            public string HoTen { get; set; }       // full_name (users table)
            public DateTime NgaySinh { get; set; }
            public string Bio { get; set; }         // bio

            public string NienKhoa { get; set; }    // batches name
            public int NamTotNghiep { get; set; }   // graduation_year
            public string Nganh { get; set; }       // majors name

            public string ViTri { get; set; }       // current_position
            public string CongTy { get; set; }      // company

            public string Email { get; set; }       // email
            public string City { get; set; }        // city
            public string Country { get; set; }     // country
            public string LinkedIn { get; set; }    // linkedin_url
            public string Facebook { get; set; }    // facebook_url
        }

        private void LoadData()
        {
            // Data giả nhưng đầy đủ
            _listSV = new List<SinhVien>()
            {
                new SinhVien { MaSV = "2001101", HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(2002, 5, 10),
                               NienKhoa = "K18", NamTotNghiep = 2024, Nganh = "Công Nghệ Thông Tin",
                               ViTri = "Backend Developer", CongTy = "FPT Software",
                               Email = "an.nguyen@email.com", City = "Hồ Chí Minh", Country = "Việt Nam", Bio="Đam mê code dạo." },

                new SinhVien { MaSV = "2001102", HoTen = "Trần Thị Bích", NgaySinh = new DateTime(2002, 8, 20),
                               NienKhoa = "K18", NamTotNghiep = 2024, Nganh = "Marketing",
                               ViTri = "Content Creator", CongTy = "Vinamilk",
                               Email = "bich.tran@email.com", City = "Hà Nội", Country = "Việt Nam", Bio="Thích viết lách." },

                new SinhVien { MaSV = "2001103", HoTen = "Lê Hoàng Nam", NgaySinh = new DateTime(2001, 1, 15),
                               NienKhoa = "K17", NamTotNghiep = 2023, Nganh = "Kinh Doanh Quốc Tế",
                               ViTri = "Sales Manager", CongTy = "Shopee",
                               Email = "nam.le@email.com", City = "Đà Nẵng", Country = "Việt Nam", Bio="Sales đỉnh cao." },

                new SinhVien { MaSV = "2001104", HoTen = "Phạm Minh Tuấn", NgaySinh = new DateTime(2000, 12, 5),
                               NienKhoa = "K16", NamTotNghiep = 2022, Nganh = "Công Nghệ Thông Tin",
                               ViTri = "Fullstack Dev", CongTy = "VNG",
                               Email = "tuan.pham@email.com", City = "Hồ Chí Minh", Country = "Việt Nam", LinkedIn="linkedin.com/in/tuanpham", Bio="Code là cuộc sống." },
            };

            RefreshGrid(_listSV);
        }

        private void RefreshGrid(List<SinhVien> data)
        {
            dgSinhVien.ItemsSource = null;
            dgSinhVien.ItemsSource = data;
            if (txtTotal != null) txtTotal.Text = data.Count.ToString();
        }

        // 2. CHỌN DÒNG -> HIỆN CHI TIẾT (Master-Detail)
        private void dgSinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as SinhVien;
            if (sv != null)
            {
                txtMaSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                dpNgaySinh.SelectedDate = sv.NgaySinh;
                txtBio.Text = sv.Bio;

                txtNienKhoa.Text = sv.NienKhoa;
                txtNamTotNghiep.Text = sv.NamTotNghiep.ToString();
                txtNganh.Text = sv.Nganh;

                txtViTri.Text = sv.ViTri;
                txtCongTy.Text = sv.CongTy;

                txtEmail.Text = sv.Email;
                txtCity.Text = sv.City;
                txtCountry.Text = sv.Country;
                txtLinkedIn.Text = sv.LinkedIn;
                txtFacebook.Text = sv.Facebook;
            }
        }

        // 3. TÌM KIẾM THÔNG MINH (Live Search)
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string k = txtSearch.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(k)) { RefreshGrid(_listSV); return; }

            var result = _listSV.Where(sv =>
                (sv.HoTen?.ToLower().Contains(k) ?? false) ||
                (sv.MaSV?.ToLower().Contains(k) ?? false) ||
                (sv.CongTy?.ToLower().Contains(k) ?? false) ||
                (sv.Nganh?.ToLower().Contains(k) ?? false)
            )
            .OrderByDescending(sv =>
                (sv.HoTen?.ToLower().StartsWith(k) ?? false) ||
                (sv.MaSV?.ToLower().StartsWith(k) ?? false)
            )
            .ToList();

            RefreshGrid(result);
        }

        // 4. LƯU SỬA (Update)
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as SinhVien;
            if (sv == null) { MessageBox.Show("Vui lòng chọn sinh viên cần sửa!"); return; }

            // Cập nhật lại Object từ TextBox
            sv.HoTen = txtHoTen.Text;
            sv.NgaySinh = dpNgaySinh.SelectedDate ?? DateTime.Now;
            sv.Bio = txtBio.Text;
            sv.NienKhoa = txtNienKhoa.Text;
            if (int.TryParse(txtNamTotNghiep.Text, out int nam)) sv.NamTotNghiep = nam;
            sv.Nganh = txtNganh.Text;
            sv.ViTri = txtViTri.Text;
            sv.CongTy = txtCongTy.Text;
            sv.Email = txtEmail.Text;
            sv.City = txtCity.Text;
            sv.Country = txtCountry.Text;
            sv.LinkedIn = txtLinkedIn.Text;
            sv.Facebook = txtFacebook.Text;

            MessageBox.Show($"Đã cập nhật thông tin cho {sv.HoTen}!");
            RefreshGrid(_listSV); // Refresh bảng
        }

        // Các nút khác giữ nguyên logic cũ...
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addWin = new AddStudentWindow();
            if (addWin.ShowDialog() == true) { }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as SinhVien;
            if (sv != null && MessageBox.Show("Xóa?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _listSV.Remove(sv);
                RefreshGrid(_listSV);
                // Clear TextBox...
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            RefreshGrid(_listSV);
            dgSinhVien.SelectedItem = null;
            // Clear TextBox...
        }
    }
}