using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class PatientSeedConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);
            builder.HasData(
            new Patient
            {
                Id = 1,
                Name = "Jia Wei",
                LastName = "Tan",
                NRIC = "S1234567G",
                Gender ="Male",
                DOB = System.DateTime.Now,
                PhoneNumber = "12345678",
                Address = "singapore",
                AllergyDes ="seafood",
                Email="pat@pat.com",
                UserId= "8607cd47-e3bc-4a1b-96f9-e83d9e4ab0e3",
                Age = null,
                ReportId = 1

            }


        );
        }
    }
}
