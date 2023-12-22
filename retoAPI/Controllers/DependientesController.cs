using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retoAPI;
using retoAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class DependientesController : Controller
{
    private readonly AppDbContext _context;

    public DependientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerListaDependientes()
    {
        try
        {
            var dependientes = await _context.Dependientes.ToListAsync();
            return Ok(dependientes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertarDependiente([FromBody] Dependiente nuevoDependiente)
    {
        try
        {

            _context.Dependientes.Add(nuevoDependiente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerDependietePorCodigo), new { codigo = nuevoDependiente.CodigoDependiente }, nuevoDependiente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> ObtenerDependietePorCodigo(int codigo)
    {
        try
        {
            var dependiente = await _context.Dependientes.FirstOrDefaultAsync(e => e.CodigoDependiente == codigo);

            if (dependiente == null)
                return NotFound();

            return Ok(dependiente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}

