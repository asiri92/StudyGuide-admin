using StudyGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide_admin.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        void AddOrder(Order order);
        void FullFillOrder(string customerID, int studyguideId);
    }
}
