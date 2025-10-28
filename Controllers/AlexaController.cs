using System.Text.Json;
using ListaDeCompras.BC.Entidades;
using ListaDeCompras.BW.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AlexaController : ControllerBase
    {
        private readonly IGestionListaBW _gestion;
        public AlexaController(IGestionListaBW gestion)
        {
            _gestion = gestion;
        }

        [HttpGet("test")]
        public IActionResult Test() => Ok("AlexaController operativo"); 

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement request)
        {
            try
            {
                Console.WriteLine("===== Petición desde Alexa =====");
                Console.WriteLine(request.ToString());

                if (!TryGetString(request, new[] { "request", "type" }, out var requestType) || string.IsNullOrWhiteSpace(requestType))
                    return AlexaSay("No entendí tu solicitud. (sin request.type)", true);

                if (requestType == "LaunchRequest")
                {
                    return AlexaSay(
                        "Bienvenido a tu lista de compras. Puedes decir: 'muéstrame mis listas', 'crea una lista supermercado' o 'elimina la lista supermercado'.",
                        endSession: false
                    );
                }

                if (requestType == "IntentRequest")
                {
                    if (!TryGetString(request, new[] { "request", "intent", "name" }, out var intentName) || string.IsNullOrWhiteSpace(intentName))
                        return AlexaSay("No entendí tu intención.", true);

                    Console.WriteLine($"Intent recibido: {intentName}");

                    if (intentName == "ObtenerListasIntent")
                    {
                        var listas = _gestion.ObtenerListas() ?? new List<ListaCompra>(); 
                        var nombres = listas.Select(l => l.Nombre).Where(n => !string.IsNullOrWhiteSpace(n)).ToList();
                        var respuesta = nombres.Any()
                            ? $"Tienes las siguientes listas: {string.Join(", ", nombres)}"
                            : "No tienes ninguna lista creada.";
                        return AlexaSay(respuesta);
                    }

                    if (intentName == "CrearListaIntent")
                    {
                        if (!TryGetSlot(request, "nombre", out var nombreLista) || string.IsNullOrWhiteSpace(nombreLista))
                            return AlexaSay("¿Cómo se llama la lista que deseas crear?", false);

                        var lista = new ListaCompra
                        {
                            IdLista = Guid.NewGuid(),
                            Nombre = nombreLista.Trim(),
                            FechaObjetivo = DateTime.Now,
                            Estado = EstadoLista.Activa,
                            Productos = new List<ItemLista>()
                        };

                        _gestion.CrearLista(lista);
                        return AlexaSay($"La lista {lista.Nombre} ha sido creada con éxito.");
                    }

                    if (intentName == "EliminarListaIntent")
                    {
                        if (!TryGetSlot(request, "nombre", out var nombreLista) || string.IsNullOrWhiteSpace(nombreLista))
                            return AlexaSay("¿Qué lista deseas eliminar?", false);

                        var listas = _gestion.ObtenerListas()
                            .Where(l => string.Equals(l.Nombre, nombreLista.Trim(), StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        if (!listas.Any())
                            return AlexaSay($"No encontré una lista llamada {nombreLista}.");

                        var objetivo = listas.OrderByDescending(l => l.FechaObjetivo).First();
                        _gestion.EliminarLista(objetivo.IdLista);
                        return AlexaSay($"La lista {objetivo.Nombre} fue eliminada correctamente.");
                    }

                    if (intentName == "ObtenerListaPorNombreIntent")
                    {
                        if (!TryGetSlot(request, "nombre", out var nombreLista) || string.IsNullOrWhiteSpace(nombreLista))
                            return AlexaSay("¿Cuál lista deseas consultar?", false);

                        var lista = _gestion.ObtenerListas()
                            .FirstOrDefault(l => string.Equals(l.Nombre, nombreLista.Trim(), StringComparison.OrdinalIgnoreCase));

                        if (lista == null)
                            return AlexaSay($"No encontré una lista llamada {nombreLista}.");

                        var prod = (lista.Productos ?? new List<ItemLista>()).ToList();
                        var texto = !prod.Any()
                            ? "No tienes productos en esta lista."
                            : $"Contiene: {string.Join(", ", prod.Select(p => p.NombreProducto))}.";
                        return AlexaSay($"La lista {lista.Nombre}. {texto}");
                    }

                    return AlexaSay($"No tengo manejador para el intent {intentName}.", true);
                }

                return AlexaSay("Hasta luego.", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR AlexaController: " + ex);
                return AlexaSay($"Ocurrió un error en el servidor: {ex.Message}", true);
            }
        }

        private IActionResult AlexaSay(string text, bool endSession = true)
        {
            return Ok(new
            {
                version = "1.0",
                response = new
                {
                    outputSpeech = new { type = "PlainText", text },
                    shouldEndSession = endSession
                }
            });
        }

        private static bool TryGetSlot(JsonElement root, string slotName, out string value)
        {
            value = null;
            try
            {
                if (root.TryGetProperty("request", out var req) &&
                    req.TryGetProperty("intent", out var intent) &&
                    intent.TryGetProperty("slots", out var slots) &&
                    slots.TryGetProperty(slotName, out var slot) &&
                    slot.TryGetProperty("value", out var v))
                {
                    value = v.GetString();
                    return true;
                }
            }
            catch {}
            return false;
        }

        private static bool TryGetString(JsonElement root, string[] path, out string value)
        {
            value = null;
            try
            {
                JsonElement cur = root;
                foreach (var p in path)
                {
                    if (!cur.TryGetProperty(p, out cur)) return false;
                }
                value = cur.GetString();
                return !string.IsNullOrWhiteSpace(value);
            }
            catch { return false; }
        }
    }
}
