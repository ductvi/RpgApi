using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enums;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Personagem> Personagens { get; set; }

        public DbSet<Arma> Armas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().HasData
            (
                //Aqui é criada nossa tabela Personagem no BD, do mesmo modo que anteriormente construímos em memória.
                new Personagem() { Id = 1, Nome = "Frodo", PontosVida = 100, Forca = 17, Defesa = 23, Inteligencia = 33, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 2, Nome = "Sam", PontosVida = 100, Forca = 15, Defesa = 25, Inteligencia = 30, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida = 100, Forca = 18, Defesa = 21, Inteligencia = 35, Classe = ClasseEnum.Clerigo },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 18, Defesa = 18, Inteligencia = 32, Classe = ClasseEnum.Mago },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20, Defesa = 17, Inteligencia = 31, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 6, Nome = "Celdron", PontosVida = 100, Forca = 21, Defesa = 13, Inteligencia = 34, Classe = ClasseEnum.Clerigo },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida = 100, Forca = 25, Defesa = 11, Inteligencia = 35, Classe = ClasseEnum.Mago }
            );

            //Área para futuros INSERTs no banco de dados.
            
            modelBuilder.Entity<Arma>().HasData
            (
            new Arma() { Id = 1, Nome = "Eduardo Vieira", Dano = 24227, Tipo = "Espada", PersonagemId = 1}, 
            new Arma() { Id = 2, Nome = "Samurai's Blade", Dano = 18500, Tipo = "Lança", PersonagemId = 2 },
            new Arma() { Id = 3, Nome = "Light of Galadriel", Dano = 12000, Tipo = "Machado", PersonagemId = 3 },
            new Arma() { Id = 4, Nome = "Gandalf's Staff", Dano = 9000, Tipo = "Cajado", PersonagemId = 4 },
            new Arma() { Id = 5, Nome = "Hobbit's Slingshot", Dano = 7000, Tipo = "Estilingue", PersonagemId = 5 },
            new Arma() { Id = 6, Nome = "Celdron's Mace", Dano = 14500, Tipo = "Mace", PersonagemId = 6 },
            new Arma() { Id = 7, Nome = "Radagast's Hammer", Dano = 11000, Tipo = "Martelo", PersonagemId = 7 }
            );

            
        }
    }
}