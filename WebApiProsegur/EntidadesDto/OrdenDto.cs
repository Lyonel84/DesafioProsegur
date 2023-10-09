namespace WebApiProsegur.EntidadesDto
{
    public class OrdenDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTienda { get; set; }
        public string Cliente { get; set; }
        public int Estado { get; set; }
        public float Cantidad { get; set; }
    }
}
