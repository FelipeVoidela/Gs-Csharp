namespace GS_Csharp.Domain.Entities;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly? DataNascimento { get; set; }

    // Navigations
    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
}
