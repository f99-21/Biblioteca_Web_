using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Web.Modelo
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime AnoPublicacion { get; set; }

        public int? Autor_id { get; set; }
        public int? Categoria_id { get; set; }
        public string Resumen { get; set; }
    }
}
