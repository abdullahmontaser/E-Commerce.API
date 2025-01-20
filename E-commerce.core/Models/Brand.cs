using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Models
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
