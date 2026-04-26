
using DapperTechProject.DTOLayer.CampaignDTOs;
using DapperTechProject.DTOLayer.CampaignTypeDTOs;
using DapperTechProject.DTOLayer.RegionDTOs;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface ICampaignRepository
    {
        Task<List<ResultCampaignDTO>> GetCampaignsPagedAsync(int page, int pageSize, string search = null, string region = null, string type = null); //listeleme ve filtreleme için metot
        Task<int> GetTotalCampaignCountAsync(string search = null, string region = null, string type = null); //toplam kayıt sayısını almak için metot

        Task<List<ResultRegionDTO>> GetRegionsAsync();
        Task<List<ResultCampaignTypeDTO>> GetCampaignTypesAsync();

        Task CreateCampaignAsync(CreateCampaignDTO createCampaignDTO);
        Task<GetByIdCampaignDTO> GetCampaignByIdAsync(int id);
        Task UpdateCampaignAsync(UpdateCampaignDTO updateCampaignDTO);
        Task DeleteCampaignAsync(int id);
    }
}
