using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;

namespace UserAuthorization.Domain.Repositories
{
    public interface ISubDomainRepository: IRepository<SubDomain>
    {
        SubDomain Insert(SubDomain subDomain);
        SubDomain Update(SubDomain subDomain);
        SubDomain SelectById(int subDomainId);
        void Delete(SubDomain subDomain);
    }
}
