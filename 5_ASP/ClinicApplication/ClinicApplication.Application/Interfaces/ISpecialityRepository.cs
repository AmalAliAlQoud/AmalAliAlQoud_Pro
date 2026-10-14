using ClinicApplication.Application.Dtos;

using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
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
