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

        public async Task<int> GetFilteredCountAsync(int? regionId, bool? isClicked, int? campaignId, int? publisherId)
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

        public Task<int> GetFilteredCountAsync(int? regionId, bool? isClicked)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalAdImpressionCountAsync()
        {
            string query = "SELECT COUNT(*) FROM AdImpressions";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query);
            }
        }

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