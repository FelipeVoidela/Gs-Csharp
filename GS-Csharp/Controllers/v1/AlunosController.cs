using GS_Csharp.Application.DTOs;
using GS_Csharp.Domain.Entities;
using GS_Csharp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace GS_Csharp.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/alunos")]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _db;
    public AlunosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAll(CancellationToken ct)
    {
        var alunos = await _db.Alunos.AsNoTracking()
            .Select(a => new AlunoDto(a.Id, a.Nome, a.Email, a.DataNascimento))
            .ToListAsync(ct);
        return Ok(alunos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlunoDto>> GetById(int id, CancellationToken ct)
    {
        var a = await _db.Alunos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (a is null) return NotFound();
        return Ok(new AlunoDto(a.Id, a.Nome, a.Email, a.DataNascimento));
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> Create([FromBody] CreateAlunoRequest req, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var entity = new Aluno { Nome = req.Nome, Email = req.Email, DataNascimento = req.DataNascimento };
        _db.Alunos.Add(entity);
        await _db.SaveChangesAsync(ct);
        var dto = new AlunoDto(entity.Id, entity.Nome, entity.Email, entity.DataNascimento);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id, version = "1.0" }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAlunoRequest req, CancellationToken ct)
    {
        var entity = await _db.Alunos.FindAsync([id], ct);
        if (entity is null) return NotFound();
        entity.Nome = req.Nome; entity.Email = req.Email; entity.DataNascimento = req.DataNascimento;
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Alunos.FindAsync([id], ct);
        if (entity is null) return NotFound();
        _db.Alunos.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
