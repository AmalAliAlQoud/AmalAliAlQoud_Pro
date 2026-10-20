using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicApplication.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Appointment
    {

        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Patient Name is required")]
        [ForeignKey("Patient")]
        public int? Patient_ID {  get; set; }
        public Patient? Patient { get; set; }



        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Doctor Name is required")]
        [ForeignKey("Doctor")]
        public int? Doctor_ID { get; set; }
        public Doctor? Doctor { get; set; }


      
      


        [DisplayName("Apponitment Date")]
        [Required(ErrorMessage = "Apponitment Date is required")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DisplayName("Apponitment Time")]
        [Required(ErrorMessage = "Apponitment Time is required")]
        public TimeOnly Time { get; set; } = new TimeOnly(8, 30);
       




    }
}
