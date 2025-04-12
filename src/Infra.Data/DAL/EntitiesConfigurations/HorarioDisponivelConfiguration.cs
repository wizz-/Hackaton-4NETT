using Domain.Entities.Cadastros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.DAL.EntitiesConfigurations
{
    public class HorarioDisponivelConfiguration : IEntityTypeConfiguration<HorarioDisponivel>
    {
        public void Configure(EntityTypeBuilder<HorarioDisponivel> builder)
        {
            builder.OwnsOne(x => x.Periodo, (ownedBuilder) =>
            {
            });
        }
    }
}
