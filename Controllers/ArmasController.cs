using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context; // Declaração do Atributo.

        public ArmasController(DataContext context)
        {
            _context = context; // Inicializando o atributo a partir de um parâmetro.
        }

        // Método GET por id da arma
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Arma arma = await _context.Armas.FirstOrDefaultAsync(a => a.Id == id);
                if (arma == null)
                {
                    return NotFound("Arma não encontrada.");
                }
                return Ok(arma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método GET para listar todas as armas
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Arma> listaArmas = await _context.Armas.ToListAsync();
                return Ok(listaArmas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método POST para adicionar uma nova arma
        [HttpPost]
        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {
                if (novaArma.Dano <= 0)
                {
                    throw new Exception("O dano da arma deve ser maior que 0.");
                }

                await _context.Armas.AddAsync(novaArma);
                await _context.SaveChangesAsync();

                return Ok(novaArma.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método PUT para atualizar uma arma
        [HttpPut]
        public async Task<IActionResult> Update(Arma armaModificada)
        {
            try
            {
                if (armaModificada.Dano <= 0)
                {
                    throw new Exception("O dano da arma deve ser maior que 0.");
                }

                _context.Armas.Update(armaModificada);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método DELETE para excluir uma arma
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Arma armaParaRemover = await _context.Armas.FirstOrDefaultAsync(a => a.Id == id);
                if (armaParaRemover == null)
                {
                    return NotFound("Arma não encontrada.");
                }

                _context.Armas.Remove(armaParaRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
