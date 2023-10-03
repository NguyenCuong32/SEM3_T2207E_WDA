using demo.DTO;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.repository
{
    public interface IuserRepository
    {
        public List<User> getAllUser();
        public User getByUsername(string useranem);

        public response CreateUser(User user);

        public response DeleteUser(string username);


        public response UpdateUser(User user, string username);

        public List<User> FindUser(string tukhoa);

    }
}
