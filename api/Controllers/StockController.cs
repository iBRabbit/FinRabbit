using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllStocks() {
            var stocks = _context.Stocks.ToList(); // Ambil semua data stock dari database
            return Ok(stocks); // Return data stock sebagai response
        }

        [HttpGet("{id}")] // Atribut -> Berguna biar controller tau kalo method ini harus nanggepin GET request dengan parameter id
        public IActionResult GetStock(int id) {
            var stock = _context.Stocks.Find(id); // Cari stock berdasarkan id
            
            return (stock != null) ? Ok(stock) : NotFound(); // Kalo stocknya ada, return stocknya. Kalo engga, return 404
        }
    }
}