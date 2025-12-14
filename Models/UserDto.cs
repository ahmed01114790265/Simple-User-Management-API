namespace Simple_User_Management_API.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace UserApi.Models
    {
        public class UserDto
        {
            [Required, MaxLength(100)]
            public string FullName { get; set; } = default!;

            [Required, EmailAddress]
            public string Email { get; set; } = default!;

            [Range(0, 150)]
            public int Age { get; set; }
        }
    }

}
