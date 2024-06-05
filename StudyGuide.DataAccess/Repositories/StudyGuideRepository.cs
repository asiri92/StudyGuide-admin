using StudyGuide.DataAccess.Interfaces;
using SG = StudyGuide.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace StudyGuide.DataAccess.Repositories
{
    public class StudyGuideRepository : IStudyGuideRepository
    {
        private string _connectionString;
        public StudyGuideRepository() 
        {
            _connectionString = DbConnectionStringProvider.Instance.ConnectionString;
        }
        public void AddStudyGuide(SG.StudyGuide studyGuide)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AddStudyGuide", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudyGuideId", studyGuide.StudyGuideId);
                command.Parameters.AddWithValue("@StudyGuideName", studyGuide.StudyGuideName);
                command.Parameters.AddWithValue("@Price", studyGuide.Price);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SG.StudyGuide> GetStudyGuides()
        {
            var StudyGuides = new List<SG.StudyGuide>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetStudyGuides", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    StudyGuides.Add(new SG.StudyGuide
                    {
                        StudyGuideId = Convert.ToInt32(reader["StudyGuideId"]),
                        StudyGuideName = reader["StudyGuideName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"].ToString())
                    });
                }
            }

            return StudyGuides;
        }
    }
}
