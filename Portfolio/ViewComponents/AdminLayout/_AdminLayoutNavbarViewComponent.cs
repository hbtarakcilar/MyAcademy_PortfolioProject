using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.AdminLayout
{
    public class _AdminLayoutNavbarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
