using System;

namespace AbrigoHub.Core.Entities
{
    public class AbrigoRecurso
    {
        public int Id { get; set; }
        public int AbrigoId { get; set; }
        public string TipoRecurso { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Relacionamentos
        public virtual Abrigo Abrigo { get; set; }
    }
} 