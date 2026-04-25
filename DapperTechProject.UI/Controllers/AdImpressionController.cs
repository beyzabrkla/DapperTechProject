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

        public async Task<IActionResult> AdImpressionList(int pageNumber, int pageSize)
        {
            var values = await _adImpressionRepository.GetAdImpressionsWithPagingAsync(pageNumber, pageSize);
            return View(values);
        }
    }
}
