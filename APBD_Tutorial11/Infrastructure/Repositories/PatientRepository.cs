using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Domain.Models;
using APBD_Tutorial11.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial11.Infrastructure.Repositories;

public class PatientRepository : RepositoryBase, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context) { }

    public async Task AddPatientAsync(Patient? patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<int?> GetPatientsIdAsync(int id)
    {
        return await _context.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => p.IdPatient)
            .SingleOrDefaultAsync();
    }

    public async Task<Patient?> GetPatientAsync(int id)
    {
        return await _context.Patients
            .Where(p => p.IdPatient == id)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .SingleOrDefaultAsync();
    }
}