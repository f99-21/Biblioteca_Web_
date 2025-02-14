using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Web.Modelo
{
    public class Autor
    {

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Nacionalidad { get; set; }
    }
}
