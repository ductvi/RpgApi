//Eduardo Custódio Vieira
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
namespace RpgApi.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API.
    [Route("[Controller]")] // Define a rota base para os endpoints deste controlador.

    public class UsuariosController : ControllerBase // Define a classe do controlador que herda de ControllerBase.
    {
        private readonly DataContext _userContext; // Declara uma variável para acessar o contexto do banco de dados.

        public UsuariosController(DataContext userContext) // Construtor que recebe o contexto do banco de dados como dependência.
        {
            _userContext = userContext; // Inicializa o contexto do banco de dados.
        }

        public async Task<bool> UsuarioExistente(string username) // Método para verificar se um usuário já existe no banco de dados.
        {
            if (await _userContext.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower())) // Verifica se existe algum usuário com o mesmo nome (case insensitive).
            {
                return true; // Retorna verdadeiro se o usuário existir.
            }
            return false; // Retorna falso se o usuário não existir.
        }

        [HttpPost("Registrar")] // Define o endpoint POST para registrar um novo usuário.

        public async Task<IActionResult> RegistrarUsuario(Usuario user) // Método para registrar um novo usuário.
        {
            try
            {
                if (await UsuarioExistente(user.Username)) // Verifica se o nome de usuário já existe.
                    throw new Exception("Nome de Usuário já existente!"); // Lança uma exceção se o nome de usuário já existir.

                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt); // Gera o hash e o salt da senha.
                user.PasswordHash = hash; // Define o hash da senha no objeto do usuário.
                user.PasswordSalt = salt; // Define o salt da senha no objeto do usuário.

                await _userContext.AddAsync(user); // Adiciona o usuário ao banco de dados.
                await _userContext.SaveChangesAsync(); // Salva as alterações no banco de dados.
                return Ok(user.Id); // Retorna o ID do usuário registrado.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna uma mensagem de erro em caso de exceção.
            }
        }

        [HttpPost("Autenticar")] // Define o endpoint POST para autenticar um usuário.

        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais) // Método para autenticar um usuário.
        {
            try
            {
                Usuario? usuario = await _userContext.Usuarios.FirstOrDefaultAsync(u => u.Username.ToLower() == credenciais.Username.ToLower()); // Busca o usuário pelo nome de usuário.

                if (usuario == null) // Verifica se o usuário não foi encontrado.
                {
                    throw new Exception("Usuário não Encontrado."); // Lança uma exceção se o usuário não existir.
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt)) // Verifica se a senha está incorreta.
                {
                    throw new Exception("Senha incorreta, tente novamente"); // Lança uma exceção se a senha estiver incorreta.
                }
                else
                {
                    usuario.DataAcesso = DateTime.UtcNow; // Atualiza a data de acesso do usuário.
                    _userContext.Usuarios.Update(usuario); // Atualiza o usuário no banco de dados.
                    await _userContext.SaveChangesAsync(); // Salva as alterações no banco de dados.
                    return Ok(usuario.Id); // Retorna o ID do usuário autenticado.
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna uma mensagem de erro em caso de exceção.
            }
        }

        [HttpPut("AlterarSenha")] // Define o endpoint PUT para alterar a senha de um usuário.

        public async Task<IActionResult> Update(Usuario modSenha) // Método para alterar a senha de um usuário.
        {
            try
            {
                Usuario? usuario = await _userContext.Usuarios.FirstOrDefaultAsync(u => u.Username.ToLower() == modSenha.Username.ToLower()); // Busca o usuário pelo nome de usuário.

                if (usuario == null) // Verifica se o usuário não foi encontrado.
                {
                    throw new Exception("Usuário não Encontrado."); // Lança uma exceção se o usuário não existir.
                }
                else
                {
                    Criptografia.CriarPasswordHash(modSenha.PasswordString, out byte[] hash, out byte[] salt); // Gera o hash e o salt da nova senha.
                    usuario.PasswordHash = hash; // Atualiza o hash da senha no objeto do usuário.
                    usuario.PasswordSalt = salt; // Atualiza o salt da senha no objeto do usuário.

                    _userContext.Usuarios.Update(usuario); // Atualiza o usuário no banco de dados.
                    await _userContext.SaveChangesAsync(); // Salva as alterações no banco de dados.

                    return Ok("Senha atualizada com sucesso!"); // Retorna uma mensagem de sucesso.
                }
            }
            catch
            {
                return BadRequest("Erro ao atualizar a senha."); // Retorna uma mensagem de erro em caso de exceção.
            }
        }

        [HttpGet("GetAll")] // Define o endpoint GET para obter todos os usuários.

        public async Task<IActionResult> GetAllUsuarios() // Método para obter todos os usuários.
        {
            try
            {
                var usuarios = await _userContext.Usuarios.ToListAsync(); // Obtém todos os usuários do banco de dados.
                return Ok(usuarios); // Retorna a lista de usuários.
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar os usuários: " + ex.Message); // Retorna uma mensagem de erro em caso de exceção.
            }
        }
    }
}