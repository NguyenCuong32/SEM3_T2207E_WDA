using demo.DTO;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.repository
{

    public class UserRepository : IuserRepository

    {
        private readonly JIRAContext _db;
        public UserRepository(JIRAContext db) => _db = db;

        public response CreateUser(User user)
        {
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return new response
                {
                    status = 200,
                    message = "Tạo thành công",
                };
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

        public List<User> getAllUser()
        {
            return _db.Users.ToList();
        }

        public User getByUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public response DeleteUser(string username)
        {
            try
            {
                User user = _db.Users.FirstOrDefault(x => x.Username == username);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    return new response
                    {
                        status = 200,
                        message = "Xóa thành công user " + username,
                    };
                }
                else
                {
                    return new response
                    {
                        status = 404,
                        message = "Không tìm thấy user " + username,
                    };
                }
            }
            catch (Exception ex)
            {
                return new response
                {
                    status = 501,
                    message = "Đã xảy ra lỗi không mong muốn: " + ex.Message,
                };
            }
           
             
        }


        public response UpdateUser(User user , string username)
        {
            try
            {
                User newUser = _db.Users.FirstOrDefault(x => x.Username == username);
                if (user != null)
                {
                    newUser.FullName = user.FullName;
                    newUser.NgayCapNhat = DateTime.Now;
                    newUser.Email = user.Email;
                    newUser.Active = user.Active;
                    newUser.Avatar = user.Avatar;
                    _db.SaveChanges();
                    return new response 
                    {
                        status = 200,
                        message = "Cập nhật thành công" ,
                    };
                }
                else
                {
                    return new response
                    {
                        status = 404,
                        message = "Không tìm thấy user " + username,
                    };
                }
            }
            catch (Exception ex)
            {

                return new response
                {
                    status = 501,
                    message = "Đã xảy ra lỗi không mong muốn: " + ex.Message,
                };
            }


        }



        public List<User> FindUser(string tukhoa)
        {
            List<User> users =(from table1 in _db.Users
             where table1.Username.ToLower().Contains(tukhoa.ToLower()) || table1.FullName.ToLower().Contains(tukhoa.ToLower()) || table1.Email.ToLower().Contains(tukhoa.ToLower())
             select table1).ToList();
            return users;
        }

    }
}
