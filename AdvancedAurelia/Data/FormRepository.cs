using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;


namespace Data
{
    public class FormRepository : BaseRepository, IFormRepository
    {
        public void AddForm(Form form)
        {
            db.Execute("INSERT INTO Form (FirstName, LastName, Email) VALUES (@firstName, @lastName, @email)", new { form.FirstName, form.LastName, form.Email });
        }

        public void DeleteForm(int id)
        {
            db.Execute("DELETE FROM Form WHERE Id=@id", new { id });
        }

        public List<Form> GetAllForms()
        {
            return db.Query<Form>("SELECT * FROM Form").ToList();
        }

        public Form GetFormById(int id)
        {
            return db.Query<Form>("SELECT * FROM Form WHERE Id = @id", new { id }).SingleOrDefault();
        }

        public void UpdateForm(int id, Form form)
        {
            var sql =
                "UPDATE Form " +
                "SET FirstName = @firstName, " +
                "    LastName  = @lastName, " +
                "    Email     = @email " +
                "WHERE Id = @id";
            db.Execute(sql, new { form.Email, form.FirstName, form.LastName, id });
        }
    }
}
