using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiProject.Dtos.Comment;

namespace MyApiProject.Dtos.Stock
{
    public class StockDto
    {
        public int ID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Indastry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}