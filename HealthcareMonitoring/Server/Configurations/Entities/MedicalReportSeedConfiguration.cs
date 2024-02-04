using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthcareMonitoring.Shared.Domain;
using System.Composition;

namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class MedicalReportSeedConfiguration : IEntityTypeConfiguration<MedicalReport>
    {
        public void Configure(EntityTypeBuilder<MedicalReport> builder)
        {

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);

            builder.HasData(
                new MedicalReport
                {
                    Id = 1,
                    heartRate = 1,
                    rhythm = "Test",
                    P_wave = "1",
                    PR_Interval = 1,
                    QRS_Complex = 1,
                    QT_Interval = 1,
                    ST_Interval = 1,
                    T_Wave = "1",
                    hb = 2,
                    hct = 2,
                    rbc = 2,
                    wbc = 2,
                    plt = 2,
                    lumarSpine = 3,
                    totalHip = 3,
                    tscoreL = 3,
                    tscoreH = 3,
                    fvc = 4,
                    fev1 = 4,
                    fev1_fvc_ratio = 4,
                    pef = 4,
                    tv = 4,
                    MedicalType = "Cardiologist,Pulmonologist,Orthopedist,General",
                    
                },
                new MedicalReport
                {
                    Id = 2,
                    heartRate = 10,
                    rhythm = "Test",
                    P_wave = "Test",
                    PR_Interval = 10,
                    QRS_Complex = 10,
                    QT_Interval = 10,
                    ST_Interval = 10,
                    T_Wave = "Test",
                    hb = 10,
                    hct = 10,
                    rbc = 10,
                    wbc = 10,
                    plt = 10,
                    lumarSpine = 10,
                    totalHip = 10,
                    tscoreL = 10,
                    tscoreH = 10,
                    fvc = 10,
                    fev1 = 10,
                    fev1_fvc_ratio = 10,
                    pef = 10,
                    tv = 10,
                    MedicalType = "Cardiologist,Pulmonologist",
                }
            );
        }
    }
}