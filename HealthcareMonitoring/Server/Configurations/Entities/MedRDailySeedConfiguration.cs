using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthcareMonitoring.Shared.Domain;
using System.Composition;

namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class MedRDailySeedConfiguration : IEntityTypeConfiguration<MedRDaily>
    {
        public void Configure(EntityTypeBuilder<MedRDaily> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);
            builder.HasData(
                new MedRDaily
                {
                    Id = 1,
                    bpm = 66,
                    systolicPressure = 1,
                    diastolicPressure = 1,
                    bloodSugarLevel=1,
                    PatientId=1,
                    DateCreated= new System.DateTime(2024, 2, 1, 12, 00, 34),

                },
                 new MedRDaily
                 {

                     Id = 2,
                     bpm = 62,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated= new System.DateTime(2024, 2, 1 ,8 ,00 ,34),


                 },
                 new MedRDaily
                 {

                     Id = 3,
                     bpm = 73,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 31, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 4,
                     bpm = 80,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 30, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 5,
                     bpm = 82,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 29, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 6,
                     bpm = 72,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 28, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 7,
                     bpm = 70,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 27, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 8,
                     bpm = 78,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 26, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 9,
                     bpm = 79,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 24, 12, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 10,
                     bpm = 74,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 23, 12, 00, 34),



                 },
                 new MedRDaily
                 {

                     Id = 11,
                     bpm = 71,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 22, 12, 00, 34),


                 }, new MedRDaily
                 {

                     Id = 12,
                     bpm = 60,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 22, 11, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 13,
                     bpm = 69,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 21, 16, 00, 34),


                 },
                 new MedRDaily
                 {

                     Id = 14,
                     bpm = 70,
                     systolicPressure = 10,
                     diastolicPressure = 12,
                     bloodSugarLevel = 15,
                     PatientId = 1,
                     DateCreated = new System.DateTime(2024, 1, 20, 16, 00, 34),


                 }
            );
        }
    }
}