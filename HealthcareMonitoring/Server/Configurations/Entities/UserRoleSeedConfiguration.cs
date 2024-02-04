using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HealthcareMonitoring.Server.Configurations.Entities
{
    public class UserRoleSeedConfiguration :
    IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "ad2bcf0c-20db-474f-8407-5a6b159518ba",
                UserId = "3781efa7-66dc-47f0-860f-e506d04102e4"
                
            },
            new IdentityUserRole<string>
            {
                RoleId = "bd2bcf0c-20db-474f-8407-5a6b159518bb",
                UserId = "693e710c-008f-435b-a997-77f10812374d"
            },
            new IdentityUserRole<string>
            {
                RoleId = "5f31c948 - 0df8 - 4ed4 - ba1b - 23efcf131af9",
                UserId = "8607cd47-e3bc-4a1b-96f9-e83d9e4ab0e3"
            }

            );
        }
    }
}
