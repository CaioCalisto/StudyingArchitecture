using MediatR;

namespace Vehicle.Register.Application.Commands.Vehicles
{
    public class CreateVehicleCommand: IRequest<Domain.Aggregates.Vehicle>
    {
        public string Name { get; set; }
        public int VehicleTypeId { get; set; }
        public int BrandId { get; set; }
    }
}
