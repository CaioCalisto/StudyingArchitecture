using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Countries
{
    public class UpdateCountryCommand: IRequest<Country>
    {
        public int CountryId { get; private set; }
        public string Name { get; set; }

        public void SetCountryId(int countryId)
        {
            this.CountryId = countryId;
        }
    }
}
