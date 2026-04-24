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

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 12; //her sayfada 12 veri gelicek
            var values = await _adImpressionRepository.GetAdImpressionsWithPagingAsync(page, pageSize);
            return View(values);
        }
    }
}
