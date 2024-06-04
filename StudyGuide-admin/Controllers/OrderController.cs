using StudyGuide.Entities;
using StudyGuide_admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyGuide_admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: Order
        public ActionResult Orders()
        {
            var orders = _orderService.GetOrders();
            return View(orders);
        }
        [HttpPost]
        public ActionResult FullfillOrder(Order order)
        {
            _orderService.FullFillOrder(order);
            return RedirectToAction("Orders");
        }
    }
}