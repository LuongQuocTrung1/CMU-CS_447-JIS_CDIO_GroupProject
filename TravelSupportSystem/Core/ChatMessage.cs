using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelSupportSystem.Core
{
    public class ChatMessage
    {
        public string Role { get; set; }   // "user" | "assistant"
        public string Content { get; set; }
    }
}
