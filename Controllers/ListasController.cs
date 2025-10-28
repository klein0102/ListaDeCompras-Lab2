using ListaDeCompras.BC.Entidades;
using ListaDeCompras.BW.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListasController : ControllerBase
    {
        private readonly IGestionListaBW _gestionListaBW;

        public ListasController(IGestionListaBW gestionListaBW)
        {
            _gestionListaBW = gestionListaBW;
        }

        // GET: api/Listas
        [HttpGet]
        public IActionResult ObtenerListas()
        {
            var listas = _gestionListaBW.ObtenerListas();
            return Ok(listas);
        }

        // GET: api/Listas/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerListaPorId(Guid id)
        {
            var lista = _gestionListaBW.ObtenerListaPorId(id);
            if (lista == null)
                return NotFound("Lista no encontrada.");

            return Ok(lista);
        }

        // POST: api/Listas
        [HttpPost]
        public IActionResult CrearLista([FromBody] ListaCompra lista)
        {
            try
            {
                _gestionListaBW.CrearLista(lista);
                return Ok("Lista creada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Listas/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarLista(Guid id)
        {
            _gestionListaBW.EliminarLista(id);
            return Ok("Lista eliminada correctamente.");
        }
    }
}
