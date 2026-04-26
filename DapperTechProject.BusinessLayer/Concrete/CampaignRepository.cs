using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.CampaignDTOs;
using DapperTechProject.DTOLayer.CampaignTypeDTOs;
using DapperTechProject.DTOLayer.RegionDTOs;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly DapperContext _context;
        public CampaignRepository(DapperContext context) => _context = context;

        public async Task<List<ResultCampaignDTO>> GetCampaignsPagedAsync(int page, int pageSize, string search, string region, string type)
        {
            var query = @"SELECT c.*, r.RegionName, t.TypeName 
                          FROM Campaigns c
                          LEFT JOIN Regions r ON c.RegionID = r.RegionID
                          LEFT JOIN CampaignTypes t ON c.TypeID = t.TypeID
                          WHERE 1=1";

            if (!string.IsNullOrEmpty(search))
                query += " AND (c.CampaignName LIKE @search OR c.ClientName LIKE @search)"; // Arama kriterini CampaignName ve ClientName kolonlarında uyguluyoruz
            if (!string.IsNullOrEmpty(region))
                query += " AND r.RegionName = @region";
            if (!string.IsNullOrEmpty(type))
                query += " AND t.TypeName = @type";

            query += " ORDER BY c.CampaignID DESC OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<ResultCampaignDTO>(query, new
                {
                    search = $"%{search}%",
                    region = region,
                    type = type,
                    offset = (page - 1) * pageSize,
                    pageSize = pageSize
                })).ToList();
            }
        }

        public async Task<int> GetTotalCampaignCountAsync(string search = null, string region = null, string type = null)
        {
            string query = @"SELECT COUNT(*) FROM Campaigns c
                            LEFT JOIN Regions r ON c.RegionID = r.RegionID
                            LEFT JOIN CampaignTypes t ON c.TypeID = t.TypeID
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(search))
                query += " AND (c.CampaignName LIKE @search OR c.ClientName LIKE @search)";
            if (!string.IsNullOrEmpty(region))
                query += " AND r.RegionName = @region";
            if (!string.IsNullOrEmpty(type))
                query += " AND t.TypeName = @type";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { search = $"%{search}%", region, type });
            }
        }

        public async Task<List<ResultRegionDTO>> GetRegionsAsync()
        {
            string query = "SELECT * FROM Regions";
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<ResultRegionDTO>(query)).ToList();
            }
        }

        public async Task<List<ResultCampaignTypeDTO>> GetCampaignTypesAsync()
        {
            string query = "SELECT * FROM CampaignTypes";
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<ResultCampaignTypeDTO>(query)).ToList();
            }
        }

        public async Task CreateCampaignAsync(CreateCampaignDTO createCampaignDTO)
        {
            string query = "INSERT INTO Campaigns (CampaignName, Budget, ClientName, CreatedDate, RegionID, TypeID) VALUES (@name, @budget, @clientName, @createdDate, @regionId, @typeId)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    name = createCampaignDTO.CampaignName,
                    budget = createCampaignDTO.Budget,
                    clientName = createCampaignDTO.ClientName,
                    regionId = createCampaignDTO.RegionID,
                    typeId = createCampaignDTO.TypeID,
                    createdDate = DateTime.Now
                });
            }
        }

        public async Task<GetByIdCampaignDTO> GetCampaignByIdAsync(int id)
        {
            string query = "SELECT * FROM Campaigns WHERE CampaignID = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetByIdCampaignDTO>(query, new { id });
            }
        }

        public async Task UpdateCampaignAsync(UpdateCampaignDTO updateCampaignDTO)
        {
            string query = @"UPDATE Campaigns SET 
                            CampaignName = @name, 
                            Budget = @budget, 
                            ClientName = @clientName, 
                            RegionID = @regionId, 
                            TypeID = @typeId 
                            WHERE CampaignID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    name = updateCampaignDTO.CampaignName,
                    budget = updateCampaignDTO.Budget,
                    clientName = updateCampaignDTO.ClientName,
                    regionId = updateCampaignDTO.RegionID,
                    typeId = updateCampaignDTO.TypeID,
                    id = updateCampaignDTO.CampaignID
                });
            }
        }

        public async Task DeleteCampaignAsync(int id)
        {
            string query = "DELETE FROM Campaigns WHERE CampaignID = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}