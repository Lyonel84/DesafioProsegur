using WebApiProsegur.Entidades;

namespace WebApiProsegur.EntidadesDto
{
    public class OrdenDetalleDto
    {
        public int? id { get; set; }
        public OrdenEntity OrdenEntity { get; set; }

        public List<DetalleOrdenEntity> ListaDetalle { get; set; }

    }
}
