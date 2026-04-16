using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public class UserServiceAsync : Connection, IUserServiceAsync
    {
        private string queryString = "SELECT * FROM Users";
        private string insertSql = "INSERT INTO Users Values(@Username, @Password)";

        public async Task<bool> AddUserAsync(User newUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Id", newUser.Id);
                    command.Parameters.AddWithValue("@Username", newUser.Username);
                    command.Parameters.AddWithValue("@Password", newUser.Password);
                    command.Connection.Open();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("Id");
                        string username = reader.GetString("Username");
                        string password = reader.GetString("Password");
                        User newUser = new User(id, username, password);
                        users.Add(newUser);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                }
            }
            return users;
        }

        public async Task<User> VerifyUserAsync(string userName, string passWord)
        {
            List<User> users = await GetAllUsersAsync();

            //Implementer kode, der gennemløber alle users og checker om der findes en user med de parameteroverførte userName og        passWord
            //Hvis denne user findes skal user returneres

            foreach (var user in users)
            {
                if (userName.Equals(user.Username) && passWord.Equals(user.Password))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
