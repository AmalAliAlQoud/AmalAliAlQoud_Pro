using ClinicApplication.Application.Interfaces.Base;

namespace ClinicApplication.Application.Interfaces.Base
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IMedical_RecordsRepository MMdical_RecordsRepository { get; }
        IPrescriptionsRepository PrescriptionsRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IApointmentRepository ApointmentRepository { get; }

        IUserRepository UserRepository { get; }





    }
}
