using DapperTechProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAdImpressionRepository _adImpressionRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DashboardController(IAdImpressionRepository adImpressionRepository,
                                   ICampaignRepository campaignRepository,
                                   ICategoryRepository categoryRepository)
        {
            _adImpressionRepository = adImpressionRepository;
            _campaignRepository = campaignRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Genel İstatistikler (AdImpressionRepository üzerinden)
            var totalImpressions = await _adImpressionRepository.GetTotalAdImpressionCountAsync(); //Tüm log sayısı
            var totalClicks = await _adImpressionRepository.GetTotalClickCountAsync(); //toplam tıklanma sayısı

            ViewBag.TotalImpressions = totalImpressions.ToString("N0");
            ViewBag.TotalClicks = totalClicks.ToString("N0");
            ViewBag.CTR = totalImpressions > 0 ? ((double)totalClicks / totalImpressions * 100).ToString("F2") : "0";

            //Kampanya ve Kategori Sayıları
            ViewBag.CampaignCount = await _campaignRepository.GetTotalCampaignCountAsync(); // Toplam kampanya sayısı
            ViewBag.CategoryCount = await _categoryRepository.GetTotalCategoryCountAsync(null); // Toplam kategori sayısı tüm kategoriler filtre yok)

            // Grafik Verileri Bölge Performansı
            var regionStats = await _adImpressionRepository.GetRegionPerformanceAsync();
            ViewBag.RegionLabels = regionStats.Select(x => (string)x.RegionName).ToList();
            ViewBag.RegionClickData = regionStats.Select(x => (int)x.TotalClicks).ToList();

            //Harcama Analizi 
            var topCampaigns = await _adImpressionRepository.GetTopSpendingCampaignsAsync();
            ViewBag.TopCampaignLabels = topCampaigns.Select(x => (string)x.CampaignName).ToList();
            ViewBag.TopCampaignSpends = topCampaigns.Select(x => (decimal)x.TotalSpend).ToList();

            //Alt Tablo İçin Kampanya Listesi
            var campaigns = await _campaignRepository.GetCampaignsPagedAsync(1, 10);

            //En Verimli Yayıncılar (Tıklama başına en düşük maliyet getirenler)
            var topPublishers = await _adImpressionRepository.GetTopEfficiencyPublishersAsync();
            ViewBag.PubLabels = topPublishers.Select(x => x.WebsiteURL).ToList();
            ViewBag.PubEfficiency = topPublishers.Select(x => x.AvgCPC).ToList();

            //Kategori Bazlı Dağılım (Hangi kategori ne kadar gösterim aldı?)
            var categoryStats = await _adImpressionRepository.GetCategoryDistributionAsync();
            ViewBag.CatLabels = categoryStats.Select(x => x.CategoryName).ToList();
            ViewBag.CatData = categoryStats.Select(x => x.ImpressionCount).ToList();

            //Ortalama Tıklama Maliyeti (Global Metrik)
            var avgCpc = await _adImpressionRepository.GetGlobalAvgCPCAsync();
            ViewBag.AvgCPC = avgCpc.ToString("C2");

            return View(campaigns);

        }
    }
}