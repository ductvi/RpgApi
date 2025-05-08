using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagemHabilidadesController : ControllerBase
    {

        //Aqui será a nossa codificação da controller.

        private readonly DataContext _phContext;

        public PersonagemHabilidadesController(DataContext context)
        {
            _phContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem? personagem = await _phContext.Personagens
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);

                if (personagem == null)
                {
                    throw new Exception("Personagem não encontrado para o ID informado");
                }

                Habilidade? habilidade = await _phContext.Habilidades
                    .FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

                if (habilidade == null)
                {
                    throw new Exception("Habilidade não encontrada!");
                }

                PersonagemHabilidade ph = new PersonagemHabilidade();
                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _phContext.PersonagemHabilidades.AddAsync(ph);
                int linhasAfetadas = await _phContext.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PersonagemId")]
        public async Task<IActionResult> GetPersonagemHabilidades(int personagemId)
        {
            try
            {
                List<PersonagemHabilidade>? phLista = new List<PersonagemHabilidade>();
                phLista = await _phContext.PersonagemHabilidades
                    .Include(p => p.Personagem)
                    .Include(p => p.Habilidade)
                    .Where(p => p.Personagem.Id == personagemId).ToListAsync();
                    return Ok(phLista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}

