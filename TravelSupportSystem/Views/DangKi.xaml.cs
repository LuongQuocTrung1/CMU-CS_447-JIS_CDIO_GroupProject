using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient; // Dùng cho SQL Server

namespace TravelSupportSystem.Views
{
    public partial class DangKy : Window
    {
        // Sử dụng chung chuỗi kết nối giống hệt bên trang Đăng Nhập
        // Thay vì dùng |DataDirectory|, ta dán thẳng đường dẫn Full Path vào:
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\CMU-CS 447_CDIO\TravelSupportSystem\TravelSupportSystem\DataBaseSystem.mdf;Integrated Security=True;Connect Timeout=30";

        public DangKy()
        {
            InitializeComponent();
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            // 1. Lấy thông tin từ giao diện
            string username = txtNewUser.Text.Trim();
            string password = txtNewPass.Password;
            string confirmPassword = txtConfirmPass.Password;

            // 2. Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 3. Kết nối và thao tác với Database
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Bước 4: Kiểm tra xem Username đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM [Account] WHERE UserName = @Username";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show("Tên đăng nhập này đã có người sử dụng. Vui lòng chọn tên khác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return; // Dừng lại, không chạy lệnh Insert bên dưới nữa
                        }
                    }

                    // Bước 5: Thêm tài khoản mới vào bảng Account
                    string insertQuery = "INSERT INTO [Account] (UserName, Password, NgayTao) VALUES (@Username, @Password, @NgayTao)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Username", username);
                        insertCmd.Parameters.AddWithValue("@Password", password);
                        insertCmd.Parameters.AddWithValue("@NgayTao", DateTime.Now); // Lấy ngày giờ hiện tại của máy tính

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        // Kiểm tra xem lệnh Insert có thành công không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Bước 6: Chuyển về trang Đăng nhập
                            DangNhap dangNhapWindow = new DangNhap();
                            dangNhapWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra, không thể tạo tài khoản!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện khi bấm nút "QUAY LẠI ĐĂNG NHẬP"
        private void BtnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            DangNhap dangNhapWindow = new DangNhap();
            dangNhapWindow.Show();
            this.Close();
        }
    }
}