using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController] // This attribute tells the controller that it should respond to web API requests
    [Route("api/stock")] // This attribute tells the controller that it should respond to requests that start with /api/stock
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context; // Konteksnya database, berguna biar controller bisa akses database. Konteks bisa berarti koneksi ke database
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        
        [HttpGet] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin GET request
        public async Task<IActionResult> GetStocks() {
            var stocks = await _context.Stocks.ToListAsync(); // Ambil semua stock dari database
            return Ok(stocks.Select(stock => stock.ToStockDto())); // Return semua stock yang ada
        }

        [HttpGet("{id}")] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin GET request dengan parameter id
        public async Task<IActionResult> GetStock(int id) {
            var stock = await _context.Stocks.FindAsync(id); // Cari stock berdasarkan id

            if (stock == null) {
                return NotFound(); // Kalo stocknya engga ada, return 404
            }

            return Ok(stock.ToStockDto()); // Return stock yang ada
        }

        [HttpPost] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin POST request
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
            var stock = new Stock { // Buat stock baru
                Symbol = stockDto.Symbol, // Set symbol
                CompanyName = stockDto.CompanyName, // Set company name
                Price = stockDto.Price, // Set price
                LastDiv = stockDto.LastDiv, // Set last div
                MarketCap = stockDto.MarketCap // Set market cap
            };

            _context.Stocks.Add(stock); // Tambahkan stock ke database
            await _context.SaveChangesAsync(); // Simpan perubahan ke database

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto()); // Return 201 Created
        }

        [HttpPut("{id}")] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin PUT request dengan parameter id
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto stockDto) {
            var stock = await _context.Stocks.FindAsync(id); // Cari stock berdasarkan id

            if (stock == null) {
                return NotFound(); // Kalo stocknya engga ada, return 404
            }

            stock.Symbol = stockDto.Symbol; // Set symbol
            stock.CompanyName = stockDto.CompanyName; // Set company name
            stock.Price = stockDto.Price; // Set price
            stock.LastDiv = stockDto.LastDiv; // Set last div
            stock.MarketCap = stockDto.MarketCap; // Set market cap

            await _context.SaveChangesAsync(); // Simpan perubahan ke database

            return NoContent(); // Return 204 No Content
        }

        [HttpDelete("{id}")] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin DELETE request dengan parameter id
        public async Task<IActionResult> Delete(int id) {
            var stock = await _context.Stocks.FindAsync(id); // Cari stock berdasarkan id

            if (stock == null) {
                return NotFound(); // Kalo stocknya engga ada, return 404
            }

            _context.Stocks.Remove(stock); // Hapus stock dari database
            await _context.SaveChangesAsync(); // Simpan perubahan ke database

            return NoContent(); // Return 204 No Content
        }


    }
}