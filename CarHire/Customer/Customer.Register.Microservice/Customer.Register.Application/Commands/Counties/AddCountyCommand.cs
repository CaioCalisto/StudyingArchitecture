using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Counties
{
    public class AddCountyCommand: IRequest<County>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
