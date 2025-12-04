using System;
using System.Windows;
using System.Windows.Input; // Cần thư viện này để xử lý kéo thả cửa sổ

namespace DoAn_QLCSV
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        // 1. Xử lý kéo thả cửa sổ (Do WindowStyle="None")
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // 2. Nút Tắt (Dấu X góc trên)
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        // 3. Link "Quay lại Đăng nhập"
        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 4. Nút ĐĂNG KÝ
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập
            string fullname = txtFullname.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;
            string confirmPass = txtConfirmPass.Password;

            // --- VALIDATION (Kiểm tra dữ liệu) ---

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên, Tên đăng nhập và Mật khẩu!",
                                "Thiếu thông tin",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra mật khẩu nhập lại
            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!",
                                "Lỗi mật khẩu",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // --- XỬ LÝ LƯU DỮ LIỆU (Giả lập) ---
            // Sau này bạn sẽ viết code lưu vào Database hoặc gọi API ở đây

            MessageBox.Show("Đăng ký tài khoản thành công!\nVui lòng đăng nhập lại.",
                            "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            // Đóng form đăng ký để quay về màn hình Login
            this.Close();
        }
    }
}