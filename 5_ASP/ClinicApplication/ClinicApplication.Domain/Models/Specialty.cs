using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicApplication.Domain.Models
{
    public class Specialty
    {

        [Key]
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("Specialty Name")]
        [Required(ErrorMessage = "Specialty name is required")]
        public string Name { get; set; } = "";

        public ICollection<Doctor>? Doctor { get; set; }


    }
}
