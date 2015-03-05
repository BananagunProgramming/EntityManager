
using EF.Implementation;

namespace EntityManager.Services
{
    public class ClientCommandService : ServiceCommandBase
    {
        public ClientCommandService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory)
        {
            
        }
        //put non generic methods here
    }
}