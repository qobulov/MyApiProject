using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Dtos.Stock;
using MyApiProject.Mappers;

namespace MyApiProject.Controllers
{
    [Route("api/Stock")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var stock = await _context.Stock.ToListAsync();
            var stockDto = stock.Select(s => s.ToStockDto());
            return Ok(stock);
        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute]int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.ID}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task <IActionResult> Update([FromRoute]int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.Company = updateDto.Company;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Indastry = updateDto.Indastry;
            stockModel.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task <IActionResult> Delete([FromRoute]int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}