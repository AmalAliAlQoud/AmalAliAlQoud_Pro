namespace ClinicApplication.Repositories.Base
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
