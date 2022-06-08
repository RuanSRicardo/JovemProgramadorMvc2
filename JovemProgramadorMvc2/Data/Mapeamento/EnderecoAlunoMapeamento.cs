using JovemProgramadorMvc2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EnderecoAlunoMapeamento : IEntityTypeConfiguration<EnderecoModel>
{
    public void Configure(EntityTypeBuilder<EnderecoModel> builder)
    {
        builder.ToTable("EnderecoAluno");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.IdAluno).HasColumnType("int");
        builder.Property(t => t.logradouro).HasColumnType("varchar(150)");
        builder.Property(t => t.complemento).HasColumnType("varchar(150)");
        builder.Property(t => t.bairro).HasColumnType("varchar(50)");
        builder.Property(t => t.localidade).HasColumnType("varchar(50)");
        builder.Property(t => t.uf).HasColumnType("varchar(2)");
        builder.Property(t => t.ddd).HasColumnType("varchar(3)");

    }
}

