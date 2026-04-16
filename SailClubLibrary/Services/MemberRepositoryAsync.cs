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
    public class MemberRepositoryAsync : Connection, IMemberRepositoryAsync
    {
        private string _addMemberSql = "INSERT INTO SailClubMember Values(@FirstName, @SurName, @PhoneNumber, @MemberAddress, @City, @Mail, @MemberType, @MemberRole, @MemberImage)";
        private string _getAllMembersSql = "SELECT * FROM SailClubMember";
        private string _removeMemberSql = "DELETE FROM SailClubMember WHERE PhoneNumber = @PhoneNumber";
        private string _searchMemberSql = "SELECT * FROM SailClubMember WHERE PhoneNumber = @PhoneNumber";
        private string _updateMemberSql = "UPDATE SailClubMember SET FirstName=@NewFirstName, SurName = @NewSurname, MemberAddress = @NewMemberAddress, City = @NewCity, Mail = @NewMail, MemberType = @NewMemberType, MemberRole = @NewMemberRole WHERE PhoneNumber = @NewPhoneNumber";
        public Task<int> Count => throw new NotImplementedException();

        public async Task AddMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(_addMemberSql, connection);
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@SurName", member.SurName);
                    command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
                    command.Parameters.AddWithValue("@MemberAddress", member.Address);
                    command.Parameters.AddWithValue("@City", member.City);
                    command.Parameters.AddWithValue("@Mail", member.Mail);
                    command.Parameters.AddWithValue("@MemberType", member.TheMemberType);
                    command.Parameters.AddWithValue("@MemberRole", member.TheMemberRole);
                    command.Parameters.AddWithValue("@MemberImage", member.MemberImage);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
        }

        public Task<List<Member>> FilterMembersAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(_getAllMembersSql, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string firstName = reader.GetString("FirstName");
                        string surName = reader.GetString("SurName");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string memberAddress = reader.GetString("MemberAddress");
                        string city = reader.GetString("City");
                        string mail = reader.GetString("Mail");
                        int memberType = reader.GetInt32("MemberType");
                        int memberRole = reader.GetInt32("MemberRole");
                        string memberImage = reader.GetString("MemberImage");
                        Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, (MemberType)memberType, (MemberRole)memberRole, memberImage);
                        members.Add(member);
                    }
                    reader.CloseAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                return members;
            }
        }

        public Task PrintAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_removeMemberSql, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@Phonenumber", member.PhoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message); ;
                }
            }
        }

        public async Task<Member?> SearchMemberAsync(string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_searchMemberSql, connection);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    await command.Connection.OpenAsync(); //Det tager lidt tid at åbne connectionen, så det gør vi asynkront ved at sige 'await' og 'OpenAsync'. Vi skal også huske at gøre metoden async 'public async Task...'
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    //await command.ExecuteNonQueryAsync();
                    if (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string firstName = reader.GetString("FirstName");
                        string surName = reader.GetString("SurName");
                        string memberAddress = reader.GetString("MemberAddress");
                        string city = reader.GetString("City");
                        string mail = reader.GetString("Mail");
                        int memberType = reader.GetInt32("MemberType");
                        int memberRole = reader.GetInt32("MemberRole");
                        string memberImage = reader.GetString("MemberImage");
                        Member memberToSearchFor = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, (MemberType)memberType, (MemberRole)memberRole, memberImage);
                        return memberToSearchFor;
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                return null;
            }
        }

        public async Task UpdateMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateMemberSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@NewFirstName", member.FirstName);
                    command.Parameters.AddWithValue("@NewSurname", member.SurName);
                    command.Parameters.AddWithValue("@NewMemberAddress", member.Address);
                    command.Parameters.AddWithValue("@NewCity", member.City);
                    command.Parameters.AddWithValue("@NewMail", member.Mail);
                    command.Parameters.AddWithValue("@NewMemberType", member.TheMemberType);
                    command.Parameters.AddWithValue("@NewMemberRole", member.TheMemberRole);
                    command.Parameters.AddWithValue("@NewPhoneNumber", member.PhoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
        }
    }
}
