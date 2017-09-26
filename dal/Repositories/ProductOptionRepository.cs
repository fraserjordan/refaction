using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Helpers;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ProductOptionRepository: IProductOptionRepository
    {
        private readonly SqlConnection _conn;
        public ProductOptionRepository()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            _conn = databaseHelper.NewConnection();
        }

        public ProductOptionRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public ProductOptionEntity GetSingle(Guid id)
        {
            var cmd = new SqlCommand($"select * from productoption where id = '{id}'", _conn);
            ProductOptionEntity entity = null;
            _conn.Open();

            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                entity = new ProductOptionEntity()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString()
                };
            }
            _conn.Close();
            return entity;
        }

        public ProductOptionEntity GetSingle(string name)
        {
            var cmd = new SqlCommand($"select * from productoption where name = '{name}'", _conn);
            ProductOptionEntity entity = null;
            _conn.Open();

            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                entity = new ProductOptionEntity()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString()
                };
            }
            _conn.Close();
            return entity;
        }

        public List<ProductOptionEntity> GetForProduct(Guid productId)
        {
            var cmd = new SqlCommand($"select * from productoption where ProductId = '{productId}'", _conn);
            List<ProductOptionEntity> entities = new List<ProductOptionEntity>();
            _conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                entities.Add(new ProductOptionEntity()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString()
                });
            }
            _conn.Close();
            return entities;
        }

        public void Save(ProductOptionEntity productOptionEntity)
        {
            bool isNew = GetSingle(productOptionEntity.Id) == null;
            var cmd = new SqlCommand($"update productoption set name = '{productOptionEntity.Name}', description = '{productOptionEntity.Description}' where id = '{productOptionEntity.Id}'", _conn);
            if (isNew)
            {
                productOptionEntity.Id = Guid.NewGuid();
                cmd.CommandText = $"insert into productoption (id, productid, name, description) values ('{productOptionEntity.Id}', '{productOptionEntity.ProductId}', '{productOptionEntity.Name}', '{productOptionEntity.Description}')";
            }
            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void Delete(Guid id)
        {
            _conn.Open();
            var cmd = new SqlCommand($"delete from productoption where id = '{id}'", _conn);
            cmd.ExecuteNonQuery();
            _conn.Close();
        }
    }
}
