using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Domain.Models
{
    public class User
    {
        //[Index(nameof(Uuid), IsUnique = true)]

        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        public string Name { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }
    }
}
