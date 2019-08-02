using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Counties
{
    public class UpdateCountyCommand: IRequest<County>
    {
        public int CountyId { get; private set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public void SetCountyId(int countyId)
        {
            this.CountyId = countyId;
        }
    }
}
