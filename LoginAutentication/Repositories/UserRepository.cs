using LoginAutentication.Models;
using System.Collections.Generic;
using System.Linq;

namespace LoginAutentication.Repositories
{
    public static class UserRepository
    {
        public static User Get(string email, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Email = "pedroramoss@email.com", Password = "123456"});
            users.Add(new User { Id = 2, Email = "teste@email.com", Password = "123teste"});
            return users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
