using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WebApiProsegur.Entidades;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/Items")]
    public class ItemsController : ControllerBase
    {
        public readonly AppDbContext Context;
        public ItemsController(AppDbContext context)
        {

            this.Context = context;
        }

        [HttpGet]
        public async Task<ProcessResult<List<ItemsEntity>>> Get()
        {
            ProcessResult<List<ItemsEntity>> result = new ProcessResult<List<ItemsEntity>>();
            try
            {
                var lista = await Context.Items.ToListAsync();
                if (lista == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Items";
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

        [HttpGet("{ItemsHabilitados}")]
        public async Task<ProcessResult<List<ItemsEntity>>> ItemsHabilitados()
        {
            ProcessResult<List<ItemsEntity>> result = new ProcessResult<List<ItemsEntity>>();
            try
            {
                var lista = await Context.Items.ToListAsync();
                var listaDetalle = await Context.DetalleItems.ToListAsync();
                var listaMateriales = await Context.Materiales.ToListAsync();
                var listamaterialescero = listaMateriales.Where<MateriaPrimaEntity>(x => x.Cantidad == 0).ToList();
                if (lista == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Items";
                }
                else
                {
                    var queryitems = from detalle in listaDetalle
                                     join mat in listamaterialescero on detalle.IdMaterial equals mat.Id
                                     select new { Id = detalle.IdItems };
                    List<ItemsEntity> listitem = new List<ItemsEntity>();
                    ItemsEntity Item;
                    foreach (var v in queryitems)
                    {
                        if (listitem.Where<ItemsEntity>( x=> x.Id== v.Id).ToList().Count==0)
                        {
                            Item = new ItemsEntity
                            {
                                Id = v.Id
                            };

                            listitem.Add(Item);
                        }
                       
                    }
                    List<ItemsEntity> listitem2 = new List<ItemsEntity>();
                    ItemsEntity Item2;
                    foreach (var it in lista)
                    {
                        if (listitem.Where<ItemsEntity>(x => x.Id == it.Id).ToList().Count == 0)
                        {
                            Item2 = new ItemsEntity
                        {

                            Id = it.Id,
                            Nombre = it.Nombre,
                            Tiempo = it.Tiempo,
                            Precio = it.Precio,
                        };

                        listitem2.Add (Item2);
                        }
                    }
                    result.IsSuccess = true;
                    result.Result = listitem2;
                
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
        public async Task<ProcessResult<ItemsEntity>> Get(int id)
        {
            ProcessResult<ItemsEntity> result = new ProcessResult<ItemsEntity>();
            try
            {
                var item = await Context.Items.FindAsync(id);
                if (item == null)
                {
                    result.IsSuccess = false;
                    result.Message = "El Item no Existe";
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
        public async Task<ProcessResult<ItemsEntity>> Post(ItemsEntity Item)
        {
            ProcessResult<ItemsEntity> result = new ProcessResult<ItemsEntity>();
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
                    result.Message = "Ingresar el Item a ser registrado";
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
        public async Task<ProcessResult<ItemsEntity>> Put(ItemsEntity Item)
        {
            ProcessResult<ItemsEntity> result = new ProcessResult<ItemsEntity>();
            try
            {
                if (Item != null)
                {
                    var item = await Context.Items.FindAsync(Item.Id);
                    if (item != null)
                    {
                        item.Nombre = Item.Nombre;
                        item.Tiempo = Item.Tiempo;
                        item.Precio = Item.Precio;
                        await Context.SaveChangesAsync();
                        result.IsSuccess = true;
                        result.Result = Item;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "No existe el Item";
                    }

                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ingresar el Itema ser modificado";
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

