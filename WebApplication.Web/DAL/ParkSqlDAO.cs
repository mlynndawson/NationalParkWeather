﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private string connectionString { get; set; }
        public ParkSqlDAO (string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Returns details about a specific park
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns>park object</returns>
        public Park GetParkDetails(string parkCode)
        {
            return GetAllParks().FirstOrDefault(p => p.ParkCode == parkCode);
        }

        /// <summary>
        /// Returns a list of all parks in the database
        /// </summary>
        /// <returns></returns>
        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand ("SELECT * FROM park;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park park = ConvertReaderToPark(reader);  
                        parks.Add(park);
                        
                    }
                }
            } 
            catch(SqlException ex)
            {
                throw;
            }
            return parks;
        }
  

        private Park ConvertReaderToPark(SqlDataReader reader)
        {
            Park park = new Park();

            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            park.Quote = Convert.ToString(reader["inspirationalQuote"]);
            park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(reader["parkDescription"]);
            park.EntryFee = Convert.ToInt32(reader["entryFee"]);
            park.AnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

            return park;
        }
    }
}
