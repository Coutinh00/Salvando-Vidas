using System;

namespace AbrigoHub.Core.Entities
{
    public class Doacao
    {
        public int Id { get; set; }
        public int AbrigoId { get; set; }
        public string NomeDoador { get; set; }
        public string TipoRecurso { get; set; }
        public int Quantidade { get; set; }
        public DateTime RecebidoEm { get; set; }

        // Relacionamentos
        public virtual Abrigo Abrigo { get; set; }
    }
} 