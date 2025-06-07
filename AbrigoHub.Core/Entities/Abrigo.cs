using System;
using System.Collections.Generic;

namespace AbrigoHub.Core.Entities
{
    public class Abrigo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int Capacidade { get; set; }
        public int OcupacaoAtual { get; set; }
        public string Status { get; set; }
        public DateTime CriadoEm { get; set; }

        // Relacionamentos
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<AbrigoNecessidade> Necessidades { get; set; }
        public virtual ICollection<AbrigoRecurso> Recursos { get; set; }
        public virtual ICollection<Doacao> Doacoes { get; set; }
        public virtual ICollection<AbrigoAvaliacao> Avaliacoes { get; set; }
    }
} 