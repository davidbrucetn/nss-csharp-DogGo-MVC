using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. 
        // This class comes from the ASP.NET framework and is useful for retrieving things 
        // out of the appsettings.json file like connection strings.
        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }


        public List<Walk> GetAllWalks(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     SELECT wk.Id, wk.Date, wk.Duration, wk.DogId, wk.WalkerId, d.Name as DogName, d.Breed, d.Notes, d.ImageUrl, 
                            o.Name as OwnerName, w.Name as WalkerName, w.ImageUrl as WalkerImage, w.NeighborhoodId, n.Name as NHName
                        FROM Walks wk
                            Join Dog d on d.Id = wk.DogId
                            Join Walker w on w.Id = wk.WalkerId
                            Join Neighborhood n on n.Id = w.NeighborhoodId
                            Join Owner o on o.Id = d.OwnerId
                         Where wk.WalkerId = @walkerId;
                    ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();
                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            OwnerName = reader.GetString(reader.GetOrdinal("OwnerName")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl"))
                            },
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("WalkerImage")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Name = reader.GetString(reader.GetOrdinal("NHName")),
                                }
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }


    }
}