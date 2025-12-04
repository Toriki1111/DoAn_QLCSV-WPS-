using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    // Đã đổi tên class thành SuKien
    public partial class SuKien : UserControl
    {
        List<Event> _listEvents;

        public SuKien()
        {
            InitializeComponent();
            LoadData();
        }

        // 1. DATA MODEL (Event)
        public class Event
        {
            public int EventID { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public int RegisteredCount { get; set; }

            public string Status 
            {
                get 
                {
                    // Trạng thái dựa trên so sánh thời gian hiện tại
                    return StartDate > DateTime.Now ? "Sắp diễn ra" : "Đã kết thúc";
                }
            }
        }

        private void LoadData()
        {
            // Data giả (Dummy Data)
            _listEvents = new List<Event>()
            {
                new Event { EventID = 1, Name = "Hội thảo Công nghệ AI & Tương lai", 
                            StartDate = new DateTime(2025, 12, 15, 14, 0, 0), Location = "Phòng Hội Nghị A1",
                            Description = "Thảo luận về các ứng dụng AI mới nhất...", RegisteredCount = 55 },
                
                new Event { EventID = 2, Name = "Gặp gỡ cựu sinh viên khóa K15", 
                            StartDate = new DateTime(2025, 1, 20, 18, 30, 0), Location = "Nhà Hàng Đại Học",
                            Description = "Tiệc tối thân mật và chia sẻ kinh nghiệm.", RegisteredCount = 120 },

                new Event { EventID = 3, Name = "Workshop: Kỹ năng phỏng vấn IT", 
                            StartDate = new DateTime(2024, 10, 5, 9, 0, 0), Location = "Online qua Zoom",
                            Description = "Hướng dẫn chi tiết cách trả lời các câu hỏi phỏng vấn khó.", RegisteredCount = 80 },
            };

            RefreshGrid(_listEvents);
        }

        private void RefreshGrid(List<Event> data)
        {
            dgEvents.ItemsSource = null;
            dgEvents.ItemsSource = data;
            // Đảm bảo TextBlock đã được tạo trước khi truy cập
            if (txtTotal != null) txtTotal.Text = data.Count.ToString();
        }

        // 2. CHỌN DÒNG -> HIỆN CHI TIẾT
        private void dgEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEvent = dgEvents.SelectedItem as Event;
            if (selectedEvent != null)
            {
                txtEventID.Text = selectedEvent.EventID.ToString();
                txtTenSuKien.Text = selectedEvent.Name;
                dpStartDate.SelectedDate = selectedEvent.StartDate.Date;
                // Hiển thị thời gian (vd: 14:00 PM)
                txtTime.Text = selectedEvent.StartDate.ToString("HH:mm tt"); 
                txtLocation.Text = selectedEvent.Location;
                txtDescription.Text = selectedEvent.Description;
            } else {
                // Xóa dữ liệu form khi không có dòng nào được chọn
                ClearForm();
            }
        }
        
        // Hàm làm sạch form chi tiết
        private void ClearForm()
        {
            txtEventID.Text = "";
            txtTenSuKien.Text = "";
            dpStartDate.SelectedDate = null;
            txtTime.Text = "";
            txtLocation.Text = "";
            txtDescription.Text = "";
        }

        // 3. TÌM KIẾM
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string k = txtSearch.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(k)) { RefreshGrid(_listEvents); return; }

            var result = _listEvents.Where(ev => 
                ev.Name.ToLower().Contains(k) || 
                ev.Location.ToLower().Contains(k) ||
                ev.Description.ToLower().Contains(k)
            )
            .ToList();

            RefreshGrid(result);
        }

        // 4. CÁC HÀM CHỨC NĂNG 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Thêm Sự Kiện mới sẽ mở cửa sổ popup...", "Chức năng");
            // Cần tạo EventAddWindow.xaml.cs sau này
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var ev = dgEvents.SelectedItem as Event;
            if (ev == null) 
            { 
                MessageBox.Show("Vui lòng chọn sự kiện cần sửa trên bảng.", "Cảnh báo"); 
                return; 
            }
            // Logic cập nhật dữ liệu từ Form (cần kết nối API)
            MessageBox.Show($"Đã cập nhật sự kiện: {txtTenSuKien.Text}", "Thành công");
            LoadData(); // Load lại data từ API
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ev = dgEvents.SelectedItem as Event;
            if (ev != null && MessageBox.Show($"Bạn có chắc chắn muốn XÓA sự kiện '{ev.Name}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _listEvents.Remove(ev);
                RefreshGrid(_listEvents);
                ClearForm();
                MessageBox.Show("Đã xóa sự kiện thành công (tạm thời)", "Thành công");
            }
        }
        
        private void btnRefresh_Click(object sender, RoutedEventArgs e) 
        {
            txtSearch.Text = "";
            dgEvents.SelectedItem = null;
            ClearForm();
            LoadData(); // Load lại dữ liệu mới nhất (từ API sau này)
        }
    }
}