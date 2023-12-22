using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retoAPI;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PuestosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PuestosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerListaPuestos()
    {
        try
        {
            var puestos = await _context.Puestos.ToListAsync();
            return Ok(puestos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
