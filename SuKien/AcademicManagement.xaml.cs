using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class AcademicManagement : UserControl
    {
        public class Faculty { public int Id { get; set; } public string Name { get; set; } }
        public class Major { public int Id { get; set; } public int FacultyId { get; set; } public string Name { get; set; } public string Code { get; set; } }

        List<Faculty> _faculties;
        List<Major> _majors;

        public AcademicManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Dữ liệu giả
            _faculties = new List<Faculty> {
                new Faculty { Id=1, Name="Công Nghệ Thông Tin" },
                new Faculty { Id=2, Name="Kinh Tế" }
            };
            _majors = new List<Major> {
                new Major { Id=1, FacultyId=1, Code="7480101", Name="Khoa học máy tính" },
                new Major { Id=2, FacultyId=1, Code="7480103", Name="Kỹ thuật phần mềm" },
                new Major { Id=3, FacultyId=2, Code="7340101", Name="Quản trị kinh doanh" }
            };

            dgFaculties.ItemsSource = _faculties;
        }

        // Khi chọn Khoa -> Lọc Ngành tương ứng
        private void dgFaculties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFaculty = dgFaculties.SelectedItem as Faculty;
            if (selectedFaculty != null)
            {
                dgMajors.ItemsSource = _majors.Where(m => m.FacultyId == selectedFaculty.Id).ToList();
            }
            else
            {
                dgMajors.ItemsSource = null;
            }
        }

        // Các hàm Thêm/Sửa/Xóa (Chỉ hiện thông báo)
        private void btnAddFaculty_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Mở popup thêm Khoa");
        private void btnEditFaculty_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Sửa Khoa");
        private void btnDelFaculty_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Xóa Khoa");

        private void btnAddMajor_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Mở popup thêm Ngành");
        private void btnEditMajor_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Sửa Ngành");
        private void btnDelMajor_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Xóa Ngành");
    }
}
