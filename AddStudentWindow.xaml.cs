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
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // 1. Kiểm tra bắt buộc
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV và Họ tên!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. Thu thập dữ liệu (Để sau này gửi API/Lưu DB)
            // Ví dụ:
            // var newStudent = new Student() {
            //     MaSV = txtMaSV.Text,
            //     HoTen = txtHoTen.Text,
            //     CongTy = txtCongTy.Text,
            //     ...
            // };

            // 3. Thông báo và Đóng
            MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true; // Báo cho cửa sổ cha biết là OK
            this.Close();
        }
    }
}