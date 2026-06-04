using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoEventos.Models
{
    public class Evento
    {
        // Sempre tem que haver uma chave primária!!!
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O Título é obrigatório")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Display(Name = "Data do Evento")]
        public DateTime Data { get; set; }

        // Relacionamento com Categoria
        // Um evento tem uma categoria apenas
        [Required(ErrorMessage = "A categoria é obrigatória!")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Relacionamento com Local
        [Required(ErrorMessage = "O Local é obrigatório!")]
        public int LocalId { get; set; }
        public Local? Local { get; set; }
    }
}
