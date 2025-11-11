using GS_Csharp.Application.DTOs;
using GS_Csharp.Domain.Entities;
using GS_Csharp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace GS_Csharp.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cursos")]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _db;
    public CursosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetAll(CancellationToken ct)
    {
        var items = await _db.Cursos.AsNoTracking()
            .Select(x => new CursoDto(x.Id, x.Titulo, x.Descricao, x.ComunidadeId))
            .ToListAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CursoDto>> GetById(int id, CancellationToken ct)
    {
        var e = await _db.Cursos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return NotFound();
        return Ok(new CursoDto(e.Id, e.Titulo, e.Descricao, e.ComunidadeId));
    }

    [HttpPost]
    public async Task<ActionResult<CursoDto>> Create([FromBody] CreateCursoRequest req, CancellationToken ct)
    {
        var entity = new Curso { Titulo = req.Titulo, Descricao = req.Descricao, ComunidadeId = req.ComunidadeId };
        _db.Cursos.Add(entity);
        await _db.SaveChangesAsync(ct);
        var dto = new CursoDto(entity.Id, entity.Titulo, entity.Descricao, entity.ComunidadeId);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id, version = "1.0" }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCursoRequest req, CancellationToken ct)
    {
        var entity = await _db.Cursos.FindAsync([id], ct);
        if (entity is null) return NotFound();
        entity.Titulo = req.Titulo; entity.Descricao = req.Descricao; entity.ComunidadeId = req.ComunidadeId;
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Cursos.FindAsync([id], ct);
        if (entity is null) return NotFound();
        _db.Cursos.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
