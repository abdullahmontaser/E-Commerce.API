using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Models
{
    public class Proudect :BaseEntity<int>
    {
     
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? TypeId { get; set; } //FK
        public Types Type  { get; set; }
        public int? BrandId { get; set; } //Fk
        public Brand Brand  { get; set; }
    }
}
