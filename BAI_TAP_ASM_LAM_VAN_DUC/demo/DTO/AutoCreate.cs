using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.DTO
{
    public class AutoCreate
    {
        private static Random random = new Random();

        public static string GenerateRandomPassword(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";

            // Tạo một mảng các ký tự ngẫu nhiên
            var randomChars = Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray();

            // Kết hợp các ký tự thành mật khẩu
            return new string(randomChars);
        }
    }
}
