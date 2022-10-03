using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAppLogin.Domain.Entities;

namespace TodoAppLogin.Infra.Map;

public class UserMap : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("User");

    builder.HasKey(p => p.Id);
    
    builder.Property(p => p.Id).HasColumnName("Id");
    builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
    builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(255).IsRequired();
    builder.Property(p => p.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
    
    builder.Navigation(t => t.Roles).AutoInclude();
    
    builder.HasMany<Roles>(t => t.Roles)
      .WithMany(t => t.Users)
      .UsingEntity<Dictionary<string, object>>(
        "UserRole",
        j => j
            .HasOne<Roles>()
            .WithMany()
            .HasForeignKey("RoleId")
            .HasConstraintName("FK_UserRoles_Users_RoleId")
            .OnDelete(DeleteBehavior.Cascade),
        j => j
            .HasOne<User>()
            .WithMany()
            .HasForeignKey("UserId")
            .HasConstraintName("FK_UserRoles_Roles_UserId")
            .OnDelete(DeleteBehavior.ClientCascade));

  }
}