using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Registration.Infrastructure.Database;
using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Application.Queries
{
    /// <summary>
    /// Queries.
    /// </summary>
    public class VehiclesQueries : IVehiclesQueries
    {
        private readonly IDatabaseQueries databaseQueries;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesQueries"/> class.
        /// </summary>
        /// <param name="databaseQueries">Database context.</param>
        /// <param name="mapper">Mapper.</param>
        public VehiclesQueries(IDatabaseQueries databaseQueries, IMapper mapper)
        {
            this.databaseQueries = databaseQueries;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public IEnumerable<Model.Vehicle> Find(Model.Vehicle vehicle)
        {
            IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>> query = this.databaseQueries.GetQuery<Infrastructure.Model.Vehicle>();

            List<TableEntityAdapter<Infrastructure.Model.Vehicle>> result = query
                .Where(c =>
            c.OriginalEntity.Brand == "Ford")
                .ToList();

            return this.mapper.Map<IEnumerable<Infrastructure.Model.Vehicle>, IEnumerable<Model.Vehicle>>(result.Select(c => c.OriginalEntity));
        }
    }
}
