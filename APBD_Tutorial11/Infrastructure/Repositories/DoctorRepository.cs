using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial11.Infrastructure.Repositories;

public class DoctorRepository : RepositoryBase, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }

    public async Task<int?> GetDoctorsIdAsync(int id)
    {
        return await _context.Doctors
            .Where(d => d.IdDoctor == id)
            .Select(d => d.IdDoctor)
            .SingleOrDefaultAsync();
    }
}