using ClinicApplication.Models;

namespace ClinicApplication.Dtos
{
    public class DoctorDto
    {
        //data we need in this form (Name/	Specialty/	Phone)
        public int Id { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SpecialtyName { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

    }


    public class DoctorCreateDto
    {
        //data we need in this form (Name/	Specialty/	Phone)
        public string Name { get; set; } = string.Empty;
        public int? SpecialtyId { get; set; }


        public string? Phone { get; set; } = string.Empty;

    }

    public class DoctorUpdateDto: DoctorCreateDto
    {
        public int Id { get; set; }
        public string? Uuid { get; set; }

        public string? SpecialtyName { get; set; }


    }

}

   
