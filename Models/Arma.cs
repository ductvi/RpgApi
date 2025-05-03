namespace RpgApi.Models
{
    public class Arma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public string Tipo { get; set; }  // Espada, Machado, etc.
        public int PersonagemId { get; set; }   // Relacionamento com Personagem (opcional)
    }
}
