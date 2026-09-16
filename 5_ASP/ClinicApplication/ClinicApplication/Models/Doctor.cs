using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ClinicApplication.Models
{
    public class Doctor
    {
        [Key]
        public int Id {  get; set; }


        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";


        [DisplayName("Specialty Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Specialty { get; set; } = "";

        public string? Phone { get; set; }



    }
}
