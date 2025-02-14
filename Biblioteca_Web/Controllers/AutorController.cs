using Biblioteca_Web.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {

        private readonly bibliotecaContext _bibliotecacontext;

        public AutorController(bibliotecaContext bibliotecacontext)
        {
            _bibliotecacontext = bibliotecacontext;
        }

        //DEVUELVE TODOS LOS REGISTROS DE LOS AUTORES
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Autor> listadoAutor = _bibliotecacontext.Autor.ToList();

            if (listadoAutor.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutor);
        }

        //buscar por id y odtener libros 
        [HttpGet("{id}")]
        public IActionResult ObtenerAutorPorId(int id)
        {
            try
            {
                var autorConLibros = (from autor in _bibliotecacontext.Autor
                                      join libro in _bibliotecacontext.libro
                                      on autor.Id equals libro.Autor_id
                                      where autor.Id == id
                                      select new
                                      {
                                          AutorId = autor.Id,
                                          AutorNombre = autor.Nombre,
                                          LibroId = libro.Id,
                                          Titulo = libro.Titulo
                                      }).ToList();

                if (!autorConLibros.Any())
                {
                    return NotFound($"Autor con Id {id} no encontrado o no tiene libros.");
                }

                return Ok(autorConLibros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //AGREGAR EL ID NO SE COLOCA PORQUE EN BD ESTA AUTO_INCREMENTAL
        [HttpPost]
        [Route("Add")]

        public IActionResult Guarda([FromBody] Autor autor)
        {
            try
            {
                _bibliotecacontext.Autor.Add(autor);
                _bibliotecacontext.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PARA MODIFICAR IGUAL ID NO SE TOCA 
        [HttpPut]
        [Route("Modificar/{id}")]

        public IActionResult ModificarAutor(int id, [FromBody] Autor ModificarAutor)
        {
            Autor? autoresActual = (from e in _bibliotecacontext.Autor
                                    where e.Id == id
                                    select e).FirstOrDefault();

            if (autoresActual == null)
            {
                return NotFound();
            }


            autoresActual.Nombre = ModificarAutor.Nombre;
            autoresActual.Nacionalidad = ModificarAutor.Nacionalidad;

            _bibliotecacontext.Entry(autoresActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
            _bibliotecacontext.SaveChanges();

            return Ok(ModificarAutor);
        }

        //ELIMINAR POR ID EL AUTOR 
        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult Eliminar(int id)
        {
            Autor? autores = (from e in _bibliotecacontext.Autor
                              where e.Id == id
                              select e).FirstOrDefault();

            if (autores == null)
            {
                return NotFound();
            }
            _bibliotecacontext.Autor.Attach(autores);
            _bibliotecacontext.Autor.Remove(autores);
            _bibliotecacontext.SaveChanges();
            return Ok(autores);


        }
    }
}
