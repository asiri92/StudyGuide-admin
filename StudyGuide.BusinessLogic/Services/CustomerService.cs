using StudyGuide.DataAccess.Interfaces;
using StudyGuide_admin.Interfaces;
using StudyGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyGuide_admin.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);

        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }
    }
}