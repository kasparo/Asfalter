using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using Asfalter.Common.Dao;

namespace Asfalter.Engine
{
    internal class DataProvider
    {
        private MySqlConnection Connection { get; set; }

        public DataProvider()
        {
            Connection = new MySqlConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Asfalter"].ConnectionString;

            OpenConnection();
        }

        public void InsertUnit(Guid systemId)
        {
            MySqlCommand insert = new MySqlCommand();
            insert.CommandText = "INSERT INTO unit (systemId, created) VALUES (@systemId, @created)";
            insert.Connection = Connection;

            insert.Parameters.Add(new MySqlParameter("systemId", systemId.ToString()));
            insert.Parameters.Add(new MySqlParameter("created", DateTime.Now));
         
            insert.ExecuteNonQuery();
        }

        public void InsertUnitRecord(UnitRecord record, int idUnit)
        {
            MySqlCommand insert = new MySqlCommand();
            insert.CommandText = "INSERT INTO unit_record (unitId, currentTime, weight, speed) VALUES (@unitId, @currentTime, @weight, @speed)";
            insert.Connection = Connection;

            insert.Parameters.Add(new MySqlParameter("unitId", idUnit));
            insert.Parameters.Add(new MySqlParameter("currentTime", record.CurrnetTime));
            insert.Parameters.Add(new MySqlParameter("weight", record.Weight));
            insert.Parameters.Add(new MySqlParameter("speed", record.Speed));

            insert.ExecuteNonQuery();
        }

        private void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch(Exception ex)
            {
                Logger.Write(ex.ToString());
            }
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
