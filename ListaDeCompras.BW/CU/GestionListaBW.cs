using ListaDeCompras.BW.Interfaces;
using ListaDeCompras.BC.Entidades;
using ListaDeCompras.DA.Interfaces;

namespace ListaDeCompras.BW.CU
{
    public class GestionListaBW : IGestionListaBW
    {
        private readonly IGestionListaDA _gestionListaDA;
        public GestionListaBW(IGestionListaDA gestionListaDA)
        {
            _gestionListaDA = gestionListaDA;
        }
        public List<ListaCompra> ObtenerListas()
        {
            return _gestionListaDA.ObtenerListas();
        }
        public ListaCompra ObtenerListaPorId(Guid id)
        {
            return _gestionListaDA.ObtenerListaPorId(id);
        }
        public void CrearLista(ListaCompra lista)
        {
            if (string.IsNullOrWhiteSpace(lista.Nombre))
            {
                throw new ArgumentException("El nombre de la lista no puede estar vacío.");
            }
            _gestionListaDA.CrearLista(lista);
        }
        public void EliminarLista(Guid id)
        {
            _gestionListaDA.EliminarLista(id);
        }
    }
}
