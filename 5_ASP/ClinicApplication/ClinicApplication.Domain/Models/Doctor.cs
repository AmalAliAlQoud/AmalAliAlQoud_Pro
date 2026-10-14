using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClinicApplication.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Doctor
    {
        [Key]
        public int Id {  get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";


        

        [DisplayName("Specialty Name")]
        [Required(ErrorMessage = "Specialty Name is required")]
        [ForeignKey("Specialty")]
        public int? Specialty_ID { get; set; }
        public Specialty? Specialty { get; set; }





        public string? Phone { get; set; }

        // Navigation property for related Appointment
        public ICollection<Appointment>? Appointments { get; set; }



    }
}
