using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Dtos.Proudects
{
    public class ProudectDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? TypeId { get; set; } //FK
        public string TypeName { get; set; }
        public int? BrandId { get; set; } //Fk
        public string BrandName { get; set; }
    }
}
