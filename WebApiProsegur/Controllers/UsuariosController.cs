using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using WebApiProsegur.Entidades;
using WebApiProsegur.EntidadesDto;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        public readonly AppDbContext Context;
        private Seguridad seguridad;
        public UsuariosController(AppDbContext context, Seguridad oseguridad)
        {

            this.Context = context;
            seguridad = oseguridad;
        }
        [HttpGet]
        public async Task<ActionResult<List<UsuarioEntity>>> Get()
        {
            return await Context.Usuarios.ToListAsync();
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ProcessResult<UsuarioEntity>> Login([FromBody] LoginDTO loginDTO)
        {
            ProcessResult<UsuarioEntity> result = new ProcessResult<UsuarioEntity>();
            try
            {
                var usuario = await Context.Usuarios.FirstOrDefaultAsync
                                  (usu => usu.Name == loginDTO.Name);
                if (usuario == null)
                {
                    result.IsSuccess = false;
                    result.Message = "El Usuario no Existe";
                }
                else if (usuario.Password != seguridad.GetHash(loginDTO.Password))
                {
                    result.IsSuccess = false;
                    result.Message = "El Password no coincide con el Usuario";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = usuario;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;           
           
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioEntity usuario)
        {
            if (usuario != null) { 
            
                usuario.Password = seguridad.GetHash(usuario.Password);
                Context.Add(usuario);
                await Context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }
        [HttpPut]
        public async Task<ActionResult> Put(UsuarioEntity usuario)
        {
            var usu = await Context.Usuarios.FindAsync(usuario.Id);
            if (usu != null)
            {
                usu.Name = usuario.Name;
                usu.Password = seguridad.GetHash(usuario.Password); 
                usu.IdRol = usuario.IdRol;
                await Context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("id")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var usu = await Context.Usuarios.FindAsync(id);
            if (usu != null)
            {
                Context.Remove(usu);
                await Context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
