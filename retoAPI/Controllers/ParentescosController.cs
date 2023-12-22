using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retoAPI;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ParentescosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ParentescosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerListaParentescos()
    {
        try
        {
            var parentescos = await _context.Parentescos.ToListAsync();
            return Ok(parentescos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}