using System.ComponentModel.DataAnnotations.Schema;
using RpgApi.Models;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public byte[]? Foto { get; set; }

        public double? Latitude { get; set; }

        public double? Longititude { get; set; }

        public DateTime? DataAcesso { get; set; } // using System;

        [NotMapped] // using System.ComponentModel.DataAnnotations.Schema

        public string PasswordString { get; set; }

        public List<Personagem>? Personagens { get; set; }//using System.Collections.generic;

        public string? Perfil { get; set; }

        public string? Email { get; set; }


    }
}