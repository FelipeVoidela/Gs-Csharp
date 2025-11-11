namespace GS_Csharp.Application.DTOs;

public record CursoDto(int Id, string Titulo, string? Descricao, int ComunidadeId);
public record CreateCursoRequest(string Titulo, string? Descricao, int ComunidadeId);
public record UpdateCursoRequest(string Titulo, string? Descricao, int ComunidadeId);
