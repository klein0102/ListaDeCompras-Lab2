using ListaDeCompras.BC.Entidades;
using ListaDeCompras.DA.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListaDeCompras.DA.Acciones
{
    public class GestionListaDA : IGestionListaDA
    {
        private readonly AppDbContext _context;

        public GestionListaDA(AppDbContext context)
        {
            _context = context;
        }

        public List<ListaCompra> ObtenerListas()
        {
            return _context.ListasCompra.ToList();
        }

        public ListaCompra ObtenerListaPorId(Guid id)
        {
            return _context.ListasCompra.FirstOrDefault(l => l.IdLista == id);
        }

        public void CrearLista(ListaCompra lista)
        {
            lista.IdLista = Guid.NewGuid();
            _context.ListasCompra.Add(lista);
            _context.SaveChanges();
        }

        public void EliminarLista(Guid id)
        {
            var lista = _context.ListasCompra.FirstOrDefault(l => l.IdLista == id);
            if (lista != null)
            {
                _context.ListasCompra.Remove(lista);
                _context.SaveChanges();
            }
        }
    }
}

