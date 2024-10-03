﻿namespace Product.Application.Requests
{
    public class CreateProductRequest
	{ 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
    }
}

