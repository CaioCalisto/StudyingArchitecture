using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Common;

namespace Customer.Register.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Aggregate.Customer>
    {
        Aggregate.Customer Insert(Aggregate.Customer costumer);
        Aggregate.Customer Update(Aggregate.Customer costumer);
        Aggregate.Customer SelectByCustomerIdentity(int customerIdentity);
        Address Insert(Address address);
        Address Update(Address address);
        Address SelectAddressById(int addressId);
        County Insert(County county);
        County Update(County county);
        County SelectCountyById(int countyId);
        Country Insert(Country country);
        Country Update(Country country);
        Country SelectCountryById(int countryId);
    }
}
