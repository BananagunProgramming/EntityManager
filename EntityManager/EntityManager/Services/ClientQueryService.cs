using EF.Implementation;
using EntityManager.Abstract;

namespace EntityManager.Services
{
    public interface IClientQueryService : IServiceQueryBase
    {
        
    }

    public class ClientQueryService : ServiceQueryBase, IClientQueryService
    {
        public ClientQueryService(DbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory)
        {}
    }
}