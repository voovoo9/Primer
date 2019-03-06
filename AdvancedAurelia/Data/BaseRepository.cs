using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;

namespace Data
{
    public class BaseRepository
    {
        protected IDbConnection db;

        public BaseRepository()
        {
            string connectionString = "Host=localhost;Username=postgres;password=27Rakun27;Database=AureliaAPI";
            db = new NpgsqlConnection(connectionString);
        }
    }
}
