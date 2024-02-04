namespace HealthcareMonitoring.Shared.Domain;

public class Patient : BaseDomainModel
{
    public string? Name { get; set; }

    public int? ReportId { get; set; }

    public int? PrescriptionId { get; set; }

    public string? NRIC { get; set; }

    public string? Gender { get; set; }
    public DateTime? DOB { get; set; }

    public int? Age { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? AllergyDes { get; set; }
    public string? LastName { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
}
