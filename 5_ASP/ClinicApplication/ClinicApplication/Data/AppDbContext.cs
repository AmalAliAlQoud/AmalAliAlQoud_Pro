using Microsoft.EntityFrameworkCore;
using ClinicApplication.Models;


namespace ClinicApplication.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Medical_Record> Medical_Records { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }



    }
}
