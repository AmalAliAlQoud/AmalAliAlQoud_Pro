using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApplication.Domain.Models
{

    [Index(nameof(Uuid), IsUnique = true)]

    public class Medical_Record
    {
        [Key]
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        [DisplayName("Patient ID")]
        [Required(ErrorMessage = "Patient ID is required")]
        [ForeignKey("Patient")]
        public int Patient_ID { get; set; }
        public Patient? Patient { get; set; }

        [DisplayName("Doctor ID")]
        [Required(ErrorMessage = "Doctor ID is required")]
        [ForeignKey("Doctor")]

        public int Doctor_ID { get; set; }
        public Doctor? Doctor { get; set; }
       

        [DisplayName("Diagnosis")]
        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; } = "";

        [DisplayName("Notes")]
        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; } = "";
    }
}
