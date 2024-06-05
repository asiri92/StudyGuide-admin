using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.Entities;

namespace StudyGuide.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        void AddOrder(Order order);
        void FullFillOrder(string customerID, int studyguideId);
    }
}
