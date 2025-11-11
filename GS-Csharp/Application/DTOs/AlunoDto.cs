namespace GS_Csharp.Application.DTOs;

public record AlunoDto(int Id, string Nome, string Email, DateOnly? DataNascimento);
public record CreateAlunoRequest(string Nome, string Email, DateOnly? DataNascimento);
public record UpdateAlunoRequest(string Nome, string Email, DateOnly? DataNascimento);
