using System.Windows;
using System.Windows.Controls; // Cần để dùng UserControl
using System.Windows.Input;    // Cần để xử lý kéo thả cửa sổ

namespace DoAn_QLCSV
{
    public partial class StudentWindow : Window
    {
        public StudentWindow()
        {
            InitializeComponent();

            // Mặc định load trang Hồ sơ
            // Nếu bạn chưa tạo StudentProfile thì comment dòng dưới lại
            // LoadView(new StudentProfile(), "Hồ sơ cá nhân"); 
        }

        // ============================================================
        // 1. XỬ LÝ ĐIỀU HƯỚNG MENU
        // ============================================================

        // Hàm hỗ trợ nạp UserControl
        private void LoadView(UserControl view, string title)
        {
            MainContent.Content = view;
            if (lblPageTitle != null) // Kiểm tra null để tránh lỗi khi khởi tạo
            {
                lblPageTitle.Text = title;
            }
        }

        // Menu: Hồ sơ của tôi
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            // LoadView(new StudentProfile(), "Hồ sơ cá nhân");
            MessageBox.Show("Đang tải hồ sơ...", "Thông báo");
        }

        // Menu: Sự kiện sắp tới (ĐÃ SỬA TÊN HÀM Ở ĐÂY CHO KHỚP)
        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            // LoadView(new EventList(), "Sự kiện & Tin tức");
            MessageBox.Show("Đang tải sự kiện...", "Thông báo");
        }

        // Nút Đăng xuất
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        // ============================================================
        // 2. XỬ LÝ CỬA SỔ (Tắt, Mở, Kéo thả)
        // ============================================================

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
            }
            else
            {
                this.WindowState = WindowState.Normal;
                MainBorder.CornerRadius = new CornerRadius(15);
            }
        }
    }
}