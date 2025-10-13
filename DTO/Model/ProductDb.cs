using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DTO.Model
{
    public class ProductDb
    {
        private readonly string _connectionString;

        public ProductDb(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public ProductResponseDTO AddProduct(CreateProductDTO dto)
        {
            var newProductId = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Product (Name, Description, Price, Stock)
                             VALUES (@Name, @Description, @Price, @Stock)";

                using (var command = new SqlCommand(query, connection))
                {
                     
                    command.Parameters.AddWithValue("@Name", dto.Name);
                    command.Parameters.AddWithValue("@Description", dto.Description);
                    command.Parameters.AddWithValue("@Price", dto.Price);
                    command.Parameters.AddWithValue("@Stock", dto.Stock); 

                    command.ExecuteNonQuery();
                }
            }

            // Return a response DTO
            return new ProductResponseDTO
            {
                Id = newProductId,
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = createdAt
            };
        }

      
    }
}

