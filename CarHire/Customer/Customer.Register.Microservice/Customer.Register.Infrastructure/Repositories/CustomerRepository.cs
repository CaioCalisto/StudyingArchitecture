using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Common;
using Customer.Register.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Register.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDBContext customerDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.customerDBContext; }
        }

        public CustomerRepository(CustomerDBContext customerDBContext)
        {
            this.customerDBContext = customerDBContext ?? throw new ArgumentNullException(nameof(customerDBContext));
        }

        public Domain.Aggregate.Customer Insert(Domain.Aggregate.Customer costumer)
        {
            return this.customerDBContext
                .Customers
                .Add(costumer)
                .Entity;
        }

        public Domain.Aggregate.Customer SelectByCustomerIdentity(int identity)
        {
            return this.customerDBContext
                .Customers
                .Where(c => c.CustomerIdentity == identity)
                .FirstOrDefault();
        }

        public Domain.Aggregate.Customer Update(Domain.Aggregate.Customer costumer)
        {
            return this.customerDBContext
                .Update(costumer)
                .Entity;
        }

        public Address Insert(Address address)
        {
            return this.customerDBContext
                   .Addresses
                   .Add(address)
                   .Entity;
        }

        public Address Update(Address address)
        {
            return this.customerDBContext
                   .Update(address)
                   .Entity;
        }

        public County Insert(County county)
        {
            return this.customerDBContext
                      .Counties
                      .Add(county)
                      .Entity;
        }

        public County Update(County county)
        {
            return this.customerDBContext
                      .Update(county)
                      .Entity;
        }

        public Country Insert(Country country)
        {
            return this.customerDBContext
                         .Countries
                         .Add(country)
                         .Entity;
        }

        public Country Update(Country country)
        {
            return this.customerDBContext
                         .Update(country)
                         .Entity;
        }

        public Address SelectAddressById(int addressId)
        {
            return this.customerDBContext
                   .Addresses
                   .Where(a => a.AddressId == addressId)
                   .FirstOrDefault();
        }

        public County SelectCountyById(int countyId)
        {
            return this.customerDBContext
                      .Counties
                      .Where(a => a.CountyId == countyId)
                      .FirstOrDefault();
        }

        public Country SelectCountryById(int countryId)
        {
            return this.customerDBContext
                         .Countries
                         .Where(a => a.CountryId == countryId)
                         .FirstOrDefault();
        }
    }
}
