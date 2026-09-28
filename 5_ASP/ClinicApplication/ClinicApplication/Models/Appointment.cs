using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ClinicApplication.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Appointment
    {

        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("Patient ID")]
        [Required(ErrorMessage = "Patient ID is required")]
        public int Patient_ID {  get; set; }


        [DisplayName("Doctor ID")]
        [Required(ErrorMessage = "Doctor ID is required")]
        public int Doctor_ID { get; set; }

        [DisplayName("Apponitment Date")]
        [Required(ErrorMessage = "Apponitment Date is required")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DisplayName("Apponitment Time")]
        [Required(ErrorMessage = "Apponitment Time is required")]
        public TimeOnly Time { get; set; } = new TimeOnly(8, 30);
       




    }
}
