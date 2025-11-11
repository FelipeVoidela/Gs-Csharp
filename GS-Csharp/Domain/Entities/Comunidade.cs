namespace GS_Csharp.Domain.Entities;

public class Comunidade
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }

    public int ProfessorId { get; set; }
    public Professor? Professor { get; set; }

    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
