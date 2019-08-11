using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Register.Application.Commands.Vehicles;
using Vehicle.Register.Domain.Repositories;

namespace Vehicle.Register.Application.CommandHandlers.Vehicles
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Domain.Aggregates.Vehicle>
    {
        private readonly IVehicleRepository vehicleRepository;

        public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<Domain.Aggregates.Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            Domain.Aggregates.Vehicle vehicle = this.vehicleRepository
                .Insert(new Domain.Aggregates.Vehicle(request.Name));
            await this.vehicleRepository.UnitOfWork.SaveEntitiesAsync();
            return vehicle;
        }
    }
}
