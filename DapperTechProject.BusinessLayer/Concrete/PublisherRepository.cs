using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.PublisherDTOs;
using DapperTechProject.EntityLayer;
using System.Data;
using System.Diagnostics.Metrics;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DapperContext _context;

        public PublisherRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultPublisherDTO>> GetPublishersWithCategoriesAsync(int pageNumber, int pageSize, int? categoryId)
        {
            int offset = (pageNumber - 1) * pageSize;

            // Kategori seçilmişse WHERE p.CategoryID = @catId şartını ekliyoruz
            string query = @"
                SELECT p.PublisherID, p.WebsiteURL, p.CategoryID, c.CategoryName  
                FROM Publishers p 
                INNER JOIN Categories c ON p.CategoryID = c.CategoryID 
                WHERE (@catId IS NULL OR p.CategoryID = @catId) 
                ORDER BY p.PublisherID DESC 
                OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("@offset", offset);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@catId", categoryId);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPublisherDTO>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<Publisher> GetByIdPublisherAsync(int id)
        {
            string query = "Select * From Publishers where PublisherID=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Publisher>(query, parameters);
            }
        }

        public async Task<int> GetTotalPublisherCountAsync(int? categoryId)
        {
            string query = "Select count(*) From Publishers where (@catId IS NULL OR CategoryID = @catId)"; // Kategori seçilmişse sadece o kategoriye ait yayıncıları, seçilmediyse tüm yayıncıları sayıyoruz
            var parameters = new DynamicParameters();
            parameters.Add("@catId", categoryId);
            using (var connection = _context.CreateConnection())
            {
                var totalCount = await connection.QueryFirstOrDefaultAsync<int>(query, parameters); // Toplam kayıt sayısını döndürüyoruz
                return totalCount;
            }
        }

        public async Task CreatePublisherAsync(CreatePublisherDTO createPublisherDTO)
        {
            string query = "Insert into Publishers (WebsiteURL, CategoryID) values (@websiteUrl, @categoryId)";

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@websiteUrl", createPublisherDTO.WebsiteURL);
                parameters.Add("@categoryId", createPublisherDTO.CategoryID);

                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdatePublisherAsync(UpdatePublisherDTO updatePublisherDTO)
        {
            string query = "UPDATE Publishers SET WebsiteURL=@websiteUrl, CategoryID=@categoryId WHERE PublisherID=@id";

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@websiteUrl", updatePublisherDTO.WebsiteURL);
                parameters.Add("@categoryId", updatePublisherDTO.CategoryID);
                parameters.Add("@id", updatePublisherDTO.PublisherID);

                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeletePublisherAsync(int id)
        {
            string query = "Delete from Publishers where PublisherID=@id";

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}