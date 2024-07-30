using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        

        public int? StockId { get; set; } // Navigation property (Allow us to navigate from one entity to another, it is a foreign key)
        public Stock? Stock { get; set; }
    }
}