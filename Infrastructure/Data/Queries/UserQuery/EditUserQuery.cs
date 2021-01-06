using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries.UserQuery
{
    public class EditUserQuery
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        public string DisplayFirstName { get; set; }
        public string DisplayLastName { get; set; }
        public string Image { get; set; }
    }
}
