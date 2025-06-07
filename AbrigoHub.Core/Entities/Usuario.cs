using System;
using System.Collections.Generic;

namespace AbrigoHub.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string TipoUsuario { get; set; }
        public string Telefone { get; set; }
        public DateTime CriadoEm { get; set; }

        // Relacionamentos
        public virtual ICollection<Abrigo> Abrigos { get; set; }
        public virtual ICollection<AbrigoAvaliacao> Avaliacoes { get; set; }
    }
} 