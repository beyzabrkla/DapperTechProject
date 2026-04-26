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
        public PublisherRepository(DapperContext context) => _context = context;

        public async Task<List<ResultPublisherDTO>> GetPublishersWithCategoriesAsync(int pageNumber, int pageSize, int? categoryId, bool? status)
        {
            int offset = (pageNumber - 1) * pageSize;

            string query = @"
                SELECT p.PublisherID, p.WebsiteURL, p.CategoryID, p.Status, c.CategoryName  
                FROM Publishers p 
                INNER JOIN Categories c ON p.CategoryID = c.CategoryID 
                WHERE (@catId IS NULL OR p.CategoryID = @catId) 
                  AND (@status IS NULL OR p.Status = @status)
                ORDER BY p.PublisherID DESC 
                OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<ResultPublisherDTO>(query,
                    new { offset, pageSize, catId = categoryId, status })).ToList();
            }
        }

        public async Task<int> GetTotalPublisherCountAsync(int? categoryId, bool? status)
        {
            string query = @"SELECT COUNT(*) FROM Publishers 
                     WHERE (@catId IS NULL OR CategoryID = @catId)
                       AND (@status IS NULL OR Status = @status)";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { catId = categoryId, status });
            }
        }

        public async Task CreatePublisherAsync(CreatePublisherDTO createPublisherDTO)
        {
            string query = "INSERT INTO Publishers (WebsiteURL, CategoryID, Status) VALUES (@url, @catId, @status)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    url = createPublisherDTO.WebsiteURL,
                    catId = createPublisherDTO.CategoryID,
                    status = createPublisherDTO.Status
                });
            }
        }

        public async Task UpdatePublisherAsync(UpdatePublisherDTO updatePublisherDTO)
        {
            string query = "UPDATE Publishers SET WebsiteURL=@url, CategoryID=@catId, Status=@status WHERE PublisherID=@id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    url = updatePublisherDTO.WebsiteURL,
                    catId = updatePublisherDTO.CategoryID,
                    status = updatePublisherDTO.Status,
                    id = updatePublisherDTO.PublisherID
                });
            }
        }

        public async Task<Publisher> GetByIdPublisherAsync(int id)
        {
            string query = "SELECT * FROM Publishers WHERE PublisherID=@id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Publisher>(query, new { id });
            }
        }

        public async Task DeletePublisherAsync(int id)
        {
            string query = "DELETE FROM Publishers WHERE PublisherID=@id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}