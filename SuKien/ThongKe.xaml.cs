using System.Windows.Controls;

namespace DoAn_QLCSV
{
    public partial class ThongKe : UserControl
    {
        public ThongKe()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private async void LoadStatistics()
        {
            // 1. GỌI API LẤY SỐ LIỆU TỔNG QUAN
            // (Ví dụ: gọi ApiService.GetOverallStatsAsync())
            // Hiện tại dùng dữ liệu tĩnh
            txtTotalSV.Text = "1250";
            txtUpcomingEvents.Text = "5";
            txtEmploymentRate.Text = "92%";


            // 2. GỌI API LẤY DỮ LIỆU CHO BIỂU ĐỒ
            // var dataByClass = await ApiService.GetStudentsByClassAsync();
            // var dataByMajor = await ApiService.GetStudentsByMajorAsync();

            // 3. VẼ BIỂU ĐỒ (Sử dụng thư viện LiveCharts)
            // Khởi tạo các SeriesCollection, đặt vào Chart Control
        }

        // Cần thêm các hàm hỗ trợ vẽ biểu đồ tại đây (khi đã cài LiveCharts)
    }
}