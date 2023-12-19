﻿using OnlineInventory.Application.Interfaces;
using OnlineInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineInventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public IEnumerable<Product> GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public async Task InsertProductAsync(Product product)
        {
            await _productRepository.InsertProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductByIdAsync(int productId)
        {
            await _productRepository.DeleteProductByIdAsync(productId);
        }

    }
}
