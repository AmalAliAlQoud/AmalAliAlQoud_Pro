using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicApplication.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";

        public string? Phone { get; set; }

        [DisplayName("Date of Birth")]
        public DateOnly Date_of_Birth {  get; set; } = DateOnly.FromDateTime(DateTime.Today);

    }
}
