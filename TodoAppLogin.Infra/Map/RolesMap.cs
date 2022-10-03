using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAppLogin.Domain.Entities;

namespace TodoAppLogin.Infra.Map;

public class RolesMap : IEntityTypeConfiguration<Roles>
{
  public void Configure(EntityTypeBuilder<Roles> builder)
  {
    builder.ToTable("Roles");

    builder.HasKey(p => p.Id);
    
    builder.Property(p => p.Id).HasColumnName("Id");
    builder.Property(p => p.Name).HasColumnName("Name");

  }
}