using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class Medical_RecordsRepository : IMedical_RecordsRepository
    {
        private readonly AppDbContext _db;
        public Medical_RecordsRepository(AppDbContext db)
        {
            _db = db;

        }

        public Task AddMedical_RecordAsync(Medical_Record md)
        {
            //  throw new NotImplementedException();
            _db.Medical_Records.Add(md);
            return _db.SaveChangesAsync();
        }

        public Task DeleteMedical_RecordAsync(string uuid)
        {

            var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == uuid);
            if (md != null)
            {
                _db.Medical_Records.Remove(md);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Medical_Record>> GetAllMedicalRecordAsyncInclude()
        {
            // throw new NotImplementedException();
            IEnumerable<Medical_Record> md = await _db.Medical_Records
                .Include(p => p.Patient).Include(d => d.Doctor)
                .ToListAsync();

            return md;


        }

        public async Task<IEnumerable<Medical_Record>> GetAllMedical_RecordAsync()
        {
            //throw new NotImplementedException();
            IEnumerable<Medical_Record> md = await _db.Medical_Records.ToListAsync();
            return md;
        }

        public async Task<Medical_Record> GetMedicalRecordAsyncByIdAsync(string uuid)
        {
            //var md = await _db.Medical_Records
            //    .Include(p => p.Patient).Include(d => d.Doctor)
            //     .FirstOrDefaultAsync(e => e.Uuid == uuid);

            var md = await _db.Medical_Records.FirstOrDefaultAsync(e => e.Uuid == uuid);
            return md;

            
        }

        public async Task<Medical_Record> GetMedicalRecordAsyncByIdAsyncInclude(string uuid)//_______
        {
            // throw new NotImplementedException();
            var md = await _db.Medical_Records.Include(p => p.Patient).Include(d => d.Doctor).FirstOrDefaultAsync(e => e.Uuid == uuid);
            return md;
        }

        public Task UpdateMedical_RecordAsync(Medical_Record md)
        {
            _db.Medical_Records.Update(md);
            return _db.SaveChangesAsync();
        }
    }
}
