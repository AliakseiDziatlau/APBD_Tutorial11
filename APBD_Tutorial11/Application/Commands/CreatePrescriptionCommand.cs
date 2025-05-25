using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Application.Results;
using MediatR;
using OneOf;

namespace APBD_Tutorial11.Application.Commands;

public class CreatePrescriptionCommand : IRequest<OneOf<SuccessResult, NotFoundResult, BadRequestResult>>
{
    public PatientDto Patient { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}