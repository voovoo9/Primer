using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class FormModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Email neme is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "FirstName neme is required")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "LastName neme is required")]
        public string LastName { get; set; }
    }
}
