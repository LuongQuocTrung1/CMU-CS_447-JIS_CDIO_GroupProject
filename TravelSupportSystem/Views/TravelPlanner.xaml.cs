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
using TravelSupportSystem.Core;

namespace TravelSupportSystem.Views
{
    /// <summary>
    /// Interaction logic for TravelPlanner.xaml
    /// </summary>
    ///private int step = 0;

    public partial class TravelPlanner : Window
    {
            



        private List<string> morningPlaces = new List<string>
{
    "Bà Nà Hills",
    "Ngũ Hành Sơn",
    "Chùa Linh Ứng",
    "Bán đảo Sơn Trà",
    "Cầu Vàng"
};

        private List<string> afternoonPlaces = new List<string>
{
    "Biển Mỹ Khê",
    "Biển Non Nước",
    "Công viên Châu Á",
    "Suối khoáng nóng Núi Thần Tài"
};

        private List<string> eveningPlaces = new List<string>
{
    "Cầu Rồng",
    "Cầu Tình Yêu",
    "Chợ đêm Helio",
    "Phố đi bộ Bạch Đằng"
};


        private Dictionary<string, int> placeCost = new Dictionary<string, int>
{
    { "Bà Nà Hills", 1000 },
    { "Ngũ Hành Sơn", 300 },
    { "Chùa Linh Ứng", 200 },
    { "Bán đảo Sơn Trà", 300 },
    { "Cầu Vàng", 800 },
    { "Biển Mỹ Khê", 200 },
    { "Biển Non Nước", 200 },
    { "Công viên Châu Á", 400 },
    { "Suối khoáng nóng Núi Thần Tài", 600 },
    { "Cầu Rồng", 100 },
    { "Cầu Tình Yêu", 100 },
    { "Chợ đêm Helio", 300 },
    { "Phố đi bộ Bạch Đằng", 100 }
};

        private List<int> SplitBudget(int totalBudget, int days)
        {
            Random rnd = new Random();
            List<int> result = new List<int>();

            int remaining = totalBudget;

            for (int i = 0; i < days - 1; i++)
            {
                // đảm bảo mỗi ngày còn đủ tiền tối thiểu 100k
                int max = remaining - (500 * (days - i - 1));
                int min = 500;

                int amount = rnd.Next(min, max);
                result.Add(amount);
                remaining -= amount;
            }

            // ngày cuối lấy phần còn lại
            result.Add(remaining);

            return result;
        }

        private Random random = new Random();



        private int days = 0;
        private int budget = 0;
        private int step = 0;

        private string GetRandomAndRemove(List<string> places)
        {
            if (places.Count == 0) return "Tự do khám phá";

            int index = random.Next(places.Count);
            string place = places[index];
            places.RemoveAt(index);
            return place;
        }

        public TravelPlanner()
        {
            InitializeComponent();
            BotBubble(GetGreetingByStyle());
            BotBubble("Bạn dự kiến đi du lịch bao nhiêu ngày?");
        }

        private int ExtractNumber(string text)
        {
            string number = new string(text.Where(char.IsDigit).ToArray());
            if (int.TryParse(number, out int result))
                return result;

            return 0;
        }
        private string GetRandomPlace(List<string> places)
        {
            int index = random.Next(places.Count);
            return places[index];
        }

        private string GenerateItinerary(int days, int budget)
        {
            // budget nhập là triệu → đổi sang nghìn
            int totalBudget = budget * 1000;

            List<int> dailyBudget = SplitBudget(totalBudget, days);

            var morningCopy = new List<string>(morningPlaces);
            var afternoonCopy = new List<string>(afternoonPlaces);
            var eveningCopy = new List<string>(eveningPlaces);

            string result = $"📅 LỊCH TRÌNH {days} NGÀY TẠI ĐÀ NẴNG\n\n";


            for (int i = 1; i <= days; i++)
            {

                string morning = GetRandomAndRemove(morningCopy);
                string afternoon = GetRandomAndRemove(afternoonCopy);
                string evening = GetRandomAndRemove(eveningCopy);

                result += $"Ngày {i} (💰 Khoảng {dailyBudget[i - 1]}k):\n";
                result += $"- Sáng: Tham quan {morning}\n";
                result += "- Trưa: Ăn đặc sản địa phương\n";
                result += $"- Chiều: Tham quan {afternoon}\n";
                result += $"- Tối: Dạo {evening}\n\n";
            }

            result += $"💰 Tổng ngân sách: {budget} triệu\n";
            result += "Chúc bạn có chuyến đi vui vẻ nhé! 😊";

            return result;
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string userText = txtUser.Text.Trim();
            if (string.IsNullOrEmpty(userText) || userText == "Aa") return;

            UserBubble(userText);
            txtUser.Clear();

           /* if (!IsTravelRelated(userText))
            {
                BotBubble("Xin lỗi, mình là hệ thống hỗ trợ du lịch nên chỉ trả lời các vấn đề liên quan đến du lịch.");
                return;
            }*/

            switch (step)
            {
                case 0:
                    days = ExtractNumber(userText);

                    if (days <= 0)
                    {
                        BotBubble("Bạn vui lòng nhập số ngày hợp lệ (ví dụ: 3 ngày).");
                        return;
                    }

                    BotBubble("Ngân sách dự kiến của bạn khoảng bao nhiêu?");
                    step++;
                    break;

                case 1:
                    budget = ExtractNumber(userText);

                    if (budget <= 0)
                    {
                        BotBubble("Bạn vui lòng nhập ngân sách hợp lệ (ví dụ: 3 triệu).");
                        return;
                    }

                    BotBubble(GenerateItinerary(days, budget));
                    BotBubble("Nếu bạn cần thêm gợi ý ăn uống, khách sạn hoặc địa điểm tham quan, cứ hỏi mình nha!");
                    step++;
                    break;

                default:

                    string input = userText.ToLower();

                    if (input.Contains("khách sạn"))
                    {
                        BotBubble("Một số khách sạn tốt tại Đà Nẵng:\n" +
                                  "- Sala Danang Beach Hotel\n" +
                                  "- Mường Thanh Luxury\n" +
                                  "- Vanda Hotel\n\n" +
                                  "Bạn có thể xem chi tiết trên Google Maps nhé!");
                    }
                    else if (input.Contains("ăn") || input.Contains("uống"))
                    {
                        BotBubble("Một số địa điểm ăn uống nổi tiếng:\n" +
                                  "- Mì Quảng Bà Mua\n" +
                                  "- Bún chả cá Bà Lữ\n" +
                                  "- Hải sản Bé Mặn\n\n" +
                                  "Bạn muốn ăn đặc sản hay hải sản?");
                    }
                    else if (input.Contains("đặc sản"))
                    {
                        BotBubble("Một số món đặc sản Đà Nẵng bạn nên thử:\n" +
                                  "- Mì Quảng\n" +
                                  "- Bún chả cá\n" +
                                  "- Bánh tráng cuốn thịt heo\n" +
                                  "- Bánh xèo\n\n" +
                                  "Bạn muốn mình gợi ý thêm quán cụ thể không?");
                    }
                    else if (input.Contains("hải sản"))
                    {
                        BotBubble("Một số món hải sản nổi tiếng:\n" +
                                  "- Tôm hùm nướng\n" +
                                  "- Ghẹ hấp\n" +
                                  "- Mực nướng sa tế\n" +
                                  "- Ốc hương xào bơ tỏi\n\n" +
                                  "Bạn thích ăn nướng hay hấp?");
                    }
                    else if (input.Contains("địa điểm") || input.Contains("tham quan"))
                    {
                        BotBubble("Các địa điểm tham quan nổi bật:\n" +
                                  "- Bà Nà Hills\n" +
                                  "- Cầu Rồng\n" +
                                  "- Ngũ Hành Sơn");
                    }
                    else
                    {
                        BotBubble("Nếu bạn cần thêm gợi ý ăn uống, khách sạn hoặc địa điểm tham quan, cứ hỏi mình nha!");
                    }

                    break;
            }
        }

        private bool IsTravelRelated(string text)
        {
            string[] keywords = { "du lịch", "đi", "khách sạn", "ăn", "chơi", "địa điểm", "Đà Nẵng" };
            return keywords.Any(k => text.ToLower().Contains(k.ToLower()));
        }

        private void BotBubble(string text)
        {
            ChatPanel.Children.Add(CreateBubble(text, "#E4E6EB", HorizontalAlignment.Left));
        }

        private void UserBubble(string text)
        {
            ChatPanel.Children.Add(CreateBubble(text, "#2196F3", HorizontalAlignment.Right, true));
        }

        private Border CreateBubble(string text, string bg, HorizontalAlignment align, bool isUser = false)
        {
            return new Border
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(bg)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                HorizontalAlignment = align,
                Child = new TextBlock
                {
                    Text = text,
                    Foreground = isUser ? Brushes.White : Brushes.Black,
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 220
                }
            };
        }

        private string GetGreetingByStyle()
        {
            switch (AppState.TravelStyle)
            {
                case "Tiết kiệm":
                    return "Chào bạn 👋 Mình sẽ giúp bạn lên kế hoạch du lịch Đà Nẵng tiết kiệm nhất.";
                case "Nghỉ dưỡng":
                    return "Xin chào 🌴 Mình sẽ đồng hành cùng bạn trong chuyến nghỉ dưỡng tại Đà Nẵng.";
                case "Phượt":
                    return "Hey 👋 Sẵn sàng khám phá Đà Nẵng theo kiểu phượt chưa?";
                default:
                    return "Xin chào! Mình là trợ lý du lịch Đà Nẵng.";
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }
    }
}
