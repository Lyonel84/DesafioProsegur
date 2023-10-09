using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WebApiProsegur.Entidades;
using WebApiProsegur.EntidadesDto;
using WebApiProsegur.Filtros;

namespace WebApiProsegur.Controllers
{
    [ApiController]
    [Route("api/Ordenes")]
    public class OrdenController : ControllerBase
    {
        public readonly AppDbContext Context;
        public OrdenController(AppDbContext context)
        {

            this.Context = context;
        }
        [HttpGet]
        public async Task<ProcessResult<List<OrdenEntity>>> Get()
        {
            ProcessResult<List<OrdenEntity>> result = new ProcessResult<List<OrdenEntity>>();
            try
            {
                var lisOrdenes = await Context.Ordenes.ToListAsync();
                if (lisOrdenes == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Ordenes";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = lisOrdenes;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("ListaOrdenes")]
        public async Task<ProcessResult<List<OrdenDto>>> ListaOrdenes([FromBody] FiltroUsuarioTienda filtro)
        {
            ProcessResult<List<OrdenDto>> result = new ProcessResult<List<OrdenDto>>();
            try
            {
                var lista = await Context.Ordenes.ToListAsync();
                var listaDetalleOrden = await Context.DetalleOrdenes.ToListAsync();



                if (lista == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Items";
                }
                else
                {

                    var listausuario = lista.Where<OrdenEntity>(x => x.IdUsuario == filtro.idusuario && x.IdTienda == filtro.idtienda);
                    var query = from detalle in listaDetalleOrden
                                join ord in listausuario on detalle.IdOrden equals ord.Id
                                select new { Id = detalle.Id, IdItems = detalle.IdItems, IdOrden = detalle.IdOrden };
                    List<DetalleOrdenEntity> list = new List<DetalleOrdenEntity>();
                    DetalleOrdenEntity Item;
                    foreach (var v in query)
                    {
                        Item = new DetalleOrdenEntity
                        {
                            Id = v.Id,
                            IdItems = v.IdItems,
                            IdOrden = v.IdOrden,
                        };

                        list.Add(Item);
                    }

                    List<OrdenDto> list2 = new List<OrdenDto>();
                    OrdenDto Item2;
                    foreach (var it in listausuario)
                    {
                        Item2 = new OrdenDto
                        {

                            Id = it.Id,
                            IdUsuario = it.IdUsuario,
                            IdTienda = it.IdTienda,
                            Cliente = it.Cliente,
                            Estado = it.Estado,
                            Cantidad = list.Where<DetalleOrdenEntity>(x => x.IdOrden == it.Id).ToList().Count,
                        };

                        list2.Add(Item2);

                    }
                    result.IsSuccess = true;
                    result.Result = list2;

                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost("ImprimirComanda")]
        public async Task<ProcessResult<OrdenEntity>> ImprimirComanda([FromBody] OrdenDto itemOrden)
        {
            ProcessResult<OrdenEntity> result = new ProcessResult<OrdenEntity>();
            try
            {
                var oOrden = await Context.Ordenes.FindAsync(itemOrden.Id);
                var listaDetalleOrden = await Context.DetalleOrdenes.ToListAsync();
                var listaItemsMateriales = await Context.DetalleItems.ToListAsync();

                listaDetalleOrden = listaDetalleOrden.Where<DetalleOrdenEntity>(x=>x.IdOrden == (oOrden==null? 0 :oOrden.Id)).ToList();

                if (oOrden == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen la orden";
                }
                else
                {
                    oOrden.Estado = 2;
                    await Context.SaveChangesAsync();
                    foreach (var item in listaDetalleOrden) {
                        var Listamat = listaItemsMateriales.Where<ItemsMaterialesEntity>(x => x.IdItems == item.IdItems).ToList();
                        foreach (var item2 in Listamat)
                        {
                            var omat = await Context.Materiales.FindAsync(item2.IdMaterial);
                            omat.Cantidad = omat.Cantidad - item.Cantidad;
                            await Context.SaveChangesAsync();
                        }
                    
                    }
                    
                    result.IsSuccess = true;
                    result.Result = oOrden;

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
        public async Task<ProcessResult<OrdenEntity>> Post(OrdenDetalleDto Item)
        {
            ProcessResult<OrdenEntity> result = new ProcessResult<OrdenEntity>();
            try
            {

                if (Item != null)
                {
                    List<DetalleOrdenEntity> listDet = Item.ListaDetalle;
                    var otienda = await Context.Tiendas.FindAsync(Item.OrdenEntity.IdTienda);
                    var oprovincia = await Context.Provincias.FindAsync(otienda == null ? 0 : otienda.IdProvincia);
                    double subtotal = 0;
                    if (!Item.id.HasValue)
                    {

                        Item.OrdenEntity.Estado = 1;
                        Item.OrdenEntity.SubTotal = 0;
                        Item.OrdenEntity.Impuesto = 0;
                        Item.OrdenEntity.Total = 0;
                        Context.Add(Item.OrdenEntity);
                        await Context.SaveChangesAsync();
                        var idNew = Item.OrdenEntity.Id;

                      
                        foreach (var idet in listDet)
                        {
                            var oItem = await Context.Items.FindAsync(idet.IdItems);
                            subtotal = subtotal + (idet.Cantidad * (oItem == null ? 0 : oItem.Precio));
                            idet.IdOrden = idNew;

                            Context.Add(idet);

                            await Context.SaveChangesAsync();
                        }
                        var itemOrden = await Context.Ordenes.FindAsync(idNew);
                        if (itemOrden != null)
                        {
                            Item.OrdenEntity.SubTotal = subtotal;
                            Item.OrdenEntity.Impuesto = subtotal * (oprovincia == null ? 0 : oprovincia.Impuesto) / 100;
                            Item.OrdenEntity.Total = Item.OrdenEntity.SubTotal + Item.OrdenEntity.Impuesto;
                            await Context.SaveChangesAsync();
                            result.IsSuccess = true;
                            result.Result = itemOrden;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "No existe una Orden";
                        }
                    }
                    else
                    {
                        foreach (var idet in listDet)
                        {
                            var oItem = await Context.Items.FindAsync(idet.IdItems);
                            subtotal = subtotal + (idet.Cantidad * (oItem == null ? 0 : oItem.Precio));
                            if (idet.Id == 0)
                            {                               
                                idet.IdOrden = (int)Item.id;
                                Context.Add(idet);
                                await Context.SaveChangesAsync();
                            }
                            else
                            {
                                var itemdetalle = await Context.DetalleOrdenes.FindAsync(idet.Id);
                                if (itemdetalle != null)
                                {
                                    itemdetalle.Cantidad = idet.Cantidad;
                                     await Context.SaveChangesAsync();
                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message = "No existe una Orden";
                                }
                            }
                           
                           
                        }
                        var itemOrden = await Context.Ordenes.FindAsync((int)Item.id);
                        if (itemOrden != null)
                        {
                            itemOrden.Cliente = Item.OrdenEntity.Cliente;
                            itemOrden.SubTotal = subtotal;
                            itemOrden.Impuesto = subtotal * (oprovincia == null ? 0 : oprovincia.Impuesto) / 100;
                            itemOrden.Total = itemOrden.SubTotal + itemOrden.Impuesto;
                            await Context.SaveChangesAsync();
                            result.IsSuccess = true;
                            result.Result = itemOrden;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "No existe una Orden";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ingresar la Orden a ser registrado";
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
