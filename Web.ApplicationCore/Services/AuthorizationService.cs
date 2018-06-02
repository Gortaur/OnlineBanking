using System;
using System.Threading.Tasks;
using OnlineBankingPrism.SharedEntities.Entities;
using Web.Infrastructure.Database.Repositories;

namespace Web.ApplicationCore.Services
{
    public static class AuthorizationService
    {
        public static async Task<Boolean> IsCredentialsValid(string login, string password)
        {
            var user = await UserRepository.GetById(login);
            if (user == null)
            {
                return false;
            }

            return user.Password == password;
        }
        private static readonly GenericRepository<User> UserRepository = new GenericRepository<User>();
    }
}
