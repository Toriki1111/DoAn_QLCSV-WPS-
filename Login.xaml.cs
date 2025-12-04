using System;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace DoAn_QLCSV
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //  Kéo thả cửa sổ
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //  Nút Thoát
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //  Nút Phóng to/Thu nhỏ
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                // 1. Phóng to cửa sổ
                this.WindowState = WindowState.Maximized;

                // 2. Chuyển icon nút thành biểu tượng "Restore"
                ((System.Windows.Controls.Button)sender).Content = "❐";

                // 3. XỬ LÝ GIAO DIỆN KHI PHÓNG TO:
                // Biến góc bo tròn thành vuông (0) để lấp đầy màn hình
                MainBorder.CornerRadius = new CornerRadius(0);
                // Bỏ viền để không bị thô
                MainBorder.BorderThickness = new Thickness(0);
            }
            else
            {
                // 1. Trả về kích thước bình thường
                this.WindowState = WindowState.Normal;

                // 2. Chuyển icon nút thành biểu tượng "Maximize"
                ((System.Windows.Controls.Button)sender).Content = "☐";

                // 3. XỬ LÝ GIAO DIỆN KHI THU NHỎ:
                // Trả lại góc bo tròn 20
                MainBorder.CornerRadius = new CornerRadius(20);
                // Trả lại viền 1
                MainBorder.BorderThickness = new Thickness(1);
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ giao diện
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password; // WPF dùng .Password thay vì .Text

            // BƯỚC 1: KIỂM TRA VALIDATION (Giữ nguyên logic của bạn)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.",
                                "Lỗi đăng nhập",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning); // WPF dùng MessageBoxImage
                return;
            }

            // BƯỚC 2: MÔ PHỎNG XÁC THỰC VÀ PHÂN QUYỀN
            string userRole = null;

            // Giả lập Tài khoản Admin
            if (username == "Admin" && password == "123")
            {
                userRole = "Admin";
            }
            // Giả lập Tài khoản Cựu Sinh viên (SV)
            else if (username == "SV" && password == "123")
            {
                userRole = "Student";
            }

            // BƯỚC 3: XỬ LÝ KẾT QUẢ ĐĂNG NHẬP
            if (userRole != null)
            {
                txtPassword.Password = ""; // Xóa mật khẩu

                // Ẩn form đăng nhập đi
                this.Hide();

                if (userRole == "Admin")
                {
                    // Mở Form dành cho Admin (DashboardWindow mà chúng ta vừa tạo)
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
             
                }
                else if (userRole == "Student")
                {
                    StudentWindow studentWindow = new StudentWindow();
                    studentWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!",
                                "Lỗi đăng nhập",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        // 4. Quên mật khẩu
        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng đang phát triển.");
        }

        // 5. ĐÂY LÀ HÀM BẠN ĐANG THIẾU (btnRegister_Click)
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Closed += (s, args) => this.Show();
            reg.ShowDialog();
        }
    }
}