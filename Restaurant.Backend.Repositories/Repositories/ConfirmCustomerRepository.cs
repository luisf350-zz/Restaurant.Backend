using Microsoft.Extensions.Logging;
using Restaurant.Backend.Entities.Context;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Infrastructure;

namespace Restaurant.Backend.Repositories.Repositories
{
    public class ConfirmCustomerRepository : GenericRepository<ConfirmCustomer>, IConfirmCustomerRepository
    {
        public ConfirmCustomerRepository(AppDbContext context, ILogger<ConfirmCustomerRepository> logger) : base(context, logger)
        {
        }
    }
}
