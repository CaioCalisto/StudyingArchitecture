using System.Threading.Tasks;
using Contoso.Registration.Domain.Aggregate;
using Contoso.Registration.Domain.Ports;

namespace Contoso.Registration.Infrastructure.Database.AzureTableStorage
{
    public class TableStorageAdapter : IVehicleRepository
    {
        private readonly IVehicleContext vehicleContext;

        public TableStorageAdapter(IVehicleContext vehicleContext)
        {
            this.vehicleContext = vehicleContext;
        }

        public async Task<Vehicle> InsertAsync(Vehicle entity)
        {
            return await this.vehicleContext.InsertAsync(entity, entity.Brand, $"{entity.Brand} {entity.Name} {entity.Category.ToString().ToUpper()}");
        }
    }
}