using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<Specialty>> GetAllSpecialty();
        Task<Specialty> GetSpecialtyById(string uuid);
        Task AddSpecialty(Specialty Sp);
        Task UpdateSpecialty(Specialty Sp);
        Task DeleteSpecialty(string uuid);
    }
}
