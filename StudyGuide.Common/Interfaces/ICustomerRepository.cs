using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using StudyGuide.Entities;

namespace StudyGuide.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        void AddCustomer(Customer customer);
    }
}
