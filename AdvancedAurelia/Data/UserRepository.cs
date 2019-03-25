using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Data
{    

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;

            //connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        private IConfiguration Configuration { get; }

        //private readonly string connectionString;

        public void AddUser(User user)
        {
            db.Execute("INSERT INTO Form (FirstName, LastName, Email) VALUES (@firstName, @lastName, @email)", new { user.FirstName, user.LastName, user.Email });
        }

        public void DeleteUser(int id)
        {
            db.Execute("DELETE FROM Form WHERE Id=@id", new { id });
        }

        public List<User> GetAllUsers()
        {
            return db.Query<User>("SELECT * FROM Form").ToList();
        }

        public User GetUserById(int id)
        {
            return db.Query<User>("SELECT * FROM Form WHERE Id = @id", new { id }).SingleOrDefault();
        }

        public void UpdateUser(int id, User user)
        {
            var sql =
                "UPDATE Form " +
                "SET FirstName = @firstName, " +
                "    LastName  = @lastName, " +
                "    Email     = @email " +
                "WHERE Id = @id";
            db.Execute(sql, new { user.Email, user.FirstName, user.LastName, id });
        }
    }
}
