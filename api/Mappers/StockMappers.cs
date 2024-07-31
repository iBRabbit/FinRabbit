using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto (this Stock stockModel) {
            return new StockDto {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Price = stockModel.Price,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap
            }; 
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto) {
            return new Stock {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Price = stockDto.Price,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap
            };
        }
    }

}