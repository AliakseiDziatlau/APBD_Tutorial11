using APBD_Tutorial11.Application.Handlers;
using APBD_Tutorial11.Application.Queries;
using APBD_Tutorial11.Domain.Models;
using Moq;

namespace APBD_Tutorial11.UnitTests;

public class GetPatientHandlerTests : BaseTest
{
    private readonly GetPatientHandler _handler;

    public GetPatientHandlerTests()
    {
        _handler = new GetPatientHandler(_patientRepoMock.Object);
    }
    
    [Fact]
    public async Task Handle_ReturnsPatientDetails_WhenPatientExists()
    {
        /*arrange*/
        var patientId = 1;
        _patientRepoMock.Setup(repo => repo.GetPatientAsync(patientId)).ReturnsAsync(GetSamplePatient());
        
        var query = new GetPatientQuery { Id = patientId };

        /*act*/
        var result = await _handler.Handle(query, CancellationToken.None);

        /*assert*/
        Assert.NotNull(result);
        Assert.Equal(patientId, result!.IdPatient);
        Assert.Equal("John", result.FirstName);
        Assert.Single(result.Prescriptions);
        Assert.Equal("Ibuprofen", result.Prescriptions[0].Medicaments[0].Name);
        Assert.Equal("Dr. Alice", result.Prescriptions[0].Doctor.FirstName);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenPatientNotFound()
    {
        /*arrange*/
        _patientRepoMock.Setup(repo => repo.GetPatientAsync(It.IsAny<int>())).ReturnsAsync((Patient?)null);
        
        var query = new GetPatientQuery { Id = 99 };

        /*act*/
        var result = await _handler.Handle(query, CancellationToken.None);

        /*assert*/
        Assert.Null(result);
    }

    private Patient GetSamplePatient()
    {
        return new Patient
        {
            IdPatient = 1,
            FirstName = "John",
            LastName = "Doe",
            Birthdate = new DateTime(1990, 1, 1),
            Prescriptions = new List<Prescription>
            {
                new Prescription
                {
                    IdPrescription = 100,
                    Date = new DateTime(2024, 5, 1),
                    DueDate = new DateTime(2024, 5, 10),
                    IdDoctor = 10,
                    Doctor = new Doctor
                    {
                        IdDoctor = 10,
                        FirstName = "Dr. Alice",
                        LastName = "Smith",
                        Email = "alice@example.com"
                    },
                    PrescriptionMedicaments = new List<PrescriptionMedicament>
                    {
                        new PrescriptionMedicament
                        {
                            IdMedicament = 1,
                            Dose = 2,
                            Details = "Take after meal",
                            Medicament = new Medicament
                            {
                                IdMedicament = 1,
                                Name = "Ibuprofen",
                                Description = "Pain relief",
                                Type = "Tablet"
                            }
                        }
                    }
                }
            }
        };
    }
}