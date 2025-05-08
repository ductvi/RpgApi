using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class PersonagensController : ControllerBase
    {

        //Aqui teremos a programação da nossa controller com os métodos HTTP;

        private readonly DataContext _context; //Declaração do Atributo.

        public PersonagensController(DataContext context)
        {
            //Inicializando o atributo a partir de um parâmetro.
            _context = context;
        }
        //Início dos métodos:
        //Método GET por id do personagem. Este método foi modificado para exibir os personagens que pertencem ao usuário.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem? p = await _context.Personagens
                    .Include(ar => ar.Arma)//Inclui na propriedade a Arma do Objeto p
                    .Include(us => us.Usuario)//Inclui na propriedade o Usuário do Objeto p
                    .Include(ph => ph.PersonagemHabilidades)
                        .ThenInclude(h => h.Habilidade)//Inclui na lista de PersonagemHabilidades de ph*/
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.Personagens.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida >= 100)
                {
                    throw new Exception("Pontos de vida não podem ser maior que 100");
                }
                await _context.Personagens.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();

                return Ok(novoPersonagem.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem modPersonagem)
        {
            try
            {
                if (modPersonagem.PontosVida >= 100)
                {
                    throw new Exception("Pontos de vida não podem ser maior que 100");
                }
                _context.Personagens.Update(modPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Personagem? pRemover = await _context.Personagens.FirstOrDefaultAsync(p => p.Id == id);

                _context.Personagens.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } //Fim da Classe Controller.
};