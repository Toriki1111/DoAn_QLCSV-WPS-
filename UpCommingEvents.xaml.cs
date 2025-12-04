using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class UpCommingEvents : UserControl
    {
        private List<UpcomingEvent> _events;
        private HashSet<long> _myRegistrations; // Track current student's registrations

        public class UpcomingEvent
        {
            public long EventID { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public int RegisteredCount { get; set; }
            public int MaxParticipants { get; set; }
            public bool IsOnline { get; set; }
            public string MeetLink { get; set; }

            public string StartDateFormatted => StartDate.ToString("dd/MM/yyyy HH:mm");
            public bool IsFull => MaxParticipants > 0 && RegisteredCount >= MaxParticipants;
        }

        public UpCommingEvents()
        {
            InitializeComponent();
            _myRegistrations = new HashSet<long>();
            LoadData();
            RefreshGrid();
        }

        private void LoadData()
        {
            // Replace with API call later
            _events = new List<UpcomingEvent>
            {
                new UpcomingEvent { EventID = 1, Name = "Hội thảo Công nghệ AI & Tương lai", StartDate = new DateTime(2025,12,15,14,0,0), Location = "Phòng Hội Nghị A1", Description = "Thảo luận về AI", RegisteredCount = 55, MaxParticipants = 100 },
                new UpcomingEvent { EventID = 2, Name = "Gặp gỡ cựu sinh viên khóa K15", StartDate = new DateTime(2025,1,20,18,30,0), Location = "Nhà Hàng Đại Học", Description = "Tiệc tối thân mật", RegisteredCount = 120, MaxParticipants = 150 },
                new UpcomingEvent { EventID = 3, Name = "Workshop: Kỹ năng phỏng vấn IT", StartDate = new DateTime(2025,1,25,19,0,0), Location = "Online via Zoom", Description = "Workshop interview", RegisteredCount = 80, MaxParticipants = 200, IsOnline = true, MeetLink = "https://zoom.us/j/123456" }
            };

            // TODO: Replace with API call to fetch current student's registrations
            // _myRegistrations = await ApiService.GetMyRegistrationsAsync();
        }

        private void RefreshGrid()
        {
            var upcoming = _events.Where(e => e.StartDate > DateTime.Now).OrderBy(e => e.StartDate).ToList();
            dgEvents.ItemsSource = upcoming;
            txtEventCount.Text = $"Tổng: {upcoming.Count} sự kiện";
            EmptyState.Visibility = upcoming.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var key = txtSearch.Text?.Trim().ToLower() ?? string.Empty;
            var list = string.IsNullOrEmpty(key)
                ? _events
                : _events.Where(ev => (ev.Name ?? "").ToLower().Contains(key) || (ev.Location ?? "").ToLower().Contains(key) || (ev.Description ?? "").ToLower().Contains(key)).ToList();

            var upcoming = list.Where(ev => ev.StartDate > DateTime.Now).OrderBy(ev => ev.StartDate).ToList();
            dgEvents.ItemsSource = upcoming;
            EmptyState.Visibility = upcoming.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadData(); // later call API
            RefreshGrid();
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng thêm sự kiện dành cho Admin. Mở modal thêm sự kiện.", "Thêm sự kiện", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnViewEvent_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ev = btn?.Tag as UpcomingEvent ?? (dgEvents.SelectedItem as UpcomingEvent);
            if (ev == null) return;

            var extra = ev.IsOnline ? $"\nLink: {ev.MeetLink}" : "";
            var capacity = ev.MaxParticipants > 0 ? $"\nĐăng ký: {ev.RegisteredCount}/{ev.MaxParticipants}" : "";
            MessageBox.Show($"{ev.Name}\n\nNgày giờ: {ev.StartDateFormatted}\nĐịa điểm: {ev.Location}\n\n{ev.Description}{extra}{capacity}",
                "Chi tiết sự kiện", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnRegisterEvent_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ev = btn?.Tag as UpcomingEvent ?? (dgEvents.SelectedItem as UpcomingEvent);
            if (ev == null) return;

            // Check if already registered
            if (_myRegistrations.Contains(ev.EventID))
            {
                MessageBox.Show("Bạn đã đăng ký sự kiện này rồi!", "Đã đăng ký", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if event is full
            if (ev.IsFull)
            {
                MessageBox.Show("Sự kiện đã đầy.", "Không thể đăng ký", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn đăng ký sự kiện '{ev.Name}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            // TODO: call API to register; currently update local model
            _myRegistrations.Add(ev.EventID);
            ev.RegisteredCount++;
            dgEvents.Items.Refresh();
            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}