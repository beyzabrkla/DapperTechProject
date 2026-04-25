using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.ViewComponents
{
    [ViewComponent(Name = "Sidebar")]
    public class _LayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
