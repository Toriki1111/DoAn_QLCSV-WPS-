using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class Student_SignUp : Window
    {
        private List<RegisteredEvent> _registrations;
        private List<RegisteredEvent> _allRegistrations; // keep original list for search

        public class RegisteredEvent
        {
            public long EventID { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public int RegisteredAtCount { get; set; }

            // UI helpers
            public string StartDateFormatted => StartDate.ToString("dd/MM/yyyy HH:mm");
            public string Status => StartDate > DateTime.Now ? "Sắp diễn ra" : "Đã diễn ra";
            public bool CanCancel => StartDate > DateTime.Now; // only allow cancel if not yet started
        }

        public Student_SignUp()
        {
            InitializeComponent();
            LoadRegistrations();
            RefreshGrid(_registrations);
        }

        private void LoadRegistrations()
        {
            // TODO: Replace with API call to fetch current student's registrations
            // Sample in-memory data:
            _allRegistrations = new List<RegisteredEvent>
            {
                new RegisteredEvent { EventID = 1, Name = "Hội thảo AI", StartDate = DateTime.Now.AddDays(10).AddHours(14), Location = "Hội trường A", Description = "AI talk", RegisteredAtCount = 55 },
                new RegisteredEvent { EventID = 2, Name = "Workshop Phỏng vấn", StartDate = DateTime.Now.AddDays(-2).AddHours(9), Location = "Online", Description = "Workshop", RegisteredAtCount = 80 },
                new RegisteredEvent { EventID = 3, Name = "Gặp gỡ K15", StartDate = DateTime.Now.AddDays(20).AddHours(18), Location = "Nhà hàng X", Description = "Gala", RegisteredAtCount = 120 }
            };

            // clone to working list
            _registrations = _allRegistrations.Select(r => r).ToList();
        }

        private void RefreshGrid(IEnumerable<RegisteredEvent> list)
        {
            var items = list?.OrderBy(e => e.StartDate).ToList() ?? new List<RegisteredEvent>();
            dgRegistrations.ItemsSource = items;
            EmptyState.Visibility = items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            txtCount.Text = $"Tổng: {items.Count} đăng ký";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var q = (txtSearch.Text ?? string.Empty).Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q))
            {
                RefreshGrid(_registrations);
                return;
            }

            var filtered = _registrations.Where(ev =>
                (!string.IsNullOrEmpty(ev.Name) && ev.Name.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(ev.Location) && ev.Location.ToLowerInvariant().Contains(q)) ||
                ev.EventID.ToString().Contains(q)
            );

            RefreshGrid(filtered);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadRegistrations(); // reload (replace with API re-fetch)
            RefreshGrid(_registrations);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ev = btn?.Tag as RegisteredEvent ?? dgRegistrations.SelectedItem as RegisteredEvent;
            if (ev == null) return;

            if (!ev.CanCancel)
            {
                MessageBox.Show("Sự kiện đã bắt đầu/đã kết thúc, không thể hủy.", "Không thể hủy", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đăng ký sự kiện '{ev.Name}'?\nNgày: {ev.StartDateFormatted}", "Xác nhận hủy", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            // TODO: Call backend API to cancel registration
            // On success, remove from local list
            _registrations.Remove(ev);
            _allRegistrations.RemoveAll(r => r.EventID == ev.EventID);
            RefreshGrid(_registrations);

            MessageBox.Show("Bạn đã hủy đăng ký thành công.", "Đã hủy", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ev = btn?.Tag as RegisteredEvent ?? dgRegistrations.SelectedItem as RegisteredEvent;
            if (ev == null) return;

            MessageBox.Show($"{ev.Name}\n\nNgày giờ: {ev.StartDateFormatted}\nĐịa điểm: {ev.Location}\n\n{ev.Description}",
                "Chi tiết sự kiện", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}