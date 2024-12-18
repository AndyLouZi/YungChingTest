using Models;

namespace InterFace
{
    public interface ICustomersCRUDService
    {
        bool CreateCustomer(Customers customers);

        Customers SearchCustomer(string? customerID);
    }
}
