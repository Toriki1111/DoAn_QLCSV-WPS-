using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class Connections : UserControl
    {
        private List<AlumniConnectionDTO> _allContacts;

        // DTO rút gọn cho danh bạ (khớp tên trường với BE)
        public class AlumniConnectionDTO
        {
            public string StudentId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string MajorName { get; set; } // Mới thêm
            public string Company { get; set; }   // Mới thêm
        }

        public Connections()
        {
            InitializeComponent();
            LoadContacts();
        }

        private void LoadContacts()
        {
            // TODO: Gọi API GET /api/alumni (lấy danh sách public)
            // Mock data khớp cấu trúc BE
            _allContacts = new List<AlumniConnectionDTO>
            {
                new AlumniConnectionDTO { StudentId = "2001101", FullName = "Nguyễn Văn An", Email = "an.nguyen@email.com", MajorName = "Kỹ Thuật Phần Mềm", Company = "FPT Software" },
                new AlumniConnectionDTO { StudentId = "2001102", FullName = "Trần Thị Bích", Email = "bich.tran@email.com", MajorName = "Marketing", Company = "Vinamilk" },
                new AlumniConnectionDTO { StudentId = "2001103", FullName = "Lê Hoàng Nam", Email = "nam.le@email.com", MajorName = "Kinh Doanh Quốc Tế", Company = "Shopee" },
                new AlumniConnectionDTO { StudentId = "2001104", FullName = "Phạm Minh Tuấn", Email = "tuan.pham@email.com", MajorName = "Công Nghệ Thông Tin", Company = "VNG" }
            };

            RefreshGrid(_allContacts);
        }

        private void RefreshGrid(IEnumerable<AlumniConnectionDTO> contacts)
        {
            var results = contacts?.ToList() ?? new List<AlumniConnectionDTO>();
            dgResults.ItemsSource = results;

            if (EmptyState != null)
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

            // Tìm kiếm mở rộng: Tên, Email, MSSV, Ngành, Công ty
            var filtered = _allContacts.Where(c =>
                (!string.IsNullOrEmpty(c.FullName) && c.FullName.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.Email) && c.Email.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.StudentId) && c.StudentId.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.MajorName) && c.MajorName.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(c.Company) && c.Company.ToLowerInvariant().Contains(q))
            );

            RefreshGrid(filtered);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadContacts(); // Re-fetch API
        }
    }
}