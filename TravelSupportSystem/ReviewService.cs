using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelSupportSystem
{
    class ReviewService
    {
        private List<Review> reviews = new List<Review>();

        public void AddReview(string user, string content)
        {
            reviews.Add(new Review
            {
                User = user,
                Content = content,
                IsApproved = false
            });
        }

        public void ApproveReview(int index)
        {
            if (index >= 0 && index < reviews.Count)
                reviews[index].IsApproved = true;
        }

        public List<Review> GetApprovedReviews()
        {
            return reviews.Where(r => r.IsApproved).ToList();
        }
    }
}
