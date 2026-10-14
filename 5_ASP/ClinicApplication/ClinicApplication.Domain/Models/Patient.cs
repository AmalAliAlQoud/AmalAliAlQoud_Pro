using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicApplication.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";

        public string? Phone { get; set; }

        [DisplayName("Date of Birth")]
        public DateOnly Date_of_Birth {  get; set; } = DateOnly.FromDateTime(DateTime.Today);

        // Navigation property for related Appointment
        public ICollection<Appointment>? Appointments { get; set; }

    }
}
