using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class StudentList : UserControl
    {
        List<AlumniDetailResponse> _listSV;

        public StudentList()
        {
            InitializeComponent();
            LoadData();
        }

        // DTO khớp với Backend
        public class AlumniDetailResponse
        {
            public long Id { get; set; }
            public string StudentId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string FacultyName { get; set; }
            public string MajorName { get; set; }
            public int GraduationYear { get; set; }
            public string CurrentPosition { get; set; }
            public string Company { get; set; }
            public string CompanyIndustry { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string LinkedinUrl { get; set; }
            public string FacebookUrl { get; set; }
            public string Bio { get; set; }
        }

        private void LoadData()
        {
            // Data giả lập theo cấu trúc mới
            _listSV = new List<AlumniDetailResponse>
            {
                new AlumniDetailResponse { StudentId="SV001", FullName="Nguyen Van A", Email="a@gmail.com", FacultyName="CNTT", MajorName="KTPM", GraduationYear=2023, Company="FPT", CurrentPosition="Dev", City="HCM" },
                new AlumniDetailResponse { StudentId="SV002", FullName="Tran Thi B", Email="b@gmail.com", FacultyName="Kinh Te", MajorName="QTKD", GraduationYear=2024, Company="Shopee", CurrentPosition="Marketing", City="Ha Noi" }
            };
            RefreshGrid(_listSV);
        }

        private void RefreshGrid(List<AlumniDetailResponse> data)
        {
            dgSinhVien.ItemsSource = null;
            dgSinhVien.ItemsSource = data;
            if (txtTotal != null) txtTotal.Text = data.Count.ToString();
        }

        private void dgSinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as AlumniDetailResponse;
            if (sv != null)
            {
                txtMaSV.Text = sv.StudentId;
                txtHoTen.Text = sv.FullName;
                txtEmail.Text = sv.Email;
                txtBio.Text = sv.Bio;

                txtFaculty.Text = sv.FacultyName;
                txtNganh.Text = sv.MajorName;
                txtNamTotNghiep.Text = sv.GraduationYear.ToString();

                txtCongTy.Text = sv.Company;
                txtViTri.Text = sv.CurrentPosition;
                txtIndustry.Text = sv.CompanyIndustry;

                txtCity.Text = sv.City;
                txtCountry.Text = sv.Country;
                txtLinkedIn.Text = sv.LinkedinUrl;
                txtFacebook.Text = sv.FacebookUrl;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string k = txtSearch.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(k)) { RefreshGrid(_listSV); return; }

            var result = _listSV.Where(sv =>
                (sv.FullName?.ToLower().Contains(k) ?? false) ||
                (sv.StudentId?.ToLower().Contains(k) ?? false) ||
                (sv.Company?.ToLower().Contains(k) ?? false)
            ).ToList();
            RefreshGrid(result);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as AlumniDetailResponse;
            if (sv == null) { MessageBox.Show("Chọn sinh viên để sửa!"); return; }

            // Map ngược từ UI vào Object
            sv.FullName = txtHoTen.Text;
            sv.Email = txtEmail.Text;
            sv.Company = txtCongTy.Text;
            // ... (Map tiếp các trường khác)

            MessageBox.Show("Cập nhật thành công (Giả lập)!");
            RefreshGrid(_listSV);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow win = new AddStudentWindow();
            if (win.ShowDialog() == true) { LoadData(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sv = dgSinhVien.SelectedItem as AlumniDetailResponse;
            if (sv != null && MessageBox.Show("Xóa?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _listSV.Remove(sv);
                RefreshGrid(_listSV);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e) => LoadData();
    }
}