using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Repository.Entities;
using Repository.Helpers;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _conn;

        public ProductRepository()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            _conn = databaseHelper.NewConnection();
        }

        public ProductRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public List<ProductEntity> Search(string productName)
        {
            List<ProductEntity> entity = new List<ProductEntity>();
            var cmd = new SqlCommand($"select * from product where lower(name) like '%{productName.ToLower()}%'", _conn);
            _conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                entity.Add(new ProductEntity
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                });
            }
            _conn.Close();
            return entity;
        }

        public ProductEntity GetSingle(Guid productOptionId)
        {
            SqlCommand cmd = new SqlCommand($"select * from product where id = '{productOptionId}'", _conn);
            _conn.Open();
            ProductEntity entity = null;
            var rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                entity = new ProductEntity()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                };
            }
            _conn.Close();
            return entity;
        }

        public ProductEntity GetSingle(string name)
        {
            SqlCommand cmd = new SqlCommand($"select * from product where name = '{name}'", _conn);
            _conn.Open();
            ProductEntity entity = null;
            var rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                entity = new ProductEntity()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                };
            }
            _conn.Close();
            return entity;
        }

        public void Save(ProductEntity productEntity)
        {
            
            bool isNew = GetSingle(productEntity.Id) == null;
            var cmd = new SqlCommand($"update product set name = '{productEntity.Name}', description = '{productEntity.Description}', price = {productEntity.Price}, deliveryprice = {productEntity.DeliveryPrice} where id = '{productEntity.Id}'", _conn);
            if (isNew)
            {
                productEntity.Id = Guid.NewGuid();
                cmd.CommandText = $"insert into product (id, name, description, price, deliveryprice) values ('{productEntity.Id}', '{productEntity.Name}', '{productEntity.Description}', {productEntity.Price}, {productEntity.DeliveryPrice})";
            }
        
            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void Delete(Guid productId)
        {
            var cmd = new SqlCommand($"delete from productoption where ProductId = '{productId}'; delete from product where id = '{productId}'", _conn);
            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public List<ProductEntity> GetAll()
        {
            List<ProductEntity> entity = new List<ProductEntity>();
            var cmd = new SqlCommand($"select * from product", _conn);
            _conn.Open();
            
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                entity.Add(new ProductEntity
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                });
            }
            _conn.Close();
            return entity;
        }
    }
}
