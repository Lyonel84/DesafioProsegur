using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiProsegur.Entidades;
using WebApiProsegur.EntidadesDto;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/Tiendas")]
    public class TiendasController : ControllerBase
    {
        public readonly AppDbContext Context;
        public TiendasController(AppDbContext context)
        {

            this.Context = context;
        }

        [HttpGet]
        public async Task<ProcessResult<List<TiendasEntity>>> Get()
        {
            ProcessResult<List<TiendasEntity>> result = new ProcessResult<List<TiendasEntity>>();
            try
            {
                var Tiendas = await Context.Tiendas.ToListAsync();
                if (Tiendas == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Tiendas";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = Tiendas;
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
        public async Task<ProcessResult<TiendasEntity>> Get(int id)
        {
            ProcessResult<TiendasEntity> result = new ProcessResult<TiendasEntity>();
            try
            {
                var Tienda = await Context.Tiendas.FindAsync(id);
                if (Tienda == null)
                {
                    result.IsSuccess = false;
                    result.Message = "La Tienda no Existe";
                }               
                else
                {
                    result.IsSuccess = true;
                    result.Result = Tienda;
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
