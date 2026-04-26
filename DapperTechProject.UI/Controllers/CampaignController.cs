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

        private async Task PrepareDropdowns()
        {
            ViewBag.Regions = await _campaignRepository.GetRegionsAsync();
            ViewBag.Types = await _campaignRepository.GetCampaignTypesAsync();
        }

        public async Task<IActionResult> CampaignList(int page = 1, string search = null, string region = null, string type = null)
        {
            await PrepareDropdowns();

            int pageSize = 12;
            var values = await _campaignRepository.GetCampaignsPagedAsync(page, pageSize, search, region, type); // Sayfalama, arama ve filtreleme parametrelerİ
            int totalCount = await _campaignRepository.GetTotalCampaignCountAsync(search, region, type); // Toplam kayıt sayısını alarak sayfa sayısını hesaplamak için

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentRegion = region;
            ViewBag.CurrentType = type;

            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCampaign()
        {
            await PrepareDropdowns();
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
            await PrepareDropdowns();

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