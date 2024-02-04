using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthcareMonitoring.Shared.Domain;
using System.Composition;

namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class PrescriptionSeedConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);
            builder.HasData(
                new Prescription
                {
                    Id=1,
                    MedicineName="Test",
                    MedicineQuantity=1,
                    MedicineDescription="Test",
                    MedicineUsage="Test",
                    MedicinePrescriptionDoctor="Test",
                    PatientId=1,
                }
            );
        }
    }
}