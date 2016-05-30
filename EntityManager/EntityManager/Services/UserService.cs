using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using EF.Implementation;
using EntityManager.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityManager.Services
{
    public interface IUserService
    {
        IPrincipal GetCurrentUser();
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
    }
}