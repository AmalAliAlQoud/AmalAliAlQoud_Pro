using ClinicApplication.Infrastructure.Data;
using ClinicApplication.Application.Interfaces;
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Infrastructure.Repositories;

namespace ClinicApplication.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            PatientRepository = new PatientRepository(db);
            DoctorRepository = new DoctorRepository(db);
            MMdical_RecordsRepository = new Medical_RecordsRepository(db);
            PrescriptionsRepository = new PrescriptionsRepository(db);
            SpecialityRepository = new SpecialtyRepository(db);
            ApointmentRepository = new ApointmentRepository(db);
            UserRepository = new UserRepository(db);


        }
        public IPatientRepository PatientRepository { get; }

        public IDoctorRepository DoctorRepository { get; }

        public IMedical_RecordsRepository MMdical_RecordsRepository { get; }

        public IPrescriptionsRepository PrescriptionsRepository { get; }

        public ISpecialityRepository SpecialityRepository { get; }

        public IApointmentRepository ApointmentRepository { get; }

       

        public IUserRepository UserRepository { get; }
    }
}
