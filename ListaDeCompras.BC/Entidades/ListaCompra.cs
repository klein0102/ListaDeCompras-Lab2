using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeCompras.BC.Entidades
{
    public class ListaCompra
    {
        [Key] public Guid IdLista { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaObjetivo { get; set; }
        public EstadoLista Estado { get; set; }
        public List<ItemLista> Productos { get; set; }
    }
    public enum EstadoLista
    {
        Activa,
        Eliminada,
    }
}
