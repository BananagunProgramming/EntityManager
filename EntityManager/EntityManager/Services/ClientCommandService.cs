
using System;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public class ClientCommandService : ServiceCommandBase, IClientCommandService
    {
        public ClientCommandService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory)
        {
            
        }

        //put non generic methods here
        public void CreateNewClientEntity(Client client)
        {
            //Client specific defaults
            client.ClientId = Guid.NewGuid();
            client.CreatedDate = DateTime.Now;

            CreateEntity(client);
        }
    }

    public interface IClientCommandService: IServiceCommandBase
    {
        void CreateNewClientEntity(Client client);
    }
}