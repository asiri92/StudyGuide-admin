using StudyGuide.DataAccess.Interfaces;
using StudyGuide.Entities;
using StudyGuide_admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyGuide_admin.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddOrder(Order order)
        {
            _orderRepository.AddOrder(order);
        }

        public void FullFillOrder(string customerID, int studyguideId)
        {
            _orderRepository.FullFillOrder(customerID, studyguideId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }
    }
}