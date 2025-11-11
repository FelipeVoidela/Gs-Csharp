namespace GS_Csharp.Domain.Entities;

public class Curso
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }

    public int ComunidadeId { get; set; }
    public Comunidade? Comunidade { get; set; }

    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
}
