using System;

namespace AbrigoHub.Core.Entities
{
    public class AbrigoAvaliacao
    {
        public int Id { get; set; }
        public int AbrigoId { get; set; }
        public int UsuarioId { get; set; }
        public int Avaliacao { get; set; }
        public string Comentario { get; set; }
        public DateTime CriadoEm { get; set; }

        // Relacionamentos
        public virtual Abrigo Abrigo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
} 