using System.Windows;
using System.Windows.Input;

namespace DoAn_QLCSV
{
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
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
            string oldPass = txtOldPass.Password;
            string newPass = txtNewPass.Password;
            string confirmPass = txtConfirmPass.Password;

            // 1. Kiểm tra nhập thiếu
            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu cũ (Giả lập là '123')
            // Sau này bạn thay bằng check database/API
            if (oldPass != "123")
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 3. Kiểm tra trùng khớp
            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 4. Lưu thành công
            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}