using Customer.Register.Application.Commands.Countries;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Countries
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Country>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCountryCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Country> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = this.customerRepository.SelectCountryById(request.CountryId);
            country.SetName(request.Name);
            this.customerRepository.Update(country);
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();

            return country;
        }
    }
}
