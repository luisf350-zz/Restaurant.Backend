using Restaurant.Backend.Common.Constants;
using Restaurant.Backend.Common.Utils;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Backend.Domain.Implementation
{
    public class CustomerDomain : DomainBase<Customer>, ICustomerDomain
    {
        public CustomerDomain(ICustomerRepository repository) : base(repository)
        {
        }

        public async Task<Customer> Login(string email, string password)
        {
            var customer = (await Repository.GetBy(x => x.Email == email)).FirstOrDefault();

            if (customer == null || !PasswordUtils.VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
            {
                throw new Exception(Constants.LoginNotValid);
            }

            return customer;
        }
    }
}
