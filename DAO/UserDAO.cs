using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BloodBanker.Model;
using Dapper;

namespace BloodBanker.DAO
{
    public class UserDAO
    {
        public static List<User> LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("select * from USER", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<User> LoadUsers(string type)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where TYPE_OF_USER='{type}'",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<User> LoadUsersDistinct(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var app_id = GetAppID(id);
                var output =
                    cnn.Query<User>(
                        $"SELECT * FROM USER WHERE (SELECT FID FROM IDS WHERE FID=SSN) IN (SELECT DISTINCT RECIP_ID FROM ORDERS WHERE DONOR_ID='{app_id.ToString()}')",
                        new DynamicParameters());
                return output.ToList();
            }
        }


        public static long GetAppID(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IDS>($"select ID from IDS where FID='{user.SSN.ToString()}'",
                    new DynamicParameters());
                return output.ToList().First().ID;
            }
        }

        public static List<User> LoadUsers(string type, string bgrp)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where TYPE_OF_USER='{type}' and B_GRP='{bgrp}'",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        public static string GetDisease(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where SSN='{id.ToString()}'",
                    new DynamicParameters());
                return output.ToList().First().DISEASE;
            }
        }

        public static User GetUser(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where SSN='{id.ToString()}'",
                    new DynamicParameters());
                return output.ToList().First();
            }
        }

        public static List<User> LoadUsers(MedicalInstitution medInst)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where MI_ID='{medInst.MI_ID.ToString()}'",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        public static User LoginUser(string username, string password)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output =
                    cnn.Query<User>($"select * from USER where USERNAME='{username}' AND PASSWORD='{password}'",
                        new DynamicParameters());
                return output.Count() == 1 ? output.ToList()[0] : null;
            }
        }

        public static bool IsUsernameAvailable(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>($"select * from USER where USERNAME='{username}'",
                    new DynamicParameters());
                Console.WriteLine(output.Count());
                return output.Count() == 0 ? true : false;
            }
        }

        public static void UpdateUser(User user, string column, string value)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update USER set '{column}'='{value}' where SSN='{user.SSN.ToString()}'");
            }
        }

        public static void StoreUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(
                    "insert into USER values (@SSN,@PH_NO,@NAME,@B_GRP,@EMAIL,@ADDRESS,@CITY,@USERNAME,@PASSWORD,@TYPE_OF_USER,@DISEASE,@MI_ID)",
                    user);
            }
        }

        public static long GetAppID(decimal SSN)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IDS>($"select ID from IDS where FID='{SSN.ToString()}'",
                    new DynamicParameters());
                return output.ToList().First().ID;
            }
        }

        public static long GetUserID(decimal ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IDS>($"select FID from IDS where ID='{ID.ToString()}'", new DynamicParameters());
                return output.ToList().First().FID;
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}