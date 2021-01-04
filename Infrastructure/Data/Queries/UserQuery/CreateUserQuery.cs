using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Queries.UserQuery
{
    public class CreateUserQuery
    {
        [Required(ErrorMessage = "Укажите Имя")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Неверный размер")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
