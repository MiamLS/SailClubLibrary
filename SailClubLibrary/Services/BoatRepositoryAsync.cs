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
    public class BoatRepositoryAsync : Connection, IBoatRepositoryAsync
    {
        private string _addBoatSql = "INSERT INTO Boat Values(@Model, @SailNumber, @EngineInfo, @Draft, @Width, @BoatLength, @YearOfConstruction, @BoatType)";
        private string _getAllBoatsSql = "SELECT * FROM Boat";
        private string _removeBoatSql = "DELETE FROM Boat WHERE SailNumber = @SailNumber";
        private string _searchBoatSql = "SELECT * FROM Boat WHERE SailNumber = @SailNumber";
        private string _updateBoatSql = "UPDATE Boat SET Model=@NewModel, EngineInfo = @NewEngineInfo, Draft = @NewDraft, Width = @NewWidth, BoatLength = @NewBoatLength, YearOfConstruction = @NewYearOfConstruction, BoatType = @NewBoatType WHERE SailNumber = @NewSailNumber";
        //private string _filterBoatSql = "SELECT * FROM Boat WHERE "
        public Task<int> Count => throw new NotImplementedException();

        public async Task AddBoatAsync(Boat boat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(_addBoatSql, connection);
                    command.Parameters.AddWithValue("@Model", boat.Model);
                    command.Parameters.AddWithValue("@SailNumber", boat.SailNumber);
                    command.Parameters.AddWithValue("@EngineInfo", boat.EngineInfo);
                    command.Parameters.AddWithValue("@Draft", boat.Draft);
                    command.Parameters.AddWithValue("@Width", boat.Width);
                    command.Parameters.AddWithValue("@BoatLength", boat.Length);
                    command.Parameters.AddWithValue("@YearOfConstruction", boat.YearOfConstruction);
                    command.Parameters.AddWithValue("@BoatType", boat.TheBoatType);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
        }

        public async Task<List<Boat>> FilterBoatsAsync(string filterCriteria)
        {
            List<Boat> allBoats = await GetAllBoatsAsync();
            List<Boat> filteredBoats = new List<Boat>();
            foreach (var item in allBoats)
            {
                if (item.ToString().ToLower().Contains(filterCriteria.ToLower()))
                {
                    filteredBoats.Add(item);
                }
            }
            return filteredBoats;
        }

        public async Task<List<Boat>> GetAllBoatsAsync()
        {
            List<Boat> boats = new List<Boat>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllBoatsSql, connection);
                    await command.Connection.OpenAsync(); //Det tager lidt tid at åbne connectionen, så det gør vi asynkront ved at sige 'await' og 'OpenAsync'. Vi skal også huske at gøre metoden async 'public async Task...'
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int boatId = reader.GetInt32("BoatId");
                        int boatType = reader.GetInt32("BoatType");
                        string model = reader.GetString("Model");
                        string sailNumber = reader.GetString("SailNumber");
                        string engineInfo = reader.GetString("EngineInfo");
                        double draft = reader.GetDouble("Draft");
                        double width = reader.GetDouble("Width");
                        double boatLength = reader.GetDouble("BoatLength");
                        string yearOfConstruction = reader.GetString("YearOfConstruction");
                        Boat boat = new Boat(boatId, (BoatType)boatType, model, sailNumber, engineInfo, draft, width, boatLength, yearOfConstruction);
                        boats.Add(boat);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                return boats;
            }
        }

        public async Task RemoveBoatAsync(string sailNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_removeBoatSql, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@SailNumber", sailNumber);
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

        public async Task<Boat?> SearchBoatAsync(string sailNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_searchBoatSql, connection);
                    command.Parameters.AddWithValue("@SailNumber", sailNumber);
                    await command.Connection.OpenAsync(); //Det tager lidt tid at åbne connectionen, så det gør vi asynkront ved at sige 'await' og 'OpenAsync'. Vi skal også huske at gøre metoden async 'public async Task...'
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    //await command.ExecuteNonQueryAsync();
                    if (await reader.ReadAsync())
                    {
                        int boatId = reader.GetInt32("BoatId");
                        int boatType = reader.GetInt32("BoatType");
                        string model = reader.GetString("Model");
                        string engineInfo = reader.GetString("EngineInfo");
                        double draft = reader.GetDouble("Draft");
                        double width = reader.GetDouble("Width");
                        double boatLength = reader.GetDouble("BoatLength");
                        string yearOfConstruction = reader.GetString("YearOfConstruction");
                        Boat boatToSearchFor = new Boat(boatId, (BoatType)boatType, model, sailNumber, engineInfo, draft, width, boatLength, yearOfConstruction);
                        return boatToSearchFor;
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

        public async Task UpdateBoatAsync(Boat boat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateBoatSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@NewModel", boat.Model);
                    command.Parameters.AddWithValue("@NewEngineInfo", boat.EngineInfo);
                    command.Parameters.AddWithValue("@NewDraft", boat.Draft);
                    command.Parameters.AddWithValue("@NewWidth", boat.Width);
                    command.Parameters.AddWithValue("@NewBoatLength", boat.Length);
                    command.Parameters.AddWithValue("@NewYearOfConstruction", boat.YearOfConstruction);
                    command.Parameters.AddWithValue("@NewBoatType", boat.TheBoatType);
                    command.Parameters.AddWithValue("@NewSailNumber", boat.SailNumber);
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
