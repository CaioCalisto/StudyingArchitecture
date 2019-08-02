using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Countries
{
    public class AddCountryCommand: IRequest<Country>
    {
        public string Name { get; set; }
    }
}
