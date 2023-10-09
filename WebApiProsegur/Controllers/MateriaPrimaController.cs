using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProsegur.Entidades;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/MateriaPrima")]
    public class MateriaPrimaController : ControllerBase
    {
        public readonly AppDbContext Context;
        public MateriaPrimaController(AppDbContext context)
        {

            this.Context = context;
        }

        [HttpGet]
        public async Task<ProcessResult<List<MateriaPrimaEntity>>> Get()
        {
            ProcessResult<List<MateriaPrimaEntity>> result = new ProcessResult<List<MateriaPrimaEntity>>();
            try
            {
                var lista = await Context.Materiales.ToListAsync();
                if (lista == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Materias Primas";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = lista;
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
        public async Task<ProcessResult<MateriaPrimaEntity>> Get(int id)
        {
            ProcessResult<MateriaPrimaEntity> result = new ProcessResult<MateriaPrimaEntity>();
            try
            {
                var item = await Context.Materiales.FindAsync(id);
                if (item == null)
                {
                    result.IsSuccess = false;
                    result.Message = "La Materia Prima no Existe";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = item;
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
        public async Task<ProcessResult<MateriaPrimaEntity>> Post(MateriaPrimaEntity materiaprima)
        {
            ProcessResult<MateriaPrimaEntity> result = new ProcessResult<MateriaPrimaEntity>();
            try
            {
                if (materiaprima != null)
                {
                    Context.Add(materiaprima);
                    await Context.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Result = materiaprima;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ingresar la Materia Prima";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;

        }
        [HttpPut]
        public async Task<ProcessResult<MateriaPrimaEntity>> Put(MateriaPrimaEntity materiaprima)
        {
            ProcessResult<MateriaPrimaEntity> result = new ProcessResult<MateriaPrimaEntity>();
            try
            {
                if (materiaprima != null)
                {
                    var item = await Context.Materiales.FindAsync(materiaprima.Id);
                    if (item != null)
                    {
                        item.Nombre = materiaprima.Nombre;
                        item.Cantidad = materiaprima.Cantidad;
                        await Context.SaveChangesAsync();
                        result.IsSuccess = true;
                        result.Result = materiaprima;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Ingresar la Materia Prima";
                    }
                   
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ingresar la Materia Prima";
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

