﻿namespace ProductManager.API.Dtos {
    public class UpdateProductDto {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
