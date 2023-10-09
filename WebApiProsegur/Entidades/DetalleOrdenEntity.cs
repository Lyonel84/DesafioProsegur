namespace WebApiProsegur.Entidades
{
    public class DetalleOrdenEntity
    {
        public int Id { get; set; }
        public int IdOrden { get; set; }
        public int IdItems { get; set; }
        public int Cantidad { get; set; }

    }
}
