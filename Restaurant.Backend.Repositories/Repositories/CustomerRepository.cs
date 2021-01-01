using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Entities.Context;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Infrastructure;

namespace Restaurant.Backend.Repositories.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context, ILogger<IdentificationTypeRepository> logger) : base(context, logger)
        {
        }

        public Task<Customer> Login(byte[] password, byte[] salt)
        {
            return Context.Customers.FirstOrDefaultAsync(x => x.PasswordHash == password 
                                                              && x.PasswordSalt == salt);
        }
    }
}
