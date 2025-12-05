using System;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class StudentProfile : UserControl
    {
        // Cờ cho phép chỉnh sửa (Dựa trên logic BE, User chỉ sửa được thông tin cá nhân/công việc)
        public bool AllowEditEmail { get; set; } = true;
        public bool AllowEditWorkInfo { get; set; } = true;
        public bool AllowEditSocials { get; set; } = true;

        // DTO chuẩn từ Backend
        public class AlumniDetailResponse
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public string FullName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? AvatarUrl { get; set; }

            public string? StudentId { get; set; }
            public int GraduationYear { get; set; }

            public int FacultyId { get; set; }
            public string FacultyName { get; set; } = null!;

            public int MajorId { get; set; }
            public string MajorName { get; set; } = null!;

            public string? CurrentPosition { get; set; }
            public string? Company { get; set; }
            public string? CompanyIndustry { get; set; }
            public string? City { get; set; }
            public string Country { get; set; } = null!;

            public string? LinkedinUrl { get; set; }
            public string? FacebookUrl { get; set; }
            public string? Bio { get; set; }

            public bool IsPublic { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
            public DateTimeOffset UpdatedAt { get; set; }
        }

        private AlumniDetailResponse _currentProfile;
        private bool _isEditMode = false;

        public StudentProfile()
        {
            InitializeComponent();
            this.Loaded += StudentProfile_Loaded;
        }

        private void StudentProfile_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProfileData();
            ToggleEditMode(false);
        }

        private void LoadProfileData()
        {
            // TODO: Gọi API thật (GET /api/alumni/profile)
            // Giả lập dữ liệu khớp cấu trúc BE
            _currentProfile = new AlumniDetailResponse
            {
                Id = 1,
                UserId = 1001,
                FullName = "Nguyễn Văn A",
                Email = "nguyenvana@email.com",
                StudentId = "1811060123",
                GraduationYear = 2022,
                FacultyName = "Công Nghệ Thông Tin",
                MajorName = "Kỹ Thuật Phần Mềm",
                CurrentPosition = "Backend Developer",
                Company = "VNG Corporation",
                CompanyIndustry = "Technology",
                City = "Hồ Chí Minh",
                Country = "Việt Nam",
                LinkedinUrl = "https://linkedin.com/in/nguyenvana",
                FacebookUrl = "https://facebook.com/nguyenvana",
                Bio = "Đam mê lập trình và xây dựng các hệ thống quy mô lớn.",
                IsPublic = true
            };

            BindProfileToUI(_currentProfile);
        }

        private void BindProfileToUI(AlumniDetailResponse profile)
        {
            // Core Info
            txtMSSV.Text = profile.StudentId;
            txtHoTen.Text = profile.FullName;
            txtEmail.Text = profile.Email;

            // Education
            txtFaculty.Text = profile.FacultyName;
            txtMajor.Text = profile.MajorName;
            txtGradYear.Text = profile.GraduationYear.ToString();

            // Work & Location
            txtCompany.Text = profile.Company;
            txtPosition.Text = profile.CurrentPosition;
            txtIndustry.Text = profile.CompanyIndustry;
            txtCity.Text = profile.City;
            txtCountry.Text = profile.Country;

            // Social & Bio
            txtLinkedIn.Text = profile.LinkedinUrl;
            txtFacebook.Text = profile.FacebookUrl;
            txtBio.Text = profile.Bio;
        }

        private void ToggleEditMode(bool isEditable)
        {
            _isEditMode = isEditable;

            // Các trường không bao giờ được sửa (Dữ liệu từ nhà trường)
            txtMSSV.IsReadOnly = true;
            txtHoTen.IsReadOnly = true;
            txtFaculty.IsReadOnly = true;
            txtMajor.IsReadOnly = true;
            txtGradYear.IsReadOnly = true;

            // Các trường cho phép sửa
            txtEmail.IsReadOnly = !(isEditable && AllowEditEmail);

            bool canEditWork = isEditable && AllowEditWorkInfo;
            txtCompany.IsReadOnly = !canEditWork;
            txtPosition.IsReadOnly = !canEditWork;
            txtIndustry.IsReadOnly = !canEditWork;
            txtCity.IsReadOnly = !canEditWork;
            txtCountry.IsReadOnly = !canEditWork;

            bool canEditSocial = isEditable && AllowEditSocials;
            txtLinkedIn.IsReadOnly = !canEditSocial;
            txtFacebook.IsReadOnly = !canEditSocial;
            txtBio.IsReadOnly = !canEditSocial;

            // Toggle Buttons
            btnEdit.Visibility = isEditable ? Visibility.Collapsed : Visibility.Visible;
            btnSave.Visibility = isEditable ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditMode(true);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update DTO từ UI
            _currentProfile.Email = txtEmail.Text;
            _currentProfile.Company = txtCompany.Text;
            _currentProfile.CurrentPosition = txtPosition.Text;
            _currentProfile.CompanyIndustry = txtIndustry.Text;
            _currentProfile.City = txtCity.Text;
            _currentProfile.Country = txtCountry.Text;
            _currentProfile.LinkedinUrl = txtLinkedIn.Text;
            _currentProfile.FacebookUrl = txtFacebook.Text;
            _currentProfile.Bio = txtBio.Text;
            _currentProfile.UpdatedAt = DateTimeOffset.Now;

            // TODO: Gọi API PUT update profile
            // await _apiService.UpdateProfile(_currentProfile);

            MessageBox.Show("Cập nhật hồ sơ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            ToggleEditMode(false);
        }
    }
}