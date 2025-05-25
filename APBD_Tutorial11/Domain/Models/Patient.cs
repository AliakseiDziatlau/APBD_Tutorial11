namespace APBD_Tutorial11.Domain.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public IEnumerable<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}