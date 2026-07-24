using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Context;

namespace Portfolio.ViewComponents.Default_Index
{
    public class _DefaultSkillViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public _DefaultSkillViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var skill = _context.Skills
                .Where(x => x.IsActive)
                .OrderBy(x => x.Id)
                .ToList();
            return View(skill);
        }
    }
}
