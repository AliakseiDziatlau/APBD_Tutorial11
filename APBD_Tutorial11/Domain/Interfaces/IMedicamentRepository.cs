using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Domain.Models;

namespace APBD_Tutorial11.Domain.Interfaces;

public interface IMedicamentRepository
{
    Task<Medicament?> GetMedicamentByIdAsync(int id);
    Task<List<int>> GetExistingMedicamentsIdsAsync(List<MedicamentDto> medicaments);
}