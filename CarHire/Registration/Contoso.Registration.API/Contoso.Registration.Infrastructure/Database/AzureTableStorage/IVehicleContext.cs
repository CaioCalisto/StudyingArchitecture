using System.Threading.Tasks;
using Contoso.Registration.Domain.Aggregate;

namespace Contoso.Registration.Infrastructure.Database.AzureTableStorage
{
    public interface IVehicleContext
    {
        /// <summary>
        /// Insert new entity in database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="partitionKey">Partition key.</param>
        /// <param name="rowKey">Row key.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Vehicle> InsertAsync(Vehicle entity, string partitionKey, string rowKey);
    }
}