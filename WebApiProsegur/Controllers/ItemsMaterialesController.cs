using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProsegur.Entidades;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/DetalleItems")]
    public class ItemsMaterialesController  : ControllerBase
    {
        public readonly AppDbContext Context;
        public ItemsMaterialesController(AppDbContext context)
        {

            this.Context = context;
        }

        [HttpGet]
        public async Task<ProcessResult<List<ItemsMaterialesEntity>>> Get()
        {
            ProcessResult<List<ItemsMaterialesEntity>> result = new ProcessResult<List<ItemsMaterialesEntity>>();
            try
            {
                var lista = await Context.DetalleItems.ToListAsync();
                if (lista == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen items Materales";
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
        public async Task<ProcessResult<ItemsMaterialesEntity>> Get(int id)
        {
            ProcessResult<ItemsMaterialesEntity> result = new ProcessResult<ItemsMaterialesEntity>();
            try
            {
                var item = await Context.DetalleItems.FindAsync(id);
                if (item == null)
                {
                    result.IsSuccess = false;
                    result.Message = "El Detalle del Item no Existe";
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
        public async Task<ProcessResult<ItemsMaterialesEntity>> Post(ItemsMaterialesEntity Item)
        {
            ProcessResult<ItemsMaterialesEntity> result = new ProcessResult<ItemsMaterialesEntity>();
            try
            {
                if (Item != null)
                {
                    Context.Add(Item);
                    Item.Id = await Context.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Result = Item;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ingresar detalle del Item";
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
        public async Task<ProcessResult<ItemsMaterialesEntity>> Put(ItemsMaterialesEntity Item)
        {
            ProcessResult<ItemsMaterialesEntity> result = new ProcessResult<ItemsMaterialesEntity>();
            try
            {
                if (Item != null)
                {
                    var item = await Context.DetalleItems.FindAsync(Item.Id);
                    if (item != null)
                    {
                        item.IdItems = Item.IdItems;
                        item.IdMaterial = Item.IdMaterial;
                        await Context.SaveChangesAsync();
                        result.IsSuccess = true;
                        result.Result = Item;
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

