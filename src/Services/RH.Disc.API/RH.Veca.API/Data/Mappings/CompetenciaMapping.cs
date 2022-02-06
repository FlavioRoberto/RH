using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Veca.API.Models;

namespace RH.Veca.API.Data.Mappings
{
    public class CompetenciaMapping : IEntityTypeConfiguration<Competencia>
    {
        public void Configure(EntityTypeBuilder<Competencia> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(c => c.IntervaloMinimo)
                   .IsRequired();

            builder.Property(c => c.IntervaloMaximo)
                  .IsRequired();

            builder.Property(c => c.UsuarioId)
                   .IsRequired();
        }
    }
}
