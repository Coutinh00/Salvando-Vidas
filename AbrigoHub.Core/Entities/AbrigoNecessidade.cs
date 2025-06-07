using System;

namespace AbrigoHub.Core.Entities
{
    public class AbrigoNecessidade
    {
        public int Id { get; set; }
        public int AbrigoId { get; set; }
        public string TipoRecurso { get; set; }
        public int QuantidadeNecessaria { get; set; }
        public string NivelUrgencia { get; set; }
        public string Descricao { get; set; }
        public bool Atendida { get; set; }
        public DateTime CriadoEm { get; set; }

        // Relacionamentos
        public virtual Abrigo Abrigo { get; set; }
    }
} 