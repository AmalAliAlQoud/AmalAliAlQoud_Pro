using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ClinicApplication.Models

{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


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
