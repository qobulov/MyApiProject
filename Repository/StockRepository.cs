using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Dtos.Stock;
using MyApiProject.Interfaces;
using MyApiProject.Model;

namespace MyApiProject.Repository
{
    public class StockRepository: IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if(stockModel== null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.Include(x => x.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(x => x.Comments).FirstOrDefaultAsync(x => x.ID == id);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(x => x.ID == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.Company = stockDto.Company;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Indastry = stockDto.Indastry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock ?? null;
        }
    }
}