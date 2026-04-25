using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.PublisherDTOs;
using DapperTechProject.EntityLayer;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DapperContext _context;

        public PublisherRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllPublisherAsync()
        {
            string query = "Select * from Publishers";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Publisher>(query);
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

        public Task CreatePublisherAsync(CreatePublisherDTO createPublisherDTO)
        {
            string query = "Insert into Publishers (WebsiteURL, CategoryID) values (@websiteUrl, @categoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@websiteUrl", createPublisherDTO.WebsiteURL);
            parameters.Add("@categoryId", createPublisherDTO.CategoryID);

            using (var connection = _context.CreateConnection())
            {
                return connection.ExecuteAsync(query, parameters);
            }
        }

        public Task UpdatePublisherAsync(UpdatePublisherDTO updatePublisherDTO)
        {
            string query ="Update Publishers set WebsiteURL=@websiteUrl, CategoryID=@categoryId where PublisherID=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@websiteUrl", updatePublisherDTO.WebsiteURL);
            parameters.Add("@categoryId", updatePublisherDTO.CategoryID);
            parameters.Add("@id", updatePublisherDTO.PublisherID);

            using (var connection = _context.CreateConnection())
            {
                return connection.ExecuteAsync(query, parameters);
            }
        }

        public Task DeletePublisherAsync(int id)
        {
            string query = "Delete from Publishers where PublisherID=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return connection.ExecuteAsync(query, parameters);
            }
        }
    }
}