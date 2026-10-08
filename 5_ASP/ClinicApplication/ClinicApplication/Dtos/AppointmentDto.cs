using ClinicApplication.Models;

namespace ClinicApplication.Dtos
{
    public class AppointmentDto
    {
        // put the data only we need in page  (Patient Name/	Doctor Name	/Date	/Time)

        public int Id { get; set; }

        public string Uuid { get; set; }
        public string? PatientName { get; set; } = string.Empty;

        public string? DoctorName { get; set; } = string.Empty;

        public DateOnly Date { get; set; } 

        public TimeOnly Time { get; set; } 


    }

    public class AppointmentCreateDto
    {
        // put the data only we need in page  (PatientId /	DoctorId	/Date	/Time)

        public int? Patient_ID { get; set; } 

        public int? Doctor_ID { get; set; } 

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
          

    }

    public class AppointmentUpdateDto : AppointmentCreateDto
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = string.Empty;

        // public string? PatientName { get; set; } 



    }







}
