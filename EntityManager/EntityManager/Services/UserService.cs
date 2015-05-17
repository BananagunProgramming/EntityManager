using System.Security.Principal;
using System.Web;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.Services;
using EntityManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityManager.Services
{
    public interface IUserService
    {
        string EntityCode { get; set; }
    }

    public class UserService : IUserService
    {
        public DbContextScopeFactory DbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();
        private readonly IPrincipal _user;
        public string EntityCode { get; set; }
        

        public UserService(DbContextScopeFactory dbContextScopeFactory)
        {
            DbContextScopeFactory = dbContextScopeFactory;
            _user = HttpContext.Current.User;
            EntityCode = GetEntityCode();
        }

        private string GetEntityCode()
        {
            using (var dbContextScope = DbContextScopeFactory.CreateReadOnly())
            {

                var context = dbContextScope.DbContexts.Get<ApplicationDbContext>();

                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                var currentUser = manager.FindById(_user.Identity.GetUserId());
                return currentUser.EntityCode;
            }
        }
    }
}