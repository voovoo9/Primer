using AutoMapper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            this.CreateMap<Form, FormModel>();
        }
    }
}
