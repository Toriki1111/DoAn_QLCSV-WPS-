using System.Windows;
using System.Windows.Input;

namespace DoAn_QLCSV
{
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV và Họ tên!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO: Tạo object DTO và gọi API POST
            /*
            var newAlumni = new AlumniDetailResponse {
                StudentId = txtMaSV.Text,
                FullName = txtHoTen.Text,
                FacultyName = txtFaculty.Text,
                MajorName = txtNganh.Text,
                // ... map các trường còn lại ...
            };
            */

            MessageBox.Show("Thêm mới thành công!", "Thông báo");
            this.DialogResult = true;
            this.Close();
        }
    }
}