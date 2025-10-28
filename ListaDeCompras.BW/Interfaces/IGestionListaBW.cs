
using ListaDeCompras.BC.Entidades;

namespace ListaDeCompras.BW.Interfaces
{
    public interface IGestionListaBW
    {
        List<ListaCompra> ObtenerListas();
        ListaCompra ObtenerListaPorId(Guid id);
        void CrearLista(ListaCompra lista);
        void EliminarLista(Guid id);
    }
}
