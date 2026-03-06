using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelSupportSystem
{
    class TravelPlannerService
    {
        public TravelPlan GeneratePlan(TravelRequest req)
        {
            var plan = new TravelPlan
            {
                Attractions = new List<string>(),
                EstimatedCost = req.Budget
            };

            //các địa điểm du lịch tại đà nẵng
            plan.Attractions.Add("Bà Nà Hills");
            plan.Attractions.Add("Cầu Rồng");
            plan.Attractions.Add("Núi Ngũ Hành Sơn");
            plan.Attractions.Add("Biển Mỹ Khê");
            plan.Attractions.Add("Chợ Hàn");

            if (req.Budget < 3000000)
                plan.Hotel = "Khách sạn bình dân khu Hải Châu";
            else if (req.Budget < 7000000)
                plan.Hotel = "Mường Thanh Hotel";
            else
                plan.Hotel = "Novotel Danang Premier";

            return plan;
        }
    }
}
