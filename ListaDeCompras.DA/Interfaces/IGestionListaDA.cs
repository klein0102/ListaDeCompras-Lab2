

using ListaDeCompras.BC.Entidades;

namespace ListaDeCompras.DA.Interfaces
{
    public interface IGestionListaDA
    {
        List<ListaCompra> ObtenerListas();
        ListaCompra ObtenerListaPorId(Guid id);
        void CrearLista(ListaCompra lista);
        void EliminarLista(Guid id);
    }
}
