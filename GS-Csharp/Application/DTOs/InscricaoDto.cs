namespace GS_Csharp.Application.DTOs;

public record InscricaoDto(int Id, int AlunoId, int CursoId, DateTime DataInscricao);
public record CreateInscricaoRequest(int AlunoId, int CursoId);
