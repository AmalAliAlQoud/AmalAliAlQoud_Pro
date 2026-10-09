using ClinicApplication.Dtos;

using ClinicApplication.Models;

namespace ClinicApplication.Repositories
{
    public interface ISpecialityRepository  
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtyAsync();
        Task<Specialty> GetSpecialtyAsyncByIdAsync(string uuid);
        Task AddSpecialtyAsync(Specialty Sp);
        Task UpdateSpecialtyAsync(Specialty Sp);
        Task DeleteSpecialtyAsync(string uuid);
    }
}
