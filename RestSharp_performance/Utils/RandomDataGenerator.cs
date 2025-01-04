using System;

namespace ProjectName.Utils
{
    public static class RandomDataGenerator
    {
        public static (string FullName, string UserName, string Email, string Phone) GenerateRegistrationData()
        {
            var userName = "testUser" + new Random().Next(1000, 9999);
            return (
                FullName: "John Doe " + new Random().Next(1000, 9999),
                UserName: userName,
                Email: userName + "@gmail.com",
                Phone: "123456789" /*+ new Random().Next(1000, 9999)*/
            );
        }

        public static (string UserName, string Email) GenerateLoginData()
        {
            var userName = "testUser" + new Random().Next(1000, 9999);
            return (
                UserName: userName,
                Email: userName + "@example.com"
            );
        }
    }
}
