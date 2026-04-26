using DapperTechProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class AdImpressionController : Controller
    {
        private readonly IAdImpressionRepository _adImpressionRepository;
        public AdImpressionController(IAdImpressionRepository adImpressionRepository)
        {
            _adImpressionRepository = adImpressionRepository;
        }

        public async Task<IActionResult> AdImpressionList(int? regionId, bool? isClicked, int? campaignId, int? publisherId, int pageNumber = 1)
        {
            const int pageSize = 12;
            if (pageNumber < 1) pageNumber = 1;

            var values = await _adImpressionRepository.GetAdImpressionsWithPagingAsync(pageNumber, pageSize, regionId, isClicked, campaignId, publisherId);

            // Filtrelenmiş toplam sayı
            int filteredCount = await _adImpressionRepository.GetFilteredCountAsync(regionId, isClicked, campaignId, publisherId);

            // Dropdown listelerini dolduruyoruz
            ViewBag.Regions = await _adImpressionRepository.GetAllRegionsAsync();
            ViewBag.Campaigns = await _adImpressionRepository.GetAllCampaignsAsync();
            ViewBag.Publishers = await _adImpressionRepository.GetAllPublishersAsync();

            // filtreleme seçenekleri sayfa değiştiğinde de aktif olsun diye viewbag'e atıyoruz
            ViewBag.SelectedRegion = regionId;
            ViewBag.SelectedStatus = isClicked;
            ViewBag.SelectedCampaign = campaignId;
            ViewBag.SelectedPublisher = publisherId;

            ViewBag.TotalCount = filteredCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(filteredCount / (double)pageSize);

            return View(values);
        }
    }
}