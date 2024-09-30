using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Models.Models
{
    public class Book
    {
        //[Key]
        public int BookID {get; set;}
        public string Title {get; set;}
        public string ISBN {get; set;}
        public decimal Price { get; set; }
        [NotMapped]
        public string PriceRange { get; set; }
    }
}
