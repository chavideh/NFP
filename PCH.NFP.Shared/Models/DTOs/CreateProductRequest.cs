﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCH.NFP.Shared.Models
{
    public class CreateProductRequest
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string IranCode { get; set; }
        public string SepidarCode { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Publish { get; set; }
    }
}
