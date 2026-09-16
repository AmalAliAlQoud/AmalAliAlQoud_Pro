using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ClinicApplication.Models

{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }


        [DisplayName("Medical Record ID")]
        [Required(ErrorMessage = "Medical Record ID is required")]
         public int mdRecord_ID { get; set; }

        
        [DisplayName("Medicine Name")]
        [Required(ErrorMessage = "Medicine Name is required")]
        public string Medicine_Name { get; set; } = "";

        [DisplayName("Dosage")]
        [Required(ErrorMessage = "Dosage is required")]
        public string Dosage { get; set; } = "";

    }
}
