using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Domain.Models;
using APBD_Tutorial11.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial11.Infrastructure.Repositories;

public class MedicamentRepository : RepositoryBase, IMedicamentRepository
{
    public MedicamentRepository(AppDbContext context) : base(context) { }

    public async Task<Medicament?> GetMedicamentByIdAsync(int id)
    {
        return await _context.Medicaments.FindAsync(id);
    }

    public async Task<List<int>> GetExistingMedicamentsIdsAsync(List<MedicamentDto> medicaments)
    {
         return await _context.Medicaments
            .Where(m => medicaments.Select(x => x.IdMedicament).Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
    }
}