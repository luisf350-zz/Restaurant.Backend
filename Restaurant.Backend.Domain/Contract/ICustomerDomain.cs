using System.Threading.Tasks;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.Domain.Contract
{
    public interface ICustomerDomain : IDomainBase<Customer>
    {
        Task<Customer> Login(string email, string password);
    }
}