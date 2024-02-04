using HealthcareMonitoring.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
            new ApplicationUser
            {
                Id = "3781efa7-66dc-47f0-860f-e506d04102e4",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "Admin",
                LastName = "User",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new ApplicationUser
            {
                Id = "693e710c-008f-435b-a997-77f10812374d",
                Email = "13858860788a@gmail.com",
                NormalizedEmail = "13858860788A@GMAIL.COM",
                FirstName = "Hu",
                LastName = "Yi",
                UserName = "13858860788a@gmail.com",
                NormalizedUserName = "13858860788A@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")

            },
            new ApplicationUser
            {
                Id = "8607cd47-e3bc-4a1b-96f9-e83d9e4ab0e3",
                Email = "pat@pat.com",
                NormalizedEmail = "PAT@PAT.COM",
                FirstName = "jiawei",
                LastName = "tan",
                UserName = "pat@pat.com",
                NormalizedUserName = "PAT@PAT.COM",
                PasswordHash = hasher.HashPassword(null, "Qwer1234!")

            }

            );
        }
    }
}