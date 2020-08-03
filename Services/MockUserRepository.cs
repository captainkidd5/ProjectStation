using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> userList;
        public MockUserRepository()
        {
            userList = new List<User>()
            {
                new User() { Id=1, Name="Michael",Email="300Cows@gmail.com",
                UserType=UserType.Admin, PhotoPath="michael.png"},
                new User() { Id=2, Name="Kosuke",Email="kShimamura@gmail.com",
                UserType=UserType.Admin, PhotoPath="kosuke.png"},
                new User() { Id=3, Name="Tuck",Email="armadillogod@gmail.com",
                UserType=UserType.Admin, PhotoPath="tuck.png"},
            };
            
        }
        public IEnumerable<User> GetAllClients()
        {
            return userList;
        }
    }
}
