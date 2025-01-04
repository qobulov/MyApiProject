using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiProject.Dtos.Stock;
using MyApiProject.Model;

namespace MyApiProject.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockDto)
        {
            return new StockDto
            {
                ID = stockDto.ID,
                Symbol = stockDto.Symbol,
                Company = stockDto.Company,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Indastry = stockDto.Indastry,
                MarketCap = stockDto.MarketCap,
                Comments = stockDto.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this  CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                Company = stockDto.Company,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Indastry = stockDto.Indastry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}