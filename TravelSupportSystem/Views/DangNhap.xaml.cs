using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient; 

namespace TravelSupportSystem.Views
{
    public partial class DangNhap : Window
    {
       
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\CMU-CS 447_CDIO\TravelSupportSystem\TravelSupportSystem\DataBaseSystem.mdf;Integrated Security=True;Connect Timeout=30";

        public DangNhap()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // 1. Lấy thông tin
            string username = txtUser.Text.Trim();
            string password = txtPass.Password;

            // 2. Kiểm tra rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Username và Password!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 3. Kết nối CSDL
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Câu lệnh đếm số dòng khớp với Username và Password trong bảng Account
                    string query = "SELECT COUNT(1) FROM [Account] WHERE UserName = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Truyền tham số để chống SQL Injection
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // ExecuteScalar trả về cột đầu tiên của dòng đầu tiên (chính là số đếm COUNT)
                        int result = (int)cmd.ExecuteScalar();

                        // 4. Xử lý kết quả
                        if (result > 0)
                        {
                           

                            Home homeWindow = new Home();
                            homeWindow.Show();

                            // ĐÓNG TRANG ĐĂNG NHẬP
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Bắt lỗi nếu file database không tồn tại hoặc lỗi chuỗi kết nối
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi Nghiêm Trọng", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // THÊM HÀM NÀY ĐỂ XỬ LÝ KHI BẤM NÚT ĐĂNG KÝ
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Mở form Đăng ký
            DangKy dangKyWindow = new DangKy();
            dangKyWindow.Show();

            // Đóng form Đăng nhập hiện tại
            this.Close();
        }
    }
}