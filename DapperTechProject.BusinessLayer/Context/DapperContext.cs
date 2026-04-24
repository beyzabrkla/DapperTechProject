using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DapperTechProject.BusinessLayer.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration; //appsetting.json dosyasındaki yazılan ayarları okur.
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString); //IDbConnection, SqlConnection sınıfının örneğini döndürürür.Yani veritabanı bağlantısı oluşturmak için kullanılır.
    }
}
