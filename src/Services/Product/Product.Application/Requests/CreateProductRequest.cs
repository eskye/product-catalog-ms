﻿namespace Product.Application.Requests
{
    public class ProductRequest
	{ 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
    }

    public class UpdateProductRequest : ProductRequest
    {
        public int Id { get; set; }
    }
}

