
using EF.Implementation;
using EntityManager.Abstract;

namespace EntityManager.Services
{
    public class ClientCommandService : ServiceCommandBase, IClientCommandService
    {
        public ClientCommandService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory)
        {
            
        }
        //put non generic methods here
    }

    public interface IClientCommandService: IServiceCommandBase
    {
        //define non generic methods here
    }
}