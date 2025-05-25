using APBD_Tutorial11.Application.DTOs;
using MediatR;

namespace APBD_Tutorial11.Application.Queries;

public class GetPatientQuery : IRequest<PatientDetailsDto?>
{
    public int Id { get; set; }
}