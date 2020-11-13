﻿using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(int UserId, string firstName,
            string emailAddress, string password, string role)
        {
            UserModel data = new UserModel
            {
                UserName = UserId,
                FirstName = firstName,
                EmailAddress = emailAddress,
                Password = password,
                Role = role
            };

            string sql = @"insert into db0.User (Username, Name, Email, PasswordHash, Role)
                           values (@Username, @LastName, @EmailAddress, @Password, @Role);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<UserModel> LoadUsers()
        {
            string sql = @"select Username, Name, Email, PasswordHash, Role
                           from db0.User;";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }
    }
}
