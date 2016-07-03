using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EF.Implementation;
using EntityManager.Models.Account;
using Microsoft.AspNet.Identity;

namespace EntityManager.Services
{
    public interface IUserService
    {
        IPrincipal GetCurrentUser();
        IList<ApplicationUser> GetAllUsers(ApplicationUserManager userManager);
        ApplicationUser GetUserByName(ApplicationUserManager userManager, string userName);
    }

    public class UserService : IUserService
    {
        public DbContextScopeFactory DbContextScopeFactory;
        private readonly IPrincipal _user;

        public UserService(DbContextScopeFactory dbContextScopeFactory)
        {
            DbContextScopeFactory = dbContextScopeFactory;
            _user = HttpContext.Current.User;
        }

        public IPrincipal GetCurrentUser()
        {
            return _user;
        }

        public IList<ApplicationUser> GetAllUsers(ApplicationUserManager userManager)
        {
            return userManager.Users.OrderBy(x => x.Email).ToList();
        }

        public ApplicationUser GetUserByName(ApplicationUserManager userManager, string userName)
        {
            var user = userManager.FindByName(userName);

            return user;
        }
    }
}