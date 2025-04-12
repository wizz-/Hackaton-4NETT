using Domain.Entities.Cadastros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.DAL.EntitiesConfigurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.OwnsOne(x => x.Crm, (ownedBuilder) =>
            {
                ownedBuilder.Property(x => x.Numero)
                    .HasMaxLength(6);
            });
        }
    }
}
