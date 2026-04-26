using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.AdImpressionDTOs;
using DapperTechProject.EntityLayer;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class AdImpressionRepository : IAdImpressionRepository
    {
        private readonly DapperContext _context;
        public AdImpressionRepository(DapperContext context) => _context = context;

        public async Task<List<ResultAdImpressionDTO>> GetAdImpressionsWithPagingAsync(int pageNumber, int pageSize, int? regionId, bool? isClicked, int? campaignId, int? publisherId)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT 
                            AI.ImpressionID, AI.IsClicked, AI.CostPerClick, AI.Timestamp,
                            C.CampaignName AS CampaignName, 
                            P.WebsiteURL AS PublisherName, 
                            R.RegionName AS RegionName
                        FROM AdImpressions AI
                        INNER JOIN Campaigns C ON AI.CampaignID = C.CampaignID
                        INNER JOIN Publishers P ON AI.PublisherID = P.PublisherID
                        INNER JOIN Regions R ON AI.RegionID = R.RegionID
                        WHERE 1=1 ";

                if (regionId.HasValue) query += " AND AI.RegionID = @RegionID"; 
                if (isClicked.HasValue) query += " AND AI.IsClicked = @IsClicked";
                if (campaignId.HasValue) query += " AND AI.CampaignID = @CampaignID";
                if (publisherId.HasValue) query += " AND AI.PublisherID = @PublisherID";

                query += " ORDER BY AI.Timestamp DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var values = await connection.QueryAsync<ResultAdImpressionDTO>(query, new
                {
                    RegionID = regionId,
                    IsClicked = isClicked,
                    CampaignID = campaignId,
                    PublisherID = publisherId,
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });
                return values.ToList();
            }
        }

        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            string query = "SELECT * FROM Campaigns ORDER BY CampaignName ASC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Campaign>(query);
                return values.ToList();
            }
        }

        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            string query = "SELECT * FROM Publishers ORDER BY WebsiteURL ASC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Publisher>(query);
                return values.ToList();
            }
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            string query = "SELECT * FROM Regions ORDER BY RegionName ASC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Region>(query);
                return values.ToList();
            }
        }

        public async Task<List<dynamic>> GetCategoryDistributionAsync() //Kategori bazlı dağılım (Hangi kategori ne kadar gösterim aldı?)
        {
            string query = @"SELECT C.CategoryName, COUNT(AI.ImpressionID) as ImpressionCount
                         FROM AdImpressions AI
                         INNER JOIN Publishers P ON AI.PublisherID = P.PublisherID
                         INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                         GROUP BY C.CategoryName
                         ORDER BY ImpressionCount DESC";
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<dynamic>(query)).ToList();
            }
        }

        public async Task<int> GetFilteredCountAsync(int? regionId, bool? isClicked, int? campaignId, int? publisherId) // Filtrelenmiş gösterim sayısı (örneğin, belirli bir bölgede kaç gösterim var veya tıklanma oranı nedir gibi)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = "SELECT COUNT(*) FROM AdImpressions WHERE 1=1";
                if (regionId.HasValue) query += " AND RegionID = @RegionID";
                if (isClicked.HasValue) query += " AND IsClicked = @IsClicked";
                if (campaignId.HasValue) query += " AND CampaignID = @CampaignID";
                if (publisherId.HasValue) query += " AND PublisherID = @PublisherID";

                return await connection.ExecuteScalarAsync<int>(query, new
                {
                    RegionID = regionId,
                    IsClicked = isClicked,
                    CampaignID = campaignId,
                    PublisherID = publisherId
                });
            }
        }

        public async Task<decimal> GetGlobalAvgCPCAsync() // Tüm tıklamalar için ortalama tıklama maliyeti
        {
            string query = "SELECT ISNULL(AVG(CostPerClick), 0) FROM AdImpressions WHERE IsClicked = 1";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<decimal>(query);
            }
        }

        public async Task<List<dynamic>> GetRegionPerformanceAsync() // Bölge bazında performans (gösterim ve tıklama sayıları)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT R.RegionName, 
                         COUNT(AI.ImpressionID) as TotalImpressions,
                         SUM(CAST(AI.IsClicked AS INT)) as TotalClicks
                         FROM Regions R
                         LEFT JOIN AdImpressions AI ON R.RegionID = AI.RegionID
                         GROUP BY R.RegionName";
                return (await connection.QueryAsync<dynamic>(query)).ToList();
            }
        }

        public async Task<List<dynamic>> GetTopEfficiencyPublishersAsync() // Tıklama başına en düşük maliyet getiren yayıncılar
        {
            string query = @"SELECT TOP 5 P.WebsiteURL, AVG(AI.CostPerClick) as AvgCPC
                         FROM AdImpressions AI
                         INNER JOIN Publishers P ON AI.PublisherID = P.PublisherID
                         WHERE AI.IsClicked = 1
                         GROUP BY P.WebsiteURL
                         ORDER BY AvgCPC ASC"; // En ucuz olan en verimlidir
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<dynamic>(query)).ToList();
            }
        }

        public async Task<List<dynamic>> GetTopSpendingCampaignsAsync() // Tıklama başına en yüksek maliyet getiren kampanyalar (en çok harcama yapanlar)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT TOP 5 C.CampaignName, SUM(AI.CostPerClick) as TotalSpend
                         FROM Campaigns C
                         JOIN AdImpressions AI ON C.CampaignID = AI.CampaignID
                         WHERE AI.IsClicked = 1
                         GROUP BY C.CampaignName
                         ORDER BY TotalSpend DESC";
                return (await connection.QueryAsync<dynamic>(query)).ToList();
            }
        }

        public async Task<int> GetTotalAdImpressionCountAsync() // Tüm log sayısı
        {
            string query = "SELECT COUNT(*) FROM AdImpressions";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query);
            }
        }

        public async Task<int> GetTotalClickCountAsync() // Toplam tıklanma sayısı
        {
            string query = "SELECT COUNT(*) FROM AdImpressions WHERE IsClicked = 1";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query);
            }
        }
    }
}