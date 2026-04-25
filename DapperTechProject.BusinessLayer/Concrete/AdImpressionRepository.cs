using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.AdImpressionDTOs;
using System.Security.Cryptography.X509Certificates;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class AdImpressionRepository :IAdImpressionRepository
    {
        private readonly DapperContext _context;

        public AdImpressionRepository(DapperContext context)
        {
            _context = context;
        }

        // 1 Milyon veriyi sayfalar halinde (Pagination) çekmek için
        public async Task<List<ResultAdImpressionDTO>> GetAdImpressionsWithPagingAsync(int pageNumber, int pageSize)
        {
            // SQL Server'da OFFSET-FETCH kullanarak sayfalama yapıyoruz
            string query = @"Select * from AdImpressions Order By ImpressionID Offset @Offset Rows Fetch Next @PageSize Rows Only";
            var parameters = new DynamicParameters();
            parameters.Add("@offset", (pageNumber - 1) * pageSize); // Kaçıncı satırdan başlayacak
            parameters.Add("@PageSize", pageSize); //kaç tane gelicek
            using (var connection = _context.CreateConnection())
            {
                var adImpressions = await connection.QueryAsync<ResultAdImpressionDTO>(query, parameters);
                return adImpressions.ToList();
            }
        }

        // Dashboard'daki sayaçlar için hızlı toplama (Aggregate) sorgusu
        public async Task<int> GetTotalClickCountAsync()
        {
            string query = "SELECT COUNT(*) FROM AdImpressions WHERE IsClicked = 1";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query);
            }
        }
    }
}
