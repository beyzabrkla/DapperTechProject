
using DapperTechProject.DTOLayer.CampaignDTOs;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface ICampaignRepository
    {
        Task<List<ResultCampaignDTO>> GetAllCampaignsAsync();
        Task CreateCampaignAsync(CreateCampaignDTO createCampaignDTO);
        Task<GetByIdCampaignDTO> GetCampaignByIdAsync(int id);
        Task UpdateCampaignAsync(UpdateCampaignDTO updateCampaignDTO);
        Task DeleteCampaignAsync(int id);
    }
}
