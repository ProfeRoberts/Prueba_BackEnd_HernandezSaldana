using LoginApi.Data;
using LoginApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class LoginsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
    {
        return await _context.ccloglogin.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> PostLogin(Login login)
    {
        if (!_context.ccUsers.Any(u => u.User_id == login.User_id))
            return BadRequest("User_id no existe.");

        // Validación de login sin logout anterior (básica)
        var existeLoginPendiente = _context.ccloglogin
            .Any(l => l.User_id == login.User_id && l.TipoMov == 1 &&
                      !_context.ccloglogin.Any(x => x.User_id == l.User_id && x.TipoMov == 0 && x.Fecha > l.Fecha));

        if (login.TipoMov == 1 && existeLoginPendiente)
            return BadRequest("Ya existe un login sin logout.");

        _context.ccloglogin.Add(login);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLogins), new { id = login.Id }, login);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLogin(int id, Login login)
    {
        if (id != login.Id) return BadRequest();

        _context.Entry(login).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLogin(int id)
    {
        var login = await _context.ccloglogin.FindAsync(id);
        if (login == null) return NotFound();

        _context.ccloglogin.Remove(login);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
