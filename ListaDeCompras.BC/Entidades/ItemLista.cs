using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeCompras.BC.Entidades
{
    public class ItemLista
    {
        [Key] public Guid IdItem { get; set; }
        public Guid IdLista { get; set; }
        public string NombreProducto { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public EstadoProducto Estado { get; set; }

        public ListaCompra Lista { get; set; }
    }
    public enum EstadoProducto
    {
        Pendiente,
        Comprado
    }
}
