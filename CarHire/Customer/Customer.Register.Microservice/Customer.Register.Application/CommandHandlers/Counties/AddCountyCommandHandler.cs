using Customer.Register.Application.Commands.Counties;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Counties
{
    public class AddCountyCommandHandler : IRequestHandler<AddCountyCommand, County>
    {
        private readonly ICustomerRepository customerRepository;

        public AddCountyCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<County> Handle(AddCountyCommand request, CancellationToken cancellationToken)
        {
            County county = this.customerRepository.Insert(County.Create(request.Name));
            Country country = this.customerRepository.SelectCountryById(request.CountryId);
            if (country != null)
            {
                county.SetCountry(country);
            }

            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();
            return county;
        }
    }
}
