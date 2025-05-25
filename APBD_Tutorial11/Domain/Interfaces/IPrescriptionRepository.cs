using APBD_Tutorial11.Domain.Models;

namespace APBD_Tutorial11.Domain.Interfaces;

public interface IPrescriptionRepository
{
    Task AddPrescription(Prescription prescription);
}