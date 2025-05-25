using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Domain.Models;

namespace APBD_Tutorial11.Domain.Interfaces;

public interface IPatientRepository
{
    Task AddPatientAsync(Patient? patient);
    Task<int?> GetPatientsIdAsync(int id);
    Task<Patient?> GetPatientAsync(int id);
}