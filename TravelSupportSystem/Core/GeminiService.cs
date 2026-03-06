using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace TravelSupportSystem.Core
{
    public class GeminiService
    {
        private const string API_KEY = "AIzaSyALiy_Q6Mf-LGKVlgdYTKctSvv2h4j168g";
        private const string GEMINI_URL =
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=";

        public async Task<string> AskTravelAI(string userQuestion)
        {
            string systemPrompt =
$@"
Bạn là trợ lý du lịch thông minh cho Đà Nẵng, Việt Nam.

QUY TẮC BẮT BUỘC:
- Các câu trả lời NGẮN, số tiền, số ngày (ví dụ: '2 triệu', '3 ngày', '5tr') 
  ĐỀU ĐƯỢC COI LÀ THÔNG TIN DU LỊCH HỢP LỆ.
- Người dùng có thể trả lời TỪNG BƯỚC, không cần câu đầy đủ.
- KHÔNG được từ chối chỉ vì câu trả lời ngắn hoặc chỉ có số.

CHỈ TỪ CHỐI nếu người dùng hỏi RÕ RÀNG về:
- Toán học
- Lập trình
- Chính trị
- Nội dung không liên quan đến du lịch

Nếu từ chối, trả lời ĐÚNG CHÍNH XÁC:
'Xin lỗi, mình là hệ thống hỗ trợ du lịch và chỉ trả lời các vấn đề liên quan đến du lịch.'

NGỮ CẢNH DU LỊCH:
- Địa điểm: Đà Nẵng
- Phong cách du lịch: {AppState.TravelStyle}

Hãy trả lời tự nhiên, thân thiện và đúng trọng tâm du lịch.
";

            //Chuẩn hóa input để AI hiểu đây là dữ liệu du lịch
            string normalizedInput =
                $"Thông tin người dùng (ngữ cảnh du lịch): {userQuestion}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = systemPrompt + "\n" + normalizedInput
                            }
                        }
                    }
                }
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        GEMINI_URL + API_KEY,
                        content
                    );

                    var result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return "API Error: " + result;
                    }

                    dynamic data = JsonConvert.DeserializeObject(result);

                    return data.candidates[0].content.parts[0].text.ToString();
                }
                catch (Exception ex)
                {
                    return "Lỗi khi kết nối AI: " + ex.Message;
                }
            }
        }
    }
}
