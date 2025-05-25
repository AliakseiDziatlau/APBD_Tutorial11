using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Application.Queries;
using APBD_Tutorial11.Domain.Interfaces;
using MediatR;

namespace APBD_Tutorial11.Application.Handlers;

public class GetPatientHandler : IRequestHandler<GetPatientQuery, PatientDetailsDto?>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDetailsDto?> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetPatientAsync(request.Id);
        if (patient == null)
            return null;

        return new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Medicaments = p.PrescriptionMedicaments
                        .Select(pm => new MedicamentDetailsDto()
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Dose = pm.Dose
                        }).ToList(),
                    Doctor = new DoctorDto()
                    {
                        IdDoctor = p.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                    }
                }).ToList(),
        };
    }
}