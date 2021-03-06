﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly DapperContext _dappercontext;
        private readonly TechContext _context;
        public MainController(TechContext context, DapperContext dappercontext)
        {
            _context = context;
            _dappercontext = dappercontext;
        }
        [HttpGet(@"GetOneProduct/{ProductId}")]
        public async Task<ActionResult<ResponseMessage<ProductWithCostsDto>>> GetOneProduct(int ProductId)
        {
            if(ProductId <= 0)
            {
                return BadRequest();
            }
           var product = await _dappercontext.QueryAsync<ProductWithCostsDto>("select p.Id, p.[Name], p.RemainCount, p.Code, av.IncomeCost, av.OptCost, av.SaleCost, u.[Name] as UnitName " +
               "from Products as p INNER JOIN AvCurrentCosts as av on p.Id = av.ProductId LEFT OUTER JOIN Units as u on p.UnitId = u.Id WHERE p.[Id] = @_ProductId  AND p.RemainCount > 0; ", new { _ProductId = ProductId });
            if(product == null)
            {
                return new ResponseMessage<ProductWithCostsDto>() { IsSuccessCode = false, ErrorMessage = "Продукт не найдено!" };
            }
            return new ResponseMessage<ProductWithCostsDto> { entity = product.FirstOrDefault()  };
          
        }

        [HttpPost(@"CloseOrder")]
        public async Task<ActionResult<ResponseMessage<int>>> CloseOrder([FromBody] ProductSales[] entity)
        {
            if (entity == null)
                return BadRequest();
            
            var lastorder = await _dappercontext.QueryAsync<int>("select * from LastOrderView");
            int LastOrder = lastorder.FirstOrDefault();
            if (LastOrder <= 0)
                LastOrder = 1;
            int executed = 0;
            foreach (var item in entity)
            {
                var n = new 
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    SaleCost = item.SaleCost,
                    IsOptCost = item.IsOptCost,
                    CustomerId = item.CustomerId,
                    RegDT = item.RegDt,
                    UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    Comments = item.Comments,
                    IncomeCost = item.IncomeCost,
                    OrderNumber = LastOrder,
                    IsBank = item.IsBank 
                };
                    executed += await _dappercontext.ExecuteAsync("SP_AddProductSale", n, CommandType: System.Data.CommandType.StoredProcedure);
            }
                return new ResponseMessage<int>() { entity = executed, IsSuccessCode = true };
        }




        [HttpGet(@"GetJustOneProduct/{ProductId}")]
        public async Task<ActionResult<ResponseMessage<Products>>> GetJustOneProduct(int ProductId)
        {
            if (ProductId <= 0)
            {
                return BadRequest();
            }
            var product = await _dappercontext.QueryAsync<Products>("Select * from Products where Id = @_ProductId;", new { _ProductId = ProductId });
            if (product == null)
            {
                return new ResponseMessage<Products>() { IsSuccessCode = false, ErrorMessage = "Продукт не найдено!" };
            }
            return new ResponseMessage<Products> { entity = product.FirstOrDefault() };
        }


        [HttpPost(@"CloseIncomeOrder")]
        public async Task<ActionResult<ResponseMessage<int>>> CloseIncomeOrder([FromBody] ProductIncoms[] entity)
        {
            if (entity == null)
                return BadRequest();
            int executed = 0;
            foreach (var item in entity)
            {
                var n = new
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    SaleCost = item.SaleCost,
                    OptCost = item.OptCost,
                    ProductionDt = DateTime.Now,
                    SupplierId = item.SupplierId,
                    RegDT = item.RegDt,
                    UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    Comments = item.Comments,
                    IncomeCost = item.IncomeCost
                };
                executed += await _dappercontext.ExecuteAsync("SP_AddProductIncome", n, CommandType: System.Data.CommandType.StoredProcedure);
            }
            return new ResponseMessage<int>() { entity = executed, IsSuccessCode = true };
        }
    }
}
