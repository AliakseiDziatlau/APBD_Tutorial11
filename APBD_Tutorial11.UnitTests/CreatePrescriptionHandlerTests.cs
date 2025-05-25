using APBD_Tutorial11.Application.Commands;
using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Application.Handlers;
using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Domain.Models;
using AutoMapper;
using Moq;

namespace APBD_Tutorial11.UnitTests;

public class CreatePrescriptionHandlerTests : BaseTest
{
    private readonly Mock<IDoctorRepository> _doctorRepoMock;
    private readonly Mock<IMedicamentRepository> _medicamentRepoMock;
    private readonly Mock<IPrescriptionRepository> _prescriptionRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreatePrescriptionHandler _handler;

    public CreatePrescriptionHandlerTests()
    {
        _doctorRepoMock = new Mock<IDoctorRepository>();
        _medicamentRepoMock = new Mock<IMedicamentRepository>();
        _prescriptionRepoMock = new Mock<IPrescriptionRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreatePrescriptionHandler(
            _doctorRepoMock.Object,
            _patientRepoMock.Object,
            _medicamentRepoMock.Object,
            _prescriptionRepoMock.Object,
            _mapperMock.Object);
    }
    
    [Fact]
    public async Task Handle_ReturnsNotFound_WhenDoctorNotFound()
    {
        /*arrange*/
        var command = CreateValidCommand();
        _doctorRepoMock.Setup(r => r.GetDoctorsIdAsync(command.DoctorId))
            .ReturnsAsync((int?)null);

        /*act*/
        var result = await _handler.Handle(command, CancellationToken.None);

        /*assert*/
        Assert.True(result.IsT1); 
        var notFoundResult = result.AsT1;
        Assert.Equal("Doctor not found", notFoundResult.Message);
    }

    [Fact]
    public async Task Handle_ReturnsBadRequest_WhenTooManyMedicaments()
    {
        /*arrange*/
        var command = CreateValidCommand();
        command.Medicaments = new List<MedicamentDto>(new MedicamentDto[11]);

        _doctorRepoMock.Setup(r => r.GetDoctorsIdAsync(command.DoctorId)).ReturnsAsync(1); 

        /*act*/
        var result = await _handler.Handle(command, CancellationToken.None);

        /*assert*/
        Assert.True(result.IsT2); 
        Assert.Equal("Medicaments count is more than 10", result.AsT2.Message);
    }

    [Fact]
    public async Task Handle_ReturnsBadRequest_WhenDueDateBeforeDate()
    {
        /*arrange*/
        var command = CreateValidCommand();
        command.DueDate = command.Date.AddDays(-1);

        _doctorRepoMock.Setup(r => r.GetDoctorsIdAsync(command.DoctorId)).ReturnsAsync(1);

        /*act*/
        var result = await _handler.Handle(command, CancellationToken.None);

        /*assert*/
        Assert.True(result.IsT2);
        Assert.Equal("Due date is greater than date", result.AsT2.Message);
    }

    [Fact]
    public async Task Handle_ReturnsNotFound_WhenSomeMedicamentsAreMissing()
    {
        /*arrange*/
        var command = CreateValidCommand();

        _doctorRepoMock.Setup(r => r.GetDoctorsIdAsync(command.DoctorId)).ReturnsAsync(1);
        _medicamentRepoMock.Setup(r => r.GetExistingMedicamentsIdsAsync(command.Medicaments))
            .ReturnsAsync(new List<int>()); 

        /*act*/
        var result = await _handler.Handle(command, CancellationToken.None);

        /*assert*/
        Assert.True(result.IsT1); 
        var notFound = result.AsT1;
        Assert.Equal("Medicaments not found", notFound.Message);
    }

    [Fact]
    public async Task Handle_CreatesPatientAndPrescription_WhenAllIsValid()
    {
        /*arrange*/
        var command = CreateValidCommand();

        _doctorRepoMock.Setup(r => r.GetDoctorsIdAsync(command.DoctorId)).ReturnsAsync(1);
        _medicamentRepoMock.Setup(r => r.GetExistingMedicamentsIdsAsync(command.Medicaments))
            .ReturnsAsync(new List<int> { 101 });
        _patientRepoMock.Setup(r => r.GetPatientsIdAsync(command.Patient.IdPatient))
            .ReturnsAsync((int?)null);
        _mapperMock.Setup(m => m.Map<Patient>(command.Patient))
            .Returns(new Patient { IdPatient = 1 });

        /*act*/
        var result = await _handler.Handle(command, CancellationToken.None);

        /*assert*/
        Assert.True(result.IsT0);
        var success = result.AsT0;
        Assert.Equal("Prescription created", success.Message);

        _patientRepoMock.Verify(r => r.AddPatientAsync(It.IsAny<Patient>()), Times.Once);
        _prescriptionRepoMock.Verify(r => r.AddPrescription(It.IsAny<Prescription>()), Times.Once);
    }
    
    private CreatePrescriptionCommand CreateValidCommand()
    {
        return new CreatePrescriptionCommand
        {
            DoctorId = 1,
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(1),
            Patient = new PatientDto
            {
                IdPatient = 1,
                FirstName = "John",
                LastName = "Doe",
                Birthdate = new DateTime(1990, 1, 1)
            },
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto
                {
                    IdMedicament = 101,
                    Dose = 1,
                    Description = "Take once daily"
                }
            }
        };
    }
}