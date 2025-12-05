using System.Windows;
using System.Windows.Input;

namespace DoAn_QLCSV
{
    public partial class StudentWindow : Window
    {
        private readonly Thickness _normalBorderMargin = new Thickness(40);
        private readonly CornerRadius _normalCornerRadius = new CornerRadius(15);

        public StudentWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Show overview immediately with Home button active
            lblPageTitle.Text = "← Tổng quan";
            btnHome.Tag = "Active";
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
                MainBorder.Margin = new Thickness(0);
            }
            else
            {
                WindowState = WindowState.Normal;
                MainBorder.CornerRadius = _normalCornerRadius;
                MainBorder.Margin = _normalBorderMargin;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Navigation handlers
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Tổng quan";
            btnHome.Tag = "Active";
            MainContent.Content = null;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Hồ sơ cá nhân";
            btnHome.Tag = null;
            var profile = new StudentProfile();
            profile.AllowEditEmail = true;
            MainContent.Content = profile;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Lịch sử cập nhật";
            btnHome.Tag = null;
            MainContent.Content = null;
            MessageBox.Show("Tính năng Lịch sử cập nhật đang phát triển.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Sự kiện sắp tới";
            btnHome.Tag = null;
            var events = new UpCommingEvents();
            MainContent.Content = events;
        }

        private void btnMyEvents_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Đăng ký của tôi";
            btnHome.Tag = null;
            var signUp = new Student_SignUp();
            signUp.Owner = this;
            signUp.ShowDialog();
        }

        private void btnNetwork_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Tìm kiếm Alumni";
            btnHome.Tag = null;
            var connections = new Connections();
            MainContent.Content = connections;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            lblPageTitle.Text = "← Cài đặt";
            btnHome.Tag = null;
            MainContent.Content = null;
            MessageBox.Show("Tính năng Cài đặt đang phát triển.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ChangePasswordWindow();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }
    }
}