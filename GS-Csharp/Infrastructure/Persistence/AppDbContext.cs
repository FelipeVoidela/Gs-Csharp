using GS_Csharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GS_Csharp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Comunidade> Comunidades => Set<Comunidade>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Inscricao> Inscricoes => Set<Inscricao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Schema for Oracle: use uppercase table/column names by convention.
        modelBuilder.Entity<Aluno>(e =>
        {
            e.ToTable("ALUNOS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Professor>(e =>
        {
            e.ToTable("PROFESSORES");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Comunidade>(e =>
        {
            e.ToTable("COMUNIDADES");
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.Professor)
             .WithMany(p => p.Comunidades)
             .HasForeignKey(x => x.ProfessorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Curso>(e =>
        {
            e.ToTable("CURSOS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.Comunidade)
             .WithMany(c => c.Cursos)
             .HasForeignKey(x => x.ComunidadeId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Inscricao>(e =>
        {
            e.ToTable("INSCRICOES");
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Aluno)
             .WithMany(a => a.Inscricoes)
             .HasForeignKey(x => x.AlunoId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Curso)
             .WithMany(c => c.Inscricoes)
             .HasForeignKey(x => x.CursoId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
