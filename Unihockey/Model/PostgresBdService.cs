using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Unihockey.Model
{
    internal class PostgresBdService
    {
        private const string DB_CONNECTION_STRING = "Server=localhost;Port=5432;Database=unihockey;User Id=postgres;Password=TPI;";

        private NpgsqlConnection conn = new NpgsqlConnection(DB_CONNECTION_STRING);

        private NpgsqlCommand cmd = new NpgsqlCommand();

        public void OpenConnection()
        {
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        //public NpgsqlDataReader ExecuteQuery(string query)
        //{
        //    NpgsqlCommand command = new NpgsqlCommand(query, conn);
        //    return command.ExecuteReader();
        //}

        public PostgresBdService() { }

    }
}
