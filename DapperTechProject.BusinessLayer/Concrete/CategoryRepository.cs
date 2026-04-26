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

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoriesAsync()
        {
            string query = "select * from Categories";
            using (var connection = _context.CreateConnection()) // Dapper ile bağlantıyı açıyoruz
            {
                var categories = await connection.QueryAsync<ResultCategoryDTO>(query); // Sorguyu çalıştırarak sonuçları ResultCategoryDTO nesnelerine dönüştürüyoruz
                return categories.ToList();
            }
        }

        public async Task<List<ResultCategoryDTO>> GetCategoriesPagedAsync(int pageNumber, int pageSize)
        {
           int offset = (pageNumber - 1) * pageSize;

            string query = "select * from Categories order by CategoryID offset @offset rows fetch next @pageSize rows only"; 
            var parameters = new DynamicParameters();
            parameters.Add("offset", offset); // Parametreye hesapladığımız offset değerini ekliyoruz
            parameters.Add("pageSize", pageSize); 

            using (var connection = _context.CreateConnection())
            {
                var categories = await connection.QueryAsync<ResultCategoryDTO>(query, parameters);
                return categories.ToList();
            }
        }

        public async Task<int> GetTotalCategoryCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Categories";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(query);
            }
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            string query = "insert into Categories (CategoryName) values (@name)";
            var parameters = new DynamicParameters();
            parameters.Add("name", createCategoryDTO.CategoryName); 

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters); 
            }
        }

        public async Task<GetByIdCategoryDTO> GetCategoryByIdAsync(int id)
        {
            string query = "select * from Categories where CategoryID = @id"; 
            var parameters = new DynamicParameters();
            parameters.Add("id", id); // Parametreye id değerini ekliyoruz

            using (var connection = _context.CreateConnection())
            {
                //eğer kayıt varsa ilkini getirir, yoksa null döner
                var category = await connection.QueryFirstOrDefaultAsync<GetByIdCategoryDTO>(query, parameters);
                return category;
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            string query = "update Categories set CategoryName = @name where CategoryID = @id";
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("name", updateCategoryDTO.CategoryName);
                parameters.Add("id", updateCategoryDTO.CategoryID);

                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            string query = "Delete From Categories Where CategoryID = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
