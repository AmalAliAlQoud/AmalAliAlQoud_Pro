using ClinicApplication.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApplication.Application.Dtos
{
    public class Medical_RecordDto
    {
        //we need (Patient Name	/Doctor Name	/Diagnosis	/Notes) Id  Uuid  PatientName   DoctorName  Diagnosis   Notes

        public int Id { get; set; }
        public string Uuid { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = "";    
        public string Notes { get; set; } = "";

    }


    public class Medical_RecordCreateDto
    {
        //we need (Patient Name	/Doctor Name	/Diagnosis	/Notes) Id  Uuid  PatientName   DoctorName  Diagnosis   Notes
        public int Patient_ID { get; set; } 
        public int Doctor_ID { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

    }

    public class Medical_RecordUpdateDto: Medical_RecordCreateDto
    {
        public int Id { get; set; }
        public string? Uuid { get; set; } //= string.Empty;

        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }




    }

}
