namespace APBD_Tutorial11.Domain.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public IEnumerable<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}