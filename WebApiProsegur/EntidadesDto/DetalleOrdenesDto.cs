namespace WebApiProsegur.EntidadesDto
{
    public class DetalleOrdenesDto
    {
        public int Id { get; set; }
        public int IdOrden { get; set; }
        public string Cliente { get; set; }
        public string NombreItems { get; set; }
        public int Cantidad { get; set; }
    }
}
