using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class UserManagement : UserControl
    {
        public class UserDTO
        {
            public long Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; } // admin, alumni, pending
            public string Status { get; set; } // Active/Inactive
        }

        public UserManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Giả lập data từ API
            var list = new List<UserDTO> {
                new UserDTO { Id=1, FullName="Nguyễn Văn A", Email="a@gmail.com", Role="alumni", Status="Active" },
                new UserDTO { Id=2, FullName="Admin Chính", Email="admin@gmail.com", Role="admin", Status="Active" },
                new UserDTO { Id=3, FullName="Trần Văn C", Email="c@gmail.com", Role="pending", Status="Pending" }, // Cần duyệt ông này
            };
            dgUsers.ItemsSource = list;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Duyệt qua list để xem role nào thay đổi và gọi API Update
            MessageBox.Show("Đã cập nhật quyền hạn thành công!");
        }

        private void btnResetPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đã reset mật khẩu về mặc định (123456)");
        }
    }
}