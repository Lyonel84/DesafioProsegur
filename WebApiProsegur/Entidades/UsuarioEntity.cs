namespace WebApiProsegur.Entidades
{
    public class UsuarioEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Password { get; set;}
        public int IdRol { get; set;}
    }
}
