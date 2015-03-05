using EF.Implementation;

namespace EntityManager.Services
{
    public class ClientQueryService : ServiceQueryBase
    {
        public ClientQueryService(DbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory)
        {
        }
        //put non generic methods here
    }
}