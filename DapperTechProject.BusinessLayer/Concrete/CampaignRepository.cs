using Dapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.DTOLayer.CampaignDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Concrete
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly DapperContext _context;

        public CampaignRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultCampaignDTO>> GetAllCampaignsAsync()
        {
            string query = "select * from Campaigns";
            using (var connection = _context.CreateConnection())
            {
                var campaigns = await connection.QueryAsync<ResultCampaignDTO>(query);
                return campaigns.ToList();
            }
        }

        public async Task CreateCampaignsAsync(CreateCampaignDTO createCampaignDTO)
        {
            string query = "insert into Campaigns (CampaignName, Budget, ClientName, CreatedDate) values (@name, @budget, @clientName, @createdDate)";
            var parameters = new DynamicParameters();
            parameters.Add("name", createCampaignDTO.CampaignName);
            parameters.Add("budget", createCampaignDTO.Budget);
            parameters.Add("clientName", createCampaignDTO.ClientName);
            parameters.Add("createdDate", DateTime.UtcNow);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetByIdCampaignDTO> GetCampaignByIdAsync(int id)
        {
            string query = "select * from Campaigns where CampaignID = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            using (var connection = _context.CreateConnection())
            {
                var campaign = await connection.QueryFirstOrDefaultAsync<GetByIdCampaignDTO>(query, parameters);
                return campaign;
            }
        }

        public async Task UpdateCampaignAsync(UpdateCampaignDTO updateCampaignDTO)
        {
            string query = "update Campaigns set CampaignName = @name, Budget = @budget, ClientName = @clientName where CampaignID = @id";
            var parameters = new DynamicParameters();
            parameters.Add("name", updateCampaignDTO.CampaignName);
            parameters.Add("budget", updateCampaignDTO.Budget);
            parameters.Add("clientName", updateCampaignDTO.ClientName);
            parameters.Add("id", updateCampaignDTO.CampaignID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCampaignAsync(int id)
        {
            string query = "delete from Campaigns where CampaignID = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}