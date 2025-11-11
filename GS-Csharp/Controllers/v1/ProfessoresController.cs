using GS_Csharp.Application.DTOs;
using GS_Csharp.Domain.Entities;
using GS_Csharp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace GS_Csharp.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/professores")]
public class ProfessoresController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProfessoresController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetAll(CancellationToken ct)
    {
        var items = await _db.Professores.AsNoTracking()
            .Select(x => new ProfessorDto(x.Id, x.Nome, x.Email))
            .ToListAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProfessorDto>> GetById(int id, CancellationToken ct)
    {
        var e = await _db.Professores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return NotFound();
        return Ok(new ProfessorDto(e.Id, e.Nome, e.Email));
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorDto>> Create([FromBody] CreateProfessorRequest req, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var entity = new Professor { Nome = req.Nome, Email = req.Email };
        _db.Professores.Add(entity);
        await _db.SaveChangesAsync(ct);
        var dto = new ProfessorDto(entity.Id, entity.Nome, entity.Email);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id, version = "1.0" }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProfessorRequest req, CancellationToken ct)
    {
        var entity = await _db.Professores.FindAsync([id], ct);
        if (entity is null) return NotFound();
        entity.Nome = req.Nome; entity.Email = req.Email;
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Professores.FindAsync([id], ct);
        if (entity is null) return NotFound();
        _db.Professores.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
