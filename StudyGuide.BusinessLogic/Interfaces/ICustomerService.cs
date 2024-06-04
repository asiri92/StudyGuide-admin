using StudyGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide_admin.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        void AddCustomer(Customer customer);
    }
}
