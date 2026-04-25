using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.ViewComponents
{
    [ViewComponent(Name = "Navbar")]
    public class _LayoutNavbarComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
