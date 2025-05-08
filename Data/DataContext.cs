using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<PersonagemHabilidade> PersonagemHabilidades { get; set; }
        
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
                new Arma() { Id = 1, Nome = "Adaga", Dano = 15, PersonagemId = 1 },
                new Arma() { Id = 2, Nome = "Arco", Dano = 18, PersonagemId = 2 },
                new Arma() { Id = 3, Nome = "Espada longa", Dano = 22, PersonagemId = 3 },
                new Arma() { Id = 4, Nome = "Espada curta", Dano = 20, PersonagemId = 4 },
                new Arma() { Id = 5, Nome = "Revolver", Dano = 50, PersonagemId = 5 },
                new Arma() { Id = 6, Nome = "Cajado", Dano = 20, PersonagemId = 6 },
                new Arma() { Id = 7, Nome = "Escudo", Dano = 10, PersonagemId = 7 }
            );

            modelBuilder.Entity<PersonagemHabilidade>().HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId});
            
            modelBuilder.Entity<Habilidade>().HasData(
                new Habilidade() {Id= 1, Nome="Adormecer", Dano=58,},
                new Habilidade() {Id=2, Nome="Congelar", Dano= 41},
                new Habilidade() {Id=3, Nome= "Hipnotizar", Dano= 37}
            );

            modelBuilder.Entity<PersonagemHabilidade>().HasData
            (
                new PersonagemHabilidade() {PersonagemId=1, HabilidadeId=1},
                new PersonagemHabilidade() {PersonagemId=1, HabilidadeId=2},
                new PersonagemHabilidade() {PersonagemId=2, HabilidadeId=2},
                new PersonagemHabilidade() {PersonagemId=3, HabilidadeId=2},
                new PersonagemHabilidade() {PersonagemId=3, HabilidadeId=3},
                new PersonagemHabilidade() {PersonagemId=4, HabilidadeId=3},
                new PersonagemHabilidade() {PersonagemId=5, HabilidadeId=1},
                new PersonagemHabilidade() {PersonagemId=6, HabilidadeId=2},
                new PersonagemHabilidade() {PersonagemId=7, HabilidadeId=3}
            );

        }
    }
}