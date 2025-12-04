using System.Windows;
using System.Windows.Controls; // Cần để dùng UserControl
using System.Windows.Input;    // Cần để xử lý kéo thả cửa sổ

namespace DoAn_QLCSV
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        // ============================================================
        // 1. XỬ LÝ NÚT ĐIỀU KHIỂN CỬA SỔ (Tắt, Mở, Kéo thả)
        // ============================================================

        // Kéo thả cửa sổ khi giữ chuột vào thanh Header
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Nút Tắt (Close)
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Nút Thu nhỏ (Minimize)
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Nút Phóng to / Thu nhỏ (Maximize / Restore)
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                // Khi phóng to hết cỡ thì bỏ bo góc đi cho đẹp
                MainBorder.CornerRadius = new CornerRadius(0);
            }
            else
            {
                this.WindowState = WindowState.Normal;
                // Khi về bình thường thì bo tròn lại
                MainBorder.CornerRadius = new CornerRadius(15);
            }
        }

        // Nút Đăng xuất
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Mở lại màn hình Đăng nhập
            Login login = new Login();
            login.Show();
            // Đóng cửa sổ Admin hiện tại
            this.Close();
        }

        // ============================================================
        // 2. XỬ LÝ MENU ĐIỀU HƯỚNG (Chuyển đổi màn hình con)
        // ============================================================

        // Hàm chung để nạp UserControl vào vùng nội dung
        private void LoadView(UserControl view, string title)
        {
            MainContent.Content = view;
            lblTitle.Text = "← " + title; // Cập nhật tiêu đề góc trên
        }

        // Menu: Trang Chủ
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = null; // Xóa nội dung để hiện màn hình chào mừng mặc định
            lblTitle.Text = "← Trang Chủ";
        }

        // Menu: Quản lý Cựu Sinh Viên
        private void btnCuuSV_Click(object sender, RoutedEventArgs e)
        {
            // Gọi UserControl Danh Sách SV
            // Nếu bạn chưa tạo file UC_StudentList.xaml thì tạm thời comment dòng dưới lại nhé
            LoadView(new StudentList(), "Quản lý Cựu Sinh Viên");
        }

        // Menu: Thống Kê (Chưa làm)
        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Thống kê đang phát triển!", "Thông báo");
        }

        // Menu: Sự Kiện (Chưa làm)
        private void btnSuKien_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Sự kiện đang phát triển!", "Thông báo");
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePass = new ChangePasswordWindow();
            changePass.ShowDialog();
        }
    }
}