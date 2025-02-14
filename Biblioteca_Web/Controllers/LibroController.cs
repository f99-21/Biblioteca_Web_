using Biblioteca_Web.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly bibliotecaContext _bibliotecaContext;
        public LibroController(bibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }


        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Libro> listadoLibros = _bibliotecaContext.libro.ToList();

            if (listadoLibros.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {

            var libroAutor = _bibliotecaContext.libro
                .Where(a => a.Id == id)
                .Select(a => new
                {

                    Nombre = a.Titulo

                })
                .FirstOrDefault();

            if (libroAutor == null)
            {
                return NotFound();
            }

            return Ok(libroAutor);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult Guarda([FromBody] Libro libro)
        {
            try
            {
                _bibliotecaContext.libro.Add(libro);
                _bibliotecaContext.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Modificar/{id}")]

        public IActionResult ModificarLibro(int id, [FromBody] Libro ModificarLibro)
        {
            Libro? librosActual = (from e in _bibliotecaContext.libro
                                   where e.Id == id
                                   select e).FirstOrDefault();

            if (librosActual == null)
            {
                return NotFound();
            }

            librosActual.Titulo = ModificarLibro.Titulo;
            librosActual.AnoPublicacion = ModificarLibro.AnoPublicacion;
            librosActual.Autor_id = ModificarLibro.Autor_id;
            librosActual.Categoria_id = ModificarLibro.Categoria_id;
            librosActual.Resumen = ModificarLibro.Resumen;


            _bibliotecaContext.Entry(librosActual).State = EntityState.Modified;
            _bibliotecaContext.SaveChanges();


            return Ok(ModificarLibro);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult Eliminar(int id)
        {
            Libro? libro = (from e in _bibliotecaContext.libro
                            where e.Id == id
                            select e).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();
            }
            _bibliotecaContext.libro.Attach(libro);
            _bibliotecaContext.libro.Remove(libro);
            _bibliotecaContext.SaveChanges();
            return Ok(libro);


        }
    }
}
