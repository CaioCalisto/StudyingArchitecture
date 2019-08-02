using Customer.Register.Application.Commands.Countries;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Countries
{
    public class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, Country>
    {
        private readonly ICustomerRepository customerRepository;

        public AddCountryCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Country> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = this.customerRepository.Insert(Country.Create(request.Name));
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();
            return country;
        }
    }
}
