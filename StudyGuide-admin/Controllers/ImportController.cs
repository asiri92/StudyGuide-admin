using SG = StudyGuide.Entities;
using StudyGuide_admin.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyGuide.Entities;

namespace StudyGuide_admin.Controllers
{
    public class ImportController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IStudyGuideService _studyGuideService;

        public ImportController(ICustomerService customerService, IOrderService orderService, IStudyGuideService studyGuideService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _studyGuideService = studyGuideService;
        }

        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Import() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".txt")
                {
                    ViewBag.Message = "Error processing file - invalid file format.";
                    return View("Import");
                }

                using (StreamReader reader = new StreamReader(file.InputStream))
                {
                    string headerLine = reader.ReadLine();
                    string[] headers = headerLine.Split('|');

                    if (headers[0] == "CUSTOMERID" && headers[1] == "CUSTOMER NAME")
                    {
                        ImportCustomers(reader);
                    }
                    else if (headers[0] == "STUDY GUIDE CODE" && headers[1] == "STUDY GUIDE NAME")
                    {
                        ImportStudyGuides(reader);
                    }
                    else if (headers[0] == "CUSTOMERID" && headers[1] == "STUDY GUIDE IDS ORDERED")
                    {
                        ImportOrders(reader);
                    }
                    else
                    {
                        ViewBag.Message = "Error processing file - invalid file format.";
                        return View("Import");
                    }
                }
            }

            return View("Import");
        }

        //private methods to import txt file data
        private void ImportCustomers(StreamReader reader)
        {
            int imported = 0, failed = 0, skipped = 0;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                string[] fields = line.Split('|');

                if (fields.Length != 3)
                {
                    failed++;
                    continue;
                }

                string customerId = fields[0];
                string customerName = fields[1];
                string customerEmail = fields[2];

                var existingCustomer = _customerService.GetCustomers().FirstOrDefault(c => c.CustomerId == customerId);

                if(existingCustomer != null)
                {
                    skipped++;
                    continue;
                }

                try
                {
                    var customer = new SG.Customer
                    {
                        CustomerId = customerId,
                        CustomerName = customerName,
                        CustomerEmail = customerEmail
                    };

                    _customerService.AddCustomer(customer);
                    imported++;
                }
                catch
                {
                    failed++;
                }

            }

            ViewBag.Message = $"{imported} customers successfully imported, {failed} failed, {skipped} skipped";
        }

        private void ImportStudyGuides(StreamReader reader)
        {
            int imported = 0, failed = 0, skipped = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split('|');

                if (fields.Length != 3)
                {
                    failed++;
                    continue;
                }

                int studyGuideId;
                if (!int.TryParse(fields[0], out studyGuideId))
                {
                    failed++;
                    continue;
                }

                string studyGuideName = fields[1];
                decimal price;
                if (!decimal.TryParse(fields[2], out price))
                {
                    failed++;
                    continue;
                }

                var existingStudyGuide = _studyGuideService.GetStudyGuides().FirstOrDefault(sg => sg.StudyGuideId == studyGuideId);
                if (existingStudyGuide != null)
                {
                    skipped++;
                    continue;
                }

                try
                {
                    var studyGuide = new SG.StudyGuide
                    {
                        StudyGuideId = studyGuideId,
                        StudyGuideName = studyGuideName,
                        Price = price
                    };

                    _studyGuideService.AddStudyGuide(studyGuide);
                    imported++;
                }
                catch
                {
                    failed++;
                }
            }

            ViewBag.Message = $"{imported} study guides successfully imported, {failed} failed, {skipped} skipped";
        }

        private void ImportOrders(StreamReader reader)
        {
            int imported = 0, failed = 0, skipped = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split('|');

                if (fields.Length != 2)
                {
                    failed++;
                    continue;
                }

                string customerId = fields[0];
                int _studyGuideId;

                string[] studyguideIds = fields[1].Split(',');

                foreach (var studyguideId in  studyguideIds)
                {
                    if (!int.TryParse(studyguideId, out _studyGuideId))
                    {
                        failed++;
                        continue;
                    }


                    var existingCustomer = _customerService.GetCustomers().FirstOrDefault(c => c.CustomerId == customerId);
                    var existingStudyGuide = _studyGuideService.GetStudyGuides().FirstOrDefault(sg => sg.StudyGuideId == _studyGuideId);
                    if (existingCustomer == null || existingStudyGuide == null)
                    {
                        failed++;
                        continue;
                    }

                    var existingOrder = _orderService.GetOrders().FirstOrDefault(o => o.CustomerId == customerId && o.StudyGuideId == _studyGuideId);
                    if (existingOrder != null)
                    {
                        skipped++;
                        continue;
                    }

                    try
                    {
                        var order = new Order
                        {
                            CustomerId = customerId,
                            StudyGuideId = _studyGuideId,
                            IsCompleted = false
                        };

                        _orderService.AddOrder(order);
                        imported++;
                    }
                    catch
                    {
                        failed++;
                    }

                }


            }

            ViewBag.Message = $"{imported} orders successfully imported, {failed} failed, {skipped} skipped";
        }
    }
}