using ClinicApplication.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApplication.Dtos
{
    public class PrescriptionDto
    {

        //we need (	Medical Record ID	Medicine Name	Dosage)  Id  Uuid   mdRecord_ID     Medicine_Name  Dosage

        public int Id { get; set; }
        public string Uuid { get; set; } 


        public int mdRecord_ID { get; set; }
              
        public string Medicine_Name { get; set; } = "";
                
        public string Dosage { get; set; } = "";
    }


    public class PrescriptionCreateDto
    {

        //we need (	Medical Record ID	Medicine Name	Dosage)  Id  Uuid   mdRecord_ID     Medicine_Name  Dosage

        public int mdRecord_ID { get; set; }

        public string Medicine_Name { get; set; } = "";

        public string Dosage { get; set; } = "";
    }

    public class PrescriptionUpdateDto: PrescriptionCreateDto
    {
        public int Id { get; set; }
        public string? Uuid { get; set; }
    }
    



    }
