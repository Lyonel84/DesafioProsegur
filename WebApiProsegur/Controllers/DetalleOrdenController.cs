using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiProsegur.Entidades;
using WebApiProsegur.EntidadesDto;
using WebApiProsegur.Filtros;

namespace WebApiProsegur.Controllers
{
    
[ApiController]
    [Route("api/DetalleOrdenes")]
    public class DetalleOrdenController : ControllerBase
    {
        
        public readonly AppDbContext Context;
        public DetalleOrdenController(AppDbContext context)
        {

            this.Context = context;
        }
        [HttpGet]
        public async Task<ProcessResult<List<DetalleOrdenEntity>>> Get()
        {
            ProcessResult<List<DetalleOrdenEntity>> result = new ProcessResult<List<DetalleOrdenEntity>>();
            try
            {
                var lisOrdenes = await Context.DetalleOrdenes.ToListAsync();
                if (lisOrdenes == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No Existen Detalle para esta Orden";
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


        [HttpPost("ListarDetalleOrdenes")]

        public async Task<ProcessResult<List<DetalleOrdenesDto>>> ListarDetalleOrdenes([FromBody] FiltroOrder filtro)
        {
            ProcessResult<List<DetalleOrdenesDto>> result = new ProcessResult<List<DetalleOrdenesDto>>();
            try
            {
                var lisOrdenes = await Context.DetalleOrdenes.ToListAsync();
                var listaItems = await Context.Items.ToListAsync();

                if (lisOrdenes == null)
                {

                    result.IsSuccess = false;
                    result.Message = "No Existen Detalle para esta Orden";
                }
                else
                {
                    List<DetalleOrdenesDto> list = new List<DetalleOrdenesDto>();
                    DetalleOrdenesDto detalleOrdenesDto;
                    foreach (var item in lisOrdenes.Where<DetalleOrdenEntity>(x => x.IdOrden == filtro.idorden).ToList())
                    {
                        var oOrden = await Context.Ordenes.FindAsync(item.IdOrden);
                        detalleOrdenesDto = new DetalleOrdenesDto
                        {
                            Id = item.Id,
                            IdOrden = item.IdOrden,
                            Cliente = oOrden==null?"":oOrden.Cliente,
                            NombreItems = listaItems.Where<ItemsEntity>(x => x.Id == item.IdItems).First().Nombre,
                            Cantidad = item.Cantidad

                        };
                        list.Add(detalleOrdenesDto);

                    }

                    result.IsSuccess = true;
                    result.Result = list;
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
