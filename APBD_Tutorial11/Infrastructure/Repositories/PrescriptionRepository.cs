using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Domain.Models;
using APBD_Tutorial11.Infrastructure.Persistence;

namespace APBD_Tutorial11.Infrastructure.Repositories;

public class PrescriptionRepository : RepositoryBase, IPrescriptionRepository
{
    public PrescriptionRepository(AppDbContext context) : base(context) { }

    public async Task AddPrescription(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }
}