﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private string connectionString { get; set; }
        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateSurvey(Survey survey)
        {
            bool saved = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result VALUES(@parkCode, @emailAddress, @state, @activityLevel);", conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return saved;
        }

        public IList<SurveyResult> Results()
        {
            IList<SurveyResult> results = new List<SurveyResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT parkCode, COUNT(parkCode) FROM survey_result GROUP BY parkCode", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SurveyResult survey = ConvertReaderToSurveyResults(reader);
                        results.Add(survey);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return results;
        }

        private SurveyResult ConvertReaderToSurveyResults(SqlDataReader reader)
        {
            SurveyResult survey = new SurveyResult();
            survey.ParkCode = Convert.ToString(reader["parkCode"]);
            return survey;
        }
    }
}