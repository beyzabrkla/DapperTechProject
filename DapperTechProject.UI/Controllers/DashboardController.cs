using DapperTechProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAdImpressionRepository _adImpressionRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DashboardController(IAdImpressionRepository adImpressionRepository, ICampaignRepository campaignRepository, ICategoryRepository categoryRepository)
        {
            _adImpressionRepository = adImpressionRepository;
            _campaignRepository = campaignRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalImpressions = 1000000; // Şimdilik statik veya Count metoduyla
            ViewBag.TotalClicks = await _adImpressionRepository.GetTotalClickCountAsync();
            ViewBag.CampaignCount = (await _campaignRepository.GetAllCampaignsAsync()).Count;
            ViewBag.CategoryCount = (await _categoryRepository.GetAllCategoriesAsync()).Count;

            return View();
        }
    }
}
