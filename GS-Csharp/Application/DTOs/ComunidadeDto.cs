namespace GS_Csharp.Application.DTOs;

public record ComunidadeDto(int Id, string Titulo, string? Descricao, int ProfessorId);
public record CreateComunidadeRequest(string Titulo, string? Descricao, int ProfessorId);
public record UpdateComunidadeRequest(string Titulo, string? Descricao, int ProfessorId);
