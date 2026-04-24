using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.CateogoryDTOs;

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

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            string query = "insert into Categories (CategoryName) values (@name)"; // SQL sorgusu, parametre olarak @Name kullanıyoruz
            var parameters = new DynamicParameters(); // Dapper'ın DynamicParameters sınıfını kullanarak parametreleri oluşturuyoruz
            parameters.Add("name", createCategoryDTO.CategoryName); // Parametreye createCategoryDTO'dan gelen CategoryName değerini ekliyoruz

            using (var connection = _context.CreateConnection()) // Dapper ile bağlantıyı açıyoruz
            {
                await connection.ExecuteAsync(query, parameters); // Sorguyu çalıştırarak yeni kategoriyi veritabanına ekliyoruz
            }
        }

        public async Task<GetByIdCategoryDTO> GetCategoryByIdAsync(int id)
        {
            string query = "select * from Categories where CategoryID = @id"; 
            var parameters = new DynamicParameters();
            parameters.Add("id", id); // Parametreye id değerini ekliyoruz

            using (var connection = _context.CreateConnection()) // Dapper ile bağlantıyı açıyoruz
            {
                //eğer kayıt varsa ilkini getirir, yoksa null döner
                var category = await connection.QueryFirstOrDefaultAsync<GetByIdCategoryDTO>(query, parameters);
                return category;
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            string query = "update Categories set CategoryName = @name where CategoryID = @id"; 
            var parameters = new DynamicParameters();
            parameters.Add("name", updateCategoryDTO.CategoryName); // Parametreye updateCategoryDTO'dan gelen CategoryName değerini ekliyoruz
            parameters.Add("id", updateCategoryDTO.CategoryID);
            using (var connection = _context.CreateConnection()) // Dapper ile bağlantıyı açıyoruz
            {
                await connection.ExecuteAsync(query, parameters); // Sorguyu çalıştırarak kategoriyi güncelliyoruz
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
