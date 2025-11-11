namespace GS_Csharp.Domain.Entities;

public class Inscricao
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public DateTime DataInscricao { get; set; } = DateTime.UtcNow;
}
