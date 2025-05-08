using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;
namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _userContext;

        public UsuariosController(DataContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<bool> UsuarioExistente(string username)
        {
            if (await _userContext.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario user)
        {
            try
            {
                if (await UsuarioExistente(user.Username))
                    throw new Exception("Nome de usuário já existente!");

                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _userContext.AddAsync(user);
                await _userContext.SaveChangesAsync();
                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _userContext.Usuarios.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(credenciais.Username.ToLower()));

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new Exception("Senha incorreta, tente novamente");
                }
                else
                {
                    usuario.DataAcesso = DateTime.Now;
                    _userContext.Usuarios.Update(usuario);
                    await _userContext.SaveChangesAsync();
                    return Ok(usuario.Id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenhaUsuario(Usuario credenciais)
        {
            try
            {
                //Aqui vamos programar o método para alterar a senha do usuário
                Usuario? usuario = await _userContext.Usuarios.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(credenciais.Username.ToLower()));
                if(usuario == null){
                    throw new Exception("Usuário não encontrado.");
                }

                Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;

                _userContext.Usuarios.Update(usuario);
                int linhasAfetadas = await _userContext.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                //Aqui vamos programar o método para buscar todos os usuários do sistema.
                List<Usuario> usuarios = await _userContext.Usuarios.ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}




