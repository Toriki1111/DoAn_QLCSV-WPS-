using System;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class StudentProfile : UserControl
    {
        // Flags to allow editing specific fields when entering edit mode
        public bool AllowEditEmail { get; set; } = false;
        public bool AllowEditPhone { get; set; } = false;
        public bool AllowEditBirthdate { get; set; } = false; // Ngày sinh
        public bool AllowEditAddress { get; set; } = false;
        public bool AllowEditCompany { get; set; } = false;
        public bool AllowEditPosition { get; set; } = false;

        // Separate model class
        public class StudentProfileModel
        {
            public string MSSV { get; set; }
            public string HoTen { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Gender { get; set; }
            public DateTime NgaySinh { get; set; }
            public string Address { get; set; }
            public string Lop { get; set; }
            public string Major { get; set; }
            public int GradYear { get; set; }
            public string Company { get; set; }
            public string Position { get; set; }
            public string Industry { get; set; }
            public string CompanyLocation { get; set; }
            public string Achievements { get; set; }
        }

        private StudentProfileModel _currentProfile;
        private bool _isEditMode = false;

        public StudentProfile()
        {
            InitializeComponent();
            LoadProfileData("SV1001");
            ToggleEditMode(false);
        }

        private void LoadProfileData(string mssv)
        {
            // TODO: Replace with actual API call
            _currentProfile = new StudentProfileModel
            {
                MSSV = mssv,
                HoTen = "Nguyễn Văn A",
                Email = "vana@alumni.edu.vn",
                Phone = "0901234567",
                Gender = "Nam",
                NgaySinh = new DateTime(1998, 5, 20),
                Address = "123 Đường 3/2, Quận 10, TP.HCM",
                Lop = "K15-CNTT",
                Major = "Công nghệ Phần mềm",
                GradYear = 2020,
                Company = "FPT Software",
                Position = "Senior Software Engineer",
                Industry = "Công nghệ Thông tin",
                CompanyLocation = "TP.HCM",
                Achievements = "Đã đạt chứng chỉ AWS Certified Developer - Associate (2022). Tham gia và hoàn thành dự án chuyển đổi số cho Ngân hàng X.",
            };

            BindProfileToUI(_currentProfile);
        }

        private void BindProfileToUI(StudentProfileModel profile)
        {
            txtMSSV.Text = profile.MSSV;
            txtHoTen.Text = profile.HoTen;
            txtEmail.Text = profile.Email;
            txtPhone.Text = profile.Phone;
            txtGender.Text = profile.Gender;
            dpNgaySinh.SelectedDate = profile.NgaySinh;
            txtAddress.Text = profile.Address;
            txtLop.Text = profile.Lop;
            txtMajor.Text = profile.Major;
            txtGradYear.Text = profile.GradYear.ToString();
            txtCompany.Text = profile.Company;
            txtPosition.Text = profile.Position;
            txtIndustry.Text = profile.Industry;
            txtCompanyLocation.Text = profile.CompanyLocation;
            txtAchievements.Text = profile.Achievements;
        }

        private void ToggleEditMode(bool isEditable)
        {
            _isEditMode = isEditable;

            // Apply read-only state to fields. Only allow editing when both edit mode is on and the AllowEdit flag is true.
            txtHoTen.IsReadOnly = true;
            txtEmail.IsReadOnly = !(isEditable && AllowEditEmail);
            txtPhone.IsReadOnly = !(isEditable && AllowEditPhone);
            txtGender.IsReadOnly = true;
            dpNgaySinh.IsEnabled = (isEditable && AllowEditBirthdate);
            txtAddress.IsReadOnly = !(isEditable && AllowEditAddress);
            txtLop.IsReadOnly = true;
            txtMajor.IsReadOnly = true;
            txtGradYear.IsReadOnly = true;
            txtCompany.IsReadOnly = !(isEditable && AllowEditCompany);
            txtPosition.IsReadOnly = !(isEditable && AllowEditPosition);
            txtIndustry.IsReadOnly = true;
            txtCompanyLocation.IsReadOnly = true;
            txtAchievements.IsReadOnly = true;

            // Toggle button visibility
            btnEdit.Visibility = isEditable ? Visibility.Collapsed : Visibility.Visible;
            btnSave.Visibility = isEditable ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditMode(true);
            MessageBox.Show("Đã chuyển sang chế độ chỉnh sửa. Chỉ các trường được phép (Email, Số điện thoại, Ngày sinh, Địa chỉ, Công ty, Vị trí) mới có thể chỉnh sửa.",
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (!ValidateProfileInput())
                return;

            // Collect updated data
            var updatedProfile = new StudentProfileModel
            {
                MSSV = txtMSSV.Text,
                HoTen = txtHoTen.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Gender = txtGender.Text,
                NgaySinh = dpNgaySinh.SelectedDate ?? DateTime.Now,
                Address = txtAddress.Text,
                Lop = txtLop.Text,
                Major = txtMajor.Text,
                GradYear = int.TryParse(txtGradYear.Text, out int gradYear) ? gradYear : 0,
                Company = txtCompany.Text,
                Position = txtPosition.Text,
                Industry = txtIndustry.Text,
                CompanyLocation = txtCompanyLocation.Text,
                Achievements = txtAchievements.Text,
            };

            // TODO: Call API to update profile
            // bool success = await ApiService.UpdateProfileAsync(updatedProfile);
            // if (!success) { MessageBox.Show("Lỗi khi lưu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            _currentProfile = updatedProfile;
            MessageBox.Show("Profile đã được lưu thành công!",
                "Lưu thành công", MessageBoxButton.OK, MessageBoxImage.Information);

            ToggleEditMode(false);
        }

        private bool ValidateProfileInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Vui lòng nhập email hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(txtGradYear.Text, out _) || txtGradYear.Text.Length != 4)
            {
                MessageBox.Show("Năm tốt nghiệp phải là 4 chữ số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}