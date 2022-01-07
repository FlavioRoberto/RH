using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Clientes.API.Domain.Models;

namespace RH.Clientes.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeRazaoSocial)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Endereco)
                     .IsRequired()
                     .HasColumnName("Email");
            });

            builder.ToTable("Clientes");
        }
    }
}
