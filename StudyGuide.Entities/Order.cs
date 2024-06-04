using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int StudyGuideId { get; set; }
        public Boolean IsCompleted { get; set; }
        public DateTime? OrderCompletedDate { get; set; }
    }
}
