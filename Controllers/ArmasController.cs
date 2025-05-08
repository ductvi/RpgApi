using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ArmasController: ControllerBase
    {
        private readonly DataContext _dataContext;

        public ArmasController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Arma? a = await _dataContext.Armas.FirstOrDefaultAsync(aBusca => aBusca.Id == id);
                return Ok(a);
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
                List<Arma> lista = await _dataContext.Armas.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {
                if(novaArma.Dano <= 0)
                {
                    throw new Exception("Uma Arma não pode ter Dano Zerado.");
                }

                Personagem? p = await _dataContext.Personagens.FirstOrDefaultAsync(p => p.Id == novaArma.PersonagemId);
                if(p == null){
                    throw new Exception("Não existe o Personagem indicado com Id=" + novaArma.PersonagemId);
                }   

                await _dataContext.Armas.AddAsync(novaArma);
                await _dataContext.SaveChangesAsync();
                return Ok(novaArma);
            }
            catch (Exception ex)
            {                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma modArma)
        {
            try
            {
                if(modArma.Dano <= 0 || modArma.Dano > 50)
                {
                    throw new Exception("O valor do Dano deverá ser > 0 (zero) <= 50!!"); 
                }
                _dataContext.Armas.Update(modArma);
                int armasAfetadas = await _dataContext.SaveChangesAsync();
                return Ok(armasAfetadas);  
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
                Arma? aRemover = await _dataContext.Armas.FirstOrDefaultAsync(ar => ar.Id == id);

                _dataContext.Armas.Remove(aRemover);
                int armasRemovidas = await _dataContext.SaveChangesAsync();
                return Ok(armasRemovidas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
    }
    
};