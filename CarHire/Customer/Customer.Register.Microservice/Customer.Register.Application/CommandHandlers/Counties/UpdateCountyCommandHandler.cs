using System.Threading;
using System.Threading.Tasks;
using Customer.Register.Application.Commands.Counties;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;

namespace Customer.Register.Application.CommandHandlers.Counties
{
    public class UpdateCountyCommandHandler : IRequestHandler<UpdateCountyCommand, County>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCountyCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<County> Handle(UpdateCountyCommand request, CancellationToken cancellationToken)
        {
            County county = this.customerRepository.SelectCountyById(request.CountyId);
            Country country = this.customerRepository.SelectCountryById(request.CountryId);
            county.SetName(request.Name);
            if (country != null)
            {
                county.SetCountry(country);
            }
            this.customerRepository.Update(county);
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();

            return county;
        }
    }
}
