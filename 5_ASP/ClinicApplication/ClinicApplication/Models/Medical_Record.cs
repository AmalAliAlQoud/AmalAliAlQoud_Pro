using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicApplication.Models
{
    public class Medical_Record
    {
        [Key]
        public int Id { get; set; }



        [DisplayName("Patient ID")]
        [Required(ErrorMessage = "Patient ID is required")]
        public int Patient_ID { get; set; }

        [DisplayName("Doctor ID")]
        [Required(ErrorMessage = "Doctor ID is required")]
        public int Doctor_ID { get; set; }

        [DisplayName("Diagnosis")]
        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; } = "";

        [DisplayName("Notes")]
        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; } = "";
    }
}
