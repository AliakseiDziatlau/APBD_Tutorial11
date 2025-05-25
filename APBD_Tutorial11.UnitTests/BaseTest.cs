using APBD_Tutorial11.Domain.Interfaces;
using Moq;

namespace APBD_Tutorial11.UnitTests;

public class BaseTest
{
    protected readonly Mock<IPatientRepository> _patientRepoMock;

    protected BaseTest()
    {
        _patientRepoMock = new Mock<IPatientRepository>();
    }
}