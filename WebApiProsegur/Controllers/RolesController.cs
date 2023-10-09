using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApiProsegur.Entidades;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/Roles")]
    public class RolesController : ControllerBase
    {
        public readonly AppDbContext Context;
        public RolesController(AppDbContext context)
        {

            this.Context = context;
        }

        [HttpGet]
        public async Task<ProcessResult<List<RolEntity>>> Get()
        {
            ProcessResult<List<RolEntity>> result = new ProcessResult<List<RolEntity>>();
            try
            {
                var lisRoles = await Context.Roles.ToListAsync();
                if (lisRoles == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Roles";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = lisRoles;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<ProcessResult<RolEntity>> Get(int id)
        {
            ProcessResult<RolEntity> result = new ProcessResult<RolEntity>();
            try
            {
                var Rol = await Context.Roles.FindAsync(id);
                if (Rol == null)
                {
                    result.IsSuccess = false;
                    result.Message = "El Rol no Existe";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = Rol;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
