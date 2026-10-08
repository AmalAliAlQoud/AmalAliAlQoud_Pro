using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Medical_Record")]

        public int mdRecord_ID { get; set; }
        public Medical_Record? Medical_Record { get; set; }



        [DisplayName("Medicine Name")]
        [Required(ErrorMessage = "Medicine Name is required")]
        public string Medicine_Name { get; set; } = "";

        [DisplayName("Dosage")]
        [Required(ErrorMessage = "Dosage is required")]
        public string Dosage { get; set; } = "";

    }
}
