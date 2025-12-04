using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class Connections : UserControl
    {
        private List<StudentContact> _allContacts;

        public class StudentContact
        {
            public string StudentId { get; set; } // optional
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }

        public Connections()
        {
            InitializeComponent();
            LoadContacts();
            RefreshGrid(_allContacts);
        }

        private void LoadContacts()
        {
            // TODO: Replace with API call to fetch contacts from backend.
            _allContacts = new List<StudentContact>
            {
                new StudentContact { StudentId = "2001101", Name = "Nguyễn Văn An", Phone = "0901234567", Email = "an.nguyen@email.com" },
                new StudentContact { StudentId = "2001102", Name = "Trần Thị Bích", Phone = "0912345678", Email = "bich.tran@email.com" },
                new StudentContact { StudentId = "2001103", Name = "Lê Hoàng Nam", Phone = "0987654321", Email = "nam.le@email.com" }
            };
        }

        private void RefreshGrid(IEnumerable<StudentContact> contacts)
        {
            var results = contacts?.ToList() ?? new List<StudentContact>();
            dgResults.ItemsSource = results;
            EmptyState.Visibility = results.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var q = (txtSearch.Text ?? string.Empty).Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q))
            {
                RefreshGrid(_allContacts);
                return;
            }

            var filtered = _allContacts.Where(c =>
                (!string.IsNullOrEmpty(c.Name) && c.Name.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.Email) && c.Email.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.StudentId) && c.StudentId.ToLowerInvariant().Contains(q))
            );

            RefreshGrid(filtered);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadContacts(); // re-load from source (replace with API in future)
            RefreshGrid(_allContacts);
        }
    }
}