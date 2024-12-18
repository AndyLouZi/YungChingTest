using Infrasturcture;
using InterFace;
using Models;
using System.Runtime.InteropServices;

namespace Services
{
    public class CustomersCRUDService : ICustomersCRUDService
    {
        private readonly ApplicationDbContext _context;
        public CustomersCRUDService(ApplicationDbContext context) {
            _context = context;
        }
  
        public bool CreateCustomer(Customers customers)
        {
            if (customers == null)
            {
                return false;
            }

            try
            {
                _context.Customers.Add(customers);
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // 錯誤處理
                return false;
            }
        }

        public Customers SearchCustomer(string? customerID)
        {

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == customerID);



            return customer;
        }

    }
}
