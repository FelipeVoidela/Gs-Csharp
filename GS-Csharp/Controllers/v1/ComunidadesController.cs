using GS_Csharp.Application.DTOs;
using GS_Csharp.Domain.Entities;
using GS_Csharp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace GS_Csharp.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/comunidades")]
public class ComunidadesController : ControllerBase
{
    private readonly AppDbContext _db;
    public ComunidadesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunidadeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _db.Comunidades.AsNoTracking()
            .Select(x => new ComunidadeDto(x.Id, x.Titulo, x.Descricao, x.ProfessorId))
            .ToListAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComunidadeDto>> GetById(int id, CancellationToken ct)
    {
        var e = await _db.Comunidades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return NotFound();
        return Ok(new ComunidadeDto(e.Id, e.Titulo, e.Descricao, e.ProfessorId));
    }

    [HttpPost]
    public async Task<ActionResult<ComunidadeDto>> Create([FromBody] CreateComunidadeRequest req, CancellationToken ct)
    {
        var entity = new Comunidade { Titulo = req.Titulo, Descricao = req.Descricao, ProfessorId = req.ProfessorId };
        _db.Comunidades.Add(entity);
        await _db.SaveChangesAsync(ct);
        var dto = new ComunidadeDto(entity.Id, entity.Titulo, entity.Descricao, entity.ProfessorId);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id, version = "1.0" }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateComunidadeRequest req, CancellationToken ct)
    {
        var entity = await _db.Comunidades.FindAsync([id], ct);
        if (entity is null) return NotFound();
        entity.Titulo = req.Titulo; entity.Descricao = req.Descricao; entity.ProfessorId = req.ProfessorId;
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Comunidades.FindAsync([id], ct);
        if (entity is null) return NotFound();
        _db.Comunidades.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
