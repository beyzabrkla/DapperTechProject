using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.DTOLayer.CampaignDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<IActionResult> CampaignList()
        {
            var values = await _campaignRepository.GetAllCampaignsAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCampaign()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CreateCampaignDTO createCampaignDTO)
        {
            await _campaignRepository.CreateCampaignAsync(createCampaignDTO);
            return RedirectToAction("CampaignList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCampaign(int id)
        {
            var value = await _campaignRepository.GetCampaignByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCampaign(UpdateCampaignDTO updateCampaignDTO)
        {
            await _campaignRepository.UpdateCampaignAsync(updateCampaignDTO);
            return RedirectToAction("CampaignList");
        }

        public async Task<IActionResult> DeleteCampaign(int id)
        {
            await _campaignRepository.DeleteCampaignAsync(id);
            return RedirectToAction("CampaignList");
        }
    }
}