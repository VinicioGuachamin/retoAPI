using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retoAPI.Models;
using retoAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpleadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ObtenerListaEmpleados")]
    public async Task<IActionResult> ObtenerListaEmpleados()
    {
        try
        {
            var empleados = await _context.Empleados.ToListAsync();
            return Ok(empleados);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarEmpleadosByCodNombreApellido(int? codigo, string? nombre, string? apellido)
    {
        try
        {
            var empleados = await _context.Empleados
                .Where(e => e.Nombre.Contains(nombre) && e.Apellido.Contains(apellido) && e.CodigoEmpleado == codigo)
                .ToListAsync();

            return Ok(empleados);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> ObtenerEmpleadoPorCodigo(int codigo)
    {
        try
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.CodigoEmpleado == codigo);

            if (empleado == null)
                return NotFound();

            return Ok(empleado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertarEmpleado([FromBody] Empleado nuevoEmpleado)
    {
        try
        {

            _context.Empleados.Add(nuevoEmpleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerEmpleadoPorCodigo), new { codigo = nuevoEmpleado.CodigoEmpleado }, nuevoEmpleado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> ActualizarEmpleado(int codigo, [FromBody] Empleado empleadoActualizado)
    {
        try
        {
            if (codigo != empleadoActualizado.CodigoEmpleado)
                return BadRequest();

            _context.Entry(empleadoActualizado).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> EliminarEmpleado(int codigo)
    {
        try
        {
            var empleado = await _context.Empleados.FindAsync(codigo);
            if (empleado == null)
                return NotFound();

            // Eliminar dependientes asociados al empleado
            var dependientes = await _context.Dependientes.Where(d => d.EmpleadoId == empleado.CodigoEmpleado).ToListAsync();
            _context.Dependientes.RemoveRange(dependientes);

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{codigo}/dependientes")]
    public async Task<IActionResult> ObtenerDependientesPorEmpleado(int codigo)
    {
        try
        {
            var empleado = await _context.Empleados.FindAsync(codigo);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            var dependientes = await _context.Dependientes
                .Where(d => d.EmpleadoId == empleado.CodigoEmpleado)
                .ToListAsync();

            return Ok(dependientes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
