using APBD_Tutorial11.Application.Commands;
using APBD_Tutorial11.Application.Results;
using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Domain.Models;
using AutoMapper;
using MediatR;
using OneOf;

namespace APBD_Tutorial11.Application.Handlers;

public class CreatePrescriptionHandler : IRequestHandler<CreatePrescriptionCommand, OneOf<SuccessResult, NotFoundResult, BadRequestResult>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;

    public CreatePrescriptionHandler(
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IMedicamentRepository medicamentRepository,
        IPrescriptionRepository prescriptionRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _medicamentRepository = medicamentRepository;
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<SuccessResult, NotFoundResult, BadRequestResult>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var doctorId = await _doctorRepository.GetDoctorsIdAsync(request.DoctorId);
        if (doctorId is null)
            return new NotFoundResult("Doctor not found");
        
        if (request.Medicaments.Count > 10)
            return new BadRequestResult("Medicaments count is more than 10");
        
        if (request.Date > request.DueDate)
            return new BadRequestResult("Due date is greater than date");

        var existingMedicamentIds = await _medicamentRepository.GetExistingMedicamentsIdsAsync(request.Medicaments);

        if (existingMedicamentIds.Count != request.Medicaments.Count)
            return new NotFoundResult("Medicaments not found");
        
        var patient = await _patientRepository.GetPatientsIdAsync(request.Patient.IdPatient);
        if (patient is null)
        {
            var mappedPatient = _mapper.Map<Patient>(request.Patient);
            await _patientRepository.AddPatientAsync(mappedPatient);
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = doctorId!.Value,
            IdPatient = request.Patient.IdPatient,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList(),
        };
        
        await _prescriptionRepository.AddPrescription(prescription);

        return new SuccessResult("Prescription created");
    }
}