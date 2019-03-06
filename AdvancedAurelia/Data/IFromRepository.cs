using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IFormRepository
    {
        void AddForm(Form form);

        void DeleteForm(int id);

        List<Form> GetAllForms();

        Form GetFormById(int id);

        void UpdateForm(int id, Form form);
    }
}
