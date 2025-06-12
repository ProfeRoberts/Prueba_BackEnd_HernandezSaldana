using LoginApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("csv")]
    public async Task<IActionResult> GetWorkedHoursCsv()
    {
        // 1. Emparejamos login y logout, calculamos duración en segundos
        var pares = await (from login in _context.ccloglogin
                           where login.TipoMov == 1
                           let logout = _context.ccloglogin
                               .Where(x => x.User_id == login.User_id && x.TipoMov == 0 && x.Fecha > login.Fecha)
                               .OrderBy(x => x.Fecha)
                               .FirstOrDefault()
                           where logout != null
                           select new
                           {
                               login.User_id,
                               Duracion = EF.Functions.DateDiffSecond(login.Fecha, logout.Fecha)
                           }).ToListAsync();

        // 2. Agrupamos por usuario y sumamos duración total en segundos
        var horasPorUsuario = pares
            .GroupBy(p => p.User_id)
            .ToDictionary(
                g => g.Key,
                g => Math.Round(g.Sum(p => p.Duracion) / 3600.0, 2) // convertimos a horas redondeadas
            );

        // 3. Traemos los usuarios y sus áreas desde la base de datos
        var usuarios = await (from user in _context.ccUsers
                              join area in _context.ccRIACat_Areas on user.IDArea equals area.IDArea
                              select new
                              {
                                  user.User_id,
                                  user.Login,
                                  NombreCompleto = $"{user.Nombres} {user.ApellidoPaterno} {user.ApellidoMaterno}",
                                  Area = area.AreaName
                              }).ToListAsync();

        // 4. Unimos con el diccionario de horas
        var resultado = usuarios.Select(u => new
        {
            u.Login,
            u.NombreCompleto,
            u.Area,
            TotalHoras = horasPorUsuario.ContainsKey(u.User_id) ? horasPorUsuario[u.User_id] : 0
        });

        // 5. Generamos el CSV
        var builder = new StringBuilder();
        builder.AppendLine("Login,NombreCompleto,Area,TotalHoras");

        foreach (var r in resultado)
        {
            builder.AppendLine($"{r.Login},{r.NombreCompleto},{r.Area},{r.TotalHoras}");
        }

        var bytes = Encoding.UTF8.GetBytes(builder.ToString());
        return File(bytes, "text/csv", "ReporteHoras.csv");
    }

}
