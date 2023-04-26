using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            var cred = _context.UserCredentials.Where(x => x.Email == email).First();
            return _context.Users.Where(x => x.Id == cred.UserId).First();
        }
    }
}
