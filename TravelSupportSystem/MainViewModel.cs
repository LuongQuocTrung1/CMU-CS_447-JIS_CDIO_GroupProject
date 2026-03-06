using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelSupportSystem
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TravelPlannerService planner = new TravelPlannerService();
        private WeatherService weather = new WeatherService();

        public double Budget { get; set; }
        public int Days { get; set; }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        public void GeneratePlan()
        {
            var req = new TravelRequest
            {
                Budget = Budget,
                Days = Days
            };

            var plan = planner.GeneratePlan(req);
            var packing = weather.GetPackingSuggestion();

            Result =
                $"Destination: Da Nang\n\n" +
                $"Hotel: {plan.Hotel}\n\n" +
                $"Attractions:\n - {string.Join("\n - ", plan.Attractions)}\n\n" +
                $"Packing suggestion:\n{packing}";
        }
    }
}
