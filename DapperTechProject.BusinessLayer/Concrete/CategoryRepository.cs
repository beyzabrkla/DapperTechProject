using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.CateogoryDTOs;
using System.Data;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;
        public CategoryRepository(DapperContext context) => _context = context;


        public async Task<List<ResultCategoryDTO>> GetCategoriesPagedAsync(int page, int pageSize, bool? status, string search = null)
        {
            var query = "SELECT * FROM Categories WHERE 1=1"; //her zaman doğru olan bir koşul ekleyerek dinamik sorgu oluşturmayı kolaylaştırıyoruz 

            if (status.HasValue)
                query += " AND Status = @status"; //kullanıcı tarafından statü sağlanmışsa, sorguya statü filtresi eklenir

            if (!string.IsNullOrEmpty(search))
                query += " AND CategoryName LIKE @search"; //kullanıcı tarafından arama kriteri sağlanmışsa, sorguya arama filtresi eklenir

            query += " ORDER BY CategoryID DESC"; 

            if (pageSize > 0)
            {
                query += " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
            }

            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<ResultCategoryDTO>(query, new
                {
                    status = status,
                    search = $"%{search}%", //& içinde geçerse & sorguya ekle
                    offset = (page - 1) * pageSize, //sayfalama için başlangıç noktasını hesapla eğer 2. sayfadaysan 12. kayıdı atla 13 ten başla
                    pageSize = pageSize
                })).ToList();
            }
        }

        public async Task<int> GetTotalCategoryCountAsync(bool? status)
        {
            string query = "SELECT COUNT(*) FROM Categories WHERE (@status IS NULL OR Status = @status)"; //Statü parametresi ile null olanlar veya belirli bir statüye sahip olanlar sayılacak

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { status });
            }
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            string query = "INSERT INTO Categories (CategoryName, Status) VALUES (@name, @status)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    name = createCategoryDTO.CategoryName,
                    status = createCategoryDTO.Status
                });
            }
        }

        public async Task<GetByIdCategoryDTO> GetCategoryByIdAsync(int id)
        {
            string query = "SELECT * FROM Categories WHERE CategoryID = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetByIdCategoryDTO>(query, new { id });
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            string query = "UPDATE Categories SET CategoryName = @name, Status = @status WHERE CategoryID = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    name = updateCategoryDTO.CategoryName,
                    status = updateCategoryDTO.Status,
                    id = updateCategoryDTO.CategoryID
                });
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            string query = "DELETE FROM Categories WHERE CategoryID = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<string> GetMostPopularCategoryNameAsync()
        {
            string query = @"
        SELECT TOP 1 c.CategoryName
        FROM AdImpressions ai
        INNER JOIN Publishers p ON ai.PublisherID = p.PublisherID
        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
        GROUP BY c.CategoryName
        ORDER BY COUNT(*) DESC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<string>(query) ?? "Belirlenmedi";
            }
        } 

    }
}