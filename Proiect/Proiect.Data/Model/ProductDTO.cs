﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Data.Model
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
