namespace GS_Csharp.Application.DTOs;

public record ProfessorDto(int Id, string Nome, string Email);
public record CreateProfessorRequest(string Nome, string Email);
public record UpdateProfessorRequest(string Nome, string Email);
