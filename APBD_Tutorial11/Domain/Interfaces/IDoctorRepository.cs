using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Domain.Models;

namespace APBD_Tutorial11.Domain.Interfaces;

public interface IDoctorRepository
{
    Task<int?> GetDoctorsIdAsync(int id);
}