using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.DTOLayer.CateogoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> CategoryList(int page = 1, bool? status = null, string search = null)
        {
            var values = await _categoryRepository.GetCategoriesPagedAsync(page, 12, status, search); 

            int totalCount = await _categoryRepository.GetTotalCategoryCountAsync(status);

            ViewBag.GrandTotal = await _categoryRepository.GetTotalCategoryCountAsync(null); //sistemdeki toplam kayıt sayısı (filtre uygulanmadan)

            ViewBag.ActiveCount = await _categoryRepository.GetTotalCategoryCountAsync(true); //sistemdeki aktif kayıt sayısı

            ViewBag.TotalCount = totalCount;
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalCount / 12);

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            await _categoryRepository.CreateCategoryAsync(createCategoryDTO);
            return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var value = await _categoryRepository.GetCategoryByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryRepository.UpdateCategoryAsync(updateCategoryDTO);
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }
    }
}