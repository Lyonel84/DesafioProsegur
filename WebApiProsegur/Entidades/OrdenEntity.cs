namespace WebApiProsegur.Entidades
{
    public class OrdenEntity
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTienda { get; set; }
        public string Cliente { get; set; }
        public int Estado { get; set; }
        public double SubTotal { get; set;}
        public double Impuesto { get; set; }
        public double Total { get; set; }

    }
}
