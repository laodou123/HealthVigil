using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);
            builder.HasData(
            new IdentityRole
            {
                Id = "ad2bcf0c-20db-474f-8407-5a6b159518ba",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "bd2bcf0c-20db-474f-8407-5a6b159518bb",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = "cd2bcf0c-20db-474f-8407-5a6b159518bc",
                Name = "Doctor",
                NormalizedName = "DOCTOR"
            },
            new IdentityRole
            {
                Id = "5f31c948 - 0df8 - 4ed4 - ba1b - 23efcf131af9",
                Name = "Patient",
                NormalizedName = "PATIENT"
            }
            );
        }
    }
}