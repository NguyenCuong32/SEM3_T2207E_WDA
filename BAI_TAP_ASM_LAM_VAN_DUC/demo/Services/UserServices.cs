using demo.DTO;
using demo.Models;
using demo.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Services
{
    public class UserServices : IuserServices
    {
        private readonly IuserRepository _userRepo;

        public UserServices(IuserRepository Iuser) => _userRepo = Iuser;

       
        public List<User> GetAllUser()
        {
            return _userRepo.getAllUser();
        }

        public User GetUserByUsername(string username)
        {
            return _userRepo.getByUsername(username);
        }

        public response CreateUser(User user)
        {
            try
            {

            
                if (user.Username == null || user.Username.Length == 0)
                {
                    return new response
                    {
                        status = 404,
                        message = "Vui lòng nhập username.",
                    };
                }
                else if (user.FullName == null || user.FullName.Length == 0)
                {
                    return new response
                    {
                        status = 404,
                        message = "Vui lòng nhập họ tên.",
                    };
                }else if (user.Email == null || user.Email.Length == 0)
                {
                    return new response
                    {
                        status = 404,
                        message = "Vui lòng nhập email.",
                    };
                }
                else
                {
                    user.NgayTao = DateTime.Now;
                    user.Active = true;
                    if (user.Avatar == null || user.Avatar.Length == 0)
                    {
                        user.Avatar = "test";
                    }
                    if (user.Password == null)
                    {
                        string password = AutoCreate.GenerateRandomPassword();

                    }
                    else
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    }
                    User checkUser = _userRepo.getByUsername(user.Username);
                    if (checkUser == null)
                    {
                        return _userRepo.CreateUser(user);

                    }
                    else
                    {
                        return new response
                        {
                            status = 404,
                            message = "Username "+user.Username+" đã tồn tại.",
                        };
                    }
                }

            }
            catch (Exception ex)
            {

                return new response
                {
                    status = 501,
                    message = ex.Message,
                };
            }

        }

        public response DeleteUser(string username)
        {
            return _userRepo.DeleteUser(username);
        }

        public response UpdateUser(User user, string username)
        {
            if (user.Username == null || user.Username.Length == 0)
            {
                return new response
                {
                    status = 404,
                    message = "Vui lòng nhập username.",
                };
            }
            else if (user.FullName == null || user.FullName.Length == 0)
            {
                return new response
                {
                    status = 404,
                    message = "Vui lòng nhập họ tên.",
                };
            }
            else if (user.Email == null || user.Email.Length == 0)
            {
                return new response
                {
                    status = 404,
                    message = "Vui lòng nhập email.",
                };
            }
            else
            {
                return _userRepo.UpdateUser(user, username);

            }
        }

        public List<User> FindUser(string tukhoa)
        {
            return _userRepo.FindUser(tukhoa);
        }
    }
}
